# Installation Guide

Install TIA Portal Code Agent through the `TiaAgent.Cli` .NET global tool.

> [!CAUTION]
> This project is experimental and is not ready for production use. Do not use it on live systems, safety programs, or workflows where an incorrect response could affect people, equipment, availability, or compliance.

## Prerequisites

| Component | Requirement | Check |
|---|---|---|
| Windows | Windows 10 or 11 x64 | System information |
| Siemens TIA Portal | V21 with Openness installed | `C:\Program Files\Siemens\Automation\Portal V21` |
| .NET SDK | .NET 8 SDK compatible with `global.json` | `dotnet --version` |
| .NET Framework | 4.8 runtime for the Add-In | Windows Features or registry |
| TiaMcpServer | Installed as a global tool | `dotnet tool list -g` |
| Openness group | Member of `Siemens TIA Openness` | `whoami /groups` |

At least one supported agent runtime must also be installed and available on `PATH`:

| Runtime | ID | Typical check |
|---|---|---|
| Mimo CLI | `mimo` | `mimo --version` |
| OpenCode | `opencode` | `opencode --version` |
| Claude Code CLI | `claude` | `claude --version` |

Install the MCP server when it is not already present:

```powershell
dotnet tool install --global TiaMcpServer
tia-mcp doctor
```

## Install the CLI

> [!IMPORTANT]
> TIA Portal Code Agent currently has only prerelease packages. The command `dotnet tool install --global TiaAgent.Cli` considers stable versions by default. Until the first stable release is published, that command can report that the package was not found in the configured NuGet feeds even though `alpha` or `beta` versions are available.

Install the latest prerelease version:

```powershell
dotnet tool install --global TiaAgent.Cli --prerelease
```

Install a specific prerelease version:

```powershell
dotnet tool install --global TiaAgent.Cli --version 0.3.0-beta.5
```

The explicit version works because it selects that prerelease package directly. The `--prerelease` option instead selects the latest available prerelease.

After a stable version is published, install the latest stable release with:

```powershell
dotnet tool install --global TiaAgent.Cli
```

Verify the command:

```powershell
tia-agent --help
tia-agent version
```

## Install the bundled payload

```powershell
tia-agent install
```

The command validates the payload embedded in the CLI package, copies it to:

```text
%LOCALAPPDATA%\TiaAgent\versions\<version>\
```

It also:

- records the installed and active versions;
- creates `%LOCALAPPDATA%\TiaAgent\config.json` when missing;
- deploys the versioned `.addin` file to `%APPDATA%\Siemens\Automation\Portal V21\UserAddIns\` when the TIA installation and directory can be resolved;
- prints the unpacked Add-In path when automatic deployment is not possible.

For a development payload:

```powershell
tia-agent install --payload-dir C:\path\to\payload
```

## Activate the Add-In

1. Close and reopen TIA Portal V21 after installation.
2. Open **Options > Settings > Add-Ins**.
3. Enable **TIA Portal Code Agent**.
4. Open a project and right-click a supported object.
5. Choose an action under **AI Code Agent**.

## Start and verify services

```powershell
tia-agent start
tia-agent doctor
tia-agent status
```

Use [CLI.md](CLI.md) for the complete command reference and [TROUBLESHOOTING.md](TROUBLESHOOTING.md) when a diagnostic fails.

## Update channel

The stored update channel can be viewed or changed with:

```powershell
tia-agent channel
tia-agent channel set stable
tia-agent channel set rc
tia-agent channel set beta
tia-agent channel set alpha
```

The channel controls which payload versions the CLI accepts during update and activation operations. It does not change how the .NET SDK resolves the NuGet package. Use `--prerelease` or `--version` when installing a prerelease CLI package.

## Uninstall

Remove the CLI global tool:

```powershell
dotnet tool uninstall --global TiaAgent.Cli
```

Remove one installed payload version:

```powershell
tia-agent uninstall --version 0.3.0-beta.5
```

Remove every installed payload version:

```powershell
tia-agent uninstall --all
```

CLI package removal and payload removal are separate operations. See [LAYOUT.md](LAYOUT.md) for the installed files and manifests.
