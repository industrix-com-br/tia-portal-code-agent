using System.Text;
using FluentAssertions;
using TiaAgent.Contracts.Diagnostics;
using Xunit;

namespace TiaAgent.Contracts.Tests;

public class TextPayloadDiagnosticsTests
{
    private const string RepresentativePayload = "# Análise 🔴\r\n\r\n- Estado: 🟡 Atenção\r\n\r\n```scl\r\nIF #ação THEN\r\n\t#saída := TRUE; // 🟢\r\nEND_IF;\r\n```\r\n\r\nFluxo: Entrada → Processo ─ Saída\r\nSímbolos: ─ ┐ ├ │";

    [Fact]
    public void DescribeText_IsDeterministicAndDoesNotLogFullPayload()
    {
        var first = TextPayloadDiagnostics.DescribeText("test", RepresentativePayload, previewScalarLimit: 8);
        var second = TextPayloadDiagnostics.DescribeText("test", RepresentativePayload, previewScalarLimit: 8);

        first.Should().Be(second);
        first.Should().Contain("boundary=test");
        first.Should().Contain("sha256=");
        first.Should().Contain("utf8Bytes=");
        first.Should().Contain("codePoints=");
        first.Should().NotContain(RepresentativePayload);
    }

    [Fact]
    public void DescribeText_CountsSupplementaryCharactersAsOneScalar()
    {
        var diagnostics = TextPayloadDiagnostics.DescribeText("emoji", "A🔴B");

        diagnostics.Should().Contain("utf16Length=4");
        diagnostics.Should().Contain("scalarCount=3");
        diagnostics.Should().Contain("U+01F534");
    }

    [Fact]
    public void DescribeText_DetectsReplacementCharacterAndKnownMojibake()
    {
        var diagnostics = TextPayloadDiagnostics.DescribeText("bad", "� ≡ƒö┤ ΓåÆ");

        diagnostics.Should().Contain("replacementCharacter=True");
        diagnostics.Should().Contain("knownMojibake=True");
    }

    [Fact]
    public void DescribeUtf8Bytes_ReportsValidUtf8AndMatchingHash()
    {
        var bytes = Encoding.UTF8.GetBytes(RepresentativePayload);

        var byteDiagnostics = TextPayloadDiagnostics.DescribeUtf8Bytes("raw", bytes);
        var textDiagnostics = TextPayloadDiagnostics.DescribeText("decoded", RepresentativePayload);
        var hash = TextPayloadDiagnostics.ComputeUtf8Sha256(RepresentativePayload);

        byteDiagnostics.Should().Contain("strictUtf8Valid=True");
        byteDiagnostics.Should().Contain($"sha256={hash}");
        textDiagnostics.Should().Contain($"sha256={hash}");
    }

    [Fact]
    public void DescribeUtf8Bytes_ReportsInvalidUtf8WithoutRepairingIt()
    {
        var invalidBytes = new byte[] { 0xF0, 0x28, 0x8C, 0x28 };

        var diagnostics = TextPayloadDiagnostics.DescribeUtf8Bytes("raw", invalidBytes);

        diagnostics.Should().Contain("strictUtf8Valid=False");
    }
}
