using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace TiaAgent.ResponseCenter.Diagnostics;

/// <summary>
/// File-based logger for the Response Center.
/// Writes to %LOCALAPPDATA%\TiaAgent\logs\response-center-YYYYMMDD.log.
/// Follows the same best-effort pattern as AddInLogger: never crashes the application.
/// </summary>
public static class ResponseCenterLogger
{
    private static string? _logDir;
    private static bool _logDirResolved;
    private static bool _fileLoggingDisabled;
    private static readonly object Lock = new();

    private static string? GetLogDir()
    {
        if (_logDirResolved)
            return _logDir;

        lock (Lock)
        {
            if (_logDirResolved)
                return _logDir;

            _logDirResolved = true;
            try
            {
                var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (!string.IsNullOrEmpty(localAppData))
                    _logDir = Path.Combine(localAppData, "TiaAgent", "logs");
            }
            catch
            {
                // Permission denied — file logging will be disabled
            }
            return _logDir;
        }
    }

    public static void Info(string message) => Log("INFO", message);
    public static void Warn(string message) => Log("WARN", message);
    public static void Debug(string message) => Log("DEBUG", message);

    public static void Error(string message, Exception? ex = null)
    {
        var fullMessage = ex != null ? $"{message}\n{ex}" : message;
        Log("ERROR", fullMessage);
    }

    public static void Startup()
    {
        try
        {
            Info("=== Response Center Startup ===");
            Info($"PID: {Environment.ProcessId}");
            Info($"Thread ID: {Environment.CurrentManagedThreadId}");
            Info($"Apartment state: {Thread.CurrentThread.GetApartmentState()}");
            Info($"64-bit process: {Environment.Is64BitProcess}");
            Info($"OS: {Environment.OSVersion}");
            Info($"CLR: {Environment.Version}");
            Info("=== Startup diagnostics complete ===");
        }
        catch
        {
            // Best-effort
        }
    }

    public static void LogWindowState(string label, long hwnd, bool isVisible, string windowState,
        double left, double top, double screenWidth, double screenHeight, string? taskId = null, string? tiaInstanceId = null)
    {
        var detail = $"hwnd={hwnd}, isVisible={isVisible}, state={windowState}, " +
                     $"pos=({left:F0},{top:F0}), screen=({screenWidth:F0}x{screenHeight:F0})";
        if (taskId != null) detail += $", taskId={taskId}";
        if (tiaInstanceId != null) detail += $", tiaInstance={tiaInstanceId}";
        Info($"{label}: {detail}");
    }

    private static void Log(string level, string message)
    {
        if (_fileLoggingDisabled)
            return;

        try
        {
            var dir = GetLogDir();
            if (dir == null)
            {
                _fileLoggingDisabled = true;
                return;
            }

            lock (Lock)
            {
                try
                {
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    var logFile = Path.Combine(dir, $"response-center-{DateTime.Now:yyyyMMdd}.log");
                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    var threadId = Environment.CurrentManagedThreadId;
                    var entry = $"[{timestamp}] [{level}] [T{threadId}] {message}";
                    File.AppendAllText(logFile, entry + Environment.NewLine);
                }
                catch
                {
                    _fileLoggingDisabled = true;
                }
            }
        }
        catch
        {
            // Logging must never crash the application
        }
    }
}
