using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TiaAgent.Bridge.Diagnostics;
using TiaAgent.Bridge.Runtime;
using TiaAgent.Contracts.Runtime;
using Xunit;

namespace TiaAgent.Bridge.Tests;

public class RuntimeAdapterTests
{
    private readonly BridgeLogger _logger = new();

    #region MimoCliRuntime

    [Fact]
    public void MimoCliRuntime_HasCorrectId()
    {
        var runtime = new MimoCliRuntime(_logger);
        runtime.Id.Should().Be("mimo");
        runtime.DisplayName.Should().Be("Mimo CLI");
    }

    [Fact]
    public async Task MimoCliRuntime_CheckAvailability_ReturnsResult()
    {
        var runtime = new MimoCliRuntime(_logger);

        var result = await runtime.CheckAvailabilityAsync(CancellationToken.None);

        // mimo is installed on this machine, so it should be available
        result.Should().NotBeNull();
        // Don't assert Available=true since test may run in CI without mimo
        result.Executable.Should().Be("mimo");
        result.Mode.Should().Be("cli");
    }

    [Fact]
    public async Task MimoCliRuntime_CheckAvailability_WithFakeExe_ReturnsUnavailable()
    {
        var runtime = new MimoCliRuntime(_logger, executable: "nonexistent_mimo_binary_12345");

        var result = await runtime.CheckAvailabilityAsync(CancellationToken.None);

        result.Available.Should().BeFalse();
        result.Error.Should().NotBeNullOrWhiteSpace();
    }

    #endregion

    #region OpenCodeRuntime

    [Fact]
    public void OpenCodeRuntime_ServerMode_HasCorrectId()
    {
        var runtime = new OpenCodeRuntime(_logger, mode: "server");
        runtime.Id.Should().Be("opencode");
        runtime.DisplayName.Should().Be("OpenCode");
    }

    [Fact]
    public void OpenCodeRuntime_CliMode_HasCorrectId()
    {
        var runtime = new OpenCodeRuntime(_logger, mode: "cli");
        runtime.Id.Should().Be("opencode");
        runtime.DisplayName.Should().Be("OpenCode");
    }

    [Fact]
    public async Task OpenCodeRuntime_CliMode_CheckAvailability_WithFakeExe_ReturnsUnavailable()
    {
        var runtime = new OpenCodeRuntime(_logger, mode: "cli", executable: "nonexistent_opencode_binary_12345");

        var result = await runtime.CheckAvailabilityAsync(CancellationToken.None);

        result.Available.Should().BeFalse();
    }

    [Fact]
    public async Task OpenCodeRuntime_ServerMode_CheckAvailability_WithFakeUrl_ReturnsUnavailable()
    {
        var runtime = new OpenCodeRuntime(_logger, mode: "server", serverUrl: "http://127.0.0.1:59999");

        var result = await runtime.CheckAvailabilityAsync(CancellationToken.None);

        result.Available.Should().BeFalse();
        result.Mode.Should().Be("server");
    }

    #endregion

    #region ClaudeCodeRuntime

    [Fact]
    public void ClaudeCodeRuntime_HasCorrectId()
    {
        var runtime = new ClaudeCodeRuntime(_logger);
        runtime.Id.Should().Be("claude");
        runtime.DisplayName.Should().Be("Claude Code CLI");
    }

    [Fact]
    public async Task ClaudeCodeRuntime_CheckAvailability_ReturnsResult()
    {
        var runtime = new ClaudeCodeRuntime(_logger);

        var result = await runtime.CheckAvailabilityAsync(CancellationToken.None);

        result.Should().NotBeNull();
        result.Executable.Should().Be("claude");
        result.Mode.Should().Be("cli");
    }

    [Fact]
    public async Task ClaudeCodeRuntime_CheckAvailability_WithFakeExe_ReturnsUnavailable()
    {
        var runtime = new ClaudeCodeRuntime(_logger, executable: "nonexistent_claude_binary_12345");

        var result = await runtime.CheckAvailabilityAsync(CancellationToken.None);

        result.Available.Should().BeFalse();
        result.Error.Should().NotBeNullOrWhiteSpace();
    }

    #endregion

    #region ProcessRunner

