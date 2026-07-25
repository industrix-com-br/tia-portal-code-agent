using System;
using System.Collections.Generic;
using System.IO;

namespace TiaAgent.Bridge.Runtime;

/// <summary>
/// Identifies the process wrapper used to execute the target script.
/// </summary>
internal enum ProcessWrapper
{
    None,
    Cmd,
    PowerShellCore,
    WindowsPowerShell
}

/// <summary>
/// Represents the resolved command to execute, including the host executable,
/// composed arguments, wrapper type, and the resolved target path.
/// </summary>
internal sealed class ResolvedCommand
{
    public string FileName { get; init; } = "";
    public IReadOnlyList<string> Arguments { get; init; } = Array.Empty<string>();
    public ProcessWrapper Wrapper { get; init; }
    public string ResolvedTargetPath { get; init; } = "";

    /// <summary>
    /// Composes the full argument string by joining wrapper arguments, target, and extra arguments.
    /// The prompt is NOT included here — it is always transported via stdin.
    /// </summary>
    public string ComposeArguments(string extraArguments)
    {
        var parts = new List<string>();
        parts.AddRange(Arguments);
        if (!string.IsNullOrEmpty(extraArguments))
            parts.Add(extraArguments);
        return string.Join(" ", parts);
    }
}

/// <summary>
/// Resolves executables with a priority order that avoids PowerShell to preserve
/// UTF-8 output. PowerShell 5.x reads child-process stdout using the OEM code
/// page (CP437), corrupting multi-byte UTF-8 sequences.
///
/// Resolution order for bare names (e.g. "claude"):
///   1. claude.exe  — execute directly
///   2. claude.cmd  — execute through cmd.exe
///   3. claude.bat   — execute through cmd.exe
///   4. claude.ps1   — execute through pwsh.exe (PowerShell Core)
///   5. claude.ps1   — execute through powershell.exe (last fallback)
///   6. bare name    — let the OS resolve
///
/// For explicit paths/extensions:
///   .exe  -> execute directly
///   .cmd/.bat -> execute through cmd.exe
///   .ps1  -> pwsh.exe first, then powershell.exe
/// </summary>
internal static class CommandResolver
{
    /// <summary>
    /// Resolves the executable for process start.
    /// </summary>
    /// <param name="executable">The requested executable name or path.</param>
    /// <param name="findOnPath">Optional function to search PATH. Uses RuntimeHelpers.FindOnPath when null.</param>
    internal static ResolvedCommand Resolve(string executable, Func<string, string?>? findOnPath = null)
    {
        var finder = findOnPath ?? RuntimeHelpers.FindOnPath;
        var isBareName = !executable.Contains('.')
                         && !executable.Contains(Path.DirectorySeparatorChar)
                         && !executable.Contains(Path.AltDirectorySeparatorChar);

        if (!isBareName)
            return ResolveExplicit(executable, finder);

        return ResolveBareName(executable, finder);
    }

    private static ResolvedCommand ResolveExplicit(string executable, Func<string, string?> findOnPath)
    {
        var ext = Path.GetExtension(executable).ToLowerInvariant();

        return ext switch
        {
            ".exe" => new ResolvedCommand
            {
                FileName = executable,
                Wrapper = ProcessWrapper.None,
                ResolvedTargetPath = executable
            },
            ".cmd" or ".bat" => new ResolvedCommand
            {
                FileName = "cmd.exe",
                Arguments = new[] { "/d", "/s", "/c", $"\"{executable}\"" },
                Wrapper = ProcessWrapper.Cmd,
                ResolvedTargetPath = executable
            },
            ".ps1" => ResolvePs1Explicit(executable, findOnPath),
            _ => new ResolvedCommand
            {
                FileName = executable,
                Wrapper = ProcessWrapper.None,
                ResolvedTargetPath = executable
            }
        };
    }

    private static ResolvedCommand ResolvePs1Explicit(string ps1Path, Func<string, string?> findOnPath)
    {
        var pwshPath = findOnPath("pwsh.exe");
        if (pwshPath != null)
            return WrapPowerShell(ps1Path, pwshPath, ProcessWrapper.PowerShellCore);

        var powershellPath = findOnPath("powershell.exe");
        if (powershellPath != null)
            return WrapPowerShell(ps1Path, powershellPath, ProcessWrapper.WindowsPowerShell);

        return new ResolvedCommand
        {
            FileName = "powershell.exe",
            Arguments = new[] { "-NoProfile", "-ExecutionPolicy", "Bypass", "-Command", $"\"& '{ps1Path}' @args\"" },
            Wrapper = ProcessWrapper.WindowsPowerShell,
            ResolvedTargetPath = ps1Path
        };
    }

    private static ResolvedCommand ResolveBareName(string name, Func<string, string?> findOnPath)
    {
        // 1. Direct native executable
        var exePath = findOnPath(name + ".exe");
        if (exePath != null)
            return new ResolvedCommand
            {
                FileName = exePath,
                Wrapper = ProcessWrapper.None,
                ResolvedTargetPath = exePath
            };

        // 2. Batch script via cmd.exe
        var cmdPath = findOnPath(name + ".cmd");
        if (cmdPath != null)
            return new ResolvedCommand
            {
                FileName = "cmd.exe",
                Arguments = new[] { "/d", "/s", "/c", $"\"{cmdPath}\"" },
                Wrapper = ProcessWrapper.Cmd,
                ResolvedTargetPath = cmdPath
            };

        var batPath = findOnPath(name + ".bat");
        if (batPath != null)
            return new ResolvedCommand
            {
                FileName = "cmd.exe",
                Arguments = new[] { "/d", "/s", "/c", $"\"{batPath}\"" },
                Wrapper = ProcessWrapper.Cmd,
                ResolvedTargetPath = batPath
            };

        // 3. PowerShell Core (pwsh.exe) — better Unicode support than Windows PowerShell
        var ps1Path = findOnPath(name + ".ps1");
        if (ps1Path != null)
        {
            var pwshPath = findOnPath("pwsh.exe");
            if (pwshPath != null)
                return WrapPowerShell(ps1Path, pwshPath, ProcessWrapper.PowerShellCore);

            // 4. Windows PowerShell (powershell.exe) — last resort
            var powershellPath = findOnPath("powershell.exe");
            if (powershellPath != null)
                return WrapPowerShell(ps1Path, powershellPath, ProcessWrapper.WindowsPowerShell);
        }

        // 5. Nothing found — return bare name (OS will attempt resolution)
        return new ResolvedCommand
        {
            FileName = name,
            Wrapper = ProcessWrapper.None,
            ResolvedTargetPath = name
        };
    }

    private static ResolvedCommand WrapPowerShell(string ps1Path, string hostExecutable, ProcessWrapper wrapper)
    {
        var setupCmd = "[Console]::InputEncoding=[Console]::OutputEncoding=$OutputEncoding=[System.Text.UTF8Encoding]::new()";
        var cmd = $"{setupCmd}; & '{ps1Path}' @args";
        var args = new[] { "-NoProfile", "-ExecutionPolicy", "Bypass", "-Command", $"\"{cmd}\"" };

        return new ResolvedCommand
        {
            FileName = hostExecutable,
            Arguments = args,
            Wrapper = wrapper,
            ResolvedTargetPath = ps1Path
        };
    }
}
