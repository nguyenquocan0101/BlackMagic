using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace bb_monitor.Utils.UserActivityMonitor;

[DesignerCategory("Code")]
public class GlobalEventProvider : Component
{
	[CompilerGenerated]
	private MouseEventHandler m_m_MouseMove;

	[CompilerGenerated]
	private MouseEventHandler m_m_MouseClick;

	[CompilerGenerated]
	private MouseEventHandler m_m_MouseDown;

	[CompilerGenerated]
	private MouseEventHandler m_m_MouseUp;

	[CompilerGenerated]
	private MouseEventHandler m_m_MouseDoubleClick;

	[CompilerGenerated]
	private EventHandler<MouseEventExtArgs> m_m_MouseMoveExt;

	[CompilerGenerated]
	private EventHandler<MouseEventExtArgs> m_m_MouseClickExt;

	[CompilerGenerated]
	private KeyPressEventHandler m_m_KeyPress;

	[CompilerGenerated]
	private KeyEventHandler m_m_KeyUp;

	[CompilerGenerated]
	private KeyEventHandler m_m_KeyDown;

	private MouseEventHandler m_MouseMove;
	private MouseEventHandler m_MouseClick;
	private MouseEventHandler m_MouseDown;
	private MouseEventHandler m_MouseUp;
	private MouseEventHandler m_MouseDoubleClick;
	private EventHandler<MouseEventExtArgs> m_MouseMoveExt;
	private EventHandler<MouseEventExtArgs> m_MouseClickExt;
	private KeyPressEventHandler m_KeyPress;
	private KeyEventHandler m_KeyUp;
	private KeyEventHandler m_KeyDown;

	protected override bool CanRaiseEvents => true;

