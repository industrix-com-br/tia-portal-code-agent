using System;

namespace TiaAgent.Cli.Release;

/// <summary>
/// Maps a product version to its update channel.
/// </summary>
public static class ReleaseStore
{
    public static string ResolveChannel(string? version)
    {
        if (string.IsNullOrWhiteSpace(version) || version.EndsWith("-dev", StringComparison.OrdinalIgnoreCase))
        {
            return "dev";
        }

        if (version.Contains("-alpha.", StringComparison.OrdinalIgnoreCase)) return "alpha";
        if (version.Contains("-beta.", StringComparison.OrdinalIgnoreCase)) return "beta";
        if (version.Contains("-rc.", StringComparison.OrdinalIgnoreCase)) return "rc";
        return "stable";
    }
}
