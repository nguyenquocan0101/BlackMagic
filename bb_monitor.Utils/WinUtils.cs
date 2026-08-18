using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace bb_monitor.Utils;

public static class WinUtils
{
	public static string GetInputDesktopName()
	{
		nint num = Win32Call(WinAPI.User32.OpenInputDesktop(0u, fInherit: false, WinAPI.User32.ACCESS_MASK.DESKTOP_NONE));
		int lpnLengthNeeded = 0;
		WinAPI.User32.GetUserObjectInformation(num, 2, global::System.IntPtr.Zero, 0, ref lpnLengthNeeded);
		nint num2 = Marshal.AllocHGlobal(lpnLengthNeeded);
		Win32Call(WinAPI.User32.GetUserObjectInformation(num, 2, num2, lpnLengthNeeded, ref lpnLengthNeeded));
		WinAPI.User32.CloseDesktop(num);
		string result = Marshal.PtrToStringAnsi((global::System.IntPtr)num2);
		Marshal.FreeHGlobal((global::System.IntPtr)num2);
		return result;
	}

	public static List<WinAPI.DbgHelp.SYMBOL_INFO> EnumerateSymbols(Process proc, ProcessModule module, string symbol = "*")
	{
		nint num = Win32Call(OpenProcessHandle(proc.Id));
		List<WinAPI.DbgHelp.SYMBOL_INFO> result = EnumerateSymbols(num, module, symbol);
		Win32Call(WinAPI.Kernel32.CloseHandle(num));
		return result;
	}

