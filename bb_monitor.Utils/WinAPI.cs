using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace bb_monitor.Utils;

public static class WinAPI
{
	public static class GDI32
	{
		public const int SRCCOPY = 13369376;

		[DllImport("gdi32.dll")]
		public static extern bool BitBlt(nint hObject, int nXDest, int nYDest, int nWidth, int nHeight, nint hObjectSource, int nXSrc, int nYSrc, int dwRop);

		[DllImport("gdi32.dll")]
		public static extern nint CreateCompatibleBitmap(nint hDC, int nWidth, int nHeight);

		[DllImport("gdi32.dll")]
		public static extern nint CreateCompatibleDC(nint hDC);

		[DllImport("gdi32.dll")]
		public static extern bool DeleteDC(nint hDC);

		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(nint hObject);

		[DllImport("gdi32.dll")]
		public static extern nint SelectObject(nint hDC, nint hObject);
	}

	public static class User32
	{
		public delegate bool EnumWindowsProc(nint hWnd, int lParam);

		public enum MapType : uint
		{
			MAPVK_VK_TO_VSC,
			MAPVK_VSC_TO_VK,
			MAPVK_VK_TO_CHAR,
			MAPVK_VSC_TO_VK_EX
		}

		public struct WINDOWPLACEMENT
		{
			public int length;

			public int flags;

			public int showCmd;

			public Point ptMinPosition;

			public Point ptMaxPosition;

			public Rectangle rcNormalPosition;
		}

		public struct POINT
		{
			internal int X;

			internal int Y;
		}

		public struct RECT
		{
			public int Left;

			public int Top;

			public int Right;

			public int Bottom;
		}

		public struct WINDOWINFO
		{
			public uint cbSize;

			public RECT rcWindow;

			public RECT rcClient;

			public uint dwStyle;

			public uint dwExStyle;

			public uint dwWindowStatus;

			public uint cxWindowBorders;

			public uint cyWindowBorders;

			public ushort atomWindowType;

			public ushort wCreatorVersion;

			public WINDOWINFO(bool? filler)
			{
				this = default(WINDOWINFO);
				cbSize = (uint)Marshal.SizeOf(typeof(WINDOWINFO));
			}
		}

		public delegate bool EnumThreadDelegate(nint hWnd, nint lParam);

		public delegate bool EnumDesktopDelegate(string lpszDesktop, nint lParam);

		public delegate bool EnumWindowsDelegate(nint hWnd, nint lParam);

		[Flags]
		public enum SetWindowPosFlags : uint
		{
			AsynchronousWindowPosition = 0x4000u,
			DeferErase = 0x2000u,
			DrawFrame = 0x20u,
			FrameChanged = 0x20u,
			HideWindow = 0x80u,
			DoNotActivate = 0x10u,
			DoNotCopyBits = 0x100u,
			IgnoreMove = 2u,
			DoNotChangeOwnerZOrder = 0x200u,
			DoNotRedraw = 8u,
			DoNotReposition = 0x200u,
			DoNotSendChangingEvent = 0x400u,
			IgnoreResize = 1u,
			IgnoreZOrder = 4u,
			ShowWindow = 0x40u
		}

		[Flags]
		public enum GetAncestorFlags
		{
			GetParent = 1,
			GetRoot = 2,
			GetRootOwner = 3
		}

		[Flags]
		public enum ACCESS_MASK : uint
		{
			DESKTOP_NONE = 0u,
			DESKTOP_READOBJECTS = 1u,
			DESKTOP_CREATEWINDOW = 2u,
			DESKTOP_CREATEMENU = 4u,
			DESKTOP_HOOKCONTROL = 8u,
			DESKTOP_JOURNALRECORD = 0x10u,
			DESKTOP_JOURNALPLAYBACK = 0x20u,
			DESKTOP_ENUMERATE = 0x40u,
			DESKTOP_WRITEOBJECTS = 0x80u,
			DESKTOP_SWITCHDESKTOP = 0x100u
		}

		[DllImport("user32.dll")]
		public static extern nint OpenDesktop(string lpszDesktop, uint dwFlags, bool fInherit, ACCESS_MASK dwDesiredAccess);

		[DllImport("user32.dll")]
		public static extern nint SetWindowDisplayAffinity([In] nint hWnd, [In] uint dwAffinity);

