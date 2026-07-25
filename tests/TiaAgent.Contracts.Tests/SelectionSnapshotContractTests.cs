using System.Text;
using FluentAssertions;
using TiaAgent.Contracts.Bridge;
using Xunit;

namespace TiaAgent.Contracts.Tests;

/// <summary>
/// Contract tests to verify that SelectionSnapshot serialization/deserialization
/// preserves the Source property correctly between Add-In and Bridge.
/// </summary>
public class SelectionSnapshotContractTests
{
    [Fact]
    public void SelectionSnapshot_WithSource_RoundTripsCorrectly()
    {
        // Arrange
        var original = new SelectionSnapshot
        {
            Name = "OB1",
            ObjectType = "Siemens.Engineering.SW.Blocks.OB",
            RuntimeType = "Siemens.Engineering.SW.Blocks.OB",
            PlcName = "PLC_1",
            TiaPath = "OB1",
            Language = "SCL",
            Source = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n< документ>\n  <Блоки>\n    <Блок Имя=\"OB1\" Тип=\"OB\" />\n  </Блоки>\n</документ>",
            SourceFormat = "xml"
        };

        // Act - Serialize to JSON (simulating Add-In serialization)
        var json = SerializeSelectionSnapshot(original);

        // Assert - JSON should contain Source
        json.Should().Contain("\"source\":");
        json.Should().Contain("\"sourceFormat\":\"xml\"");
        json.Should().Contain("OB1");

        // Act - Deserialize from JSON (simulating Bridge deserialization)
        var deserialized = DeserializeSelectionSnapshot(json);

        // Assert - Source should survive the round trip
        deserialized.Should().NotBeNull();
        deserialized!.Name.Should().Be("OB1");
        deserialized.Source.Should().Be(original.Source);
        deserialized.SourceFormat.Should().Be("xml");
    }

