using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TiaAgent.Bridge.Diagnostics;
using TiaAgent.Bridge.Runtime;
using Xunit;

namespace TiaAgent.Bridge.Tests;

/// <summary>
/// Tests that ProcessRunner preserves exact output content across process boundaries.
/// Verifies that line endings, indentation, blank lines, Unicode, and Markdown
/// formatting survive the stdout capture → decode → return pipeline.
/// </summary>
public class ProcessRunnerOutputPreservationTests
{
    private readonly BridgeLogger _logger = new();

    /// <summary>
    /// Representative payload containing Markdown, indentation, code, tables,
    /// accents, symbols, and emojis.
    /// </summary>
    private const string RepresentativePayload =
        "# Análise 🔴\n" +
        "\n" +
        "O código contém uma **condição crítica**.\n" +
        "\n" +
        "- Estado: 🟡 Atenção\n" +
        "- Saída: `Q0.0`\n" +
        "\n" +
        "```scl\n" +
        "IF #ação THEN\n" +
        "    #saída := TRUE; // 🟢\n" +
        "END_IF;\n" +
        "```\n" +
        "\n" +
        "| Estado | Ícone |\n" +
        "|--------|-------|\n" +
        "| Alarme | 🔴 |\n" +
        "\n" +
        "Fluxo: Entrada → Processo ─ Saída";

    /// <summary>
    /// Writes raw UTF-8 bytes to stdout via a temporary PowerShell script,
    /// bypassing PowerShell's line-ending normalization.
    /// Uses base64 encoding to safely pass arbitrary text to PowerShell.
    /// </summary>
    private async Task<string> WriteRawUtf8(string text)
    {
        var b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        var script = "$bytes = [Convert]::FromBase64String('" + b64 + "')\n" +
                     "$stdout = [Console]::OpenStandardOutput()\n" +
                     "$stdout.Write($bytes, 0, $bytes.Length)\n" +
                     "$stdout.Flush()";

        var tempFile = Path.Combine(Path.GetTempPath(), $"tia-raw-{Guid.NewGuid():N}.ps1");
        try
        {
            File.WriteAllText(tempFile, script, new UTF8Encoding(false));

            using var runner = new ProcessRunner(_logger);
            var result = await runner.RunAsync(
                "pwsh",
                $"-NoProfile -ExecutionPolicy Bypass -File \"{tempFile}\"",
                null, TimeSpan.FromSeconds(15),
                cancellationToken: CancellationToken.None);
            result.ExitCode.Should().Be(0, because: $"stderr: {result.StdErr}");
            return result.StdOut;
        }
        finally
        {
            try { File.Delete(tempFile); } catch { }
        }
    }

    [Fact]
    public async Task NoFinalNewline_RemainsNoFinalNewline()
    {
        var input = "line1\nline2\nline3";
        var output = await WriteRawUtf8(input);
        output.Should().Be(input, "no final newline should be added");
    }

    [Fact]
    public async Task FinalNewline_RemainsExactlyOneFinalNewline()
    {
        var input = "line1\nline2\n";
        var output = await WriteRawUtf8(input);
        output.Should().Be(input, "exactly one final newline should be preserved");
    }

    [Fact]
    public async Task LF_Input_NotRebuiltAsCRLF()
    {
        var input = "line1\nline2\nline3";
        var output = await WriteRawUtf8(input);
        output.Should().NotContain("\r\n", "LF input must not be silently converted to CRLF");
        output.Should().Contain("\n", "LF newlines must be preserved");
    }

    [Fact]
    public async Task CRLF_Input_IsPreservedAsCRLF()
    {
        var input = "line1\r\nline2\r\nline3";
        var output = await WriteRawUtf8(input);
        output.Should().Be(input, "CRLF input must be preserved exactly");
    }

    [Fact]
    public async Task BlankLines_ArePreserved()
    {
        var input = "line1\n\n\nline4";
        var output = await WriteRawUtf8(input);
        output.Should().Be(input, "blank lines must be preserved exactly");
    }

    [Fact]
    public async Task Indentation_InFencedCode_IsPreserved()
    {
        var input = "```\n    indented\n        double indented\n```";
        var output = await WriteRawUtf8(input);
        output.Should().Be(input, "indentation inside fenced code must be preserved");
    }

