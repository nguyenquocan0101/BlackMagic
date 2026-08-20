using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using bb_monitor.Utils;

namespace bb_monitor.Forms;

public class OpeningForm : Form, IOpeningForm
{
	private string _curStatus = "Initializing";

	private IContainer components;

	private TableLayoutPanel tableLayoutPanel1;

	private Label label1;

	private Label label2;

	private Label labCurStatus;

	public OpeningForm()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		InitializeComponent();
		((Form)this).Load += (EventHandler)([CompilerGenerated] (object? s, EventArgs e) =>
		{
			((Control)labCurStatus).Text = _curStatus;
			((Control)labCurStatus).Invalidate();
			((Control)this).Invalidate();
		});
	}

	public void SetStatus(string status)
	{
		string status2 = status;
		((Control)this).InvokeOnUiThread(delegate
		{
			_curStatus = status2;
			((Control)labCurStatus).Text = status2;
			((Control)labCurStatus).Invalidate();
			((Control)this).Invalidate();
		});
	}

	void IOpeningForm.Close()
	{
		((Control)this).InvokeOnUiThread((Action)([CompilerGenerated] () =>
		{
			((Form)this).Close();
		}));
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			((global::System.IDisposable)components).Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		tableLayoutPanel1 = new TableLayoutPanel();
		label1 = new Label();
		label2 = new Label();
		labCurStatus = new Label();
		((Control)tableLayoutPanel1).SuspendLayout();
		((Control)this).SuspendLayout();
		tableLayoutPanel1.BackColor = Color.White;
		tableLayoutPanel1.ColumnCount = 1;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle((SizeType)2, 100f));
		tableLayoutPanel1.Controls.Add((Control)(object)label1, 0, 0);
		tableLayoutPanel1.Controls.Add((Control)(object)label2, 0, 1);
		tableLayoutPanel1.Controls.Add((Control)(object)labCurStatus, 0, 2);
		((Control)tableLayoutPanel1).Dock = (DockStyle)5;
		((Control)tableLayoutPanel1).Location = new Point(0, 0);
		((Control)tableLayoutPanel1).Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 3;
		tableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)1, 52f));
		tableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)1, 22f));
		tableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)2, 26f));
		tableLayoutPanel1.Padding = new Padding(10);
		((Control)tableLayoutPanel1).Size = new Size(360, 110);
		((Control)tableLayoutPanel1).TabIndex = 0;
		((Control)label1).Dock = (DockStyle)5;
		((Control)label1).AutoSize = false;
		((Control)label1).Font = new Font("Segoe UI", 14f, FontStyle.Regular);
		((Control)label1).ForeColor = Color.Black;
		((Control)label1).Location = new Point(3, 3);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(340, 40);
		((Control)label1).TabIndex = 0;
		label1.TextAlign = ContentAlignment.MiddleCenter;
		((Control)label1).Text = "bb_monitor";
		((Control)label2).Dock = (DockStyle)5;
		((Control)label2).AutoSize = false;
		((Control)label2).ForeColor = Color.Gray;
		((Control)label2).Location = new Point(3, 80);
		((Control)label2).Name = "label2";
		((Control)label2).Size = new Size(340, 24);
		((Control)label2).TabIndex = 1;
		label2.TextAlign = ContentAlignment.MiddleCenter;
		((Control)label2).Text = "Starting...";
		tableLayoutPanel1.SetColumnSpan((Control)(object)labCurStatus, 1);
		((Control)labCurStatus).Dock = (DockStyle)5;
		((Control)labCurStatus).ForeColor = Color.Gray;
		((Control)labCurStatus).Location = new Point(3, 112);
		((Control)labCurStatus).Name = "labCurStatus";
		((Control)labCurStatus).Size = new Size(340, 26);
		((Control)labCurStatus).TabIndex = 2;
		((Control)labCurStatus).Text = "Initializing";
		labCurStatus.TextAlign = ContentAlignment.MiddleCenter;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(8f, 20f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).BackColor = Color.White;
		((Form)this).ClientSize = new Size(360, 110);
		((Control)this).Controls.Add((Control)(object)tableLayoutPanel1);
		((Form)this).FormBorderStyle = FormBorderStyle.FixedDialog;
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "OpeningForm";
		((Control)this).Padding = new Padding(0);
		((Form)this).StartPosition = FormStartPosition.CenterScreen;
		((Form)this).ShowInTaskbar = false;
		((Control)this).Text = "bb_monitor";
		((Control)tableLayoutPanel1).ResumeLayout(false);
		((Control)tableLayoutPanel1).PerformLayout();
		((Control)this).ResumeLayout(false);
	}
}
