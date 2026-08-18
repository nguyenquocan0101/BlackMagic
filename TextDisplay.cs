using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using bb_monitor.Utils;

namespace bb_monitor;

public class TextDisplay : UserControl
{
	public bool ShowMatch = true;

	public bool AntiAliasing = true;

	public bool Suspended;

	private string _text = "";

	private List<ValueTuple<int, int>> _lines = new List<ValueTuple<int, int>>();

	private int _topIndex;

	private int _sublineOffset;

	private string? _curMatchString;

	private Regex? _curMatchRegex;

	private Match? _curMatch;

	private bool _matchRunning;

	private CancellationTokenSource? _matchCts;

	private object _matchLock = new object();

	private IContainer components;

	private int TopIndex
	{
		get
		{
			return _topIndex;
		}
		set
		{
			if (value <= 0)
			{
				value = 0;
			}
			else if (value >= _lines.Count)
			{
				value = _lines.Count - 1;
			}
			_topIndex = value;
			_sublineOffset = 0;
			((Control)this).Invalidate();
		}
	}

	public TextDisplay()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		InitializeComponent();
		SetStyle((ControlStyles)2, true);
		SetStyle((ControlStyles)131072, true);
		DoubleBuffered = true;
		SetStyle((ControlStyles)2048, true);
		SetStyle((ControlStyles)8192, true);
		SetStyle((ControlStyles)16, true);
		((Control)this).BackColor = Color.Transparent;
	}

	private string GetLine(int index)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		ValueTuple<int, int> val = _lines[index];
		return _text.Substring(val.Item1, val.Item2 - val.Item1);
	}

	private int TextIndexToLineIndex(int textIndex)
	{
		return _lines.FindIndex((Predicate<ValueTuple<int, int>>)((ValueTuple<int, int> x) => x.Item1 <= textIndex && textIndex <= x.Item2));
	}

	public void AddLine(string input)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if (input != null)
		{
			string[] array = input.Replace("\r\n", "\n").Split('\n', (StringSplitOptions)0);
			foreach (string text in array)
			{
				_text = _text + text + "\n";
				_lines.Add(new ValueTuple<int, int>(_text.Length - 1 - text.Length, _text.Length - 1));
			}
		}
	}

	public void AddLines(global::System.Collections.Generic.IEnumerable<string> inputs)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		StringBuilder val = new StringBuilder();
		int length = _text.Length;
		global::System.Collections.Generic.IEnumerator<string> enumerator = inputs.GetEnumerator();
		try
		{
			while (((global::System.Collections.IEnumerator)enumerator).MoveNext())
			{
				string current = enumerator.Current;
				string[] array = current.Replace("\r\n", "\n").Split('\n', (StringSplitOptions)0);
				foreach (string text in array)
				{
					val.Append(text + "\n");
					int num = val.Length + length;
					_lines.Add(new ValueTuple<int, int>(num - 1 - text.Length, num - 1));
				}
				AddLine(current);
			}
		}
		finally
		{
			((global::System.IDisposable)enumerator)?.Dispose();
		}
		_text += ((object)val).ToString();
	}

	public void AddLines(params string[] lines)
	{
		AddLines(Enumerable.AsEnumerable<string>((global::System.Collections.Generic.IEnumerable<string>)lines));
	}

	public void AddFromReader(StreamReader reader)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		StringBuilder val = new StringBuilder();
		int length = _text.Length;
		while (!reader.EndOfStream)
		{
			string[] array = ((TextReader)reader).ReadLine().Replace("\r\n", "\n").Split('\n', (StringSplitOptions)0);
			foreach (string text in array)
			{
				val.Append(text + "\n");
				int num = val.Length + length;
				_lines.Add(new ValueTuple<int, int>(num - 1 - text.Length, num - 1));
			}
		}
		_text += StringUtils.ReplaceDiacritics(((object)val).ToString());
	}

	public void SetMatch(string? match)
	{
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		if (Suspended)
		{
			return;
		}
		match = ((match == null) ? null : Regex.Replace(match.Trim(), " +", " "));
		if (match != null)
		{
			match = StringUtils.ReplaceDiacritics(match);
		}
		if (_curMatchRegex != null && _curMatchString == match)
		{
			return;
		}
		_ = _matchRunning;
		CancellationTokenSource? matchCts = _matchCts;
		if (matchCts != null)
		{
			matchCts.Cancel();
		}
		_matchRunning = false;
		lock (_matchLock)
		{
			if (string.IsNullOrEmpty(match))
			{
				_curMatchRegex = null;
				_curMatchString = null;
			}
			else
			{
				try
				{
					_curMatchString = match;
					CancellationTokenSource? matchCts2 = _matchCts;
					if (matchCts2 != null)
					{
						matchCts2.Cancel();
					}
					_matchRunning = false;
					match = string.Join("(?:.{0,50})", Enumerable.Select<string, string>((global::System.Collections.Generic.IEnumerable<string>)match.Split(' ', (StringSplitOptions)0), (Func<string, string>)((string x) => Regex.Escape(x))));
					_curMatchRegex = new Regex(match, (RegexOptions)537, TimeSpan.FromSeconds(5.0));
				}
				catch
				{
					_curMatchRegex = null;
					_curMatchString = null;
				}
			}
			_curMatch = null;
		}
		NextMatch();
	}

	public void NextMatch()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		if (Suspended)
		{
			return;
		}
		if (_curMatchRegex == null || _lines.Count == 0)
		{
			TopIndex = 0;
		}
		else
		{
			if (_matchRunning)
			{
				return;
			}
			_matchRunning = true;
			_matchCts = new CancellationTokenSource();
			CancellationTokenSource cts = _matchCts;
			Match curMatch = _curMatch;
			Regex curMatchRegex = _curMatchRegex;
			global::System.Threading.Tasks.Task.Run((Action)delegate
			{
				bool flag = false;
				try
				{
					if (!cts.IsCancellationRequested)
					{
						if (curMatch == null)
						{
							curMatch = curMatchRegex.Match(_text);
							if (!((Group)curMatch).Success)
							{
								flag = true;
							}
						}
						else
						{
							curMatch = curMatch.NextMatch();
							if (!cts.IsCancellationRequested && !((Group)curMatch).Success)
							{
								curMatch = curMatchRegex.Match(_text);
							}
						}
					}
				}
				catch (RegexMatchTimeoutException)
				{
					curMatch = null;
					flag = true;
				}
				finally
				{
					if (!cts.IsCancellationRequested)
					{
						lock (_matchLock)
						{
							_curMatch = curMatch;
							_matchRunning = false;
							TopIndex = ((!flag) ? TextIndexToLineIndex(((Capture)curMatch).Index) : 0);
						}
					}
				}
			}, cts.Token);
		}
	}

	public void ScrollText(bool up)
	{
		if (!Suspended)
		{
			if (up)
			{
				_sublineOffset--;
			}
			else
			{
				_sublineOffset++;
			}
			((Control)this).Invalidate();
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		PaintEventArgs e2 = e;
		if (Suspended)
		{
			return;
		}
		float curY = 0f;
		int num = TopIndex;
		SolidBrush val = new SolidBrush(((Control)this).ForeColor);
		bool flag = false;
		bool flag2 = false;
		e2.Graphics.TextRenderingHint = (TextRenderingHint)(AntiAliasing ? 4 : 2);
		if (ShowMatch && !string.IsNullOrWhiteSpace(_curMatchString))
		{
			try
			{
				SizeF val2 = e2.Graphics.MeasureString(_curMatchString, ((Control)this).Font);
				e2.Graphics.DrawString(_curMatchString, ((Control)this).Font, (Brush)(object)val, PointF.Empty);
				curY += val2.Height + 10f;
			}
			catch
			{
			}
		}
		while (true)
		{
			float num2 = curY;
			Rectangle clipRectangle = e2.ClipRectangle;
			if (!(num2 < clipRectangle.Height) || num >= _lines.Count)
			{
				break;
			}
			string text = GetLine(num);
			if (text == "")
			{
				text = "\n";
			}
			try
			{
				SizeF val3 = getSize(text);
				SizeF val4 = getSize(text, singleLine: true);
				float height = val4.Height;
				int num3 = Math.Max(1, (int)(val3.Height / height));
				if (flag)
				{
					goto IL_01eb;
				}
				if (flag2)
				{
					flag2 = false;
					_sublineOffset += num3;
				}
				if (_sublineOffset < 0)
				{
					if (num == 0)
					{
						_sublineOffset = 0;
						goto IL_01d1;
					}
					flag2 = true;
					_topIndex--;
					num--;
					continue;
				}
				if (_sublineOffset <= 0 || _sublineOffset < num3)
				{
					goto IL_01d1;
				}
				if (num == _lines.Count - 1)
				{
					_sublineOffset = num3 - 1;
					goto IL_01d1;
				}
				_sublineOffset -= num3;
				_topIndex++;
				num++;
				goto end_IL_00fe;
				IL_01eb:
				e2.Graphics.DrawString(text, ((Control)this).Font, (Brush)(object)val, maxRegion(), StringFormat.GenericTypographic);
				curY += val3.Height + (float)((num3 > 1) ? 5 : 0);
				goto IL_0238;
				IL_01d1:
				flag = true;
				curY -= (float)_sublineOffset * height;
				goto IL_01eb;
				end_IL_00fe:;
			}
			catch
			{
				flag = true;
				goto IL_0238;
			}
			continue;
			IL_0238:
			num++;
		}
		[CompilerGenerated]
		SizeF getSize(string s, bool singleLine = false)
		{
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (!singleLine)
			{
				Graphics graphics = e2.Graphics;
				Font font = ((Control)this).Font;
				RectangleF val5 = maxRegion();
				return graphics.MeasureString(s, font, val5.Size, StringFormat.GenericTypographic);
			}
			return e2.Graphics.MeasureString(s, ((Control)this).Font, 2147483647, StringFormat.GenericTypographic);
		}
		[CompilerGenerated]
		RectangleF maxRegion()
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			float num4 = curY;
			Rectangle clipRectangle2 = e2.ClipRectangle;
			return new RectangleF(0f, num4, clipRectangle2.Width, 2.1474836E+09f);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		components = (IContainer)new Container();
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
	}
}
