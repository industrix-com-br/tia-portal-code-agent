#if SIEMENS
using System;
using System.IO;
using Siemens.Engineering;
using Siemens.Engineering.SW;
using Siemens.Engineering.SW.Blocks;
using TiaAgent.AddIn.Diagnostics;
using TiaAgent.Contracts.Bridge;

namespace TiaAgent.AddIn.Providers;

/// <summary>
/// Captures a SelectionSnapshot from IEngineeringObject.
/// Uses direct interface members only — no reflection (reflection on COM-interop
/// types triggers VerificationException in TIA Portal's partial-trust sandbox).
/// </summary>
public static class SelectionSnapshotFactory
{
    public static SelectionSnapshot Create(IEngineeringObject obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        var name = GetSafeName(obj);
        var typeName = obj.GetType().Name;

        var snapshot = new SelectionSnapshot
        {
            Name = name,
            ObjectType = typeName,
            RuntimeType = obj.GetType().FullName ?? "",
            PlcName = "",
            TiaPath = name,
            Language = ""
        };

        // Extract PLC source code for block objects
        var source = ExtractSource(obj);
        if (source != null)
        {
            snapshot.Source = source;
            snapshot.SourceFormat = "xml"; // SIMATIC ML export format
            AddInLogger.Info($"SelectionSnapshotFactory: extracted {source.Length} chars of source for {name}");
        }
        else
        {
            AddInLogger.Warn($"SelectionSnapshotFactory: no source extracted for {name} (type: {typeName})");
        }

        return snapshot;
    }

    /// <summary>
    /// Extracts PLC source code from an engineering object.
    /// For PLC blocks (OB, FB, FC, DB), exports as SIMATIC ML XML.
    /// Returns null if the object type is not supported for source extraction.
    /// </summary>
    private static string? ExtractSource(IEngineeringObject obj)
    {
        try
        {
            // Check if this is a PLC block
            if (obj is PlcBlock plcBlock)
            {
                return ExportPlcBlock(plcBlock);
            }

            // Check if this is a PlcSoftware (e.g., when user selects a PLC device)
            if (obj is PlcSoftware plcSoftware)
            {
                return ExportPlcSoftwareOverview(plcSoftware);
            }

            // For other object types, try to get useful metadata
            return ExtractMetadata(obj);
        }
        catch (Exception ex)
        {
            AddInLogger.Warn($"SelectionSnapshotFactory: failed to extract source: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Exports a PLC block as SIMATIC ML XML.
    /// </summary>
    private static string? ExportPlcBlock(PlcBlock block)
    {
        try
        {
            // Create a temporary file for export
            var tempDir = Path.Combine(Path.GetTempPath(), "TiaAgent", "export");
            Directory.CreateDirectory(tempDir);

            var fileName = $"{block.Name}_{DateTime.UtcNow:yyyyMMddHHmmss}.xml";
            var tempFile = Path.Combine(tempDir, fileName);

            // Export as SIMATIC ML
            var fileInfo = new FileInfo(tempFile);
            block.Export(fileInfo, ExportOptions.None);

            // Read the exported XML
            if (File.Exists(tempFile))
            {
                var xml = File.ReadAllText(tempFile);

                // Clean up temporary file
                try { File.Delete(tempFile); } catch { }

                AddInLogger.Info($"SelectionSnapshotFactory: exported block {block.Name} to XML ({xml.Length} chars)");
                return xml;
            }

            return null;
        }
        catch (Exception ex)
        {
            AddInLogger.Warn($"SelectionSnapshotFactory: failed to export block {block.Name}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Exports an overview of a PLC software container.
    /// This is a lightweight export that doesn't require exporting all blocks.
    /// </summary>
    private static string? ExportPlcSoftwareOverview(PlcSoftware software)
    {
        try
        {
            // Get block count and list
            var blockGroup = software.BlockGroup;
            var blocks = new System.Text.StringBuilder();
            blocks.AppendLine($"PLC Software: {software.Name}");
            blocks.AppendLine($"Block Groups:");

            foreach (var group in blockGroup.Groups)
            {
                blocks.AppendLine($"  - {group.Name}");
            }

            blocks.AppendLine($"Blocks:");
            foreach (var block in blockGroup.Blocks)
            {
                blocks.AppendLine($"  - {block.Name} ({block.GetType().Name})");
            }

            return blocks.ToString();
        }
        catch (Exception ex)
        {
            AddInLogger.Warn($"SelectionSnapshotFactory: failed to export PLC software overview: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Extracts metadata from an engineering object.
    /// Used as a fallback when direct source export is not available.
    /// </summary>
    private static string ExtractMetadata(IEngineeringObject obj)
    {
        var metadata = new System.Text.StringBuilder();
        metadata.AppendLine($"Object: {obj}");
        metadata.AppendLine($"Type: {obj.GetType().Name}");
        metadata.AppendLine($"Full Type: {obj.GetType().FullName}");

        // Try to get common properties without reflection
        // Note: IEngineeringObjectWithComposition is not available in V21 API
        // We just log the basic metadata

        return metadata.ToString();
    }

    /// <summary>
    /// Gets a human-readable name for the selected object.
    /// Uses ToString() — avoids reflection on COM-interop types which triggers
    /// VerificationException in TIA Portal's partial-trust sandbox.
    /// </summary>
    private static string GetSafeName(IEngineeringObject obj)
    {
        try
        {
            return obj.ToString() ?? "Unknown";
        }
        catch (Exception ex)
        {
            AddInLogger.Warn($"Failed to get name from {obj.GetType().Name}: {ex.Message}");
            return "Unknown";
        }
    }
}
#endif
