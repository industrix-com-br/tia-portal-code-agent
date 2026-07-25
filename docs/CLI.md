# CLI Reference

`tia-agent` is the command installed by the `TiaAgent.Cli` .NET global tool package.

```powershell
dotnet tool install --global TiaAgent.Cli
tia-agent --help
```

This reference reflects the command routing implemented in `src/TiaAgent.Cli/Program.cs`.

## Commands

| Command | Purpose |
|---|---|
| `install` | Extract, validate, install, and activate the bundled payload. |
| `activate` | Activate an already installed payload version. |
| `uninstall` | Remove one installed payload version or all versions. |
| `update` | Install or activate the payload bundled with the current CLI package. Alias: `upgrade`. |
| `rollback` | Activate a previously installed version. Alias: `downgrade`. |
| `start` | Start the Bridge and the configured runtime services. Alias: `run`. |
| `stop` | Stop runtime services. |
| `status` | Show runtime process and health status. |
| `doctor` | Validate the installation, Siemens environment, runtimes, and MCP setup. |
| `config` | View or modify `%LOCALAPPDATA%\TiaAgent\config.json`. Alias: `configuration`. |
| `channel` | View or change the update channel. |
| `runtime` | List, select, and diagnose agent runtimes. Alias: `runtimes`. |
| `version` | Show CLI and installed payload version information. |

There is no separate `tia-agent versions` command. Use `tia-agent version --verbose` to list installed payload versions.

## Installation lifecycle

```powershell
# Install the payload bundled in the current CLI package
tia-agent install

# Install a payload from a development or diagnostic directory
tia-agent install --payload-dir C:\path\to\payload

# Activate an installed version
tia-agent activate 0.3.0-beta.1

# Remove one installed version
tia-agent uninstall --version 0.3.0-beta.1

# Remove every installed payload version
tia-agent uninstall --all
```

Common options for installation commands:

- `--version <version>`
- `--payload-dir <directory>` where supported
- `--custom-root <directory>`
- `--user-addins-dir <directory>`
- `--force` or `-f`

## Runtime lifecycle

```powershell
tia-agent start
tia-agent status
tia-agent stop
```

Useful options:

```powershell
tia-agent start --no-monitor
tia-agent start --config C:\path\to\settings.json
tia-agent status --json
tia-agent stop --force
```

## Runtime selection

```powershell
tia-agent runtime list
tia-agent runtime use opencode --mode server
tia-agent runtime use claude --mode cli
tia-agent runtime doctor
tia-agent runtime doctor claude
tia-agent runtime status
```

Supported runtime IDs are `opencode`, `mimo`, and `claude`.

## Configuration

```powershell
tia-agent config list
tia-agent config get defaultRuntime
tia-agent config set defaultRuntime claude
tia-agent config set runtimes.claude.executable C:\tools\claude.cmd
tia-agent config path
tia-agent config reset
```

Supported runtime properties are:

- `enabled`
- `executable`
- `mode`
- `serverUrl`

## Update channel

```powershell
tia-agent channel
tia-agent channel show
tia-agent channel set stable
tia-agent channel set rc
tia-agent channel set beta
tia-agent channel set alpha
```

Changing to a less stable channel may require `--force`.

## Diagnostics and version information

```powershell
tia-agent doctor
tia-agent doctor --verbose
tia-agent doctor --json
tia-agent version
tia-agent version --verbose
tia-agent version --json
```

Use `tia-agent <command> --help` for the options implemented by a specific command.