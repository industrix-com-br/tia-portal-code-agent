using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TiaAgent.Bridge.Diagnostics;

namespace TiaAgent.Bridge.Runtime;

/// <summary>
/// Shared process execution infrastructure for CLI-based runtime adapters.
/// Handles ProcessStartInfo setup, stdout/stderr capture, cancellation,
/// timeout, ANSI stripping, exit code handling, and orphaned process prevention.
/// </summary>
public sealed class ProcessRunner : IDisposable
{
    private readonly BridgeLogger _logger;
    private static readonly Regex AnsiEscapePattern = new Regex(
        @"\x1B\[[0-9;]*[A-Za-z]|\x1B\][^\x07]*\x07|\x1B\([AB01]",
        RegexOptions.Compiled);

    /// <summary>
    /// Strict UTF-8 decoder: no BOM, throws on invalid byte sequences.
    /// Used to detect encoding corruption at the earliest possible boundary.
    /// </summary>
    private static readonly Encoding s_strictUtf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

    private static readonly string[] s_newlineSeparators = new[] { "\r\n", "\n" };

    public ProcessRunner(BridgeLogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Executes a process and captures its output.
    /// Reads raw bytes from StandardOutput.BaseStream and StandardError.BaseStream
    /// BEFORE any string decoding, then decodes explicitly with strict UTF-8.
    /// </summary>
    /// <param name="executable">Path or name of the executable.</param>
    /// <param name="arguments">Command-line arguments.</param>
    /// <param name="workingDirectory">Working directory for the process.</param>
    /// <param name="timeout">Maximum execution time.</param>
    /// <param name="environmentVariables">Optional environment variables to set.</param>
    /// <param name="progress">Optional progress reporter for stdout lines.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The process result with exit code, stdout, and stderr.</returns>
    public async Task<ProcessResult> RunAsync(
        string executable,
        string arguments,
        string? workingDirectory,
        TimeSpan timeout,
        System.Collections.Generic.Dictionary<string, string>? environmentVariables = null,
        IProgress<string>? progress = null,
        CancellationToken cancellationToken = default)
    {
        var stdout = new StringBuilder();
        var stderr = new StringBuilder();
        Process? process = null;

        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = executable,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
                CreateNoWindow = true,
            };

            if (!string.IsNullOrEmpty(workingDirectory))
                startInfo.WorkingDirectory = workingDirectory;

            if (environmentVariables != null)
            {
                foreach (var kvp in environmentVariables)
                    startInfo.Environment[kvp.Key] = kvp.Value;
            }

            _logger.Info($"ProcessRunner: starting '{executable}' with args: {Truncate(arguments, 500)}");
            _logger.Info($"ProcessRunner: stdout encoding = {startInfo.StandardOutputEncoding.WebName} ({startInfo.StandardOutputEncoding.CodePage}), stderr encoding = {startInfo.StandardErrorEncoding.WebName} ({startInfo.StandardErrorEncoding.CodePage})");

            process = new Process { StartInfo = startInfo };
            process.Start();

            // Close stdin immediately to signal non-interactive mode
            try { process.StandardInput.Close(); } catch { }

            _logger.Info($"ProcessRunner: process started (PID={process.Id})");

            // Use a linked CTS for timeout + external cancellation
            using var timeoutCts = new CancellationTokenSource(timeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken, timeoutCts.Token);

            // ═══════════════════════════════════════════════════════════════════
            // BOUNDARY 1: Raw bytes from process stdout/stderr BEFORE decoding
            // Read directly from BaseStream to capture exact bytes the process emitted.
            // This is the FIRST point where we can observe the raw byte encoding.
            // ═══════════════════════════════════════════════════════════════════
            byte[] stdoutBytes, stderrBytes;
            try
            {
                stdoutBytes = await ReadAllBytesAsync(process.StandardOutput.BaseStream, linkedCts.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested && !cancellationToken.IsCancellationRequested)
            {
                _logger.Warn($"ProcessRunner: stdout read timed out after {timeout.TotalSeconds}s");
                KillProcessTree(process);
                return new ProcessResult
                {
                    ExitCode = -1,
                    StdOut = stdout.ToString(),
                    StdErr = stderr.ToString(),
                    TimedOut = true,
                    Error = $"Process timed out after {timeout.TotalSeconds} seconds"
                };
            }
            catch (OperationCanceledException)
            {
                _logger.Info("ProcessRunner: process cancelled during stdout read");
                KillProcessTree(process);
                return new ProcessResult
                {
                    ExitCode = -1,
                    StdOut = stdout.ToString(),
                    StdErr = stderr.ToString(),
                    Cancelled = true,
                    Error = "Process was cancelled"
                };
            }

            try
            {
                stderrBytes = await ReadAllBytesAsync(process.StandardError.BaseStream, linkedCts.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                stderrBytes = Array.Empty<byte>();
            }

            await process.WaitForExitAsync(linkedCts.Token).ConfigureAwait(false);

            // ═══════════════════════════════════════════════════════════════════
            // BOUNDARY 1 DIAGNOSTICS: Log raw bytes BEFORE any decoding
            // ═══════════════════════════════════════════════════════════════════
            _logger.Info($"ProcessRunner [BOUNDARY 1 - raw process output]: stdout={stdoutBytes.Length} bytes, stderr={stderrBytes.Length} bytes");
            if (stdoutBytes.Length > 0)
            {
                _logger.Info($"ProcessRunner [BOUNDARY 1 - raw hex sample]: {FormatHexSample(stdoutBytes, 128)}");
            }

            // ═══════════════════════════════════════════════════════════════════
            // DECODE: Strict UTF-8 with error detection
            // If the raw bytes are NOT valid UTF-8, this will throw or produce
            // replacement characters — proving the corruption happened BEFORE here.
            // ═══════════════════════════════════════════════════════════════════
            string decodedStdout, decodedStderr;
            try
            {
                decodedStdout = s_strictUtf8.GetString(stdoutBytes);
            }
            catch (DecoderFallbackException ex)
            {
                _logger.Error($"ProcessRunner [BOUNDARY 1 - INVALID UTF-8 in stdout]: {ex.Message}. Hex: {FormatHexSample(stdoutBytes, 256)}");
                // Fall back to replacement-char decoding to continue processing
                decodedStdout = Encoding.UTF8.GetString(stdoutBytes);
            }

            try
            {
                decodedStderr = s_strictUtf8.GetString(stderrBytes);
            }
            catch (DecoderFallbackException ex)
            {
                _logger.Error($"ProcessRunner [BOUNDARY 1 - INVALID UTF-8 in stderr]: {ex.Message}. Hex: {FormatHexSample(stderrBytes, 256)}");
                decodedStderr = Encoding.UTF8.GetString(stderrBytes);
            }

            // ═══════════════════════════════════════════════════════════════════
            // BOUNDARY 2: Decoded string — log code points for comparison
            // ═══════════════════════════════════════════════════════════════════
            _logger.Info($"ProcessRunner [BOUNDARY 2 - decoded string]: stdout={decodedStdout.Length} chars, stderr={decodedStderr.Length} chars");
            if (decodedStdout.Length > 0)
            {
                _logger.Info($"ProcessRunner [BOUNDARY 2 - code points sample]: {FormatCodePoints(decodedStdout, 128)}");
            }

            // Split decoded text into lines for progress reporting and StringBuilder storage
            var stdoutLines = decodedStdout.Split(s_newlineSeparators, StringSplitOptions.None);
            var stderrLines = decodedStderr.Split(s_newlineSeparators, StringSplitOptions.None);

            foreach (var line in stdoutLines)
            {
                lock (stdout) { stdout.AppendLine(line); }
                progress?.Report(line);
            }
            foreach (var line in stderrLines)
            {
                lock (stderr) { stderr.AppendLine(line); }
            }

            var exitCode = process.ExitCode;
            _logger.Info($"ProcessRunner: process exited with code {exitCode}");

            return new ProcessResult
            {
                ExitCode = exitCode,
                StdOut = stdout.ToString(),
                StdErr = stderr.ToString(),
                RawStdoutBytes = stdoutBytes,
                RawStderrBytes = stderrBytes
            };
        }
        catch (Exception ex)
        {
            _logger.Error($"ProcessRunner: failed to start process '{executable}'", ex);
            return new ProcessResult
            {
                ExitCode = -1,
                StdOut = stdout.ToString(),
                StdErr = stderr.ToString(),
                Error = $"Failed to start process: {ex.Message}"
            };
        }
        finally
        {
            try
            {
                if (process != null && !process.HasExited)
                {
                    KillProcessTree(process);
                }
            }
            catch (InvalidOperationException)
            {
                // Process was never started (e.g. executable not found)
            }
            process?.Dispose();
        }
    }

    /// <summary>
    /// Reads all bytes from a stream until EOF. Used to capture raw process output
    /// before any string decoding — the earliest possible observation point.
    /// </summary>
    private static async Task<byte[]> ReadAllBytesAsync(Stream stream, CancellationToken cancellationToken)
    {
        using var ms = new MemoryStream();
        var buffer = new byte[8192];
        Memory<byte> memory = buffer;
        int bytesRead;
        while ((bytesRead = await stream.ReadAsync(memory, cancellationToken).ConfigureAwait(false)) > 0)
        {
            ms.Write(buffer, 0, bytesRead);
        }
        return ms.ToArray();
    }

    /// <summary>
    /// Formats a byte array as a hex string for diagnostic logging.
    /// Shows first N bytes in hex with ASCII representation.
    /// </summary>
    private static string FormatHexSample(byte[] bytes, int maxBytes)
    {
        if (bytes.Length == 0) return "(empty)";
        var len = Math.Min(bytes.Length, maxBytes);
        var sb = new StringBuilder(len * 3 + 20);
        sb.Append($"{bytes.Length} total bytes, first {len}: ");
        for (int i = 0; i < len; i++)
        {
            sb.Append($"{bytes[i]:X2} ");
            if ((i + 1) % 32 == 0 && i + 1 < len) sb.Append("\n    ");
        }
        if (bytes.Length > maxBytes) sb.Append($"... ({bytes.Length - maxBytes} more)");
        return sb.ToString();
    }

    /// <summary>
    /// Formats a string's Unicode code points for diagnostic logging.
    /// Shows each character as U+XXXX to reveal encoding corruption.
    /// </summary>
    private static string FormatCodePoints(string text, int maxChars)
    {
        if (string.IsNullOrEmpty(text)) return "(empty)";
        var len = Math.Min(text.Length, maxChars);
        var sb = new StringBuilder(len * 7);
        for (int i = 0; i < len; i++)
        {
            var c = text[i];
            if (c >= 0x20 && c < 0x7F)
                sb.Append(c); // Printable ASCII — show as-is
            else
                sb.Append($"U+{(int)c:X4} "); // Show code point
        }
        if (text.Length > maxChars) sb.Append($"... ({text.Length - maxChars} more)");
        return sb.ToString();
    }

    /// <summary>
    /// Kills the process and its children to prevent orphaned processes.
    /// </summary>
    private static void KillProcessTree(Process process)
    {
        try
        {
            if (!process.HasExited)
            {
                process.Kill(entireProcessTree: true);
            }
        }
        catch
        {
            // Best-effort kill
            try { if (!process.HasExited) process.Kill(); } catch { }
        }
    }

    /// <summary>
    /// Strips ANSI escape sequences from text.
    /// Should be called at the presentation boundary, not during capture.
    /// </summary>
    public static string StripAnsiEscapes(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        return AnsiEscapePattern.Replace(text, string.Empty);
    }

    private static string Truncate(string s, int maxLength)
    {
        if (s == null) return "";
        return s.Length <= maxLength ? s : string.Concat(s.AsSpan(0, maxLength), "...");
    }

    public void Dispose()
    {
        // No persistent state to dispose
    }
}

/// <summary>
/// Result of a process execution.
/// </summary>
public sealed class ProcessResult
{
    public int ExitCode { get; init; }
    public string StdOut { get; init; } = "";
    public string StdErr { get; init; } = "";
    public bool TimedOut { get; init; }
    public bool Cancelled { get; init; }
    public string? Error { get; init; }
    public bool Success => ExitCode == 0 && Error == null;

    /// <summary>
    /// Raw bytes captured from process stdout BEFORE string decoding.
    /// Used for encoding diagnostics — may be null if raw capture was not performed.
    /// </summary>
    public byte[]? RawStdoutBytes { get; init; }

    /// <summary>
    /// Raw bytes captured from process stderr BEFORE string decoding.
    /// Used for encoding diagnostics — may be null if raw capture was not performed.
    /// </summary>
    public byte[]? RawStderrBytes { get; init; }
}
