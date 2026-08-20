using System.Drawing;
using bb_monitor.Utils;

namespace bb_monitor.Tests;

public class GraphicsUtilsTests
{
    [Fact]
    public void Rescale_SingleFactor_ScalesBothDimensions()
    {
        var result = new Size(100, 50).Rescale(2.0);
        Assert.Equal(new Size(200, 100), result);
    }

    [Fact]
    public void Rescale_SeparateWidthAndHeightFactors()
    {
        var result = new Size(100, 50).Rescale(2.0, 3.0);
        Assert.Equal(new Size(200, 150), result);
    }

    [Fact]
    public void Rescale_NullHeightFactor_UsesWidthFactorForBoth()
    {
        var result = new Size(10, 20).Rescale(1.5, null);
        Assert.Equal(new Size(15, 30), result);
    }

    [Fact]
    public void Rescale_TruncatesTowardZero()
    {
        // 10 * 0.25 = 2.5 -> 2, 10 * 0.19 = 1.9 -> 1
        var result = new Size(10, 10).Rescale(0.25, 0.19);
        Assert.Equal(new Size(2, 1), result);
    }

    [Fact]
    public void Rescale_ZeroFactor_ProducesEmptySize()
    {
        var result = new Size(100, 100).Rescale(0.0);
        Assert.Equal(new Size(0, 0), result);
    }

    [Fact]
    public void ScaleToWidth_DownscalesProportionally()
    {
        var result = new Size(200, 100).ScaleToWidth(100);
        Assert.Equal(new Size(100, 50), result);
    }

    [Fact]
    public void ScaleToWidth_UpscalesProportionally()
    {
        var result = new Size(50, 20).ScaleToWidth(100);
        Assert.Equal(new Size(100, 40), result);
    }

    [Fact]
    public void ScaleToWidth_WidthAlreadyMatches_ReturnsSameSize()
    {
        var original = new Size(120, 80);
        var result = original.ScaleToWidth(120);
        Assert.Equal(original, result);
    }
}
