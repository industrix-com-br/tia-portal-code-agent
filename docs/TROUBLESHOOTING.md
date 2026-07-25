# Troubleshooting Guide

Use the CLI diagnostics first, then inspect the component logs associated with the failure.

> [!CAUTION]
> This project is experimental and is not ready for production use.

## Diagnostic commands

```powershell
tia-agent doctor
tia-agent doctor --verbose
tia-agent status
tia-agent runtime doctor
tia-agent runtime doctor claude
tia-agent version --verbose
```

Use `tia-agent version --verbose` to list installed payload versions. There is no separate `tia-agent versions` command.

## Log locations

| Component | Path |
|---|---|
| Add-In | `%LOCALAPPDATA%\TiaAgent\logs\addin-YYYYMMDD.log` |
| Bridge | `%LOCALAPPDATA%\TiaAgent\logs\bridge.log` |
| Runtime Supervisor | `%LOCALAPPDATA%\TiaAgent\logs\supervisor.log` |
| OpenCode server, when used | `%LOCALAPPDATA%\TiaAgent\logs\opencode.log` |

Examples:

```powershell
Get-Content "$env:LOCALAPPDATA\TiaAgent\logs\addin-$(Get-Date -Format yyyyMMdd).log" -Tail 100
Get-Content "$env:LOCALAPPDATA\TiaAgent\logs\bridge.log" -Tail 100
Get-Content "$env:LOCALAPPDATA\TiaAgent\logs\supervisor.log" -Tail 100
```

## Payload validation failed

The CLI could not validate the payload manifest, hashes, or required files.

```powershell
tia-agent doctor --verbose
tia-agent install --force
```

When the bundled payload itself is incorrect, reinstall the CLI package:

```powershell
dotnet tool uninstall --global TiaAgent.Cli
dotnet tool install --global TiaAgent.Cli
tia-agent install
```

## Requested version is not installed

Inspect the installed payloads:

```powershell
tia-agent version --verbose
```

Install the CLI package containing the required version, then install its payload:

```powershell
dotnet tool update --global TiaAgent.Cli --version <version>
tia-agent install
```

## No previous version is available

Rollback requires at least two installed payload versions or a valid `previousVersion` entry. Install the desired CLI package and payload before retrying rollback.

## Add-In was not deployed automatically

`tia-agent install` reports the unpacked Add-In path when TIA Portal V21 or its UserAddIns directory cannot be resolved.

Check:

```powershell
tia-agent doctor --verbose
```

The normal deployment directory is:

```text
%APPDATA%\Siemens\Automation\Portal V21\UserAddIns\
```

A custom directory can be supplied during installation:

```powershell
tia-agent install --user-addins-dir C:\path\to\UserAddIns
```

TIA Portal discovery checks, in order:

1. `--user-addins-dir`;
2. the `TiaPublicApiDir` environment variable;
3. `C:\Program Files\Siemens\Automation\Portal V21`;
4. an existing UserAddIns directory under `%APPDATA%`.

Restart TIA Portal after deployment.

## Add-In does not appear in TIA Portal

1. Confirm a `.addin` file exists under the UserAddIns directory.
2. Restart TIA Portal completely.
3. Open **Options > Settings > Add-Ins** and enable **TIA Portal Code Agent**.
4. Inspect the dated Add-In log.
5. Run `tia-agent doctor --verbose`.

## WPF window falls back to MessageBox

The Add-In attempts to create its WPF result window and falls back to a MessageBox when UI creation fails.

Search the dated Add-In log for WPF and fallback messages:

```powershell
Get-Content "$env:LOCALAPPDATA\TiaAgent\logs\addin-$(Get-Date -Format yyyyMMdd).log" |
  Select-String "WPF|MessageBox|fallback"
```

Common causes include an old Add-In artifact, TIA Portal not being restarted after deployment, missing package permissions, or an assembly-loading failure.

## Bridge is not running

```powershell
tia-agent start
tia-agent status
```

The default Bridge port is `43119`, but the supervisor can allocate another port from its configured range. The Add-In discovers the active port through `%LOCALAPPDATA%\TiaAgent\runtime\runtime.json`; do not assume that the default port is always active.

## Port conflict

```powershell
netstat -ano | Select-String ":43119"
```

Stop the conflicting process or restart the supervisor so it can allocate an available port.

## Runtime is unknown or unavailable

```powershell
tia-agent runtime list
tia-agent runtime doctor
tia-agent runtime use claude --mode cli
```

Supported runtime IDs are `opencode`, `mimo`, and `claude`. An executable path can be configured explicitly:

```powershell
tia-agent config set runtimes.claude.executable C:\tools\claude.cmd
```

The Bridge does not silently switch to another runtime when the selected runtime fails.

## OpenCode server is unhealthy

When OpenCode uses server mode:

```powershell
opencode --version
netstat -ano | Select-String ":43120"
Get-Content "$env:LOCALAPPDATA\TiaAgent\logs\opencode.log" -Tail 100
```

Switch to CLI mode only when the installed OpenCode version supports it:

```powershell
tia-agent runtime use opencode --mode cli
```

## TiaMcpServer cannot access TIA Portal

```powershell
tia-mcp doctor
```

Check that:

- TIA Portal V21 is installed and running;
- a project is open;
- the Windows user belongs to `Siemens TIA Openness`;
- the configured Public API path belongs to V21.

## Configuration recovery

```powershell
tia-agent config path
tia-agent config list
tia-agent config reset
```

`config reset` restores the default runtime configuration. It does not remove installed payload versions.

## Manifest corruption

The installation state is stored in `current.json` and `installations.json` under `%LOCALAPPDATA%\TiaAgent`.

```powershell
tia-agent doctor --verbose
tia-agent install --force
```

See [LAYOUT.md](LAYOUT.md) for the complete installed layout and [CLI.md](CLI.md) for verified command syntax.