---
title: TIA Portal Code Agent Architecture
document_type: architecture-reference
status: current-implementation
audience:
  - contributors
  - maintainers
  - reviewers
language: en-US
---

# Architecture

This document describes the architecture implemented by the current repository. Future capabilities belong in the product specification or an ADR and must not be presented here as shipped behavior.

## Product boundary

TIA Portal Code Agent is a local Windows application composed of:

```text
TIA Portal V21
  -> TiaAgent.AddIn (.NET Framework 4.8)
    -> loopback HTTP
      -> TiaAgent.Bridge (.NET 8)
        -> selected agent runtime
          -> TiaMcpServer / tia-mcp through stdio
            -> TIA Portal Openness

TiaAgent.Cli (.NET 8)
  -> installs and activates versioned payloads
  -> deploys the Add-In
  -> starts, stops, and diagnoses runtime services
```

The repository does not contain a second MCP or Openness host. TIA project access beyond the selection snapshot is delegated to the external `TiaMcpServer` integration.

## Projects in the solution

| Project | Responsibility | Target |
|---|---|---|
| `TiaAgent.AddIn` | TIA provider, selected-object snapshot, Bridge client, WPF result UI, logging | `net48` |
| `TiaAgent.Application` | Application abstractions and reusable policies | `netstandard2.0` |
| `TiaAgent.Contracts` | DTOs, task contracts, runtime contracts, configuration models, errors | `netstandard2.0` |
| `TiaAgent.Bridge` | Local HTTP API, task lifecycle, authentication, runtime adapters | `net8.0` |
| `TiaAgent.Cli` | Payload installation, activation, rollback, diagnostics, runtime supervision | `net8.0` |

## Add-In flow

1. TIA Portal loads `TiaAgent.AddIn` from the installed `.addin` package.
2. `ProjectTreeProvider` registers the **AI Code Agent** context menu.
3. The selected `IEngineeringObject` is resolved only for the current operation.
4. `SelectionSnapshotFactory` creates a serializable snapshot, including source content when supported.
5. The Add-In creates a `BridgeTaskRequest` with a correlation ID and action profile.
6. The Bridge accepts the task and the Add-In polls its status.
7. The result is displayed in a WPF window; a MessageBox is the fallback when WPF creation fails.

Current actions:

| Menu action | Action ID | Agent profile |
|---|---|---|
| Explain selected object | `explain` | `tia-explain` |
| Review selected object | `review` | `tia-review` |
| Propose change | `propose` | `tia-change` |

The proposal action requests recommendations. The Add-In does not implement approval or application of a change set.

## Bridge responsibilities

The Bridge:

- binds to loopback;
- validates bearer authentication for protected endpoints;
- creates, tracks, polls, and cancels tasks;
- resolves the selected runtime;
- invokes CLI-mode runtimes as child processes;
- communicates with OpenCode over HTTP in server mode;
- publishes structured status, diagnostics, and errors;
- preserves correlation and runtime metadata.

The Bridge must not receive or retain live Siemens engineering objects.

## Runtime selection

Supported runtime IDs:

- `opencode` — server or CLI mode;
- `mimo` — CLI mode;
- `claude` — CLI mode.

Selection precedence:

1. task request override;
2. `TIA_AGENT_RUNTIME`;
3. `%LOCALAPPDATA%\TiaAgent\config.json`;
4. `opencode`.

There is no silent fallback.

## Runtime Supervisor

The supervisor starts the Bridge and, when required, a server-mode runtime. It writes discovery state to:

```text
%LOCALAPPDATA%\TiaAgent\runtime\runtime.json
```

The manifest is not a health guarantee. The Add-In and CLI validate the advertised endpoint.

The Bridge defaults to port `43119`, but the supervisor may allocate another available port. Code and documentation should use runtime discovery rather than assuming a fixed port.

## Installation architecture

The NuGet global tool package ID is `TiaAgent.Cli`. It contains a payload under:

```text
tools/net8.0/any/payload/
```

The payload contains:

- published Bridge files;
- the versioned `.addin` package;
- configuration templates;
- notices and license files;
- `payload-manifest.json` with hashes and product metadata.

`tia-agent install` copies the complete payload to `%LOCALAPPDATA%\TiaAgent\versions\<version>\`, records installation manifests, creates a default configuration when needed, and deploys the `.addin` file to the TIA Portal V21 UserAddIns directory when available.

## Versioning and release

All first-party components share one product version. The Git tag is the public source of truth. `build.ps1` passes the resolved version to MSBuild and creates:

- `TiaAgent-<version>.addin`;
- `TiaAgent.Cli.<version>.nupkg`.

The tag workflow in `.github/workflows/pipeline.yml` runs the release build on the V21 self-hosted runner, publishes NuGet, and creates a GitHub Release.

## Safety invariants

- Keep services on loopback.
- Treat project content as untrusted model input.
- Never pass live Siemens objects across threads or process boundaries.
- Do not log credentials, bearer tokens, or unnecessary source payloads.
- Propagate cancellation and timeouts.
- Return structured errors and correlation IDs.
- Do not document PLC download, online control, safety changes, hardware/network changes, or direct project writes as supported behavior.

## Dependency rules

- `TiaAgent.AddIn` references `TiaAgent.Contracts`; it does not reference `TiaAgent.Application`.
- `TiaAgent.Application` references `TiaAgent.Contracts` and remains independent of the Add-In host.
- Bridge and CLI may use .NET 8 libraries unavailable to the Add-In.
- Siemens assemblies are resolved from the installed V21 Public API and must not be copied into source control or the NuGet payload.
- Agent runtimes and `TiaMcpServer` are external prerequisites, not bundled executables.

## Validation sources

When this document conflicts with implementation, verify against:

- `TiaAgent.sln` and project files;
- `src/TiaAgent.AddIn/Providers/ProjectTreeProvider.cs`;
- `src/TiaAgent.Bridge/Program.cs` and runtime adapters;
- `src/TiaAgent.Cli/Program.cs` and command implementations;
- `build.ps1`;
- `.github/workflows/pipeline.yml`;
- tests covering architecture, payload bundling, commands, and runtimes.

Architectural changes should update this file and, when they introduce a durable design decision, add an ADR under `docs/adr/`.