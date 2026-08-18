using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace bb_monitor.Utils.UserActivityMonitor;

public static class HookManager
{
	private delegate int HookProc(int nCode, int wParam, nint lParam);

	private struct Point
	{
		public int X;

		public int Y;
	}

	private struct MouseLLHookStruct
	{
		public Point Point;

		public int MouseData;

		public int Flags;

		public int Time;

		public int ExtraInfo;
	}

	private struct KeyboardHookStruct
	{
		public int VirtualKeyCode;

		public int ScanCode;

		public int Flags;

		public int Time;

		public int ExtraInfo;
	}

	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static HookProc _003C0_003E__MouseHookProc;

		public static HookProc _003C1_003E__KeyboardHookProc;

		public static EventHandler _003C2_003E__DoubleClickTimeElapsed;

		public static MouseEventHandler _003C3_003E__OnMouseUp;
	}

	private static HookProc s_MouseDelegate;

	private static int s_MouseHookHandle;

	private static int m_OldX;

	private static int m_OldY;

	private static HookProc s_KeyboardDelegate;

	private static int s_KeyboardHookHandle;

	[CompilerGenerated]
	private static MouseEventHandler m_s_MouseMove;

	[CompilerGenerated]
	private static EventHandler<MouseEventExtArgs> m_s_MouseMoveExt;

	[CompilerGenerated]
	private static MouseEventHandler m_s_MouseClick;

	[CompilerGenerated]
	private static EventHandler<MouseEventExtArgs> m_s_MouseClickExt;

	[CompilerGenerated]
	private static MouseEventHandler m_s_MouseDown;

	[CompilerGenerated]
	private static MouseEventHandler m_s_MouseUp;

	[CompilerGenerated]
	private static MouseEventHandler m_s_MouseWheel;

	[CompilerGenerated]
	private static MouseEventHandler m_s_MouseDoubleClick;

	private static MouseButtons s_PrevClickedButton;

	private static System.Windows.Forms.Timer s_DoubleClickTimer;

	[CompilerGenerated]
	private static KeyPressEventHandler m_s_KeyPress;

	[CompilerGenerated]
	private static KeyEventHandler m_s_KeyUp;

	[CompilerGenerated]
	private static KeyEventHandler m_s_KeyDown;

	private static MouseEventHandler s_MouseMove;
	private static EventHandler<MouseEventExtArgs> s_MouseMoveExt;
	private static MouseEventHandler s_MouseClick;
	private static EventHandler<MouseEventExtArgs> s_MouseClickExt;
	private static MouseEventHandler s_MouseDown;
	private static MouseEventHandler s_MouseUp;
	private static MouseEventHandler s_MouseWheel;
	private static MouseEventHandler s_MouseDoubleClick;
	private static KeyPressEventHandler s_KeyPress;
	private static KeyEventHandler s_KeyUp;
	private static KeyEventHandler s_KeyDown;

	private const int WH_MOUSE_LL = 14;

	private const int WH_KEYBOARD_LL = 13;

	private const int WH_MOUSE = 7;

	private const int WH_KEYBOARD = 2;

	private const int WM_MOUSEMOVE = 512;

	private const int WM_LBUTTONDOWN = 513;

	private const int WM_RBUTTONDOWN = 516;

	private const int WM_MBUTTONDOWN = 519;

	private const int WM_LBUTTONUP = 514;

	private const int WM_RBUTTONUP = 517;

	private const int WM_MBUTTONUP = 520;

	private const int WM_LBUTTONDBLCLK = 515;

	private const int WM_RBUTTONDBLCLK = 518;

	private const int WM_MBUTTONDBLCLK = 521;

	private const int WM_MOUSEWHEEL = 522;

	private const int WM_KEYDOWN = 256;

	private const int WM_KEYUP = 257;

	private const int WM_SYSKEYDOWN = 260;

	private const int WM_SYSKEYUP = 261;

	private const byte VK_SHIFT = 16;

	private const byte VK_CAPITAL = 20;

	private const byte VK_NUMLOCK = 144;

	private static event MouseEventHandler s_MouseMoveEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseMove;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseMove, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseMove;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseMove, val3, val2);
			}
			while (val != val2);
		}
	}

	public static event MouseEventHandler MouseMove
	{
		add
		{
			EnsureSubscribedToGlobalMouseEvents();
			s_MouseMove += value;
		}
		remove
		{
			s_MouseMove -= value;
			TryUnsubscribeFromGlobalMouseEvents();
		}
	}

	private static event EventHandler<MouseEventExtArgs> s_MouseMoveExtEvent
	{
		[CompilerGenerated]
		add
		{
			EventHandler<MouseEventExtArgs> val = HookManager.m_s_MouseMoveExt;
			EventHandler<MouseEventExtArgs> val2;
			do
			{
				val2 = val;
				EventHandler<MouseEventExtArgs> val3 = (EventHandler<MouseEventExtArgs>)(object)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<EventHandler<MouseEventExtArgs>>(ref HookManager.m_s_MouseMoveExt, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler<MouseEventExtArgs> val = HookManager.m_s_MouseMoveExt;
			EventHandler<MouseEventExtArgs> val2;
			do
			{
				val2 = val;
				EventHandler<MouseEventExtArgs> val3 = (EventHandler<MouseEventExtArgs>)(object)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<EventHandler<MouseEventExtArgs>>(ref HookManager.m_s_MouseMoveExt, val3, val2);
			}
			while (val != val2);
		}
	}

	public static event EventHandler<MouseEventExtArgs> MouseMoveExt
	{
		add
		{
			EnsureSubscribedToGlobalMouseEvents();
			s_MouseMoveExt += value;
		}
		remove
		{
			s_MouseMoveExt -= value;
			TryUnsubscribeFromGlobalMouseEvents();
		}
	}

	private static event MouseEventHandler s_MouseClickEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseClick;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseClick, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseClick;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseClick, val3, val2);
			}
			while (val != val2);
		}
	}

	public static event MouseEventHandler MouseClick
	{
		add
		{
			EnsureSubscribedToGlobalMouseEvents();
			s_MouseClick += value;
		}
		remove
		{
			s_MouseClick -= value;
			TryUnsubscribeFromGlobalMouseEvents();
		}
	}

	private static event EventHandler<MouseEventExtArgs> s_MouseClickExtEvent
	{
		[CompilerGenerated]
		add
		{
			EventHandler<MouseEventExtArgs> val = HookManager.m_s_MouseClickExt;
			EventHandler<MouseEventExtArgs> val2;
			do
			{
				val2 = val;
				EventHandler<MouseEventExtArgs> val3 = (EventHandler<MouseEventExtArgs>)(object)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<EventHandler<MouseEventExtArgs>>(ref HookManager.m_s_MouseClickExt, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler<MouseEventExtArgs> val = HookManager.m_s_MouseClickExt;
			EventHandler<MouseEventExtArgs> val2;
			do
			{
				val2 = val;
				EventHandler<MouseEventExtArgs> val3 = (EventHandler<MouseEventExtArgs>)(object)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<EventHandler<MouseEventExtArgs>>(ref HookManager.m_s_MouseClickExt, val3, val2);
			}
			while (val != val2);
		}
	}

	public static event EventHandler<MouseEventExtArgs> MouseClickExt
	{
		add
		{
			EnsureSubscribedToGlobalMouseEvents();
			s_MouseClickExt += value;
		}
		remove
		{
			s_MouseClickExt -= value;
			TryUnsubscribeFromGlobalMouseEvents();
		}
	}

	private static event MouseEventHandler s_MouseDownEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseDown;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseDown, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseDown;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseDown, val3, val2);
			}
			while (val != val2);
		}
	}

	public static event MouseEventHandler MouseDown
	{
		add
		{
			EnsureSubscribedToGlobalMouseEvents();
			s_MouseDown += value;
		}
		remove
		{
			s_MouseDown -= value;
			TryUnsubscribeFromGlobalMouseEvents();
		}
	}

	private static event MouseEventHandler s_MouseUpEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseUp;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseUp, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseUp;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseUp, val3, val2);
			}
			while (val != val2);
		}
	}

	public static event MouseEventHandler MouseUp
	{
		add
		{
			EnsureSubscribedToGlobalMouseEvents();
			s_MouseUp += value;
		}
		remove
		{
			s_MouseUp -= value;
			TryUnsubscribeFromGlobalMouseEvents();
		}
	}

	private static event MouseEventHandler s_MouseWheelEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseWheel;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseWheel, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseWheel;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseWheel, val3, val2);
			}
			while (val != val2);
		}
	}

	public static event MouseEventHandler MouseWheel
	{
		add
		{
			EnsureSubscribedToGlobalMouseEvents();
			s_MouseWheel += value;
		}
		remove
		{
			s_MouseWheel -= value;
			TryUnsubscribeFromGlobalMouseEvents();
		}
	}

	private static event MouseEventHandler s_MouseDoubleClickEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseDoubleClick;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseDoubleClick, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			MouseEventHandler val = HookManager.m_s_MouseDoubleClick;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref HookManager.m_s_MouseDoubleClick, val3, val2);
			}
			while (val != val2);
		}
	}

	public static event MouseEventHandler MouseDoubleClick
	{
		add
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Expected O, but got Unknown
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected O, but got Unknown
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			EnsureSubscribedToGlobalMouseEvents();
			if (HookManager.s_MouseDoubleClick == null)
			{
				s_DoubleClickTimer = new System.Windows.Forms.Timer
				{
					Interval = GetDoubleClickTime(),
					Enabled = false
				};
				System.Windows.Forms.Timer obj = s_DoubleClickTimer;
				object obj2 = _003C_003EO._003C2_003E__DoubleClickTimeElapsed;
				if (obj2 == null)
				{
					EventHandler val = DoubleClickTimeElapsed;
					_003C_003EO._003C2_003E__DoubleClickTimeElapsed = val;
					obj2 = (object)val;
				}
				obj.Tick += (EventHandler)obj2;
				object obj3 = _003C_003EO._003C3_003E__OnMouseUp;
				if (obj3 == null)
				{
					MouseEventHandler val2 = OnMouseUp;
					_003C_003EO._003C3_003E__OnMouseUp = val2;
					obj3 = (object)val2;
				}
				MouseUp += (MouseEventHandler)obj3;
			}
			s_MouseDoubleClick += value;
		}
		remove
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			if (HookManager.s_MouseDoubleClick != null)
			{
				s_MouseDoubleClick -= value;
				if (HookManager.s_MouseDoubleClick == null)
				{
					object obj = _003C_003EO._003C3_003E__OnMouseUp;
					if (obj == null)
					{
						MouseEventHandler val = OnMouseUp;
						_003C_003EO._003C3_003E__OnMouseUp = val;
						obj = (object)val;
					}
					MouseUp -= (MouseEventHandler)obj;
					System.Windows.Forms.Timer obj2 = s_DoubleClickTimer;
					object obj3 = _003C_003EO._003C2_003E__DoubleClickTimeElapsed;
					if (obj3 == null)
					{
						EventHandler val2 = DoubleClickTimeElapsed;
						_003C_003EO._003C2_003E__DoubleClickTimeElapsed = val2;
						obj3 = (object)val2;
					}
					obj2.Tick -= (EventHandler)obj3;
					s_DoubleClickTimer = null;
				}
			}
			TryUnsubscribeFromGlobalMouseEvents();
		}
	}

	private static event KeyPressEventHandler s_KeyPressEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			KeyPressEventHandler val = HookManager.m_s_KeyPress;
			KeyPressEventHandler val2;
			do
			{
				val2 = val;
				KeyPressEventHandler val3 = (KeyPressEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyPressEventHandler>(ref HookManager.m_s_KeyPress, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			KeyPressEventHandler val = HookManager.m_s_KeyPress;
			KeyPressEventHandler val2;
			do
			{
				val2 = val;
				KeyPressEventHandler val3 = (KeyPressEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyPressEventHandler>(ref HookManager.m_s_KeyPress, val3, val2);
			}
			while (val != val2);
		}
	}

	public static event KeyPressEventHandler KeyPress
	{
		add
		{
			EnsureSubscribedToGlobalKeyboardEvents();
			s_KeyPress += value;
		}
		remove
		{
			s_KeyPress -= value;
			TryUnsubscribeFromGlobalKeyboardEvents();
		}
	}

	private static event KeyEventHandler s_KeyUpEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			KeyEventHandler val = HookManager.m_s_KeyUp;
			KeyEventHandler val2;
			do
			{
				val2 = val;
				KeyEventHandler val3 = (KeyEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyEventHandler>(ref HookManager.m_s_KeyUp, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			KeyEventHandler val = HookManager.m_s_KeyUp;
			KeyEventHandler val2;
			do
			{
				val2 = val;
				KeyEventHandler val3 = (KeyEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyEventHandler>(ref HookManager.m_s_KeyUp, val3, val2);
			}
			while (val != val2);
		}
	}

	public static event KeyEventHandler KeyUp
	{
		add
		{
			EnsureSubscribedToGlobalKeyboardEvents();
			s_KeyUp += value;
		}
		remove
		{
			s_KeyUp -= value;
			TryUnsubscribeFromGlobalKeyboardEvents();
		}
	}

	private static event KeyEventHandler s_KeyDownEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			KeyEventHandler val = HookManager.m_s_KeyDown;
			KeyEventHandler val2;
			do
			{
				val2 = val;
				KeyEventHandler val3 = (KeyEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyEventHandler>(ref HookManager.m_s_KeyDown, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			KeyEventHandler val = HookManager.m_s_KeyDown;
			KeyEventHandler val2;
			do
			{
				val2 = val;
				KeyEventHandler val3 = (KeyEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyEventHandler>(ref HookManager.m_s_KeyDown, val3, val2);
			}
			while (val != val2);
		}
	}

	public static event KeyEventHandler KeyDown
	{
		add
		{
			EnsureSubscribedToGlobalKeyboardEvents();
			s_KeyDown += value;
		}
		remove
		{
			s_KeyDown -= value;
			TryUnsubscribeFromGlobalKeyboardEvents();
		}
	}

	private static int MouseHookProc(int nCode, int wParam, nint lParam)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		if (nCode >= 0)
		{
			MouseLLHookStruct mouseLLHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure((global::System.IntPtr)lParam, typeof(MouseLLHookStruct));
			MouseButtons buttons = (MouseButtons)0;
			short num = 0;
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			switch (wParam)
			{
			case 513:
				flag = true;
				buttons = (MouseButtons)1048576;
				num2 = 1;
				break;
			case 514:
				flag2 = true;
				buttons = (MouseButtons)1048576;
				num2 = 1;
				break;
			case 515:
				buttons = (MouseButtons)1048576;
				num2 = 2;
				break;
			case 516:
				flag = true;
				buttons = (MouseButtons)2097152;
				num2 = 1;
				break;
			case 517:
				flag2 = true;
				buttons = (MouseButtons)2097152;
				num2 = 1;
				break;
			case 518:
				buttons = (MouseButtons)2097152;
				num2 = 2;
				break;
			case 519:
				flag = true;
				buttons = (MouseButtons)4194304;
				num2 = 1;
				break;
			case 520:
				flag2 = true;
				buttons = (MouseButtons)4194304;
				num2 = 1;
				break;
			case 521:
				buttons = (MouseButtons)4194304;
				num2 = 2;
				break;
			case 522:
				num = (short)((mouseLLHookStruct.MouseData >> 16) & 0xFFFF);
				break;
			case 523:
			case 524:
				switch ((short)((mouseLLHookStruct.MouseData >> 16) & 0xFFFF))
				{
				case 1:
					buttons = (MouseButtons)8388608;
					break;
				case 2:
					buttons = (MouseButtons)16777216;
					break;
				}
				flag2 = wParam == 524;
				flag = wParam == 523;
				break;
			}
			MouseEventExtArgs mouseEventExtArgs = new MouseEventExtArgs(buttons, num2, mouseLLHookStruct.Point.X, mouseLLHookStruct.Point.Y, num);
			if (HookManager.s_MouseUp != null && flag2)
			{
				HookManager.s_MouseUp.Invoke((object)null, (MouseEventArgs)(object)mouseEventExtArgs);
			}
			if (HookManager.s_MouseDown != null && flag)
			{
				HookManager.s_MouseDown.Invoke((object)null, (MouseEventArgs)(object)mouseEventExtArgs);
			}
			if (HookManager.s_MouseClick != null && num2 > 0)
			{
				HookManager.s_MouseClick.Invoke((object)null, (MouseEventArgs)(object)mouseEventExtArgs);
			}
			if (HookManager.s_MouseClickExt != null && num2 > 0)
			{
				HookManager.s_MouseClickExt.Invoke((object)null, mouseEventExtArgs);
			}
			if (HookManager.s_MouseDoubleClick != null && num2 == 2)
			{
				HookManager.s_MouseDoubleClick.Invoke((object)null, (MouseEventArgs)(object)mouseEventExtArgs);
			}
			if (HookManager.s_MouseWheel != null && num != 0)
			{
				HookManager.s_MouseWheel.Invoke((object)null, (MouseEventArgs)(object)mouseEventExtArgs);
			}
			if ((HookManager.s_MouseMove != null || HookManager.s_MouseMoveExt != null) && (m_OldX != mouseLLHookStruct.Point.X || m_OldY != mouseLLHookStruct.Point.Y))
			{
				m_OldX = mouseLLHookStruct.Point.X;
				m_OldY = mouseLLHookStruct.Point.Y;
				if (HookManager.s_MouseMove != null)
				{
					HookManager.s_MouseMove.Invoke((object)null, (MouseEventArgs)(object)mouseEventExtArgs);
				}
				if (HookManager.s_MouseMoveExt != null)
				{
					HookManager.s_MouseMoveExt.Invoke((object)null, mouseEventExtArgs);
				}
			}
			if (mouseEventExtArgs.Handled)
			{
				return -1;
			}
		}
		return CallNextHookEx(s_MouseHookHandle, nCode, wParam, lParam);
	}

	private static void EnsureSubscribedToGlobalMouseEvents()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (s_MouseHookHandle == 0)
		{
			s_MouseDelegate = MouseHookProc;
			s_MouseHookHandle = SetWindowsHookEx(14, s_MouseDelegate, GetModuleHandle(null), 0);
			if (s_MouseHookHandle == 0)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}
	}

	private static void TryUnsubscribeFromGlobalMouseEvents()
	{
		if (HookManager.s_MouseClick == null && HookManager.s_MouseDown == null && HookManager.s_MouseMove == null && HookManager.s_MouseUp == null && HookManager.s_MouseClickExt == null && HookManager.s_MouseMoveExt == null && HookManager.s_MouseWheel == null)
		{
			ForceUnsunscribeFromGlobalMouseEvents();
		}
	}

	private static void ForceUnsunscribeFromGlobalMouseEvents()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (s_MouseHookHandle != 0)
		{
			int num = UnhookWindowsHookEx(s_MouseHookHandle);
			s_MouseHookHandle = 0;
			s_MouseDelegate = null;
			if (num == 0)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}
	}

	private static int KeyboardHookProc(int nCode, int wParam, nint lParam)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		bool flag = false;
		if (nCode >= 0)
		{
			KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure((global::System.IntPtr)lParam, typeof(KeyboardHookStruct));
			if (HookManager.s_KeyDown != null && (wParam == 256 || wParam == 260))
			{
				KeyEventArgs val = new KeyEventArgs((Keys)keyboardHookStruct.VirtualKeyCode);
				HookManager.s_KeyDown.Invoke((object)null, val);
				flag = val.Handled;
			}
			if (HookManager.s_KeyPress != null && wParam == 256)
			{
				bool flag2 = (GetKeyState(16) & 0x80) == 128;
				bool flag3 = GetKeyState(20) != 0;
				byte[] array = new byte[256];
				GetKeyboardState(array);
				byte[] array2 = new byte[2];
				if (ToAscii(keyboardHookStruct.VirtualKeyCode, keyboardHookStruct.ScanCode, array, array2, keyboardHookStruct.Flags) == 1)
				{
					char c = (char)array2[0];
					if ((flag3 ^ flag2) && char.IsLetter(c))
					{
						c = char.ToUpper(c);
					}
					KeyPressEventArgs val2 = new KeyPressEventArgs(c);
					HookManager.s_KeyPress.Invoke((object)null, val2);
					flag = flag || val2.Handled;
				}
			}
			if (HookManager.s_KeyUp != null && (wParam == 257 || wParam == 261))
			{
				KeyEventArgs val3 = new KeyEventArgs((Keys)keyboardHookStruct.VirtualKeyCode);
				HookManager.s_KeyUp.Invoke((object)null, val3);
				flag = flag || val3.Handled;
			}
		}
		if (flag)
		{
			return -1;
		}
		return CallNextHookEx(s_KeyboardHookHandle, nCode, wParam, lParam);
	}

	private static void EnsureSubscribedToGlobalKeyboardEvents()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (s_KeyboardHookHandle == 0)
		{
			s_KeyboardDelegate = KeyboardHookProc;
			s_KeyboardHookHandle = SetWindowsHookEx(13, s_KeyboardDelegate, GetModuleHandle(null), 0);
			if (s_KeyboardHookHandle == 0)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}
	}

	private static void TryUnsubscribeFromGlobalKeyboardEvents()
	{
		if (HookManager.s_KeyDown == null && HookManager.s_KeyUp == null && HookManager.s_KeyPress == null)
		{
			ForceUnsunscribeFromGlobalKeyboardEvents();
		}
	}

	private static void ForceUnsunscribeFromGlobalKeyboardEvents()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (s_KeyboardHookHandle != 0)
		{
			int num = UnhookWindowsHookEx(s_KeyboardHookHandle);
			s_KeyboardHookHandle = 0;
			s_KeyboardDelegate = null;
			if (num == 0)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}
	}

	private static void DoubleClickTimeElapsed(object sender, EventArgs e)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		s_DoubleClickTimer.Enabled = false;
		s_PrevClickedButton = (MouseButtons)0;
	}

	private static void OnMouseUp(object sender, MouseEventArgs e)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (e.Clicks < 1)
		{
			return;
		}
		MouseButtons button = e.Button;
		if (button == s_PrevClickedButton)
		{
			if (HookManager.s_MouseDoubleClick != null)
			{
				HookManager.s_MouseDoubleClick.Invoke((object)null, e);
			}
			s_DoubleClickTimer.Enabled = false;
			s_PrevClickedButton = (MouseButtons)0;
		}
		else
		{
			s_DoubleClickTimer.Enabled = true;
			s_PrevClickedButton = e.Button;
		}
	}

	[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
	private static extern int CallNextHookEx(int idHook, int nCode, int wParam, nint lParam);

	[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
	private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, nint hMod, int dwThreadId);

	[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
	private static extern int UnhookWindowsHookEx(int idHook);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	private static extern nint GetModuleHandle(string? lpModuleName);

	[DllImport("user32")]
	public static extern int GetDoubleClickTime();

	[DllImport("user32")]
	private static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

	[DllImport("user32")]
	private static extern int GetKeyboardState(byte[] pbKeyState);

	[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
	private static extern short GetKeyState(int vKey);
}