	private event MouseEventHandler m_MouseMoveEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			MouseEventHandler val = this.m_m_MouseMove;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref this.m_m_MouseMove, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			MouseEventHandler val = this.m_m_MouseMove;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref this.m_m_MouseMove, val3, val2);
			}
			while (val != val2);
		}
	}

	public event MouseEventHandler MouseMove
	{
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (this.m_MouseMove == null)
			{
				HookManager.MouseMove += new MouseEventHandler(HookManager_MouseMove);
			}
			m_MouseMove += value;
		}
		remove
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			m_MouseMove -= value;
			if (this.m_MouseMove == null)
			{
				HookManager.MouseMove -= new MouseEventHandler(HookManager_MouseMove);
			}
		}
	}

	private event MouseEventHandler m_MouseClickEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			MouseEventHandler val = this.m_m_MouseClick;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref this.m_m_MouseClick, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			MouseEventHandler val = this.m_m_MouseClick;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref this.m_m_MouseClick, val3, val2);
			}
			while (val != val2);
		}
	}

	public event MouseEventHandler MouseClick
	{
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (this.m_MouseClick == null)
			{
				HookManager.MouseClick += new MouseEventHandler(HookManager_MouseClick);
			}
			m_MouseClick += value;
		}
		remove
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			m_MouseClick -= value;
			if (this.m_MouseClick == null)
			{
				HookManager.MouseClick -= new MouseEventHandler(HookManager_MouseClick);
			}
		}
	}

	private event MouseEventHandler m_MouseDownEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			MouseEventHandler val = this.m_m_MouseDown;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref this.m_m_MouseDown, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			MouseEventHandler val = this.m_m_MouseDown;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref this.m_m_MouseDown, val3, val2);
			}
			while (val != val2);
		}
	}

	public event MouseEventHandler MouseDown
	{
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (this.m_MouseDown == null)
			{
				HookManager.MouseDown += new MouseEventHandler(HookManager_MouseDown);
			}
			m_MouseDown += value;
		}
		remove
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			m_MouseDown -= value;
			if (this.m_MouseDown == null)
			{
				HookManager.MouseDown -= new MouseEventHandler(HookManager_MouseDown);
			}
		}
	}

	private event MouseEventHandler m_MouseUpEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			MouseEventHandler val = this.m_m_MouseUp;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref this.m_m_MouseUp, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			MouseEventHandler val = this.m_m_MouseUp;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref this.m_m_MouseUp, val3, val2);
			}
			while (val != val2);
		}
	}

	public event MouseEventHandler MouseUp
	{
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (this.m_MouseUp == null)
			{
				HookManager.MouseUp += new MouseEventHandler(HookManager_MouseUp);
			}
			m_MouseUp += value;
		}
		remove
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			m_MouseUp -= value;
			if (this.m_MouseUp == null)
			{
				HookManager.MouseUp -= new MouseEventHandler(HookManager_MouseUp);
			}
		}
	}

	private event MouseEventHandler m_MouseDoubleClickEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			MouseEventHandler val = this.m_m_MouseDoubleClick;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref this.m_m_MouseDoubleClick, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			MouseEventHandler val = this.m_m_MouseDoubleClick;
			MouseEventHandler val2;
			do
			{
				val2 = val;
				MouseEventHandler val3 = (MouseEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<MouseEventHandler>(ref this.m_m_MouseDoubleClick, val3, val2);
			}
			while (val != val2);
		}
	}

	public event MouseEventHandler MouseDoubleClick
	{
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (this.m_MouseDoubleClick == null)
			{
				HookManager.MouseDoubleClick += new MouseEventHandler(HookManager_MouseDoubleClick);
			}
			m_MouseDoubleClick += value;
		}
		remove
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			m_MouseDoubleClick -= value;
			if (this.m_MouseDoubleClick == null)
			{
				HookManager.MouseDoubleClick -= new MouseEventHandler(HookManager_MouseDoubleClick);
			}
		}
	}

	private event EventHandler<MouseEventExtArgs> m_MouseMoveExtEvent
	{
		[CompilerGenerated]
		add
		{
			EventHandler<MouseEventExtArgs> val = this.m_m_MouseMoveExt;
			EventHandler<MouseEventExtArgs> val2;
			do
			{
				val2 = val;
				EventHandler<MouseEventExtArgs> val3 = (EventHandler<MouseEventExtArgs>)(object)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<EventHandler<MouseEventExtArgs>>(ref this.m_m_MouseMoveExt, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler<MouseEventExtArgs> val = this.m_m_MouseMoveExt;
			EventHandler<MouseEventExtArgs> val2;
			do
			{
				val2 = val;
				EventHandler<MouseEventExtArgs> val3 = (EventHandler<MouseEventExtArgs>)(object)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<EventHandler<MouseEventExtArgs>>(ref this.m_m_MouseMoveExt, val3, val2);
			}
			while (val != val2);
		}
	}

	public event EventHandler<MouseEventExtArgs> MouseMoveExt
	{
		add
		{
			if (this.m_MouseMoveExt == null)
			{
				HookManager.MouseMoveExt += HookManager_MouseMoveExt;
			}
			m_MouseMoveExt += value;
		}
		remove
		{
			m_MouseMoveExt -= value;
			if (this.m_MouseMoveExt == null)
			{
				HookManager.MouseMoveExt -= HookManager_MouseMoveExt;
			}
		}
	}

	private event EventHandler<MouseEventExtArgs> m_MouseClickExtEvent
	{
		[CompilerGenerated]
		add
		{
			EventHandler<MouseEventExtArgs> val = this.m_m_MouseClickExt;
			EventHandler<MouseEventExtArgs> val2;
			do
			{
				val2 = val;
				EventHandler<MouseEventExtArgs> val3 = (EventHandler<MouseEventExtArgs>)(object)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<EventHandler<MouseEventExtArgs>>(ref this.m_m_MouseClickExt, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler<MouseEventExtArgs> val = this.m_m_MouseClickExt;
			EventHandler<MouseEventExtArgs> val2;
			do
			{
				val2 = val;
				EventHandler<MouseEventExtArgs> val3 = (EventHandler<MouseEventExtArgs>)(object)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<EventHandler<MouseEventExtArgs>>(ref this.m_m_MouseClickExt, val3, val2);
			}
			while (val != val2);
		}
	}

	public event EventHandler<MouseEventExtArgs> MouseClickExt
	{
		add
		{
			if (this.m_MouseClickExt == null)
			{
				HookManager.MouseClickExt += HookManager_MouseClickExt;
			}
			m_MouseClickExt += value;
		}
		remove
		{
			m_MouseClickExt -= value;
			if (this.m_MouseClickExt == null)
			{
				HookManager.MouseClickExt -= HookManager_MouseClickExt;
			}
		}
	}

	private event KeyPressEventHandler m_KeyPressEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			KeyPressEventHandler val = this.m_m_KeyPress;
			KeyPressEventHandler val2;
			do
			{
				val2 = val;
				KeyPressEventHandler val3 = (KeyPressEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyPressEventHandler>(ref this.m_m_KeyPress, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			KeyPressEventHandler val = this.m_m_KeyPress;
			KeyPressEventHandler val2;
			do
			{
				val2 = val;
				KeyPressEventHandler val3 = (KeyPressEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyPressEventHandler>(ref this.m_m_KeyPress, val3, val2);
			}
			while (val != val2);
		}
	}

	public event KeyPressEventHandler KeyPress
	{
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (this.m_KeyPress == null)
			{
				HookManager.KeyPress += new KeyPressEventHandler(HookManager_KeyPress);
			}
			m_KeyPress += value;
		}
		remove
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			m_KeyPress -= value;
			if (this.m_KeyPress == null)
			{
				HookManager.KeyPress -= new KeyPressEventHandler(HookManager_KeyPress);
			}
		}
	}

	private event KeyEventHandler m_KeyUpEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			KeyEventHandler val = this.m_m_KeyUp;
			KeyEventHandler val2;
			do
			{
				val2 = val;
				KeyEventHandler val3 = (KeyEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyEventHandler>(ref this.m_m_KeyUp, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			KeyEventHandler val = this.m_m_KeyUp;
			KeyEventHandler val2;
			do
			{
				val2 = val;
				KeyEventHandler val3 = (KeyEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyEventHandler>(ref this.m_m_KeyUp, val3, val2);
			}
			while (val != val2);
		}
	}

	public event KeyEventHandler KeyUp
	{
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (this.m_KeyUp == null)
			{
				HookManager.KeyUp += new KeyEventHandler(HookManager_KeyUp);
			}
			m_KeyUp += value;
		}
		remove
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			m_KeyUp -= value;
			if (this.m_KeyUp == null)
			{
				HookManager.KeyUp -= new KeyEventHandler(HookManager_KeyUp);
			}
		}
	}

	private event KeyEventHandler m_KeyDownEvent
	{
		[CompilerGenerated]
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			KeyEventHandler val = this.m_m_KeyDown;
			KeyEventHandler val2;
			do
			{
				val2 = val;
				KeyEventHandler val3 = (KeyEventHandler)global::System.Delegate.Combine((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyEventHandler>(ref this.m_m_KeyDown, val3, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			KeyEventHandler val = this.m_m_KeyDown;
			KeyEventHandler val2;
			do
			{
				val2 = val;
				KeyEventHandler val3 = (KeyEventHandler)global::System.Delegate.Remove((global::System.Delegate)(object)val2, (global::System.Delegate)(object)value);
				val = Interlocked.CompareExchange<KeyEventHandler>(ref this.m_m_KeyDown, val3, val2);
			}
			while (val != val2);
		}
	}

	public event KeyEventHandler KeyDown
	{
		add
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (this.m_KeyDown == null)
			{
				HookManager.KeyDown += new KeyEventHandler(HookManager_KeyDown);
			}
			m_KeyDown += value;
		}
		remove
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			m_KeyDown -= value;
			if (this.m_KeyDown == null)
			{
				HookManager.KeyDown -= new KeyEventHandler(HookManager_KeyDown);
			}
		}
	}

	private void HookManager_MouseMove(object sender, MouseEventArgs e)
	{
		if (this.m_MouseMove != null)
		{
			this.m_MouseMove.Invoke((object)this, e);
		}
	}

	private void HookManager_MouseClick(object sender, MouseEventArgs e)
	{
		if (this.m_MouseClick != null)
		{
			this.m_MouseClick.Invoke((object)this, e);
		}
	}

	private void HookManager_MouseDown(object sender, MouseEventArgs e)
	{
		if (this.m_MouseDown != null)
		{
			this.m_MouseDown.Invoke((object)this, e);
		}
	}

	private void HookManager_MouseUp(object sender, MouseEventArgs e)
	{
		if (this.m_MouseUp != null)
		{
			this.m_MouseUp.Invoke((object)this, e);
		}
	}

	private void HookManager_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (this.m_MouseDoubleClick != null)
		{
			this.m_MouseDoubleClick.Invoke((object)this, e);
		}
	}

	private void HookManager_MouseMoveExt(object sender, MouseEventExtArgs e)
	{
		if (this.m_MouseMoveExt != null)
		{
			this.m_MouseMoveExt.Invoke((object)this, e);
		}
	}

	private void HookManager_MouseClickExt(object sender, MouseEventExtArgs e)
	{
		if (this.m_MouseClickExt != null)
		{
			this.m_MouseClickExt.Invoke((object)this, e);
		}
	}

	private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (this.m_KeyPress != null)
		{
			this.m_KeyPress.Invoke((object)this, e);
		}
	}

	private void HookManager_KeyUp(object sender, KeyEventArgs e)
	{
		if (this.m_KeyUp != null)
		{
			this.m_KeyUp.Invoke((object)this, e);
		}
	}

	private void HookManager_KeyDown(object sender, KeyEventArgs e)
	{
		this.m_KeyDown.Invoke((object)this, e);
	}
}
