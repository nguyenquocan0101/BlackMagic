using System;
using System.Windows.Forms;

namespace bb_monitor.Utils;

public static class ControlExtensions
{
	public static void PostOnUiThread(this Control control, Action action)
	{
		if (control.IsHandleCreated)
		{
			control.BeginInvoke(action);
		}
	}

	public static void InvokeOnUiThread(this Control control, Action action)
	{
		try
		{
			if (control.InvokeRequired)
			{
				control.Invoke(action);
			}
			else
			{
				action();
			}
		}
		catch
		{
		}
	}
}
