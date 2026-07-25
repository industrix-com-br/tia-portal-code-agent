# AGENTS.md

## Repository purpose

TIA Portal Code Agent is a Siemens TIA Portal V21 Add-In that sends selected engineering context to a local Bridge, an interchangeable coding-agent runtime, and the external `TiaMcpServer` MCP integration.

## Non-negotiable constraints

- **TIA target:** TIA Portal V21 and the V21 modular Public API assemblies.
- **Framework split:** `TiaAgent.AddIn` targets .NET Framework 4.8; `TiaAgent.Bridge` and `TiaAgent.Cli` target .NET 8. Do not retarget the Add-In to modern .NET.
- **Architecture:** Windows x64 only.
- **Assembly model:** do not reference the removed monolithic `Siemens.Engineering.AddIn.dll` or the old `PublicAPI\V21.AddIn` path.
- **MCP server:** use [Czarnak/tia-portal-mcp](https://github.com/Czarnak/tia-portal-mcp) through `TiaMcpServer`; do not duplicate TIA access inside this repository.
- **Current product workflow:** read-only explanations, reviews, and change proposals. No direct project writes, PLC download, safety changes, or hardware/network changes.
- **Siemens binaries:** never commit or redistribute Siemens assemblies.
- **CI and release:** never use `--skipEngMemberCheck`.
- **User Add-Ins directory:** `%APPDATA%\Siemens\Automation\Portal V21\UserAddIns`.

## Current topology

```text
TIA Portal V21
  -> TiaAgent.AddIn (net48, context capture and WPF UI)
    -> loopback HTTP
      -> TiaAgent.Bridge (net8.0)
        -> Mimo CLI, OpenCode, or Claude Code CLI
          -> TiaMcpServer / tia-mcp (stdio MCP)
            -> TIA Portal Openness

TiaAgent.Cli (net8.0)
  -> installs versioned payloads
  -> deploys the Add-In
  -> starts, stops, and diagnoses runtime services
```

Repository projects:

- `TiaAgent.AddIn` — TIA Portal provider, context extraction, Bridge client, and result UI.
- `TiaAgent.Application` — application-level abstractions and policies.
- `TiaAgent.Contracts` — DTOs, runtime contracts, errors, and shared schemas.
- `TiaAgent.Bridge` — local HTTP API, tasks, authentication, and runtime adapters.
- `TiaAgent.Cli` — installation, version activation, update, rollback, diagnostics, and runtime supervision.

## Supported runtimes

| Runtime | ID | Supported mode |
|---|---|---|
| Mimo CLI | `mimo` | CLI |
| OpenCode | `opencode` | Server or CLI |
| Claude Code CLI | `claude` | CLI |

Runtime selection precedence:

1. task request override;
2. `TIA_AGENT_RUNTIME` environment variable;
3. `%LOCALAPPDATA%\TiaAgent\config.json`;
4. default `opencode`.

There is no silent runtime fallback.

## Runtime lifecycle

```powershell
tia-agent start
tia-agent status
tia-agent stop
tia-agent runtime doctor
```

Repository-local scripts under `src/runtime/Scripts/` are development entry points. User documentation should prefer the CLI commands.

## Current Add-In actions

- `Explain selected object` -> `tia-explain`
- `Review selected object` -> `tia-review`
- `Propose change` -> `tia-change`

The Add-In's proposal request asks the runtime to propose improvements. It does not implement an approval or apply workflow. Do not document direct writes as supported product behavior.

## Working in this repository

- Build: `.\build.ps1 build`
- Test: `.\build.ps1 test`
- Package: `.\build.ps1 pack`
- Development install: `.\build.ps1 install-dev`
- Release validation: `.\build.ps1 release -Version X.Y.Z[-alpha.N|-beta.N|-rc.N]`

Additional rules:

- keep engineering objects local to the operation that resolves them;
- never cache or transfer live `IEngineeringObject` instances;
- propagate `CancellationToken` through long-running operations;
- use structured errors and a `correlationId` for every task;
- keep services on loopback;
- update documentation whenever a command, package path, configuration key, workflow, or supported feature changes.

See [docs/CLI.md](docs/CLI.md), [docs/spec/ARCHITECTURE.md](docs/spec/ARCHITECTURE.md), and [docs/spec/SECURITY_MODEL.md](docs/spec/SECURITY_MODEL.md).