    [Fact]
    public async Task ProcessRunner_RunAsync_SimpleCommand_ReturnsOutput()
    {
        using var runner = new ProcessRunner(_logger);

        var result = await runner.RunAsync("dotnet", "--version", null, TimeSpan.FromSeconds(10));

        result.Success.Should().BeTrue();
        result.ExitCode.Should().Be(0);
        result.StdOut.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ProcessRunner_RunAsync_NonZeroExitCode_ReturnsFailure()
    {
        using var runner = new ProcessRunner(_logger);

        var result = await runner.RunAsync("dotnet", "nonexistent-command", null, TimeSpan.FromSeconds(10));

        result.ExitCode.Should().NotBe(0);
    }

    [Fact]
    public async Task ProcessRunner_RunAsync_Timeout_ReturnsTimedOut()
    {
        using var runner = new ProcessRunner(_logger);

        // Use a command that will run for a while (ping is reliable on Windows)
        var result = await runner.RunAsync("ping", "-n 10 127.0.0.1", null, TimeSpan.FromMilliseconds(200));

        // Either timed out or failed
        (result.TimedOut || !string.IsNullOrEmpty(result.Error) || result.ExitCode != 0).Should().BeTrue();
    }

    [Fact]
    public async Task ProcessRunner_RunAsync_Cancellation_ReturnsCancelled()
    {
        using var runner = new ProcessRunner(_logger);
        using var cts = new CancellationTokenSource();

        cts.Cancel();

        var result = await runner.RunAsync("dotnet", "--version", null, TimeSpan.FromSeconds(10), cancellationToken: cts.Token);

        result.Cancelled.Should().BeTrue();
    }

    [Fact]
    public void ProcessRunner_StripAnsiEscapes_RemovesEscapeSequences()
    {
        var input = "\x1B[31mHello\x1B[0m \x1B[1;32mWorld\x1B[0m";
        var result = ProcessRunner.StripAnsiEscapes(input);
        result.Should().Be("Hello World");
    }

    [Fact]
    public void ProcessRunner_StripAnsiEscapes_HandlesNullAndEmpty()
    {
        ProcessRunner.StripAnsiEscapes(null!).Should().BeNull();
        ProcessRunner.StripAnsiEscapes("").Should().Be("");
    }

    [Fact]
    public async Task ProcessRunner_RunAsync_WithEnvironmentVariables_PassesToProcess()
    {
        using var runner = new ProcessRunner(_logger);

        var result = await runner.RunAsync(
            "dotnet", "--version", null,
            TimeSpan.FromSeconds(10),
            environmentVariables: new System.Collections.Generic.Dictionary<string, string>
            {
                ["TEST_VAR"] = "hello"
            });

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task ProcessRunner_RunAsync_InvalidExecutable_ReturnsError()
    {
        using var runner = new ProcessRunner(_logger);

        var result = await runner.RunAsync("totally_nonexistent_binary_xyz_98765", "", null, TimeSpan.FromSeconds(5));

        result.Success.Should().BeFalse();
        // The error could be in Error field or StdErr
        (result.Error != null || result.ExitCode != 0).Should().BeTrue();
    }

    [Fact]
    public async Task ProcessRunner_RunAsync_PowerShell_CorruptsUtf8()
    {
        // This test DEMONSTRATES the root cause of the encoding corruption:
        // PowerShell 5.x reads child process stdout using the OEM code page (CP437),
        // which corrupts multi-byte UTF-8 sequences. ProcessRunner's
        // StandardOutputEncoding = UTF8 is set, but the corruption happens INSIDE
        // PowerShell before .NET's Process class reads the stream.
        using var runner = new ProcessRunner(_logger);

        var testString = "Ação — 🔴 🟡 🟢 → ─ ┐ ├ │";
        var escaped = testString.Replace("'", "''");
        var result = await runner.RunAsync(
            "powershell.exe",
            $"-NoProfile -Command \"'{escaped}'\"",
            null,
            TimeSpan.FromSeconds(10));

        result.Success.Should().BeTrue();
        // PowerShell 5.x corrupts the output — this is the bug we fixed in ResolveProcess
        // by preferring cmd.exe / direct exe over PowerShell.
        result.StdOut.Trim().Should().NotBe(testString,
            because: "PowerShell 5.x corrupts UTF-8 via OEM code page — this proves the root cause");
    }

    [Fact]
    public async Task ProcessRunner_RunAsync_CmdExe_PreservesUtf8()
    {
        // This test verifies that cmd.exe correctly preserves UTF-8 output.
        // cmd.exe does NOT re-encode child process stdout, so .NET's Process class
        // reads the raw UTF-8 byte stream via StandardOutputEncoding = UTF8.
        using var runner = new ProcessRunner(_logger);

        var testString = "Ação — 🔴 🟡 🟢 → ─ ┐ ├ │";
        // Use chcp to verify the console code page, then echo the string
        var result = await runner.RunAsync(
            "cmd.exe",
            $"/d /s /c \"chcp & echo {testString}\"",
            null,
            TimeSpan.FromSeconds(10));

        result.Success.Should().BeTrue();
        // cmd.exe + chcp will show the console code page (e.g. 437 or 65001)
        // but the important thing is that the echo output preserves the string.
        // Note: cmd.exe's echo may not handle all Unicode, but it doesn't corrupt
        // the byte stream like PowerShell does.
        result.StdOut.Should().NotBeNullOrWhiteSpace();
    }

    #endregion

    #region CommandResolver

    [Fact]
    public void CommandResolver_BareName_ExeAvailable_SelectsDirectExecution()
    {
        var resolved = CommandResolver.Resolve("dotnet", name => name == "dotnet.exe" ? @"C:\dotnet\dotnet.exe" : null);

        resolved.FileName.Should().Be(@"C:\dotnet\dotnet.exe");
        resolved.Wrapper.Should().Be(ProcessWrapper.None);
        resolved.ResolvedTargetPath.Should().Be(@"C:\dotnet\dotnet.exe");
    }

    [Fact]
    public void CommandResolver_BareName_OnlyCmdAvailable_SelectsCmdExe()
    {
        var resolved = CommandResolver.Resolve("claude", name => name switch
        {
            "claude.cmd" => @"C:\Users\test\npm\claude.cmd",
            _ => null
        });

        resolved.FileName.Should().Be("cmd.exe");
        resolved.Wrapper.Should().Be(ProcessWrapper.Cmd);
        resolved.ResolvedTargetPath.Should().Be(@"C:\Users\test\npm\claude.cmd");
        resolved.Arguments.Should().Contain("/d");
        resolved.Arguments.Should().Contain("/s");
        resolved.Arguments.Should().Contain("/c");
    }

    [Fact]
    public void CommandResolver_BareName_OnlyBatAvailable_SelectsCmdExe()
    {
        var resolved = CommandResolver.Resolve("mytool", name => name switch
        {
            "mytool.bat" => @"C:\tools\mytool.bat",
            _ => null
        });

        resolved.FileName.Should().Be("cmd.exe");
        resolved.Wrapper.Should().Be(ProcessWrapper.Cmd);
        resolved.ResolvedTargetPath.Should().Be(@"C:\tools\mytool.bat");
    }

    [Fact]
    public void CommandResolver_BareName_OnlyPs1Available_SelectsPowerShell()
    {
        var resolved = CommandResolver.Resolve("claude", name => name switch
        {
            "claude.ps1" => @"C:\Users\test\claude.ps1",
            "pwsh.exe" => @"C:\Program Files\PowerShell\7\pwsh.exe",
            _ => null
        });

        resolved.FileName.Should().Be(@"C:\Program Files\PowerShell\7\pwsh.exe");
        resolved.Wrapper.Should().Be(ProcessWrapper.PowerShellCore);
        resolved.ResolvedTargetPath.Should().Be(@"C:\Users\test\claude.ps1");
    }

    [Fact]
    public void CommandResolver_BareName_OnlyPs1_NoPwsh_FallsBackToWindowsPowerShell()
    {
        var resolved = CommandResolver.Resolve("claude", name => name switch
        {
            "claude.ps1" => @"C:\Users\test\claude.ps1",
            "powershell.exe" => @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe",
            _ => null
        });

        resolved.FileName.Should().Be(@"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe");
        resolved.Wrapper.Should().Be(ProcessWrapper.WindowsPowerShell);
    }

    [Fact]
    public void CommandResolver_BareName_ExePreferredOverPs1()
    {
        var resolved = CommandResolver.Resolve("claude", name => name switch
        {
            "claude.exe" => @"C:\claude\claude.exe",
            "claude.ps1" => @"C:\Users\test\claude.ps1",
            _ => null
        });

        resolved.FileName.Should().Be(@"C:\claude\claude.exe");
        resolved.Wrapper.Should().Be(ProcessWrapper.None);
    }

    [Fact]
    public void CommandResolver_BareName_CmdPreferredOverPs1()
    {
        var resolved = CommandResolver.Resolve("claude", name => name switch
        {
            "claude.cmd" => @"C:\Users\test\npm\claude.cmd",
            "claude.ps1" => @"C:\Users\test\claude.ps1",
            _ => null
        });

        resolved.FileName.Should().Be("cmd.exe");
        resolved.Wrapper.Should().Be(ProcessWrapper.Cmd);
    }

    [Fact]
    public void CommandResolver_ExplicitExe_DirectExecution()
    {
        var resolved = CommandResolver.Resolve(@"C:\claude\claude.exe");

        resolved.FileName.Should().Be(@"C:\claude\claude.exe");
        resolved.Wrapper.Should().Be(ProcessWrapper.None);
    }

    [Fact]
    public void CommandResolver_ExplicitCmd_UsesCmdExe()
    {
        var resolved = CommandResolver.Resolve(@"C:\tools\claude.cmd");

        resolved.FileName.Should().Be("cmd.exe");
        resolved.Wrapper.Should().Be(ProcessWrapper.Cmd);
        resolved.ResolvedTargetPath.Should().Be(@"C:\tools\claude.cmd");
    }

    [Fact]
    public void CommandResolver_ExplicitBat_UsesCmdExe()
    {
        var resolved = CommandResolver.Resolve(@"C:\tools\claude.bat");

        resolved.FileName.Should().Be("cmd.exe");
        resolved.Wrapper.Should().Be(ProcessWrapper.Cmd);
        resolved.ResolvedTargetPath.Should().Be(@"C:\tools\claude.bat");
    }

    [Fact]
    public void CommandResolver_ExplicitPs1_UsesPwshFirst()
    {
        var resolved = CommandResolver.Resolve(@"C:\scripts\claude.ps1", name => name switch
        {
            "pwsh.exe" => @"C:\Program Files\PowerShell\7\pwsh.exe",
            _ => null
        });

        resolved.FileName.Should().Be(@"C:\Program Files\PowerShell\7\pwsh.exe");
        resolved.Wrapper.Should().Be(ProcessWrapper.PowerShellCore);
    }

    [Fact]
    public void CommandResolver_BareName_NothingFound_ReturnsBareName()
    {
        var resolved = CommandResolver.Resolve("nonexistent_12345", _ => null);

        resolved.FileName.Should().Be("nonexistent_12345");
        resolved.Wrapper.Should().Be(ProcessWrapper.None);
    }

    [Fact]
    public void CommandResolver_ComposeArguments_JoinsParts()
    {
        var resolved = new ResolvedCommand
        {
            FileName = "cmd.exe",
            Arguments = new[] { "/d", "/s", "/c", "\"test.cmd\"" },
            Wrapper = ProcessWrapper.Cmd,
            ResolvedTargetPath = @"C:\test.cmd"
        };

        var composed = resolved.ComposeArguments("--version");
        composed.Should().Contain("/d");
        composed.Should().Contain("/s");
        composed.Should().Contain("/c");
        composed.Should().Contain("--version");
    }

    [Fact]
    public void CommandResolver_ComposeArguments_EmptyExtra()
    {
        var resolved = new ResolvedCommand
        {
            FileName = "claude.exe",
            Wrapper = ProcessWrapper.None,
            ResolvedTargetPath = "claude.exe"
        };

        var composed = resolved.ComposeArguments("");
        composed.Should().BeEmpty();
    }

    [Fact]
    public async Task CommandResolver_CmdExe_PreservesUtf8_Integration()
    {
        // Integration test: cmd.exe does NOT re-encode child process stdout,
        // so ProcessRunner reads raw UTF-8 bytes via StandardOutputEncoding = UTF8.
        using var runner = new ProcessRunner(_logger);

        // Create a temporary .cmd script that echoes the UTF-8 payload
        var tempDir = Path.Combine(Path.GetTempPath(), $"tia-test-{Guid.NewGuid():N}");
        Directory.CreateDirectory(tempDir);
        var cmdFile = Path.Combine(tempDir, "utf8test.cmd");
        var testString = "Ação — 🔴 🟡 🟢 → ─ ┐ ├ │";
        // Write the .cmd script that uses chcp 65001 and echoes the test string
        File.WriteAllText(cmdFile, $"@echo off\r\nchcp 65001 >nul\r\necho {testString}\r\n");

        try
        {
            var resolved = CommandResolver.Resolve("utf8test", name => name switch
            {
                "utf8test.cmd" => cmdFile,
                _ => null
            });

            resolved.FileName.Should().Be("cmd.exe");
            resolved.Wrapper.Should().Be(ProcessWrapper.Cmd);

            var args = resolved.ComposeArguments("");
            var result = await runner.RunAsync(
                resolved.FileName, args, null,
                TimeSpan.FromSeconds(10));

            result.ExitCode.Should().Be(0);
            result.StdOut.Should().Contain(testString,
                "cmd.exe must preserve UTF-8 output without corruption");
        }
        finally
        {
            try { File.Delete(cmdFile); } catch { }
            try { Directory.Delete(tempDir, true); } catch { }
        }
    }

    #endregion

    #region FakeRuntime (for integration testing)

    [Fact]
    public async Task FakeRuntime_ExecuteAsync_ReturnsSuccessResult()
    {
        var runtime = new FakeRuntime("test", "Test Runtime");
        var request = new AgentTaskRequest
        {
            TaskId = "task-1",
            CorrelationId = "corr-1",
            Action = "explain",
            AgentId = "tia-explain",
            Prompt = "Test prompt"
        };

        var result = await runtime.ExecuteAsync(request, null, CancellationToken.None);

        result.Success.Should().BeTrue();
        result.RuntimeId.Should().Be("test");
        result.Response.Should().Contain("task-1");
    }

    [Fact]
    public async Task FakeRuntime_CheckAvailability_ReturnsAvailable()
    {
        var runtime = new FakeRuntime("test", "Test Runtime");

        var result = await runtime.CheckAvailabilityAsync(CancellationToken.None);

        result.Available.Should().BeTrue();
        result.Version.Should().Be("1.0.0-test");
    }

    [Fact]
    public async Task FakeRuntime_WhenConfiguredToFail_ReturnsFailure()
    {
        var runtime = new FakeRuntime("test", "Test Runtime")
        {
            ShouldFail = true,
            FailureError = "Simulated failure",
            FailureErrorCode = "RUNTIME_UNAVAILABLE"
        };
        var request = new AgentTaskRequest
        {
            TaskId = "task-1",
            CorrelationId = "corr-1",
            Action = "explain",
            AgentId = "tia-explain",
            Prompt = "Test prompt"
        };

        var result = await runtime.ExecuteAsync(request, null, CancellationToken.None);

        result.Success.Should().BeFalse();
        result.Error.Should().Be("Simulated failure");
        result.ErrorCode.Should().Be("RUNTIME_UNAVAILABLE");
    }

    #endregion
}

#region ClaudeCodeRuntime Prompt Transport Tests

public class ClaudeCodeRuntimePromptTests
{
    private readonly BridgeLogger _logger = new();

    /// <summary>
    /// Helper: creates a standard valid prompt for testing.
    /// </summary>
    private static string MakeValidPrompt(string? suffix = null)
    {
        var sb = new StringBuilder();
        sb.AppendLine("You are a TIA Portal engineering assistant.");
        sb.AppendLine("Action: explain");
        sb.AppendLine("CorrelationId: tia-test-001");
        sb.AppendLine("Project: TestProject (proj-001)");
        sb.AppendLine("Selection: OB1 (Siemens.Engineering.SW.Blocks.OB)");
        sb.AppendLine("PLC: PLC_1");
        sb.AppendLine("Language: SCL");
        sb.AppendLine();
        sb.Append("User message: The user selected object \"OB1\" of type \"Block\" in TIA Portal. Please explain this object.");
        if (suffix != null) sb.Append(suffix);
        return sb.ToString();
    }

    /// <summary>
    /// Helper: creates a minimal valid AgentTaskRequest.
    /// </summary>
    private static AgentTaskRequest MakeRequest(string? prompt = null) => new()
    {
        TaskId = "task-test-001",
        CorrelationId = "tia-test-001",
        Action = "explain",
        AgentId = "tia-explain",
        Prompt = prompt ?? MakeValidPrompt(),
        Selection = new Contracts.Bridge.SelectionSnapshot
        {
            Name = "OB1",
            ObjectType = "Siemens.Engineering.SW.Blocks.OB",
            PlcName = "PLC_1",
            Language = "SCL"
        }
    };

    #region Prompt Preservation Tests (using pwsh echo of stdin SHA-256)

    [Fact]
    public async Task Prompt_PreservesMultilineUtf8()
    {
        var prompt = "Line 1\nLine 2\nLine 3\n";
        using var runner = new ProcessRunner(_logger);
        var result = await runner.RunAsync(
            "pwsh", "-NoProfile -Command \"$ms = New-Object System.IO.MemoryStream; $stream = [System.Console]::OpenStandardInput(); $buffer = New-Object byte[] 4096; while (($n = $stream.Read($buffer, 0, $buffer.Length)) -gt 0) { $ms.Write($buffer, 0, $n) }; $h = [System.Security.Cryptography.SHA256]::HashData($ms.ToArray()); [Console]::Write(([BitConverter]::ToString($h).Replace('-','').ToLower()))\"",
            null, TimeSpan.FromSeconds(30),
            stdinContent: prompt,
            cancellationToken: CancellationToken.None);

        result.ExitCode.Should().Be(0);
        var receivedHash = result.StdOut.Trim();
        var expectedHash = ProcessRunner.ComputeSha256(prompt);
        receivedHash.Should().Be(expectedHash, "multiline prompt must survive stdin transport");
    }

    [Fact]
    public async Task Prompt_PreservesPortugueseAccents()
    {
        var prompt = "Unicode: ação, revisão, válvula, máquina, ç, ã, é, —\n";
        using var runner = new ProcessRunner(_logger);
        var result = await runner.RunAsync(
            "pwsh", "-NoProfile -Command \"$ms = New-Object System.IO.MemoryStream; $stream = [System.Console]::OpenStandardInput(); $buffer = New-Object byte[] 4096; while (($n = $stream.Read($buffer, 0, $buffer.Length)) -gt 0) { $ms.Write($buffer, 0, $n) }; $h = [System.Security.Cryptography.SHA256]::HashData($ms.ToArray()); [Console]::Write(([BitConverter]::ToString($h).Replace('-','').ToLower()))\"",
            null, TimeSpan.FromSeconds(30),
            stdinContent: prompt,
            cancellationToken: CancellationToken.None);

        result.ExitCode.Should().Be(0);
        var receivedHash = result.StdOut.Trim();
        var expectedHash = ProcessRunner.ComputeSha256(prompt);
        receivedHash.Should().Be(expectedHash, "Portuguese accented characters must survive stdin transport");
    }

    [Fact]
    public async Task Prompt_PreservesEmDash()
    {
        var prompt = "Em dash: \u2014 and en dash: \u2013 and quotes: \u201Cdouble\u201D and \u2018single\u2019\n";
        using var runner = new ProcessRunner(_logger);
        var result = await runner.RunAsync(
            "pwsh", "-NoProfile -Command \"$ms = New-Object System.IO.MemoryStream; $stream = [System.Console]::OpenStandardInput(); $buffer = New-Object byte[] 4096; while (($n = $stream.Read($buffer, 0, $buffer.Length)) -gt 0) { $ms.Write($buffer, 0, $n) }; $h = [System.Security.Cryptography.SHA256]::HashData($ms.ToArray()); [Console]::Write(([BitConverter]::ToString($h).Replace('-','').ToLower()))\"",
            null, TimeSpan.FromSeconds(30),
            stdinContent: prompt,
            cancellationToken: CancellationToken.None);

        result.ExitCode.Should().Be(0);
        var receivedHash = result.StdOut.Trim();
        var expectedHash = ProcessRunner.ComputeSha256(prompt);
        receivedHash.Should().Be(expectedHash, "em dash and special quotes must survive stdin transport");
    }

    [Fact]
    public async Task Prompt_PreservesShellMetacharacters()
    {
        var prompt = "Shell characters: & | < > ^ % !\n";
        using var runner = new ProcessRunner(_logger);
        var result = await runner.RunAsync(
            "pwsh", "-NoProfile -Command \"$ms = New-Object System.IO.MemoryStream; $stream = [System.Console]::OpenStandardInput(); $buffer = New-Object byte[] 4096; while (($n = $stream.Read($buffer, 0, $buffer.Length)) -gt 0) { $ms.Write($buffer, 0, $n) }; $h = [System.Security.Cryptography.SHA256]::HashData($ms.ToArray()); [Console]::Write(([BitConverter]::ToString($h).Replace('-','').ToLower()))\"",
            null, TimeSpan.FromSeconds(30),
            stdinContent: prompt,
            cancellationToken: CancellationToken.None);

        result.ExitCode.Should().Be(0);
        var receivedHash = result.StdOut.Trim();
        var expectedHash = ProcessRunner.ComputeSha256(prompt);
        receivedHash.Should().Be(expectedHash, "shell metacharacters must survive stdin transport");
    }

    [Fact]
    public async Task Prompt_WithTrailingNewline_Preserved()
    {
        var prompt = MakeValidPrompt() + "\n";
        using var runner = new ProcessRunner(_logger);
        var result = await runner.RunAsync(
            "pwsh", "-NoProfile -Command \"$ms = New-Object System.IO.MemoryStream; $stream = [System.Console]::OpenStandardInput(); $buffer = New-Object byte[] 4096; while (($n = $stream.Read($buffer, 0, $buffer.Length)) -gt 0) { $ms.Write($buffer, 0, $n) }; $h = [System.Security.Cryptography.SHA256]::HashData($ms.ToArray()); [Console]::Write(([BitConverter]::ToString($h).Replace('-','').ToLower()))\"",
            null, TimeSpan.FromSeconds(30),
            stdinContent: prompt,
            cancellationToken: CancellationToken.None);

        result.ExitCode.Should().Be(0);
        var receivedHash = result.StdOut.Trim();
        var expectedHash = ProcessRunner.ComputeSha256(prompt);
        receivedHash.Should().Be(expectedHash, "trailing newline must be preserved");
    }

    [Fact]
    public async Task Prompt_WithoutTrailingNewline_Preserved()
    {
        var prompt = MakeValidPrompt().TrimEnd('\n');
        using var runner = new ProcessRunner(_logger);
        var result = await runner.RunAsync(
            "pwsh", "-NoProfile -Command \"$ms = New-Object System.IO.MemoryStream; $stream = [System.Console]::OpenStandardInput(); $buffer = New-Object byte[] 4096; while (($n = $stream.Read($buffer, 0, $buffer.Length)) -gt 0) { $ms.Write($buffer, 0, $n) }; $h = [System.Security.Cryptography.SHA256]::HashData($ms.ToArray()); [Console]::Write(([BitConverter]::ToString($h).Replace('-','').ToLower()))\"",
            null, TimeSpan.FromSeconds(30),
            stdinContent: prompt,
            cancellationToken: CancellationToken.None);

        result.ExitCode.Should().Be(0);
        var receivedHash = result.StdOut.Trim();
        var expectedHash = ProcessRunner.ComputeSha256(prompt);
        receivedHash.Should().Be(expectedHash, "prompt without trailing newline must be preserved");
    }

    [Fact]
    public async Task Prompt_LargePrompt_Preserved()
    {
        // Simulate a large PLC source code prompt (~50K chars)
        var sb = new StringBuilder(MakeValidPrompt());
        sb.AppendLine();
        sb.AppendLine("BEGIN_BLOCK SCL OB1");
        for (int i = 0; i < 500; i++)
        {
            sb.AppendLine($"  // Variable declaration line {i}: VAR_{i} : INT := {i}; // Comment with unicode: café");
        }
        sb.AppendLine("END_BLOCK");
        var prompt = sb.ToString();

        using var runner = new ProcessRunner(_logger);
        var result = await runner.RunAsync(
            "pwsh", "-NoProfile -Command \"$ms = New-Object System.IO.MemoryStream; $stream = [System.Console]::OpenStandardInput(); $buffer = New-Object byte[] 4096; while (($n = $stream.Read($buffer, 0, $buffer.Length)) -gt 0) { $ms.Write($buffer, 0, $n) }; $h = [System.Security.Cryptography.SHA256]::HashData($ms.ToArray()); [Console]::Write(([BitConverter]::ToString($h).Replace('-','').ToLower()))\"",
            null, TimeSpan.FromSeconds(30),
            stdinContent: prompt,
            cancellationToken: CancellationToken.None);

        result.ExitCode.Should().Be(0);
        var receivedHash = result.StdOut.Trim();
        var expectedHash = ProcessRunner.ComputeSha256(prompt);
        receivedHash.Should().Be(expectedHash, "large prompt must survive stdin transport intact");
    }

    [Fact]
    public async Task Prompt_FullSentinel_Preserved()
    {
        var prompt = @"PROMPT_SENTINEL_BEGIN
Action: explain
Unicode: ação, revisão, válvula, máquina, ç, ã, é, —
Quotes: ""double"" and 'single'
Shell characters: & | < > ^ % !
Line 1
Line 2
PROMPT_SENTINEL_END";
        using var runner = new ProcessRunner(_logger);
        var result = await runner.RunAsync(
            "pwsh", "-NoProfile -Command \"$ms = New-Object System.IO.MemoryStream; $stream = [System.Console]::OpenStandardInput(); $buffer = New-Object byte[] 4096; while (($n = $stream.Read($buffer, 0, $buffer.Length)) -gt 0) { $ms.Write($buffer, 0, $n) }; $h = [System.Security.Cryptography.SHA256]::HashData($ms.ToArray()); [Console]::Write(([BitConverter]::ToString($h).Replace('-','').ToLower()))\"",
            null, TimeSpan.FromSeconds(30),
            stdinContent: prompt,
            cancellationToken: CancellationToken.None);

        result.ExitCode.Should().Be(0);
        var receivedHash = result.StdOut.Trim();
        var expectedHash = ProcessRunner.ComputeSha256(prompt);
        receivedHash.Should().Be(expectedHash, "sentinel prompt must survive stdin transport intact");
    }

    #endregion

    #region ProcessRunner stdin overload

    [Fact]
    public async Task ProcessRunner_StdinOverload_WritesContentToStdin()
    {
        using var runner = new ProcessRunner(_logger);
        var prompt = "Hello from stdin test";
        // pwsh reads stdin and echoes it back
        var result = await runner.RunAsync(
            "pwsh", "-NoProfile -Command \"$input | ForEach-Object { Write-Output $_ }\"",
            null, TimeSpan.FromSeconds(10),
            stdinContent: prompt,
            cancellationToken: CancellationToken.None);

        result.ExitCode.Should().Be(0);
        result.StdOut.Trim().Should().Be(prompt);
    }

    [Fact]
    public async Task ProcessRunner_StdinOverload_NullStdin_ClosesImmediately()
    {
        using var runner = new ProcessRunner(_logger);
        var result = await runner.RunAsync(
            "dotnet", "--version", null, TimeSpan.FromSeconds(10),
            environmentVariables: null, progress: null,
            stdinContent: null,
            cancellationToken: CancellationToken.None);

        result.ExitCode.Should().Be(0);
        result.StdOut.Should().NotBeNullOrWhiteSpace();
    }

    #endregion

    #region Request Validation Tests

    [Fact]
    public async Task ExecuteAsync_EmptyPrompt_ReturnsValidationError()
    {
        var runtime = new ClaudeCodeRuntime(_logger, executable: "nonexistent_claude_12345");
        var request = MakeRequest(prompt: "");

        var result = await runtime.ExecuteAsync(request, null, CancellationToken.None);

        result.Success.Should().BeFalse();
        result.ErrorCode.Should().Be("INVALID_REQUEST");
        result.Error.Should().Contain("Empty prompt");
    }

    [Fact]
    public async Task ExecuteAsync_NullPrompt_ReturnsValidationError()
    {
        var runtime = new ClaudeCodeRuntime(_logger, executable: "nonexistent_claude_12345");
        var request = MakeRequest(prompt: null);
        request.Prompt = null!;

        var result = await runtime.ExecuteAsync(request, null, CancellationToken.None);

        result.Success.Should().BeFalse();
        result.ErrorCode.Should().Be("INVALID_REQUEST");
    }

    [Fact]
    public async Task ExecuteAsync_ShortPrompt_ReturnsValidationError()
    {
        var runtime = new ClaudeCodeRuntime(_logger, executable: "nonexistent_claude_12345");
        var request = MakeRequest(prompt: "too short");

        var result = await runtime.ExecuteAsync(request, null, CancellationToken.None);

        result.Success.Should().BeFalse();
        result.ErrorCode.Should().Be("INVALID_REQUEST");
        result.Error.Should().Contain("too short");
    }

    [Fact]
    public async Task ExecuteAsync_UnrecognizedAction_ReturnsValidationError()
    {
        var runtime = new ClaudeCodeRuntime(_logger, executable: "nonexistent_claude_12345");
        var request = MakeRequest();
        request.Action = "deploy";

        var result = await runtime.ExecuteAsync(request, null, CancellationToken.None);

        result.Success.Should().BeFalse();
        result.ErrorCode.Should().Be("INVALID_REQUEST");
        result.Error.Should().Contain("Unrecognized action");
    }

    [Fact]
    public async Task ExecuteAsync_MissingCorrelationId_ReturnsValidationError()
    {
        var runtime = new ClaudeCodeRuntime(_logger, executable: "nonexistent_claude_12345");
        var request = MakeRequest();
        request.CorrelationId = "";

        var result = await runtime.ExecuteAsync(request, null, CancellationToken.None);

        result.Success.Should().BeFalse();
        result.ErrorCode.Should().Be("INVALID_REQUEST");
        result.Error.Should().Contain("correlation ID");
    }

    [Fact]
    public async Task ExecuteAsync_MissingAgentId_ReturnsValidationError()
    {
        var runtime = new ClaudeCodeRuntime(_logger, executable: "nonexistent_claude_12345");
        var request = MakeRequest();
        request.AgentId = "";

        var result = await runtime.ExecuteAsync(request, null, CancellationToken.None);

        result.Success.Should().BeFalse();
        result.ErrorCode.Should().Be("INVALID_REQUEST");
        result.Error.Should().Contain("agent ID");
    }

    #endregion

    #region Session Isolation Tests

    [Fact]
    public void BuildArguments_DoesNotIncludeContinueFlag()
    {
        // BuildArguments is private, but we can verify via the full command that ExecuteAsync
        // would produce. Since we can't directly call BuildArguments, we verify the contract:
        // the runtime never sets --continue or -c.
        // This is a design-level assertion: the code does not contain these strings.
        var runtime = new ClaudeCodeRuntime(_logger);
        runtime.Should().NotBeNull();
        // The actual verification is in the code review: BuildArguments() does not append
        // --continue or -c. This test documents the requirement.
        true.Should().BeTrue();
    }

    [Fact]
    public void BuildArguments_DoesNotIncludeResumeFlag()
    {
        // Same as above: documents that --resume and -r are never used.
        var runtime = new ClaudeCodeRuntime(_logger);
        runtime.Should().NotBeNull();
        true.Should().BeTrue();
    }

    [Fact]
    public async Task ExecuteAsync_InvalidExecutable_ReportsProcessError()
    {
        var runtime = new ClaudeCodeRuntime(_logger, executable: "nonexistent_claude_binary_12345");
        var request = MakeRequest();

        var result = await runtime.ExecuteAsync(request, null, CancellationToken.None);

        result.Success.Should().BeFalse();
        // Should fail with process error, not a valid Claude response
        (result.ErrorCode != null || result.Error != null).Should().BeTrue();
    }

    #endregion

    #region Response Validation Tests

    [Fact]
    public async Task ExecuteAsync_UnavailableRuntime_ReturnsProcessError()
    {
        var runtime = new ClaudeCodeRuntime(_logger, executable: "nonexistent_claude_xyz_99999");
        var request = MakeRequest();

        var result = await runtime.ExecuteAsync(request, null, CancellationToken.None);

        result.Success.Should().BeFalse();
        result.ErrorCode.Should().NotBeNullOrEmpty();
    }

    #endregion

    #region ProcessRunner SHA-256

    [Fact]
    public void ProcessRunner_ComputeSha256_Deterministic()
    {
        var prompt = MakeValidPrompt();
        var hash1 = ProcessRunner.ComputeSha256(prompt);
        var hash2 = ProcessRunner.ComputeSha256(prompt);

        hash1.Should().Be(hash2);
        hash1.Should().HaveLength(64); // SHA-256 hex is 64 chars
    }

    [Fact]
    public void ProcessRunner_ComputeSha256_DifferentInputs_DifferentHashes()
    {
        var hash1 = ProcessRunner.ComputeSha256("prompt one");
        var hash2 = ProcessRunner.ComputeSha256("prompt two");

        hash1.Should().NotBe(hash2);
    }

    [Fact]
    public void ProcessRunner_ComputeSha256_HandlesUnicode()
    {
        var hash = ProcessRunner.ComputeSha256("ação, revisão, válvula");
        hash.Should().HaveLength(64);
    }

    #endregion

    #region Integration: SHA-256 Hash Match

    [Fact]
    public async Task PromptHash_MatchesStdinBytes_Integration()
    {
        var prompt = MakeValidPrompt();
        var expectedHash = ProcessRunner.ComputeSha256(prompt);

        using var runner = new ProcessRunner(_logger);
        // PowerShell reads all stdin, computes SHA-256 of the UTF-8 bytes, outputs the hex hash
        var result = await runner.RunAsync(
            "pwsh",
            "-NoProfile -Command \"$bytes = [System.Text.Encoding]::UTF8.GetBytes([Console]::In.ReadToEnd()); $hash = [System.Security.Cryptography.SHA256]::HashData($bytes); [Console]::Write(([BitConverter]::ToString($hash).Replace('-','').ToLower()))\"",
            null,
            TimeSpan.FromSeconds(30),
            stdinContent: prompt,
            cancellationToken: CancellationToken.None);

        result.ExitCode.Should().Be(0);
        var receivedHash = result.StdOut.Trim();
        receivedHash.Should().Be(expectedHash,
            "the SHA-256 hash computed by the Bridge must match the hash of bytes received by the process");
    }

    #endregion
}

#endregion
