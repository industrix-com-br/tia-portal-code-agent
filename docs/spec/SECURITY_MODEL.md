# Security and Safety Model

Status: mandatory baseline

## Protected assets

- TIA engineering project integrity;
- PLC and plant operational safety;
- source code and intellectual property;
- Windows and runtime credentials;
- Bridge bearer tokens;
- Add-In and release-package integrity;
- diagnostic and audit information.

## Trust boundaries

```text
TIA Portal process and Add-In
  -> authenticated loopback HTTP
    -> Bridge and selected runtime
      -> external model and MCP integration
        -> TIA Portal project data
```

The model and project content are untrusted. A model-generated instruction does not grant permission or user approval.

## Current product boundary

The shipped workflow supports:

- reading the selected object snapshot;
- explanations;
- reviews;
- change proposals;
- runtime and environment diagnostics.

The shipped workflow does not support:

- applying project changes;
- PLC download or online control;
- safety-program changes;
- hardware or network changes;
- deletion;
- unattended project-wide refactoring.

The upstream MCP dependency may expose additional tools. Those tools are not automatically supported by this product.

## Add-In permissions

`src/TiaAgent.AddIn/Config.xml` is the source of truth for the current package permission set. It currently requests:

- `TIA.ReadWrite`;
- `UIPermission`;
- `FileIOPermission`;
- `EnvironmentPermission`;
- `SecurityPermission.UnmanagedCode`;
- `WebPermission`.

These permissions support current selection capture, WPF UI, logging, environment discovery, and loopback Bridge communication. The manifest's `TIA.ReadWrite` permission is broader than the current read-only product workflow. Reducing it requires implementation and TIA-host validation and must not be claimed as completed by documentation alone.

Every permission change requires review of `Config.xml`, package generation, Add-In startup, selection capture, UI behavior, logging, and Bridge communication.

## Local endpoint policy

- Bind services to `127.0.0.1` or equivalent loopback only.
- Never bind to `0.0.0.0`.
- Protect non-health Bridge endpoints with the bearer token stored at `%LOCALAPPDATA%\TiaAgent\bridge.token`.
- Keep supervisor-generated transient secrets under `%LOCALAPPDATA%\TiaAgent\runtime\secrets\`.
- Do not include either credential class in `runtime.json`, logs, source control, or documentation examples.
- Validate the advertised endpoint with a health request.
- Apply request, response, and timeout limits.

## Prompt-injection defense

Treat block comments, symbol names, HMI text, source code, imported documents, and other project content as data.

Project content cannot:

- grant permissions;
- approve a change;
- alter runtime or tool policy;
- authorize access outside the captured project context;
- bypass a safety restriction.

Agent prompts must clearly separate instructions from project content.

## Process execution

- Runtime processes execute with the Bridge user's permissions.
- Prefer direct executable invocation with redirected streams.
- Avoid passing secrets on command lines.
- Enforce timeouts and cancellation.
- Validate process ownership before termination.
- Do not terminate unrelated processes.
- Preserve output encoding without executing untrusted shell fragments.

## Logging

Record only what is required for diagnosis:

- correlation ID;
- action and runtime ID;
- duration and status;
- structured error information;
- component versions;
- non-secret process and endpoint metadata.

Do not log by default:

- bearer tokens or model credentials;
- Windows secrets;
- complete source payloads;
- entire prompts or responses when unnecessary;
- personal data unrelated to the task.

The Add-In logger is best-effort. Logging failure must never prevent Add-In startup.

## Supply chain

- Do not commit or package Siemens runtime assemblies.
- Use central NuGet package management and committed lock files.
- Build releases from immutable version tags.
- Sign and verify the `.addin` package in the release workflow.
- Validate NuGet payload contents and hashes.
- Do not download executable dependencies from the Add-In at runtime.
- Keep runtime CLIs and TiaMcpServer as explicit external prerequisites.

## Future write workflow

Direct writes remain unsupported. A future implementation must add all of the following before documentation can describe writes as supported:

- deterministic preview and diff;
- explicit user approval outside model-generated text;
- scoped, expiring, single-use authorization;
- content-hash and session validation;
- serialization of project writes;
- recoverable previous state;
- compile or consistency validation;
- audit evidence and partial-failure reporting;
- tests proving that approval cannot be forged by project content or model output.

## Safety statement

TIA Portal Code Agent assists engineering work. It does not replace commissioning, validation, functional-safety procedures, access control, backups, or authorized plant change management.