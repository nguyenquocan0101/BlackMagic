using System.Windows.Forms;

namespace bb_monitor.Utils.UserActivityMonitor;

public class MouseEventExtArgs : MouseEventArgs
{
	private bool m_Handled;

	public bool Handled
	{
		get
		{
			return m_Handled;
		}
		set
		{
			m_Handled = value;
		}
	}

	public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta)
		: base(buttons, clicks, x, y, delta)
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)


	internal MouseEventExtArgs(MouseEventArgs e)
		: base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
	{
	}//IL_0002: Unknown result type (might be due to invalid IL or missing references)

}