    [Fact]
    public void SelectionSnapshot_WithoutSource_RoundTripsCorrectly()
    {
        // Arrange
        var original = new SelectionSnapshot
        {
            Name = "PLC_1",
            ObjectType = "Siemens.Engineering.HW.Device",
            RuntimeType = "Siemens.Engineering.HW.Device",
            PlcName = "",
            TiaPath = "PLC_1",
            Language = ""
        };

        // Act
        var json = SerializeSelectionSnapshot(original);
        var deserialized = DeserializeSelectionSnapshot(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Name.Should().Be("PLC_1");
        deserialized.Source.Should().BeNull();
        deserialized.SourceFormat.Should().BeNull();
    }

    [Fact]
    public void SelectionSnapshot_WithLargeSource_RoundTripsCorrectly()
    {
        // Arrange - Simulate a large PLC source (50K+ chars)
        var sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        sb.AppendLine("<СИМВОЛИЧЕСКИЙ_ИСТОЧНИК>");
        for (int i = 0; i < 1000; i++)
        {
            sb.AppendLine($"  <Строка Номер=\"{i}\">Переменная_{i} : ЦЕЛЫЙ := {i}; // комментарий</Строка>");
        }
        sb.AppendLine("</СИМВОЛИЧЕСКИЙ_ИСТОЧНИК>");
        var largeSource = sb.ToString();

        var original = new SelectionSnapshot
        {
            Name = "FB100",
            ObjectType = "Siemens.Engineering.SW.Blocks.FB",
            RuntimeType = "Siemens.Engineering.SW.Blocks.FB",
            PlcName = "PLC_1",
            TiaPath = "FB100",
            Language = "SCL",
            Source = largeSource,
            SourceFormat = "xml"
        };

        // Act
        var json = SerializeSelectionSnapshot(original);
        var deserialized = DeserializeSelectionSnapshot(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Source.Should().Be(largeSource);
        deserialized.Source.Length.Should().BeGreaterThan(50000);
    }

    [Fact]
    public void SelectionSnapshot_WithUnicodeSource_RoundTripsCorrectly()
    {
        // Arrange - Test Portuguese accents, em dashes, and other Unicode
        var unicodeSource = "Ação — revisão; válvula; máquina; ç, ã, é, ñ; «comillas»";
        var original = new SelectionSnapshot
        {
            Name = "FB_Unicode",
            ObjectType = "Siemens.Engineering.SW.Blocks.FB",
            RuntimeType = "Siemens.Engineering.SW.Blocks.FB",
            PlcName = "PLC_1",
            TiaPath = "FB_Unicode",
            Language = "SCL",
            Source = unicodeSource,
            SourceFormat = "xml"
        };

        // Act
        var json = SerializeSelectionSnapshot(original);
        var deserialized = DeserializeSelectionSnapshot(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Source.Should().Be(unicodeSource);
    }

    [Fact]
    public void SelectionSnapshot_WithShellCharactersInSource_RoundTripsCorrectly()
    {
        // Arrange - Test shell metacharacters that could cause issues
        var shellSource = "Command: echo \"Hello & World | Pipe > Redirect < Input ^ Escape % Variable ! Negation\"";
        var original = new SelectionSnapshot
        {
            Name = "FB_Shell",
            ObjectType = "Siemens.Engineering.SW.Blocks.FB",
            RuntimeType = "Siemens.Engineering.SW.Blocks.FB",
            PlcName = "PLC_1",
            TiaPath = "FB_Shell",
            Language = "SCL",
            Source = shellSource,
            SourceFormat = "xml"
        };

        // Act
        var json = SerializeSelectionSnapshot(original);
        var deserialized = DeserializeSelectionSnapshot(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Source.Should().Be(shellSource);
    }

    [Fact]
    public void SelectionSnapshot_SourceHash_RemainsConsistent()
    {
        // Arrange
        var source = "Test PLC source code with special chars: àáâãäå ñç €£¥";
        var original = new SelectionSnapshot
        {
            Name = "FB_Hash",
            ObjectType = "Siemens.Engineering.SW.Blocks.FB",
            RuntimeType = "Siemens.Engineering.SW.Blocks.FB",
            PlcName = "PLC_1",
            TiaPath = "FB_Hash",
            Language = "SCL",
            Source = source,
            SourceFormat = "xml"
        };

        // Act
        var json = SerializeSelectionSnapshot(original);
        var deserialized = DeserializeSelectionSnapshot(json);

        // Assert - Hash of source should be identical before and after round trip
        var originalHash = ComputeSha256(original.Source!);
        var deserializedHash = ComputeSha256(deserialized!.Source!);
        originalHash.Should().Be(deserializedHash);
    }

    [Fact]
    public void BridgeTaskRequest_WithSource_SerializesCorrectly()
    {
        // Arrange
        var request = new BridgeTaskRequest
        {
            ContractVersion = "1.0",
            CorrelationId = "tia-test-001",
            Action = "explain",
            AgentId = "tia-explain",
            TiaInstance = new TiaInstanceSnapshot
            {
                ProcessId = 1234,
                SessionId = "test-session",
                Version = "21.0"
            },
            Project = new ProjectSnapshot
            {
                Id = "proj-001",
                Name = "TestProject",
                Path = @"C:\Projects\Test"
            },
            Selection = new SelectionSnapshot
            {
                Name = "OB1",
                ObjectType = "Siemens.Engineering.SW.Blocks.OB",
                RuntimeType = "Siemens.Engineering.SW.Blocks.OB",
                PlcName = "PLC_1",
                TiaPath = "OB1",
                Language = "SCL",
                Source = "<?xml version=\"1.0\"?>\n<Block Name=\"OB1\">...</Block>",
                SourceFormat = "xml"
            },
            UserMessage = "Please explain this object."
        };

        // Act
        var json = SerializeBridgeTaskRequest(request);

        // Assert
        json.Should().Contain("\"source\":");
        json.Should().Contain("\"sourceFormat\":\"xml\"");
        // The source XML contains special characters that get JSON-escaped
        // Check that the source field is present and contains the XML content
        json.Should().Contain("Block Name");
    }

    /// <summary>
    /// Simulates Add-In JSON serialization (manual, matching AgentBridgeClient.BuildTaskRequestJson).
    /// </summary>
    private static string SerializeSelectionSnapshot(SelectionSnapshot snapshot)
    {
        var sb = new StringBuilder();
        sb.Append('{');
        sb.AppendFormat("\"name\":\"{0}\"", EscapeJson(snapshot.Name));
        sb.AppendFormat(",\"objectType\":\"{0}\"", EscapeJson(snapshot.ObjectType));
        sb.AppendFormat(",\"runtimeType\":\"{0}\"", EscapeJson(snapshot.RuntimeType));
        sb.AppendFormat(",\"plcName\":\"{0}\"", EscapeJson(snapshot.PlcName));
        sb.AppendFormat(",\"tiaPath\":\"{0}\"", EscapeJson(snapshot.TiaPath));
        sb.AppendFormat(",\"language\":\"{0}\"", EscapeJson(snapshot.Language));

        if (!string.IsNullOrEmpty(snapshot.Source))
        {
            sb.AppendFormat(",\"source\":\"{0}\"", EscapeJson(snapshot.Source));
            sb.AppendFormat(",\"sourceFormat\":\"{0}\"", EscapeJson(snapshot.SourceFormat ?? "xml"));
        }

        sb.Append('}');
        return sb.ToString();
    }

    /// <summary>
    /// Simulates Bridge JSON deserialization (matching BridgeController or TaskManager).
    /// </summary>
    private static SelectionSnapshot? DeserializeSelectionSnapshot(string json)
    {
        // Simple JSON parsing for testing (not production code)
        var name = ExtractJsonString(json, "name");
        var objectType = ExtractJsonString(json, "objectType");
        var source = ExtractJsonString(json, "source");
        var sourceFormat = ExtractJsonString(json, "sourceFormat");

        if (name == null || objectType == null)
            return null;

        return new SelectionSnapshot
        {
            Name = name,
            ObjectType = objectType,
            RuntimeType = ExtractJsonString(json, "runtimeType") ?? "",
            PlcName = ExtractJsonString(json, "plcName") ?? "",
            TiaPath = ExtractJsonString(json, "tiaPath") ?? "",
            Language = ExtractJsonString(json, "language") ?? "",
            Source = source,
            SourceFormat = sourceFormat
        };
    }

    private static string SerializeBridgeTaskRequest(BridgeTaskRequest request)
    {
        var sb = new StringBuilder();
        sb.Append('{');
        sb.AppendFormat("\"contractVersion\":\"{0}\"", EscapeJson(request.ContractVersion));
        sb.AppendFormat(",\"correlationId\":\"{0}\"", EscapeJson(request.CorrelationId));
        sb.AppendFormat(",\"action\":\"{0}\"", EscapeJson(request.Action));
        sb.AppendFormat(",\"agentId\":\"{0}\"", EscapeJson(request.AgentId));
        sb.AppendFormat(",\"userMessage\":\"{0}\"", EscapeJson(request.UserMessage));

        if (request.Selection != null)
        {
            sb.Append(",\"selection\":{");
            sb.AppendFormat("\"name\":\"{0}\"", EscapeJson(request.Selection.Name));
            sb.AppendFormat(",\"objectType\":\"{0}\"", EscapeJson(request.Selection.ObjectType));
            sb.AppendFormat(",\"runtimeType\":\"{0}\"", EscapeJson(request.Selection.RuntimeType));
            sb.AppendFormat(",\"plcName\":\"{0}\"", EscapeJson(request.Selection.PlcName));
            sb.AppendFormat(",\"tiaPath\":\"{0}\"", EscapeJson(request.Selection.TiaPath));
            sb.AppendFormat(",\"language\":\"{0}\"", EscapeJson(request.Selection.Language));

            if (!string.IsNullOrEmpty(request.Selection.Source))
            {
                sb.AppendFormat(",\"source\":\"{0}\"", EscapeJson(request.Selection.Source));
                sb.AppendFormat(",\"sourceFormat\":\"{0}\"", EscapeJson(request.Selection.SourceFormat ?? "xml"));
            }

            sb.Append('}');
        }

        sb.Append('}');
        return sb.ToString();
    }

    private static string EscapeJson(string value)
    {
        if (value == null) return "";
        return value
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\n", "\\n")
            .Replace("\r", "\\r")
            .Replace("\t", "\\t");
    }

    private static string? ExtractJsonString(string json, string key)
    {
        var search = "\"" + key + "\"";
        var idx = json.IndexOf(search, System.StringComparison.OrdinalIgnoreCase);
        if (idx < 0) return null;

        idx = json.IndexOf(':', idx + search.Length);
        if (idx < 0) return null;

        idx++;
        while (idx < json.Length && json[idx] == ' ') idx++;

        if (idx >= json.Length) return null;

        if (json[idx] == '"')
        {
            var start = idx + 1;
            var i = start;
            while (i < json.Length)
            {
                if (json[i] == '\\')
                {
                    i += 2;
                    continue;
                }
                if (json[i] == '"')
                    break;
                i++;
            }

            if (i >= json.Length) return null;
            var raw = json.Substring(start, i - start);
            return raw
                .Replace("\\n", "\n")
                .Replace("\\r", "\r")
                .Replace("\\t", "\t")
                .Replace("\\\"", "\"")
                .Replace("\\\\", "\\");
        }

        return null;
    }

    private static string ComputeSha256(string text)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(text);
        var hash = System.Security.Cryptography.SHA256.HashData(bytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}
