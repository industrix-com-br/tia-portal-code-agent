using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TiaAgent.ResponseCenter.Services;

public sealed record BridgeConnectionSettings(string BridgeUrl, string? AuthToken);

/// <summary>
/// Resolves the local Bridge endpoint and bearer token from the runtime files maintained by the CLI.
/// Explicit command-line values win, but the token normally stays out of process arguments.
/// </summary>
public static class BridgeConnectionDiscovery
{
    private const string DefaultBridgeUrl = "http://127.0.0.1:43119";

    private static readonly Regex s_portRegex = new(
        "\\\"port\\\"\\s*:\\s*(?<port>[0-9]+)",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    public static BridgeConnectionSettings Resolve(
        string? explicitBridgeUrl,
        string? explicitAuthToken,
        string? installationRoot = null)
    {
        var root = installationRoot;
        if (string.IsNullOrWhiteSpace(root))
        {
            root = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TiaAgent");
        }

        var bridgeUrl = string.IsNullOrWhiteSpace(explicitBridgeUrl)
            ? DiscoverBridgeUrl(root)
            : explicitBridgeUrl.TrimEnd('/');

        var token = string.IsNullOrWhiteSpace(explicitAuthToken)
            ? DiscoverAuthToken(root)
            : explicitAuthToken;

        return new BridgeConnectionSettings(bridgeUrl, token);
    }

    internal static string DiscoverBridgeUrl(string installationRoot)
    {
        var runtimeManifestPath = Path.Combine(installationRoot, "runtime", "runtime.json");
        try
        {
            if (!File.Exists(runtimeManifestPath))
            {
                return DefaultBridgeUrl;
            }

            var manifest = File.ReadAllText(runtimeManifestPath);
            var match = s_portRegex.Match(manifest);
            if (match.Success &&
                int.TryParse(match.Groups["port"].Value, out var port) &&
                port is > 0 and <= 65535)
            {
                return $"http://127.0.0.1:{port}";
            }
        }
        catch (IOException)
        {
            // The Bridge may be rewriting the file; the default endpoint remains a safe fallback.
        }
        catch (UnauthorizedAccessException)
        {
            // Surface connection failure through the normal Response Center error state.
        }

        return DefaultBridgeUrl;
    }

    internal static string? DiscoverAuthToken(string installationRoot)
    {
        var tokenPath = Path.Combine(installationRoot, "bridge.token");
        try
        {
            if (!File.Exists(tokenPath))
            {
                return null;
            }

            var token = File.ReadAllText(tokenPath).Trim();
            return token.Length == 0 ? null : token;
        }
        catch (IOException)
        {
            return null;
        }
        catch (UnauthorizedAccessException)
        {
            return null;
        }
    }
}
