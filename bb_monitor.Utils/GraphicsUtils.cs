using System.Drawing;

namespace bb_monitor.Utils;

public static class GraphicsUtils
{
	public static Size Rescale(this Size size, double kW, double? kH = null)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		return new Size((int)(size.Width * kW), (int)(size.Height * kH.GetValueOrDefault(kW)));
	}

	public static Size ScaleToWidth(this Size size, double targetWidth)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (size.Width == targetWidth)
		{
			return size;
		}
		return size.Rescale(targetWidth / size.Width);
	}
}
