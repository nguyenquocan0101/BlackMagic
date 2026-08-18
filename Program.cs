using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using bb_monitor.Forms;
using bb_monitor.Utils;

namespace bb_monitor;

internal static class Program
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static ThreadStart _003C0_003E__FormThread;

		public static ThreadStart _003C1_003E__SideThread;

		public static ThreadStart _003C2_003E__FormThreadGuts;

		public static Action _003C3_003E__SetTop;
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static EventHandler _003C_003E9__5_1;

		public static FormClosingEventHandler _003C_003E9__5_2;

		public static ThreadStart _003C_003E9__5_0;

		public static EventHandler _003C_003E9__10_0;

		public static EventArrivedEventHandler _003C_003E9__20_0;

		internal void _003CMain_003Eb__5_0()
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			IOpeningForm? openingForm = (_opening = new OpeningForm());
			object obj = _003C_003E9__5_1;
			if (obj == null)
			{
				EventHandler val = delegate
				{
				};
				_003C_003E9__5_1 = val;
				obj = (object)val;
			}
			((Form)openingForm).Load += (EventHandler)obj;
			object obj2 = _003C_003E9__5_2;
			if (obj2 == null)
			{
				FormClosingEventHandler val2 = delegate
				{
					_opening = null;
				};
				_003C_003E9__5_2 = val2;
				obj2 = (object)val2;
			}
			((Form)openingForm).FormClosing += (FormClosingEventHandler)obj2;
			Application.Run((Form)openingForm);
		}

		internal void _003CMain_003Eb__5_1(object? s, EventArgs e)
		{
		}

		internal void _003CMain_003Eb__5_2(object? s, FormClosingEventArgs e)
		{
			_opening = null;
		}

		internal void _003CFormThreadGuts_003Eb__10_0(object? s, EventArgs e)
		{
			SetTop();
		}

		internal void _003CStartCheckInject_003Eb__20_0(object sender, EventArrivedEventArgs e)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			global::System.Threading.Tasks.Task.Run(new Action(new _003C_003Ec__DisplayClass20_0
			{
				e = e
			}._003CStartCheckInject_003Eb__1));
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass20_0
	{
		public EventArrivedEventArgs e;

		internal void _003CStartCheckInject_003Eb__1()
		{
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			string text = null;
			uint num = 0u;
			PropertyDataCollection properties = e.NewEvent.Properties;
			if (!_useInstanceCreationEvent)
			{
				text = properties["ProcessName"].Value.ToString();
				Trace.Assert(text != null);
				num = (uint)properties["ProcessID"].Value;
			}
			else
			{
				PropertyDataCollection properties2 = ((ManagementBaseObject)properties["TargetInstance"].Value).Properties;
				text = properties2["Name"].Value.ToString();
				num = (uint)properties2["ProcessId"].Value;
			}
			try
			{
				nuint num2 = WinUtils.Win32Call(WinUtils.InjectLibrary((int)num, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hooking.dll")));
				Logging.WriteLog($"WMI: Process {num} ({text}): Inject result = {num2:X}");
			}
			catch (global::System.Exception ex)
			{
				Logging.WriteLog($"WMI: Process {num} ({text}): Exception during injection: {((object)ex).ToString()}");
			}
		}
	}

	private static bool _quit;

	private static bool _doForm;

	private static IOpeningForm? _opening;

	private static string _curDesktopName;

	private const string QUIT_FILE_NAME = "delete_to_quit.file";

	private static OracleForm? _form;

	private static ManagementEventWatcher? _processWatcher;

	private static bool _useInstanceCreationEvent;

	private const string SINGLE_INSTANCE_MUTEX_NAME = "Local\\bb_monitor_single_instance";

	private static void Main()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		ApplicationConfiguration.Initialize();
		using Mutex instanceMutex = new Mutex(initiallyOwned: true, SINGLE_INSTANCE_MUTEX_NAME, out bool createdNew);
		if (!createdNew)
		{
			MessageBox.Show("bb_monitor đã chạy.", "bb_monitor", MessageBoxButtons.OK, MessageBoxIcon.Information);
			return;
		}
		object obj = _003C_003Ec._003C_003E9__5_0;
		if (obj == null)
		{
			ThreadStart val = delegate
			{
				//IL_0020: Unknown result type (might be due to invalid IL or missing references)
				//IL_0025: Unknown result type (might be due to invalid IL or missing references)
				//IL_002b: Expected O, but got Unknown
				//IL_0045: Unknown result type (might be due to invalid IL or missing references)
				//IL_004a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0050: Expected O, but got Unknown
				IOpeningForm? openingForm = (_opening = new OpeningForm());
				object obj4 = _003C_003Ec._003C_003E9__5_1;
				if (obj4 == null)
				{
					EventHandler val6 = delegate
					{
					};
					_003C_003Ec._003C_003E9__5_1 = val6;
					obj4 = (object)val6;
				}
				((Form)openingForm).Load += (EventHandler)obj4;
				object obj5 = _003C_003Ec._003C_003E9__5_2;
				if (obj5 == null)
				{
					FormClosingEventHandler val7 = delegate
					{
						_opening = null;
					};
					_003C_003Ec._003C_003E9__5_2 = val7;
					obj5 = (object)val7;
				}
				((Form)openingForm).FormClosing += (FormClosingEventHandler)obj5;
				Application.Run((Form)openingForm);
			};
			_003C_003Ec._003C_003E9__5_0 = val;
			obj = (object)val;
		}
		new Thread((ThreadStart)obj).Start();
		if (!Prerequisites())
		{
			Quit();
			return;
		}
		Thread val2 = null;
		if (Directory.Exists("input") && Directory.Exists("tess"))
		{
			object obj2 = _003C_003EO._003C0_003E__FormThread;
			if (obj2 == null)
			{
				ThreadStart val3 = FormThread;
				_003C_003EO._003C0_003E__FormThread = val3;
				obj2 = (object)val3;
			}
			val2 = new Thread((ThreadStart)obj2);
			val2.Start();
			_doForm = true;
		}
		else
		{
			_doForm = false;
		}
		object obj3 = _003C_003EO._003C1_003E__SideThread;
		if (obj3 == null)
		{
			ThreadStart val4 = SideThread;
			_003C_003EO._003C1_003E__SideThread = val4;
			obj3 = (object)val4;
		}
		Thread val5 = new Thread((ThreadStart)obj3)
		{
			IsBackground = true
		};
		val5.Start();
		val5.Join();
		if (val2 != null)
		{
			val2.Join();
		}
	}

	private static bool Prerequisites()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		_opening?.SetStatus("Checking admin rights");
		if (!IsAdministrator())
		{
			Logging.WriteLog("Not running as administrator, exiting.");
			MessageBox.Show("Please run this application as an administrator.", "Error", (MessageBoxButtons)0, (MessageBoxIcon)16);
			return false;
		}
		_opening?.SetStatus("Checking if hooking.dll is deleted");
		if (!File.Exists("hooking.dll"))
		{
			Logging.WriteLog("No hooking.dll found");
			MessageBox.Show("No hooking.dll file found, your AV must've zapped it. Please disable it and redownload the tool.", "Error", (MessageBoxButtons)0, (MessageBoxIcon)16);
			return false;
		}
		_opening?.SetStatus("Getting current desktop");
		try
		{
			_curDesktopName = WinUtils.GetInputDesktopName();
		}
		catch (global::System.Exception ex)
		{
			Logging.WriteLog("Couldn't get current desktop's name: " + ex.Message);
			MessageBox.Show("Couldn't acquire current desktop's name..", "Error", (MessageBoxButtons)0, (MessageBoxIcon)16);
			return false;
		}
		_opening?.SetStatus("Setting debug privileges");
		try
		{
			WinUtils.EnableDebugPrivilege();
		}
		catch (global::System.Exception ex2)
		{
			Logging.WriteLog("Couldn't get debug privilege: " + ex2.Message);
			MessageBox.Show("Couldn't acquire necessary privilege, some features may not work.", "Error", (MessageBoxButtons)0, (MessageBoxIcon)48);
		}
		_opening?.SetStatus("Checking WMI services");
		if (!CheckWMIServices())
		{
			Logging.WriteLog("WMI services check failed, exiting.");
			MessageBox.Show("WMI services check failed, please ensure WMI is working correctly.", "Error", (MessageBoxButtons)0, (MessageBoxIcon)16);
			return false;
		}
		_opening?.SetStatus("Creating quit file");
		File.WriteAllText("delete_to_quit.file", "abc");
		return true;
	}

	private static bool CheckWMIServices()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		try
		{
			_opening?.SetStatus("Checking WMI services: Win32_ProcessStartTrace event");
			ManagementEventWatcher val = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName LIKE '%notepad%'");
			try
			{
				ManualResetEvent ev = new ManualResetEvent(false);
				val.EventArrived += (EventArrivedEventHandler)delegate
				{
					((EventWaitHandle)ev).Set();
				};
				val.Start();
				Process val2 = Process.Start(new ProcessStartInfo
				{
					FileName = "notepad.exe",
					CreateNoWindow = true,
					WindowStyle = (ProcessWindowStyle)1
				});
				Trace.Assert(val2 != null);
				Thread.Sleep(100);
				try
				{
					if (!((WaitHandle)ev).WaitOne(3000))
					{
						throw new global::System.Exception();
					}
				}
				catch
				{
					Logging.WriteLog("WMI: Win32_ProcessStartTrace failed, using  __InstanceCreationEvent instead.");
					_useInstanceCreationEvent = true;
				}
				val2.Kill();
				val.Stop();
			}
			finally
			{
				((global::System.IDisposable)val)?.Dispose();
			}
			Logging.WriteLog("WMI: All checks passed.");
			return true;
		}
		catch (global::System.Exception ex)
		{
			Logging.WriteLog("WMI: checks failed (" + ex.Message + ")");
			return false;
		}
	}

	private static bool IsAdministrator()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole((WindowsBuiltInRole)544);
	}

	private static void SideThread()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (_doForm)
		{
			_opening?.SetStatus("Loading text display");
			WaitForFormReady();
		}
		_opening?.SetStatus("Hooking keys");
		_opening?.SetStatus("Starting process checker");
		StartCheckInject();
		_opening?.SetStatus("Restarting SEB service");
		if (!RestartService())
		{
			MessageBox.Show("Failed to restart service", "Error", (MessageBoxButtons)0, (MessageBoxIcon)16);
			Quit();
			return;
		}
		_opening?.Close();
		while (!_quit)
		{
			try
			{
				if (_doForm)
				{
					CheckForm();
					CheckDesktop();
				}
				CheckManualQuit();
			}
			catch (global::System.Exception ex)
			{
				Logging.WriteLog("Exception in side thread:");
				Logging.WriteLog(((object)ex).ToString());
			}
			Thread.Sleep(500);
		}
	}

	private static void WaitForFormReady()
	{
		DateTime deadline = DateTime.UtcNow.AddSeconds(15.0);
		while (!_quit && DateTime.UtcNow < deadline)
		{
			OracleForm? form = _form;
			if (form != null && form.IsTextDisplayReady)
			{
				Logging.WriteLog("Text display loaded before SEB startup.");
				return;
			}
			Thread.Sleep(50);
		}
		Logging.WriteLog("Text display loading timed out; continuing startup.");
	}

	private static void FormThreadGuts()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		try
		{
			nint num = WinUtils.Win32Call(WinAPI.User32.OpenInputDesktop(0u, fInherit: false, WinAPI.User32.ACCESS_MASK.DESKTOP_CREATEWINDOW | WinAPI.User32.ACCESS_MASK.DESKTOP_HOOKCONTROL));
			WinUtils.Win32Call(WinAPI.User32.SetThreadDesktop(num));
			Thread.CurrentThread.TrySetApartmentState((ApartmentState)0);
			_form = new OracleForm();
			OracleForm? form = _form;
			object obj = _003C_003Ec._003C_003E9__10_0;
			if (obj == null)
			{
				EventHandler val = delegate
				{
					SetTop();
				};
				_003C_003Ec._003C_003E9__10_0 = val;
				obj = (object)val;
			}
			((Form)form).Load += (EventHandler)obj;
			Application.Run((Form)(object)_form);
			_form = null;
			WinAPI.User32.CloseDesktop(num);
		}
		catch (global::System.Exception ex)
		{
			Logging.WriteLog("Exception in form thread guts:");
			Logging.WriteLog(((object)ex).ToString());
		}
	}

	private static void FormThread()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		while (!_quit)
		{
			Logging.WriteLog("Form trying to start");
			object obj = _003C_003EO._003C2_003E__FormThreadGuts;
			if (obj == null)
			{
				ThreadStart val = FormThreadGuts;
				_003C_003EO._003C2_003E__FormThreadGuts = val;
				obj = (object)val;
			}
			Thread val2 = new Thread((ThreadStart)obj);
			val2.Start();
			val2.Join();
			Logging.WriteLog("Form stopped");
			Thread.Sleep(500);
		}
	}

	private static void Quit()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		_quit = true;
		OracleForm? form = _form;
		if (form != null)
		{
			((Control)form).Invoke(new Action(((Form)_form).Close));
		}
		_opening?.Close();
		ManagementEventWatcher? processWatcher = _processWatcher;
		if (processWatcher != null)
		{
			processWatcher.Stop();
		}
	}

	private static void SetTop()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		if (_form != null)
		{
			if (!_form.OverlayRequested)
			{
				((Form)_form).Hide();
				return;
			}
			((Form)_form).DesktopLocation = new Point(50, 50);
			((Form)_form).TopMost = true;
			((Form)_form).TopLevel = true;
			((Form)_form).WindowState = (FormWindowState)0;
			WinAPI.User32.SetWindowPos(((Control)_form).Handle, -1, 0, 0, 0, 0, WinAPI.User32.SetWindowPosFlags.IgnoreMove | WinAPI.User32.SetWindowPosFlags.IgnoreResize | WinAPI.User32.SetWindowPosFlags.ShowWindow);
		}
	}

	private static void CheckForm()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		if (_form != null)
		{
			OracleForm? form = _form;
			object obj = _003C_003EO._003C3_003E__SetTop;
			if (obj == null)
			{
				Action val = SetTop;
				_003C_003EO._003C3_003E__SetTop = val;
				obj = (object)val;
			}
			((Control)form).Invoke((Action)obj);
			object[] array = new object[4];
			Point val2 = ((Form)_form).Location;
			array[0] = val2.ToString();
			array[1] = ((Form)_form).WindowState;
			Size size = ((Form)_form).Size;
			array[2] = size.ToString();
			val2 = Cursor.Position;
			array[3] = val2.ToString();
			string text = string.Join(",", array);
			Logging.WriteLog("Form state: " + text);
		}
	}

	private static void CheckManualQuit()
	{
		if (!File.Exists("delete_to_quit.file"))
		{
			Logging.WriteLog("Manual quit indicated.");
			Quit();
		}
	}

	private static void CheckDesktop()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		try
		{
			string inputDesktopName = WinUtils.GetInputDesktopName();
			if (_curDesktopName != inputDesktopName)
			{
				Logging.WriteLog("Desktop changed to " + inputDesktopName + ". Restarting form.");
				OracleForm? form = _form;
				if (form != null)
				{
					((Control)form).Invoke(new Action(((Form)_form).Close));
				}
			}
			_curDesktopName = inputDesktopName;
		}
		catch (global::System.Exception ex)
		{
			Logging.WriteLog("Couldn't get current desktop name: " + ex.Message);
		}
	}

	private static void StartCheckInject()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_001f: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		ManagementEventWatcher val = new ManagementEventWatcher(_useInstanceCreationEvent ? "SELECT * FROM __InstanceCreationEvent WITHIN 0.1 WHERE TargetInstance ISA 'Win32_Process' and TargetInstance.Name LIKE '%SafeExamBrowser%'" : "SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName LIKE '%SafeExamBrowser%'");
		_processWatcher = val;
		ManagementEventWatcher val2 = val;
		try
		{
			ManagementEventWatcher? processWatcher = _processWatcher;
			object obj = _003C_003Ec._003C_003E9__20_0;
			if (obj == null)
			{
				EventArrivedEventHandler val3 = delegate(object sender, EventArrivedEventArgs e)
				{
					//IL_0012: Unknown result type (might be due to invalid IL or missing references)
					//IL_001c: Expected O, but got Unknown
					global::System.Threading.Tasks.Task.Run((Action)delegate
					{
						//IL_0063: Unknown result type (might be due to invalid IL or missing references)
						string text = null;
						uint num = 0u;
						PropertyDataCollection properties = e.NewEvent.Properties;
						if (!_useInstanceCreationEvent)
						{
							text = properties["ProcessName"].Value.ToString();
							Trace.Assert(text != null);
							num = (uint)properties["ProcessID"].Value;
						}
						else
						{
							PropertyDataCollection properties2 = ((ManagementBaseObject)properties["TargetInstance"].Value).Properties;
							text = properties2["Name"].Value.ToString();
							num = (uint)properties2["ProcessId"].Value;
						}
						try
						{
							nuint num2 = WinUtils.Win32Call(WinUtils.InjectLibrary((int)num, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hooking.dll")));
							Logging.WriteLog($"WMI: Process {num} ({text}): Inject result = {num2:X}");
						}
						catch (global::System.Exception ex)
						{
							Logging.WriteLog($"WMI: Process {num} ({text}): Exception during injection: {((object)ex).ToString()}");
						}
					});
				};
				_003C_003Ec._003C_003E9__20_0 = val3;
				obj = (object)val3;
			}
			processWatcher.EventArrived += (EventArrivedEventHandler)obj;
			_processWatcher.Start();
			Logging.WriteLog("Started WMI.");
		}
		finally
		{
			((global::System.IDisposable)val2)?.Dispose();
		}
	}

	private static bool RestartService()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		string text = "safeexambrowser";
		int num = 2500;
		try
		{
			Process obj = Process.Start(new ProcessStartInfo
			{
				FileName = "cmd.exe",
				Arguments = $"/c \"net stop {text} & net start {text}\"",
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true
			}) ?? throw new global::System.Exception("Couldn't start restart process");
			if (obj != null)
			{
				obj.WaitForExit();
			}
			ServiceController val = new ServiceController(text);
			try
			{
				TimeSpan val2 = TimeSpan.FromMilliseconds((double)num);
				val.WaitForStatus((ServiceControllerStatus)4, val2);
			}
			finally
			{
				((global::System.IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (global::System.Exception ex)
		{
			Logging.WriteLog("Failed to restart service '" + text + "': " + ex.Message);
			return false;
		}
	}
}
