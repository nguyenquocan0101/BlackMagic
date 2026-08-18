using System.Text.RegularExpressions;

namespace bb_monitor.Utils;

public static class StringUtils
{
	public static string ReplaceDiacritics(string input)
	{
		input = Regex.Replace(input, "[áàảãạăắằẳẵặâấầẩẫậ]", "a");
		input = Regex.Replace(input, "[éèẻẽẹêếềểễệ]", "e");
		input = Regex.Replace(input, "[óòỏõọôốồổỗộơớờởỡợ]", "o");
		input = Regex.Replace(input, "[úùủũụưứừửữự]", "u");
		input = Regex.Replace(input, "[íìỉĩị]", "i");
		input = Regex.Replace(input, "[ýỳỷỹỵ]", "y");
		input = Regex.Replace(input, "[đ]", "d");
		input = Regex.Replace(input, "[ÁÀẢÃẠẮẰẲẴẶÂẤẦẨẪẬ]", "A");
		input = Regex.Replace(input, "[ÉÈẺẼẸÊẾỀỂỄỆ]", "E");
		input = Regex.Replace(input, "[ÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ]", "O");
		input = Regex.Replace(input, "[ÚÙỦŨỤƯỨỪỬỮỰ]", "U");
		input = Regex.Replace(input, "[ÍÌỈĨỊ]", "I");
		input = Regex.Replace(input, "[ÝỲỶỸỴ]", "Y");
		input = Regex.Replace(input, "[Đ]", "D");
		return input;
	}
}