		[DllImport("user32.dll")]
		public static extern nint GetWindowDisplayAffinity([In] nint hWnd, out uint dwAffinity);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetWindowPos(nint hWnd, nint hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

		[DllImport("user32.dll")]
		public static extern nint GetDesktopWindow();

		[DllImport("user32.dll")]
		public static extern nint GetWindowDC(nint hWnd);

		[DllImport("user32.dll")]
		public static extern nint ReleaseDC(nint hWnd, nint hDC);

		[DllImport("user32.dll")]
		public static extern nint GetWindowRect(nint hWnd, ref RECT rect);

		[DllImport("user32.dll")]
		public static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, nint lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(nint hWnd, out uint lpdwProcessId);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool PrintWindow(nint hwnd, nint hDC, uint nFlags);

		[DllImport("USER32.DLL")]
		public static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

		[DllImport("USER32.DLL")]
		public static extern int GetWindowText(nint hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("USER32.DLL")]
		public static extern int GetWindowTextLength(nint hWnd);

		[DllImport("USER32.DLL")]
		public static extern bool IsWindowVisible(nint hWnd);

		[DllImport("user32.dll")]
		public static extern bool IsIconic(nint hWnd);

		[DllImport("USER32.DLL")]
		public static extern nint GetShellWindow();

		[DllImport("user32.dll")]
		public static extern bool GetWindowInfo(nint hwnd, ref WINDOWINFO pwi);

		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern nint GetAncestor(nint hwnd, GetAncestorFlags flags);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetClassName(nint hWnd, StringBuilder lpClassName, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern nint GetLastActivePopup(nint hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern nint CallNextHookEx(nint hhk, int nCode, nint wParam, nint lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool CloseClipboard();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool CloseDesktop(nint hDesktop);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern nint CreateDesktop(string lpszDesktop, nint lpszDevice, nint pDevmode, int dwFlags, uint dwDesiredAccess, nint lpsa);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(nint hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool EmptyClipboard();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool EnumDesktops(nint hwinsta, EnumDesktopDelegate lpEnumFunc, nint lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool EnumWindows(EnumWindowsDelegate enumProc, nint lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern nint FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetCursorPos(ref POINT pt);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern nint GetThreadDesktop(int dwThreadId);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern nint GetProcessWindowStation();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetUserObjectInformation(nint hObj, int nIndex, nint pvInfo, int nLength, ref int lpnLengthNeeded);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetWindowPlacement(nint hWnd, ref WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool OpenClipboard(nint hWndNewOwner);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern nint OpenInputDesktop(uint dwFlags, bool fInherit, ACCESS_MASK dwDesiredAccess);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool PostMessage(nint hWnd, uint msg, nint wParam, nint lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern nint SendMessage(nint hWnd, uint msg, nint wParam, nint lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetForegroundWindow(nint hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool ShowWindowAsync(nint hWnd, int nCmdShow);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SwitchDesktop(nint hDesktop);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetThreadDesktop(nint hDesktop);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool UnhookWinEvent(nint hWinEventHook);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool UnhookWindowsHookEx(nint hhk);

		[DllImport("user32.dll")]
		public static extern int ToUnicode(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out][MarshalAs(21)] StringBuilder pwszBuff, int cchBuff, uint wFlags);

		[DllImport("user32.dll")]
		public static extern bool GetKeyboardState(byte[] lpKeyState);

		[DllImport("user32.dll")]
		public static extern uint MapVirtualKey(uint uCode, MapType uMapType);
	}

	public static class ShellCore
	{
		public enum PROCESS_DPI_AWARENESS
		{
			PROCESS_DPI_UNAWARE,
			PROCESS_SYSTEM_DPI_AWARE,
			PROCESS_PER_MONITOR_DPI_AWARE
		}

		public enum MONITOR_DPI_TYPE
		{
			MDT_EFFECTIVE_DPI,
			MDT_ANGULAR_DPI,
			MDT_RAW_DPI,
			MDT_DEFAULT
		}

		[DllImport("Shcore.dll")]
		public static extern uint SetProcessDpiAwareness([In] PROCESS_DPI_AWARENESS value);
	}

	public static class Kernel32
	{
		[Flags]
		public enum AllocationType
		{
			Commit = 0x1000,
			Reserve = 0x2000,
			Decommit = 0x4000,
			Release = 0x8000,
			Reset = 0x80000,
			Physical = 0x400000,
			TopDown = 0x100000,
			WriteWatch = 0x200000,
			LargePages = 0x20000000
		}

		[Flags]
		public enum MemoryProtection
		{
			Execute = 0x10,
			ExecuteRead = 0x20,
			ExecuteReadWrite = 0x40,
			ExecuteWriteCopy = 0x80,
			NoAccess = 1,
			ReadOnly = 2,
			ReadWrite = 4,
			WriteCopy = 8,
			GuardModifierflag = 0x100,
			NoCacheModifierflag = 0x200,
			WriteCombineModifierflag = 0x400
		}

		[Flags]
		public enum ProcessAccessFlags : uint
		{
			All = 0x1F0FFFu,
			Terminate = 1u,
			CreateThread = 2u,
			VirtualMemoryOperation = 8u,
			VirtualMemoryRead = 0x10u,
			VirtualMemoryWrite = 0x20u,
			DuplicateHandle = 0x40u,
			CreateProcess = 0x80u,
			SetQuota = 0x100u,
			SetInformation = 0x200u,
			QueryInformation = 0x400u,
			QueryLimitedInformation = 0x1000u,
			Synchronize = 0x100000u
		}

		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern nint OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern nint VirtualAllocEx(nint hProcess, nint lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(nint hProcess, nint lpBaseAddress, nint lpBuffer, uint nSize, out nuint lpNumberOfBytesWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(nint hProcess, nint lpBaseAddress, byte[] lpBuffer, uint nSize, out nuint lpNumberOfBytesWritten);

		[DllImport("kernel32")]
		public static extern nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes, uint dwStackSize, nint lpStartAddress, nint lpParameter, uint dwCreationFlags, out uint lpThreadId);

		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern nint GetProcAddress(nint hModule, string procName);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern nint GetModuleHandle(string lpModuleName);

		[DllImport("kernel32.dll")]
		public static extern nint CloseHandle(nint handle);

		[DllImport("kernel32.dll")]
		public static extern uint WaitForSingleObject(nint handle, uint time);

		[DllImport("kernel32.dll")]
		public static extern uint GetExitCodeThread([In] nint handle, out uint lpExitCode);

		[DllImport("kernel32.dll")]
		public static extern bool TerminateThread(nint handle, uint code);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool VirtualFreeEx(nint hProcess, nint lpAddress, nuint dwSize, AllocationType dwFreeType);
	}

	public static class DbgHelp
	{
		public delegate bool PSYM_ENUMERATESYMBOLS_CALLBACK([In] ref SYMBOL_INFO pSymInfo, [In] uint SymbolSize, [Optional][In] nint UserContext);

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct SYMBOL_INFO
		{
			public uint SizeOfStruct;

			public uint TypeIndex;

			public ulong Reserved_0;

			public ulong Reserved_1;

			public uint Index;

			public uint Size;

			public ulong ModBase;

			public uint Flags;

			public ulong Value;

			public ulong Address;

			public uint Register;

			public uint Scope;

			public uint Tag;

			public uint NameLen;

			public uint MaxNameLen;

			[MarshalAs(23, SizeConst = 1024)]
			public string Name;
		}

		[DllImport("dbghelp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool SymInitialize([In] nint hProcess, [Optional][In] string? UserSearchPath, [In] bool fInvadeProcess);

		[DllImport("dbghelp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(10)]
		public static extern ulong SymLoadModuleEx([In] nint hProcess, [In] nint hFile, [In] string ImageName, [In] string ModuleName, [In][MarshalAs(10)] ulong BaseOfDll, [In] uint DllSize, [In] nint Data, [In] uint Flags);

		[DllImport("dbghelp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool SymEnumSymbols([In] nint hProcess, [In] ulong BaseOfDll, [Optional][In] string Mask, [In] PSYM_ENUMERATESYMBOLS_CALLBACK EnumSymbolsCallback, [Optional][In] nint UserContext);

		[DllImport("dbghelp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool SymCleanup([In] nint hProcess);
	}

	public static class Advapi32
	{
		public struct LUID
		{
			public uint LowPart;

			public int HighPart;
		}

		public struct TOKEN_PRIVILEGES
		{
			public uint PrivilegeCount;

			public LUID Luid;

			public uint Attributes;
		}

		public const uint TOKEN_ADJUST_PRIVILEGES = 32u;

		public const uint TOKEN_QUERY = 8u;

		public const int SE_PRIVILEGE_ENABLED = 2;

		public const string SE_DEBUG_NAME = "SeDebugPrivilege";

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool OpenProcessToken(nint ProcessHandle, uint DesiredAccess, out nint TokenHandle);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool LookupPrivilegeValue(string? lpSystemName, string lpName, out LUID lpLuid);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool AdjustTokenPrivileges(nint TokenHandle, bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, uint BufferLengthInBytes, nint PreviousState, nint ReturnLength);
	}
}
