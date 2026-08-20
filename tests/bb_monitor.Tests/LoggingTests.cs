using bb_monitor.Utils;

namespace bb_monitor.Tests;

// All Logging tests live in a single class: xunit runs test methods within one
// class sequentially, which keeps the process-wide "text.txt" log file (written
// by WriteLog) stable across these cases.
public class LoggingTests
{
    [Fact]
    public void EnsureValidPath_CreatesMissingDirectory_AndReturnsPath()
    {
        string dir = Path.Combine(Path.GetTempPath(), "bb_monitor_tests", Guid.NewGuid().ToString("N"));
        string file = Path.Combine(dir, "sub", "log.txt");
        try
        {
            Assert.False(Directory.Exists(Path.GetDirectoryName(file)));

            string returned = Logging.EnsureValidPath(file);

            Assert.Equal(file, returned);
            Assert.True(Directory.Exists(Path.GetDirectoryName(file)));
        }
        finally
        {
            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
            }
        }
    }

    [Fact]
    public void EnsureValidPath_ExistingDirectory_ReturnsPathWithoutError()
    {
        string dir = Path.Combine(Path.GetTempPath(), "bb_monitor_tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(dir);
        string file = Path.Combine(dir, "existing.txt");
        try
        {
            string returned = Logging.EnsureValidPath(file);

            Assert.Equal(file, returned);
            Assert.True(Directory.Exists(dir));
        }
        finally
        {
            Directory.Delete(dir, true);
        }
    }

    [Fact]
    public void WriteLog_AppendsLinesWithNewline()
    {
        // WriteLog targets a fixed relative file; isolate it to a temp working directory.
        string tempCwd = Path.Combine(Path.GetTempPath(), "bb_monitor_tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempCwd);
        string originalCwd = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(tempCwd);
        try
        {
            Logging.WriteLog("first");
            Logging.WriteLog("second");

            string[] lines = File.ReadAllLines("text.txt");
            Assert.Equal(new[] { "first", "second" }, lines);
        }
        finally
        {
            Directory.SetCurrentDirectory(originalCwd);
            Directory.Delete(tempCwd, true);
        }
    }
}