	private static List<WinAPI.DbgHelp.SYMBOL_INFO> EnumerateSymbols(nint procHandle, ProcessModule module, string symbol = "*")
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Win32Call(WinAPI.DbgHelp.SymInitialize(procHandle, Path.GetDirectoryName(module.FileName) ?? throw new AggregateException(), fInvadeProcess: false));
		List<WinAPI.DbgHelp.SYMBOL_INFO> symbols = new List<WinAPI.DbgHelp.SYMBOL_INFO>();
		try
		{
			Win32Call(WinAPI.DbgHelp.SymLoadModuleEx(procHandle, global::System.IntPtr.Zero, module.ModuleName, null, (ulong)(nint)module.BaseAddress, (uint)module.ModuleMemorySize, global::System.IntPtr.Zero, 0u));
			WinAPI.DbgHelp.PSYM_ENUMERATESYMBOLS_CALLBACK enumSymbolsCallback = enumSyms;
			Win32Call(WinAPI.DbgHelp.SymEnumSymbols(procHandle, (ulong)(nint)module.BaseAddress, symbol, enumSymbolsCallback, global::System.IntPtr.Zero));
		}
		finally
		{
			Win32Call(WinAPI.DbgHelp.SymCleanup(procHandle));
		}
		return symbols;
		bool enumSyms(ref WinAPI.DbgHelp.SYMBOL_INFO pSymInfo, uint SymbolSizem, nint UserContext)
		{
			symbols.Add(pSymInfo);
			return true;
		}
	}

	private static nint BeginFuncCall(int processId, string module, string function, out nint procHandle, out nint funcAddr)
	{
		string module2 = module;
		string function2 = function;
		procHandle = OpenProcessHandle(processId);
		ProcessModule module3 = Enumerable.First<ProcessModule>(Enumerable.Cast<ProcessModule>((global::System.Collections.IEnumerable)Process.GetProcessById(processId).Modules), (Func<ProcessModule, bool>)((ProcessModule x) => x.FileName.EndsWith(module2, (StringComparison)3)));
		funcAddr = (nint)Enumerable.First<WinAPI.DbgHelp.SYMBOL_INFO>((global::System.Collections.Generic.IEnumerable<WinAPI.DbgHelp.SYMBOL_INFO>)EnumerateSymbols(procHandle, module3), (Func<WinAPI.DbgHelp.SYMBOL_INFO, bool>)((WinAPI.DbgHelp.SYMBOL_INFO x) => x.Name.Equals(function2, (StringComparison)3))).Address;
		return funcAddr;
	}

	public static nuint InjectLibrary(int processId, string dllPath)
	{
		BeginFuncCall(processId, "kernel32.dll", "LoadLibraryA", out var procHandle, out var funcAddr);
		nuint result = CallFunction(procHandle, funcAddr, Encoding.ASCII.GetBytes(dllPath + "\0"));
		Win32Call(WinAPI.Kernel32.CloseHandle(procHandle));
		return result;
	}

	public static void EnableDebugPrivilege()
	{
		Win32Call(WinAPI.Advapi32.OpenProcessToken(Process.GetCurrentProcess().Handle, 40u, out var TokenHandle));
		Win32Call(WinAPI.Advapi32.LookupPrivilegeValue(null, "SeDebugPrivilege", out var lpLuid));
		WinAPI.Advapi32.TOKEN_PRIVILEGES NewState = default(WinAPI.Advapi32.TOKEN_PRIVILEGES);
		NewState.PrivilegeCount = 1u;
		NewState.Luid = lpLuid;
		NewState.Attributes = 2u;
		Win32Call(WinAPI.Advapi32.AdjustTokenPrivileges(TokenHandle, DisableAllPrivileges: false, ref NewState, 0u, global::System.IntPtr.Zero, global::System.IntPtr.Zero));
	}

	public static nuint CallFunction<T>(Process process, nint funcPtr, T input) where T : struct
	{
		if (process == null || funcPtr == 0 || process.HasExited || process.Handle == (global::System.IntPtr)0)
		{
			throw new global::System.Exception();
		}
		nint handle = OpenProcessHandle(process.Id);
		nuint result = CallFunction((nint)process.Id, funcPtr, input);
		Win32Call(WinAPI.Kernel32.CloseHandle(handle));
		return result;
	}

	public static nuint CallFunction(Process process, nint funcPtr, byte[] inputData)
	{
		if (process == null || funcPtr == 0 || process.HasExited || process.Handle == (global::System.IntPtr)0)
		{
			throw new global::System.Exception();
		}
		nint handle = OpenProcessHandle(process.Id);
		nuint result = CallFunction((nint)process.Id, funcPtr, inputData);
		Win32Call(WinAPI.Kernel32.CloseHandle(handle));
		return result;
	}

	private static nint OpenProcessHandle(int id)
	{
		return Win32Call(WinAPI.Kernel32.OpenProcess(WinAPI.Kernel32.ProcessAccessFlags.CreateThread | WinAPI.Kernel32.ProcessAccessFlags.VirtualMemoryOperation | WinAPI.Kernel32.ProcessAccessFlags.VirtualMemoryRead | WinAPI.Kernel32.ProcessAccessFlags.VirtualMemoryWrite | WinAPI.Kernel32.ProcessAccessFlags.QueryInformation, bInheritHandle: false, id));
	}

	private static nuint WaitThread(nint s)
	{
		Win32Call(WinAPI.Kernel32.WaitForSingleObject(s, 4294967295u), (uint r) => r == 4294967295u);
		Win32Call(WinAPI.Kernel32.TerminateThread(s, 0u));
		Win32Call(WinAPI.Kernel32.GetExitCodeThread(s, out var lpExitCode));
		Win32Call(WinAPI.Kernel32.CloseHandle(s));
		return lpExitCode;
	}

	private static nuint CallFunction<T>(nint procHandle, nint funcPtr, T input) where T : struct
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		int bufSize = Marshal.SizeOf<T>(input);
		nint num = Marshal.AllocHGlobal(bufSize);
		if (num == 0)
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		Marshal.StructureToPtr<T>(input, (global::System.IntPtr)num, false);
		nint num2 = Win32Call(WinAPI.Kernel32.VirtualAllocEx(procHandle, 0, (uint)bufSize, WinAPI.Kernel32.AllocationType.Commit | WinAPI.Kernel32.AllocationType.Reserve, WinAPI.Kernel32.MemoryProtection.ReadWrite));
		Win32Call(WinAPI.Kernel32.WriteProcessMemory(procHandle, num2, num, (uint)bufSize, out var written), (bool r) => !r || (int)written != bufSize);
		uint lpThreadId;
		nuint result = WaitThread(Win32Call(WinAPI.Kernel32.CreateRemoteThread(procHandle, global::System.IntPtr.Zero, 0u, funcPtr, num2, 0u, out lpThreadId)));
		Win32Call(result);
		Win32Call(WinAPI.Kernel32.VirtualFreeEx(procHandle, num2, (nuint)bufSize, WinAPI.Kernel32.AllocationType.Release));
		Marshal.FreeHGlobal((global::System.IntPtr)num);
		return result;
	}

	private static nuint CallFunction(nint procHandle, nint funcPtr, byte[] inputData)
	{
		nint zero = global::System.IntPtr.Zero;
		uint dataBufSize = 0u;
		dataBufSize = (uint)(inputData.Length * Marshal.SizeOf<byte>());
		zero = Win32Call(WinAPI.Kernel32.VirtualAllocEx(procHandle, global::System.IntPtr.Zero, dataBufSize, WinAPI.Kernel32.AllocationType.Commit | WinAPI.Kernel32.AllocationType.Reserve, WinAPI.Kernel32.MemoryProtection.ReadWrite));
		Win32Call(WinAPI.Kernel32.WriteProcessMemory(procHandle, zero, inputData, dataBufSize, out var written), (bool r) => !r || written != dataBufSize);
		uint lpThreadId;
		nuint result = WaitThread(Win32Call(WinAPI.Kernel32.CreateRemoteThread(procHandle, global::System.IntPtr.Zero, 0u, funcPtr, zero, 0u, out lpThreadId)));
		Win32Call(WinAPI.Kernel32.VirtualFreeEx(procHandle, zero, 0u, WinAPI.Kernel32.AllocationType.Release));
		return result;
	}

	public static T Win32Call<T>(T result, Func<T, bool>? isError = null)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (isError == null)
		{
			isError = delegate(T r)
			{
				//IL_0105: Unknown result type (might be due to invalid IL or missing references)
				if (r is bool flag)
				{
					return !flag;
				}
				if (r is int num)
				{
					return num == 0;
				}
				if (r is uint num2)
				{
					return num2 == 0;
				}
				if (r is long num3)
				{
					return num3 == 0;
				}
				if (r is ulong num4)
				{
					return num4 == 0;
				}
				if (r is nint num5)
				{
					return num5 == (nint)global::System.IntPtr.Zero;
				}
				if (r is nuint num6)
				{
					return num6 == (nuint)global::System.UIntPtr.Zero;
				}
				throw new NotImplementedException();
			};
		}
		if (isError.Invoke(result))
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		return result;
	}
}
