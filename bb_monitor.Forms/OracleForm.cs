using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using bb_monitor.Utils;
using bb_monitor.Utils.UserActivityMonitor;

namespace bb_monitor.Forms;

public class OracleForm : Form, IMessageFilter
{
	private volatile bool _textDispReady;

	private string? _curSearchString;

	private bool _typingEnabled;

	private Point? _lastClickLocation;

	private bool _overlayRequested;

	private const int WS_EX_NOACTIVATE = 134217728;

	private const int WS_EX_TOOLWINDOW = 128;

	private IContainer components;

	private TextDisplay textDisp;

	private string? CurSearchString
	{
		get
		{
			return _curSearchString;
		}
		set
		{
			_curSearchString = value;
			if (_textDispReady)
			{
				textDisp.SetMatch(value);
			}
		}
	}

	protected override bool ShowWithoutActivation => true;

	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			createParams.ExStyle |= 0x8000080;
			return createParams;
		}
	}

	public OracleForm()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		InitializeComponent();
		((Form)this).Size = new Size(500, 125);
		textDisp.Suspended = true;
		global::System.Threading.Tasks.Task.Run(new Action(InitializeTextDisplay));
		RegisterHooks();
		InitializeRendering();
	}

	public bool PreFilterMessage(ref Message m)
	{
		if (m.Msg == 513)
		{
			return true;
		}
		return false;
	}

	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
		WinAPI.User32.SetWindowDisplayAffinity(((Control)this).Handle, 17u);
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		if (!_overlayRequested)
		{
			Hide();
		}
	}

	public bool OverlayRequested => _overlayRequested;

	public bool IsTextDisplayReady => _textDispReady;

	public void ShowOverlay()
	{
		_overlayRequested = true;
		if (!IsHandleCreated || IsDisposed)
		{
			return;
		}
		try
		{
			BeginInvoke((Action)(() =>
			{
				if (!IsDisposed && !Visible)
				{
					Show();
				}
			}));
		}
		catch
		{
		}
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		base.OnFormClosing(e);
		UnregisterHooks();
	}

	private void InitializeRendering()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		SetStyle((ControlStyles)133122, true);
		((Control)this).BackColor = Color.DarkGray;
		((Form)this).TransparencyKey = Color.DarkGray;
		((Form)this).TopMost = true;
		((Form)this).Opacity = 0.15;
	}

	private void InitializeTextDisplay()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		textDisp.AddLines(Enumerable.Repeat<string>("", 100));
		_textDispReady = false;
		global::System.Collections.Generic.IEnumerator<string> enumerator = Directory.EnumerateFiles("input", "*.*", (SearchOption)1).GetEnumerator();
		try
		{
			while (((global::System.Collections.IEnumerator)enumerator).MoveNext())
			{
				StreamReader val = new StreamReader((Stream)(object)File.OpenRead(enumerator.Current));
				try
				{
					textDisp.AddFromReader(val);
				}
				finally
				{
					((global::System.IDisposable)val)?.Dispose();
				}
			}
		}
		finally
		{
			((global::System.IDisposable)enumerator)?.Dispose();
		}
		((Control)textDisp).Invoke((Action)([CompilerGenerated] () =>
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			((Control)textDisp).ForeColor = Color.Gray;
			textDisp.AntiAliasing = false;
			if (CurSearchString != null)
			{
				textDisp.SetMatch(CurSearchString);
			}
			textDisp.Suspended = false;
		}));
		_textDispReady = true;
	}

	private void RegisterHooks()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		HookManager.MouseWheel += new MouseEventHandler(HookManager_MouseWheel);
		HookManager.KeyDown += new KeyEventHandler(HookManager_KeyDown);
		HookManager.MouseDown += new MouseEventHandler(HookManager_MouseDown);
		HookManager.MouseUp += new MouseEventHandler(HookManager_MouseUp);
		HookManager.KeyPress += new KeyPressEventHandler(HookManager_KeyPress);
	}

	private void HookManager_KeyPress(object? sender, KeyPressEventArgs e)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		KeyPressEventArgs e2 = e;
		ShowOverlay();
		((Control)this).BeginInvoke((Action)delegate
		{
			if (char.IsLetterOrDigit(e2.KeyChar) || char.IsWhiteSpace(e2.KeyChar))
			{
				OracleForm oracleForm = this;
			char keyChar = e2.KeyChar;
			oracleForm.CurSearchString = string.Concat(CurSearchString, keyChar);
			}
		});
	}

	private void UnregisterHooks()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		HookManager.MouseWheel -= new MouseEventHandler(HookManager_MouseWheel);
		HookManager.KeyDown -= new KeyEventHandler(HookManager_KeyDown);
		HookManager.MouseDown -= new MouseEventHandler(HookManager_MouseDown);
		HookManager.MouseUp -= new MouseEventHandler(HookManager_MouseUp);
		HookManager.KeyPress -= new KeyPressEventHandler(HookManager_KeyPress);
	}

	private void HookManager_MouseUp(object? sender, MouseEventArgs e)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		MouseEventArgs e2 = e;
		((Control)this).PostOnUiThread((Action)delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Invalid comparison between Unknown and I4
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			if ((int)e2.Button == 2097152 && _lastClickLocation.HasValue)
			{
				ProcessOcr(_lastClickLocation.Value, e2.Location);
				_lastClickLocation = null;
			}
		});
	}

	private void HookManager_KeyDown(object? sender, KeyEventArgs e)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		KeyEventArgs e2 = e;
		((Control)this).PostOnUiThread((Action)delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Invalid comparison between Unknown and I4
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Invalid comparison between Unknown and I4
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Invalid comparison between Unknown and I4
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Invalid comparison between Unknown and I4
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Invalid comparison between Unknown and I4
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Invalid comparison between Unknown and I4
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Invalid comparison between Unknown and I4
			Keys keyCode = e2.KeyCode;
			if ((int)keyCode <= 13)
			{
				if ((int)keyCode != 8)
				{
					if ((int)keyCode == 13)
					{
						textDisp.NextMatch();
					}
				}
				else
				{
					CurSearchString = (((CurSearchString?.Length ?? 0) < 1) ? null : CurSearchString.Substring(0, CurSearchString.Length - 1));
				}
			}
			else if ((int)keyCode != 69)
			{
				if ((int)keyCode == 220 || (int)keyCode == 226)
				{
					CurSearchString = null;
				}
			}
			else if ((int)Control.ModifierKeys == 131072)
			{
				textDisp.Suspended = !textDisp.Suspended;
			}
		});
	}

	private void HookManager_MouseWheel(object? sender, MouseEventArgs e)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		MouseEventArgs e2 = e;
		((Control)this).PostOnUiThread((Action)delegate
		{
			if (e2.Delta < 0)
			{
				textDisp.ScrollText(up: false);
			}
			if (e2.Delta > 0)
			{
				textDisp.ScrollText(up: true);
			}
		});
	}

	private void HookManager_MouseDown(object? sender, MouseEventArgs e)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		MouseEventArgs e2 = e;
		((Control)this).PostOnUiThread((Action)delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Invalid comparison between Unknown and I4
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Invalid comparison between Unknown and I4
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Invalid comparison between Unknown and I4
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Invalid comparison between Unknown and I4
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Invalid comparison between Unknown and I4
			MouseButtons button = e2.Button;
			if ((int)button <= 4194304)
			{
				if ((int)button != 2097152)
				{
					if ((int)button == 4194304)
					{
						textDisp.NextMatch();
					}
				}
				else
				{
					_lastClickLocation = new Point(e2.X, e2.Y);
				}
			}
			else if ((int)button != 8388608)
			{
				if ((int)button == 16777216)
				{
					CurSearchString = null;
				}
			}
			else
			{
				textDisp.NextMatch();
			}
		});
	}

	private void ProcessOcr(Point mouseStart, Point mouseEnd)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Expected O, but got Unknown
		try
		{
			Rectangle val = new Rectangle(
				Math.Min(mouseEnd.X, mouseStart.X),
				Math.Min(mouseEnd.Y, mouseStart.Y),
				Math.Abs(mouseEnd.X - mouseStart.X),
				Math.Abs(mouseEnd.Y - mouseStart.Y));
			if (val.Width * val.Height < 20)
			{
				CurSearchString = null;
				return;
			}
			string text = "";
			double conf = 0.0;
			Bitmap val2 = new Bitmap(val.Width, val.Height);
			try
			{
				Graphics val3 = Graphics.FromImage((Image)(object)val2);
				try
				{
					val3.CopyFromScreen(val.Location, Point.Empty, val.Size, (CopyPixelOperation)13369376);
					val3.Save();
				}
				finally
				{
					((global::System.IDisposable)val3)?.Dispose();
				}
				Size val4 = val.Size.ScaleToWidth(2000.0);
				Bitmap val5 = new Bitmap(val4.Width, val4.Height);
				try
				{
					Graphics val6 = Graphics.FromImage((Image)(object)val5);
					try
					{
						val6.InterpolationMode = (InterpolationMode)2;
						val6.CompositingQuality = (CompositingQuality)2;
						val6.SmoothingMode = (SmoothingMode)4;
						val6.DrawImage((Image)(object)val2, new Rectangle(Point.Empty, val4));
						val6.Save();
					}
					finally
					{
						((global::System.IDisposable)val6)?.Dispose();
					}
					TesseractEngine val7 = new TesseractEngine("tess", "eng+vie", (EngineMode)3);
					try
					{
						Page val8 = TesseractDrawingExtensions.Process(val7, val5, (PageSegMode?)null);
						try
						{
							text = val8.GetText();
							conf = val8.GetMeanConfidence();
						}
						finally
						{
							((global::System.IDisposable)val8)?.Dispose();
						}
					}
					finally
					{
						((global::System.IDisposable)val7)?.Dispose();
					}
					if (conf < 0.5 || string.IsNullOrWhiteSpace(text))
					{
						CurSearchString = null;
						return;
					}
					ShowOverlay();
				}
				finally
				{
					((global::System.IDisposable)val5)?.Dispose();
				}
			}
			finally
			{
				((global::System.IDisposable)val2)?.Dispose();
			}
			text = text.ToLower();
			text = StringUtils.ReplaceDiacritics(text);
			text = Regex.Replace(text, "[^A-Za-z0-9]", " ");
			text = Regex.Replace(text, " +", " ").Trim();
			Random rand = new Random();
			global::System.Collections.Generic.IEnumerable<string> enumerable = Enumerable.Select<string, string>(Enumerable.Select<string, string>(Enumerable.Where<string>((global::System.Collections.Generic.IEnumerable<string>)text.Split(' ', (StringSplitOptions)0), (Func<string, bool>)((string x) => x.Length > 2)), (Func<string, string>)((string x) => (x == "itis") ? "it is" : x)), (Func<string, string>)((string x) => (x.Length <= 4) ? x : string.Join<char>("", Enumerable.Select<char, char>((global::System.Collections.Generic.IEnumerable<char>)x, (Func<char, char>)((char y) => (!(rand.NextDouble() > conf)) ? y : ' ')))));
			text = string.Join(" ", enumerable);
			CurSearchString = text;
		}
		catch
		{
			CurSearchString = null;
		}
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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		textDisp = new TextDisplay();
		((Control)this).SuspendLayout();
		((Control)textDisp).BackColor = Color.Transparent;
		((Control)textDisp).Dock = (DockStyle)5;
		((Control)textDisp).Location = new Point(0, 0);
		((Control)textDisp).Name = "textDisp";
		((Control)textDisp).Size = new Size(800, 450);
		((Control)textDisp).TabIndex = 0;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(8f, 20f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(800, 450);
		((Control)this).Controls.Add((Control)(object)textDisp);
		((Form)this).FormBorderStyle = (FormBorderStyle)0;
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "OracleForm";
		((Form)this).ShowIcon = false;
		((Form)this).ShowInTaskbar = false;
		((Control)this).Text = "Form1";
		((Control)this).ResumeLayout(false);
	}
}
