using bb_monitor.Utils;

namespace bb_monitor.Tests;

public class StringUtilsTests
{
    [Theory]
    // Lowercase Vietnamese vowels collapse to their base ASCII letter.
    [InlineData("áàảãạăắằẳẵặâấầẩẫậ", "aaaaaaaaaaaaaaaaa")]
    [InlineData("éèẻẽẹêếềểễệ", "eeeeeeeeeee")]
    [InlineData("óòỏõọôốồổỗộơớờởỡợ", "ooooooooooooooooo")]
    [InlineData("úùủũụưứừửữự", "uuuuuuuuuuu")]
    [InlineData("íìỉĩị", "iiiii")]
    [InlineData("ýỳỷỹỵ", "yyyyy")]
    [InlineData("đ", "d")]
    public void ReplaceDiacritics_LowercaseVowels_MapToBaseLetter(string input, string expected)
    {
        Assert.Equal(expected, StringUtils.ReplaceDiacritics(input));
    }

    [Theory]
    // Uppercase Vietnamese vowels collapse to their base ASCII letter.
    [InlineData("ÁÀẢÃẠẮẰẲẴẶÂẤẦẨẪẬ", "AAAAAAAAAAAAAAAA")]
    [InlineData("ÉÈẺẼẸÊẾỀỂỄỆ", "EEEEEEEEEEE")]
    [InlineData("ÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ", "OOOOOOOOOOOOOOOOO")]
    [InlineData("ÚÙỦŨỤƯỨỪỬỮỰ", "UUUUUUUUUUU")]
    [InlineData("ÍÌỈĨỊ", "IIIII")]
    [InlineData("ÝỲỶỸỴ", "YYYYY")]
    [InlineData("Đ", "D")]
    public void ReplaceDiacritics_UppercaseVowels_MapToBaseLetter(string input, string expected)
    {
        Assert.Equal(expected, StringUtils.ReplaceDiacritics(input));
    }

    [Fact]
    public void ReplaceDiacritics_MixedSentence_ReplacesOnlyDiacritics()
    {
        Assert.Equal("Xin chao, Viet Nam!", StringUtils.ReplaceDiacritics("Xin chào, Việt Nam!"));
    }

    [Fact]
    public void ReplaceDiacritics_PreservesCase()
    {
        Assert.Equal("DdAaEe", StringUtils.ReplaceDiacritics("Đđ\u00c2\u00e2\u00ca\u00ea"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("Hello World 123 !@#")]
    [InlineData("   spaces   kept   ")]
    public void ReplaceDiacritics_NoDiacritics_ReturnsInputUnchanged(string input)
    {
        Assert.Equal(input, StringUtils.ReplaceDiacritics(input));
    }

    [Fact]
    public void ReplaceDiacritics_LeavesNonVietnameseAccentsUntouched()
    {
        // Characters like ñ, ç, ü are not part of the replacement tables.
        const string input = "ñ ç Über";
        Assert.Equal(input, StringUtils.ReplaceDiacritics(input));
    }
}
