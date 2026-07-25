# Product Specification

Status: active MVP specification  
Product name: TIA Portal Code Agent  
Current target: Windows x64, Siemens TIA Portal V21, Openness V21

## Product statement

TIA Portal Code Agent adds contextual AI-assisted analysis to TIA Portal V21. The Add-In captures the selected engineering object, sends a serializable snapshot to a local Bridge and configured agent runtime, and displays the response inside TIA Portal.

The external `TiaMcpServer` integration provides additional TIA Portal context through MCP. This repository does not implement a second Openness or MCP server.

## Users

### Automation engineer

Needs explanations, code review, dependency reasoning, diagnostics, and practical improvement proposals without leaving TIA Portal.

### Contributor

Needs predictable contracts, safe TIA-host integration, reproducible packaging, and clear component boundaries.

### Reviewer or engineering lead

Needs traceable responses, explicit assumptions and risks, and confidence that the MVP did not modify the project.

## Current use cases

### Explain selected object

The user right-clicks a supported object and chooses **AI Code Agent > Explain selected object**. The Add-In captures available metadata and source content, then displays an explanation.

### Review selected object

The user requests a review. The response identifies defects, risks, assumptions, and improvements without changing the project.

### Propose change

The user requests improvement suggestions. The response describes a proposed change, but the current Add-In does not preview or apply a change set.

## Implemented product components

- TIA Portal V21 Add-In;
- WPF response window with MessageBox fallback;
- local authenticated Bridge API;
- Mimo, OpenCode, and Claude Code runtime adapters;
- Runtime Supervisor and service discovery;
- `TiaAgent.Cli` global tool;
- versioned payload installation, activation, update, rollback, diagnostics, and removal;
- packaging and release through one product version and one NuGet package.

## Functional requirements

### Context and selection

- The Add-In exposes actions from the project-tree context menu.
- The initiating selection is captured once for the task.
- Live Siemens objects do not cross process or contract boundaries.
- Unsupported source extraction is reported without crashing TIA Portal.

### Task execution

- Every task has a correlation ID.
- Runtime execution occurs outside the TIA UI thread.
- The Add-In can poll status and display completion, failure, cancellation, or timeout.
- Runtime selection follows request, environment, configuration, then default precedence.
- Runtime failure does not silently select another runtime.

### Installation

- The CLI package ID is `TiaAgent.Cli`.
- The NuGet package contains the Bridge, Add-In, configuration templates, notices, and payload manifest.
- Payload hashes and required files are validated before installation.
- Installed payloads are stored side-by-side under `%LOCALAPPDATA%\TiaAgent\versions\`.
- The Add-In is deployed to the TIA Portal V21 UserAddIns directory when available.

### Diagnostics

- The CLI exposes environment, version, configuration, runtime, and service diagnostics.
- Add-In logging is best-effort and cannot prevent Add-In startup.
- Logs and errors preserve correlation context without exposing credentials.

## Non-functional requirements

- Do not block the TIA Portal UI thread with agent or HTTP work.
- Keep local HTTP services on loopback.
- Propagate cancellation and bounded timeouts.
- Use structured errors at component boundaries.
- Treat project content as untrusted model input.
- Exclude Siemens binaries from source control and release payloads.
- Keep the Add-In compatible with the .NET Framework 4.8 TIA host.
- Keep Bridge and CLI code on .NET 8.

## MVP safety boundary

The current product workflow is read-only.

Unsupported product behavior includes:

- direct project writes;
- PLC download or online control;
- safety-program modification;
- hardware or network configuration changes;
- object deletion;
- unattended project-wide refactoring;
- non-loopback service exposure.

The external MCP dependency may contain write tools, and the current Add-In manifest requests `TIA.ReadWrite`. Neither fact means that direct writes are a supported TIA Portal Code Agent workflow.

## Future capability: approved writes

A future write workflow would require, at minimum:

- deterministic preview and diff;
- explicit user approval outside model text;
- scoped and expiring authorization;
- optimistic concurrency validation;
- recoverable previous state;
- compile or consistency validation;
- audit evidence and partial-failure handling;
- a separate security and host-permission review.

These are future requirements, not implemented MVP behavior.

## MVP acceptance criteria

- TIA Portal V21 loads the packaged Add-In.
- The **AI Code Agent** context menu exposes explain, review, and proposal actions.
- Supported selections produce serializable snapshots.
- The Add-In communicates with a healthy local Bridge.
- At least one configured runtime can complete a task through `TiaMcpServer`.
- The response is displayed without freezing TIA Portal.
- Failures are recoverable and diagnosable.
- No project write or online operation occurs.
- Build, test, package, install, activation, and runtime startup are documented with commands that exist in the repository.

## Release acceptance criteria

- One tag supplies one product version to all first-party components.
- `build.ps1 release -Version <version>` succeeds on the V21 release runner.
- The `.addin` and `.nupkg` artifacts use the same product version.
- The NuGet package installs as the `TiaAgent.Cli` global tool.
- The package contains the validated payload and no Siemens runtime assemblies.
- The GitHub release and NuGet publication are created by `.github/workflows/pipeline.yml`.