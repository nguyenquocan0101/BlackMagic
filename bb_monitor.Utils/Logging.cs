using System;
using System.Diagnostics;
using System.IO;

namespace bb_monitor.Utils;

public static class Logging
{
	private const string FOLDER_NAME = "test";

	private const string LOG_FILE = "text.txt";

	static Logging()
	{
		if (Directory.Exists("test"))
		{
			Directory.Delete("test", true);
		}
		if (File.Exists("text.txt"))
		{
			File.Delete("text.txt");
		}
	}

	public static string EnsureValidPath(string path)
	{
		string directoryName = Path.GetDirectoryName(path);
		Trace.Assert(directoryName != null);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
		return path;
	}

	public static void WriteLog(string text)
	{
		File.AppendAllText("text.txt", text + Environment.NewLine);
	}
}
