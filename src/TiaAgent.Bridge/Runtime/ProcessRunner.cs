using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TiaAgent.Bridge.Diagnostics;
using TiaAgent.Contracts.Diagnostics;

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
    /// When stdinContent is provided, writes it as UTF-8 bytes to stdin before closing,
    /// enabling safe transport of dynamic prompts without shell interpretation.
    /// Reads raw bytes from StandardOutput.BaseStream and StandardError.BaseStream
    /// BEFORE any string decoding, then decodes explicitly with strict UTF-8.
    /// </summary>
    /// <param name="executable">Path or name of the executable.</param>
    /// <param name="arguments">Command-line arguments.</param>
    /// <param name="workingDirectory">Working directory for the process.</param>
    /// <param name="timeout">Maximum execution time.</param>
    /// <param name="environmentVariables">Optional environment variables to set.</param>
    /// <param name="progress">Optional progress reporter for stdout lines.</param>
    /// <param name="stdinContent">Optional content to write to stdin as UTF-8 before closing.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The process result with exit code, stdout, and stderr.</returns>
    public async Task<ProcessResult> RunAsync(
        string executable,
        string arguments,
        string? workingDirectory,
        TimeSpan timeout,
        System.Collections.Generic.Dictionary<string, string>? environmentVariables = null,
        IProgress<string>? progress = null,
        string? stdinContent = null,
        CancellationToken cancellationToken = default)
    {
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
            _logger.Info($"ProcessRunner: process started (PID={process.Id})");

            using var timeoutCts = new CancellationTokenSource(timeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken, timeoutCts.Token);

            // Start draining both output streams immediately. A child process may
            // write diagnostics and exit while stdin is still being written.
            var stdoutTask = ReadAllBytesAsync(process.StandardOutput.BaseStream, linkedCts.Token);
            var stderrTask = ReadAllBytesAsync(process.StandardError.BaseStream, linkedCts.Token);

            string? stdinWriteError = null;
            try
            {
                if (stdinContent != null)
                {
                    var stdinBytes = Encoding.UTF8.GetBytes(stdinContent);
                    _logger.Info($"ProcessRunner: writing {stdinBytes.Length} UTF-8 bytes to stdin");

                    await process.StandardInput.BaseStream.WriteAsync(stdinBytes.AsMemory(), linkedCts.Token)
                        .ConfigureAwait(false);
                    await process.StandardInput.BaseStream.FlushAsync(linkedCts.Token).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested && !cancellationToken.IsCancellationRequested)
            {
                _logger.Warn($"ProcessRunner: stdin write timed out after {timeout.TotalSeconds}s");
                KillProcessTree(process);
                return new ProcessResult
                {
                    ExitCode = -1,
                    StdOut = string.Empty,
                    StdErr = string.Empty,
                    TimedOut = true,
                    Error = $"Process timed out after {timeout.TotalSeconds} seconds while writing stdin"
                };
            }
            catch (OperationCanceledException)
            {
                _logger.Info("ProcessRunner: process cancelled during stdin write");
                KillProcessTree(process);
                return new ProcessResult
                {
                    ExitCode = -1,
                    StdOut = string.Empty,
                    StdErr = string.Empty,
                    Cancelled = true,
                    Error = "Process was cancelled"
                };
            }
            catch (Exception ex) when (ex is IOException or ObjectDisposedException or InvalidOperationException)
            {
                // The child may have exited because of invalid arguments or another
                // startup error. Preserve its stdout/stderr instead of treating this
                // as a process-start failure and discarding the real diagnostics.
                stdinWriteError = $"Process stdin closed before all input was written: {ex.Message}";
                _logger.Warn($"ProcessRunner: stdin write failed after process start: {ex.Message}");
            }
            finally
            {
                try { process.StandardInput.Close(); } catch { }
            }

            byte[] stdoutBytes;
            byte[] stderrBytes;
            try
            {
                stdoutBytes = await stdoutTask.ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested && !cancellationToken.IsCancellationRequested)
            {
                _logger.Warn($"ProcessRunner: stdout read timed out after {timeout.TotalSeconds}s");
                KillProcessTree(process);
                return new ProcessResult
                {
                    ExitCode = -1,
                    StdOut = string.Empty,
                    StdErr = string.Empty,
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
                    StdOut = string.Empty,
                    StdErr = string.Empty,
                    Cancelled = true,
                    Error = "Process was cancelled"
                };
            }

            try
            {
                stderrBytes = await stderrTask.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                stderrBytes = Array.Empty<byte>();
            }

            await process.WaitForExitAsync(linkedCts.Token).ConfigureAwait(false);

            _logger.Info(TextPayloadDiagnostics.DescribeUtf8Bytes("1.process.stdout.raw", stdoutBytes));
            _logger.Info(TextPayloadDiagnostics.DescribeUtf8Bytes("1.process.stderr.raw", stderrBytes));

            string decodedStdout;
            try
            {
                decodedStdout = s_strictUtf8.GetString(stdoutBytes);
            }
            catch (DecoderFallbackException ex)
            {
                _logger.Error($"ProcessRunner: stdout is not valid UTF-8: {ex.Message}");
                return new ProcessResult
                {
                    ExitCode = process.ExitCode,
                    StdOut = string.Empty,
                    StdErr = string.Empty,
                    RawStdoutBytes = stdoutBytes,
                    RawStderrBytes = stderrBytes,
                    Error = "Process stdout contained invalid UTF-8 bytes"
                };
            }

            string decodedStderr;
            try
            {
                decodedStderr = s_strictUtf8.GetString(stderrBytes);
            }
            catch (DecoderFallbackException ex)
            {
                _logger.Error($"ProcessRunner: stderr is not valid UTF-8: {ex.Message}");
                return new ProcessResult
                {
                    ExitCode = process.ExitCode,
                    StdOut = decodedStdout,
                    StdErr = string.Empty,
                    RawStdoutBytes = stdoutBytes,
                    RawStderrBytes = stderrBytes,
                    Error = "Process stderr contained invalid UTF-8 bytes"
                };
            }

            _logger.Info(TextPayloadDiagnostics.DescribeText("2.process.stdout.decoded", decodedStdout));
            _logger.Info(TextPayloadDiagnostics.DescribeText("2.process.stderr.decoded", decodedStderr));

            // Progress reporting is observational only; decodedStdout remains the source of truth.
            if (progress != null)
            {
                var stdoutLines = decodedStdout.Split(s_newlineSeparators, StringSplitOptions.None);
                foreach (var line in stdoutLines)
                    progress.Report(line);
            }

            var exitCode = process.ExitCode;
            _logger.Info($"ProcessRunner: process exited with code {exitCode}");

            var executionError = stdinWriteError;
            if (executionError != null && !string.IsNullOrWhiteSpace(decodedStderr))
            {
                var stderrSummary = StripAnsiEscapes(decodedStderr.Trim());
                executionError += $" Child stderr: {Truncate(stderrSummary, 1000)}";
            }

            return new ProcessResult
            {
                ExitCode = exitCode,
                StdOut = decodedStdout,
                StdErr = decodedStderr,
                RawStdoutBytes = stdoutBytes,
                RawStderrBytes = stderrBytes,
                Error = executionError
            };
        }
        catch (Exception ex)
        {
            _logger.Error($"ProcessRunner: process execution failed for '{executable}'", ex);
            return new ProcessResult
            {
                ExitCode = -1,
                StdOut = string.Empty,
                StdErr = string.Empty,
                Error = $"Process execution failed: {ex.Message}"
            };
        }
        finally
        {
            try
            {
                if (process != null && !process.HasExited)
                    KillProcessTree(process);
            }
            catch (InvalidOperationException)
            {
                // Process was never started.
            }
            process?.Dispose();
        }
    }

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

    private static void KillProcessTree(Process process)
    {
        try
        {
            if (!process.HasExited)
                process.Kill(entireProcessTree: true);
        }
        catch
        {
            // Some Windows/.NET combinations cannot kill an entire tree.
            try
            {
                if (!process.HasExited)
                    process.Kill();
            }
            catch
            {
                // Best effort only.
            }
        }
    }

    public static string StripAnsiEscapes(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        return AnsiEscapePattern.Replace(text, string.Empty);
    }

    public static string ComputeSha256(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        var hash = SHA256.HashData(bytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    private static string Truncate(string s, int maxLength)
    {
        if (s == null) return string.Empty;
        return s.Length <= maxLength ? s : string.Concat(s.AsSpan(0, maxLength), "...");
    }

    public void Dispose()
    {
        // No persistent state to dispose.
    }
}

public sealed class ProcessResult
{
    public int ExitCode { get; init; }
    public string StdOut { get; init; } = string.Empty;
    public string StdErr { get; init; } = string.Empty;
    public bool TimedOut { get; init; }
    public bool Cancelled { get; init; }
    public string? Error { get; init; }
    public bool Success => ExitCode == 0 && Error == null;
    public byte[]? RawStdoutBytes { get; init; }
    public byte[]? RawStderrBytes { get; init; }
}
