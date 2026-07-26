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
    public void CmdWrapper_QuotesTheTargetWhenThereAreNoExtraArguments()
    {
        var resolved = CommandResolver.Resolve(@"C:\Program Files\nodejs\claude.cmd");

        var arguments = resolved.ComposeArguments(string.Empty);

        arguments.Should().Be(
            "/d /s /c \"\"C:\\Program Files\\nodejs\\claude.cmd\"\"");
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
}
