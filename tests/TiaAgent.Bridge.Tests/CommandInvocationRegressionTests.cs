using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TiaAgent.Bridge.Diagnostics;
using TiaAgent.Bridge.Runtime;
using Xunit;

namespace TiaAgent.Bridge.Tests;

public class CommandInvocationRegressionTests
{
    private readonly BridgeLogger _logger = new();

    [Fact]
    public void CmdWrapper_QuotesTheEntireCommandLine()
    {
        var resolved = CommandResolver.Resolve("claude", name => name switch
        {
            "claude.cmd" => @"C:\Program Files\nodejs\claude.cmd",
            _ => null
        });

        var arguments = resolved.ComposeArguments(
            "-p \"Process stdin.\" --mcp-config \"C:\\Users\\test\\App Data\\claude-mcp.json\"");

        arguments.Should().Be(
            "/d /s /c \"\"C:\\Program Files\\nodejs\\claude.cmd\" -p \"Process stdin.\" --mcp-config \"C:\\Users\\test\\App Data\\claude-mcp.json\"\"");
    }

    [Fact]
    public void CmdWrapper_UsesConfiguredWrapperArguments()
    {
        var resolved = new ResolvedCommand
        {
            FileName = "cmd.exe",
            Arguments = new[] { "/d", "/q", "/c" },
            Wrapper = ProcessWrapper.Cmd,
            ResolvedTargetPath = @"C:\Program Files\nodejs\claude.cmd"
        };

        var arguments = resolved.ComposeArguments(string.Empty);

        arguments.Should().Be(
            "/d /q /c \"\"C:\\Program Files\\nodejs\\claude.cmd\"\"");
    }

    [Fact]
    public void CmdWrapper_QuotesTheTargetWhenThereAreNoExtraArguments()
    {
        var resolved = CommandResolver.Resolve(@"C:\Program Files\nodejs\claude.cmd");

        var arguments = resolved.ComposeArguments(string.Empty);

        arguments.Should().Be(
            "/d /s /c \"\"C:\\Program Files\\nodejs\\claude.cmd\"\"");
    }

    [Fact]
    public async Task CmdWrapper_ExecutesComplexArgumentsAndPipedStdin()
    {
        if (!OperatingSystem.IsWindows())
            return;

        var batchPath = Path.Combine(Path.GetTempPath(), $"tia-stdin-echo-{Guid.NewGuid():N}.cmd");
        try
        {
            await File.WriteAllTextAsync(
                batchPath,
                "@echo off\r\necho arg1=%~1\r\necho arg2=%~2\r\nmore\r\n");

            var resolved = CommandResolver.Resolve(batchPath);
            using var runner = new ProcessRunner(_logger);

            var result = await runner.RunAsync(
                resolved.FileName,
                resolved.ComposeArguments("\"value with spaces\" \"C:\\Users\\test\\App Data\\claude-mcp.json\""),
                workingDirectory: null,
                timeout: TimeSpan.FromSeconds(15),
                stdinContent: "payload-áção",
                cancellationToken: CancellationToken.None);

            result.Success.Should().BeTrue(result.Error);
            result.StdOut.Should().Contain("arg1=value with spaces");
            result.StdOut.Should().Contain(@"arg2=C:\Users\test\App Data\claude-mcp.json");
            result.StdOut.Should().Contain("payload-áção");
        }
        finally
        {
            try { File.Delete(batchPath); } catch { }
        }
    }

    [Fact]
    public async Task ProcessRunner_WhenChildClosesStdin_PreservesExitCodeAndStderr()
    {
        if (!OperatingSystem.IsWindows())
            return;

        var batchPath = Path.Combine(Path.GetTempPath(), $"tia-close-stdin-{Guid.NewGuid():N}.cmd");
        try
        {
            await File.WriteAllTextAsync(
                batchPath,
                "@echo off\r\necho child-startup-error 1>&2\r\nexit /b 23\r\n");

            var resolved = CommandResolver.Resolve(batchPath);
            using var runner = new ProcessRunner(_logger);

            var result = await runner.RunAsync(
                resolved.FileName,
                resolved.ComposeArguments(string.Empty),
                workingDirectory: null,
                timeout: TimeSpan.FromSeconds(15),
                stdinContent: new string('x', 1024 * 1024),
                cancellationToken: CancellationToken.None);

            result.ExitCode.Should().Be(23);
            result.StdErr.Should().Contain("child-startup-error");
            result.Error.Should().Contain("stdin closed");
        }
        finally
        {
            try { File.Delete(batchPath); } catch { }
        }
    }