    [Fact]
    public async Task Tabs_ArePreserved()
    {
        var input = "col1\tcol2\tcol3";
        var output = await WriteRawUtf8(input);
        output.Should().Be(input, "tabs must be preserved");
    }

    [Fact]
    public async Task SurrogatePairEmojis_ArePreserved()
    {
        var input = "Emojis: \U0001F534 \U0001F7E1 \U0001F7E2 \U0001F1E7\U0001F1F7";
        var output = await WriteRawUtf8(input);
        output.Should().Be(input, "surrogate-pair emojis must survive");
    }

    [Fact]
    public async Task PortugueseAccents_Arrows_BoxDrawing_ArePreserved()
    {
        var input = "Ação — révisão → ─ ┐ ├ │ ═ ║";
        var output = await WriteRawUtf8(input);
        output.Should().Be(input, "Portuguese accents, arrows, and box-drawing characters must survive");
    }

    [Fact]
    public async Task RepresentativeMarkdownPayload_IsPreserved()
    {
        var output = await WriteRawUtf8(RepresentativePayload);
        output.Should().Be(RepresentativePayload, "the representative Markdown payload must survive exactly");
    }

    [Fact]
    public async Task StdoutAndStderr_BothProduceOutput_WithoutBlocking()
    {
        using var runner = new ProcessRunner(_logger);
        var result = await runner.RunAsync(
            "pwsh",
            "-NoProfile -Command \"Write-Output 'stdout-line'; Write-Error 'stderr-line'\"",
            null, TimeSpan.FromSeconds(15),
            cancellationToken: CancellationToken.None);

        result.StdOut.Should().Contain("stdout-line");
        result.TimedOut.Should().BeFalse("concurrent reads must not deadlock");
    }

    [Fact]
    public async Task StdOut_EqualsDecodedStringExactly()
    {
        var input = "Hello World\n\tindented\ncafé\n🔴";
        var output = await WriteRawUtf8(input);
        output.Should().Be(input, "ProcessRunner.StdOut must equal the decoded string exactly");
    }

    [Fact]
    public async Task ProcessRunner_DoesNotMutateReturnedOutput()
    {
        var input = "line1\nline2\nline3";
        using var runner = new ProcessRunner(_logger);
        var reportedLines = new System.Collections.Concurrent.ConcurrentBag<string>();

        var progress = new Progress<string>(line => reportedLines.Add(line));

        // Encode input as base64 to avoid PowerShell string escaping issues
        var b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        var script = "$bytes = [Convert]::FromBase64String('" + b64 + "')\n" +
                     "$stdout = [Console]::OpenStandardOutput()\n" +
                     "$stdout.Write($bytes, 0, $bytes.Length)\n" +
                     "$stdout.Flush()";

        var tempFile = Path.Combine(Path.GetTempPath(), $"tia-raw-{Guid.NewGuid():N}.ps1");
        try
        {
            File.WriteAllText(tempFile, script, new UTF8Encoding(false));

            var result = await runner.RunAsync(
                "pwsh",
                $"-NoProfile -ExecutionPolicy Bypass -File \"{tempFile}\"",
                null, TimeSpan.FromSeconds(15),
                progress: progress,
                cancellationToken: CancellationToken.None);

            result.ExitCode.Should().Be(0);
            result.StdOut.Should().Be(input, "returned output must not be mutated by progress reporting");
        }
        finally
        {
            try { File.Delete(tempFile); } catch { }
        }
    }

    [Fact]
    public async Task LargePayload_IsPreserved()
    {
        var sb = new StringBuilder();
        sb.AppendLine("# Large Response");
        sb.AppendLine();
        for (int i = 0; i < 500; i++)
        {
            sb.AppendLine($"## Section {i}");
            sb.AppendLine();
            sb.AppendLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit. 🟡");
            sb.AppendLine();
        }
        var input = sb.ToString().TrimEnd('\n');

        var output = await WriteRawUtf8(input);
        output.Should().Be(input, "large payload must be preserved exactly");
    }

    [Fact]
    public async Task MixedLineEndings_ArePreserved()
    {
        var input = "line1\nline2\r\nline3\rline4";
        var output = await WriteRawUtf8(input);
        output.Should().Be(input, "mixed line endings must be preserved exactly");
    }
}
