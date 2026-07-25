# Running End-to-End

This guide covers the current source-build workflow and the installed CLI workflow.

## Prerequisites

- Windows x64;
- TIA Portal V21 with Openness;
- membership in `Siemens TIA Openness`;
- .NET SDK compatible with `global.json`;
- .NET Framework 4.8 runtime;
- `TiaMcpServer` and at least one supported agent runtime.

Run these checks before troubleshooting the application:

```powershell
dotnet --version
tia-mcp doctor
opencode --version   # or mimo --version / claude --version
```

## Build from source

```powershell
cd C:\github\tia-portal-code-agent
.\build.ps1 build
.\build.ps1 test
.\build.ps1 pack
```

`pack` creates:

- `artifacts\TiaAgent-0.0.0-dev.addin` for an untagged development build;
- `artifacts\TiaAgent.Cli.0.0.0-dev.nupkg`;
- `artifacts\cli-payload\` used by the package.

Install the development Add-In directly with:

```powershell
.\build.ps1 install-dev
```

Restart TIA Portal after deployment.

## Install through the CLI

For a published package:

```powershell
dotnet tool install --global TiaAgent.Cli --prerelease
tia-agent install
```

For the payload produced by the local build:

```powershell
tia-agent install --payload-dir .\artifacts\cli-payload --force
```

## Configure the runtime

```powershell
tia-agent runtime list
tia-agent runtime use opencode --mode server
```

Examples:

```powershell
tia-agent runtime use claude --mode cli
tia-agent runtime use mimo --mode cli
tia-agent config set runtimes.claude.executable C:\tools\claude.cmd
```

The main configuration file is `%LOCALAPPDATA%\TiaAgent\config.json`. See [CONFIGURATION.md](CONFIGURATION.md) for all configuration files and [RUNTIME.md](RUNTIME.md) for runtime behavior.

## Start services

Preferred user-facing commands:

```powershell
tia-agent start
tia-agent status
```

The Runtime Supervisor always starts the Bridge. It starts an additional runtime server only when the selected runtime uses server mode.

For source-level debugging, equivalent repository scripts exist under `src\runtime\Scripts\`:

```powershell
.\src\runtime\Scripts\run.ps1
.\src\runtime\Scripts\status.ps1
.\src\runtime\Scripts\stop.ps1
```

The CLI commands should remain the primary documentation because they work independently of a repository checkout.

## Activate and use the Add-In

1. Open TIA Portal V21 and a project.
2. Open **Options > Settings > Add-Ins**.
3. Enable **TIA Portal Code Agent**.
4. Right-click a supported project object.
5. Choose an action under **AI Code Agent**:
   - **Explain selected object**;
   - **Review selected object**;
   - **Propose change**.

The current proposal workflow returns recommendations. It does not directly modify the project.

## Runtime discovery

The Add-In reads:

```text
%LOCALAPPDATA%\TiaAgent\runtime\runtime.json
```

The manifest contains the active Bridge endpoint and runtime metadata. It is discovery metadata, not proof that a service is healthy; consumers also call the health endpoint.

The default Bridge port is `43119`, but the supervisor can select another available port. Do not hard-code the default port in automation when `runtime.json` is available.

## Bridge API

The current Bridge exposes local endpoints for:

| Method | Path | Purpose |
|---|---|---|
| `GET` | `/health` | Bridge and selected-runtime health |
| `POST` | `/v1/tasks` | Create a task |
| `GET` | `/v1/tasks/{id}` | Poll task status |
| `POST` | `/v1/tasks/{id}/cancel` | Cancel a task |
| `GET` | `/api/runtimes` | List runtime availability |
| `GET` | `/api/runtimes/{id}/health` | Diagnose one runtime |
| `GET` | `/api/settings/runtime` | Read the default runtime |
| `PUT` | `/api/settings/runtime` | Change the default runtime |
| `GET` | `/diagnostics` | Bridge diagnostics |

Authenticated endpoints use the bearer token stored in `%LOCALAPPDATA%\TiaAgent\bridge.token`. Do not log, commit, or publish that token.

## Verify the system

```powershell
tia-agent doctor --verbose
tia-agent status
tia-agent runtime doctor
```

Relevant logs:

```text
%LOCALAPPDATA%\TiaAgent\logs\supervisor.log
%LOCALAPPDATA%\TiaAgent\logs\bridge.log
%LOCALAPPDATA%\TiaAgent\logs\addin-YYYYMMDD.log
```

## Stop services

```powershell
tia-agent stop
```

Use [TROUBLESHOOTING.md](TROUBLESHOOTING.md) for common failures and [CLI.md](CLI.md) for all implemented command syntax.