    [Fact]
    public async Task ProcessRunner_WhenOutputStreamsCloseButProcessKeepsRunning_ReturnsTimedOut()
    {
        if (!OperatingSystem.IsWindows())
            return;

        var markerPath = Path.Combine(Path.GetTempPath(), $"tia-wait-timeout-{Guid.NewGuid():N}.ready");
        var scriptPath = await CreateCloseOutputHandlesScriptAsync(markerPath);
        try
        {
            using var runner = new ProcessRunner(_logger);

            var result = await runner.RunAsync(
                "powershell.exe",
                $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\"",
                workingDirectory: null,
                timeout: TimeSpan.FromSeconds(5),
                cancellationToken: CancellationToken.None);

            File.Exists(markerPath).Should().BeTrue("the child must close its output handles before the timeout");
            result.TimedOut.Should().BeTrue();
            result.Cancelled.Should().BeFalse();
            result.Error.Should().Contain("waiting for exit");
        }
        finally
        {
            try { File.Delete(scriptPath); } catch { }
            try { File.Delete(markerPath); } catch { }
        }
    }

    [Fact]
    public async Task ProcessRunner_WhenCancelledAfterOutputStreamsClose_ReturnsCancelled()
    {
        if (!OperatingSystem.IsWindows())
            return;

        var markerPath = Path.Combine(Path.GetTempPath(), $"tia-wait-cancel-{Guid.NewGuid():N}.ready");
        var scriptPath = await CreateCloseOutputHandlesScriptAsync(markerPath);
        try
        {
            using var runner = new ProcessRunner(_logger);
            using var cts = new CancellationTokenSource();

            var runTask = runner.RunAsync(
                "powershell.exe",
                $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\"",
                workingDirectory: null,
                timeout: TimeSpan.FromSeconds(30),
                cancellationToken: cts.Token);

            await WaitForFileAsync(markerPath, TimeSpan.FromSeconds(10));
            cts.Cancel();

            var result = await runTask;

            result.Cancelled.Should().BeTrue();
            result.TimedOut.Should().BeFalse();
            result.Error.Should().Be("Process was cancelled");
        }
        finally
        {
            try { File.Delete(scriptPath); } catch { }
            try { File.Delete(markerPath); } catch { }
        }
    }

    private static async Task<string> CreateCloseOutputHandlesScriptAsync(string markerPath)
    {
        var scriptPath = Path.Combine(Path.GetTempPath(), $"tia-close-output-{Guid.NewGuid():N}.ps1");
        var escapedMarkerPath = markerPath.Replace("'", "''", StringComparison.Ordinal);
        var script =
            "$source = @'\r\n" +
            "using System;\r\n" +
            "using System.Runtime.InteropServices;\r\n" +
            "public static class NativeMethods\r\n" +
            "{\r\n" +
            "    [DllImport(\"kernel32.dll\", SetLastError = true)]\r\n" +
            "    public static extern IntPtr GetStdHandle(int nStdHandle);\r\n" +
            "    [DllImport(\"kernel32.dll\", SetLastError = true)]\r\n" +
            "    public static extern bool CloseHandle(IntPtr hObject);\r\n" +
            "}\r\n" +
            "'@\r\n" +
            "Add-Type -TypeDefinition $source\r\n" +
            "[NativeMethods]::CloseHandle([NativeMethods]::GetStdHandle(-11)) | Out-Null\r\n" +
            "[NativeMethods]::CloseHandle([NativeMethods]::GetStdHandle(-12)) | Out-Null\r\n" +
            $"[System.IO.File]::WriteAllText('{escapedMarkerPath}', 'ready')\r\n" +
            "Start-Sleep -Seconds 30\r\n";

        await File.WriteAllTextAsync(scriptPath, script);
        return scriptPath;
    }

    private static async Task WaitForFileAsync(string path, TimeSpan timeout)
    {
        var deadline = DateTime.UtcNow + timeout;
        while (!File.Exists(path))
        {
            if (DateTime.UtcNow >= deadline)
                throw new TimeoutException($"Timed out waiting for marker file '{path}'.");

            await Task.Delay(50);
        }
    }
}
