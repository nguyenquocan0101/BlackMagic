using System.Windows.Forms;

namespace bb_monitor.Utils;

public static class Dialogs
{
	public static DialogResult ShowError(string message, string title = "Error")
	{
		return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
	}

	public static DialogResult ShowWarning(string message, string title = "Error")
	{
		return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
	}

	public static DialogResult ShowInfo(string message, string title)
	{
		return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
	}
}
