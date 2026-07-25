# Known Unknowns and Validation Queue

Status: open

This file records questions that cannot be answered safely from repository code alone. Resolved implementation facts belong in the maintained guides and specifications, not in this queue.

## Confirmed baseline

The following items are no longer unknown:

- the only current TIA target is V21;
- the Add-In targets .NET Framework 4.8;
- the Bridge and CLI target .NET 8;
- the Add-In uses the V21 modular Public API assemblies;
- the context menu is implemented through `ProjectTreeAddInProvider`;
- results use a WPF window with MessageBox fallback;
- Mimo, OpenCode, and Claude Code are registered runtimes;
- `TiaMcpServer` is the external MCP integration;
- the CLI package ID is `TiaAgent.Cli`;
- the current product workflow is read-only.

## KU-001 — Exact V21 validation environment

Record the exact TIA Portal V21 update/build, Openness assembly versions, Add-In Publisher version, Windows version, and installed engineering licenses used for each public release.

Evidence:

- `tia-agent doctor --verbose` output;
- file versions from the installed V21 Public API and Publisher;
- release-runner inventory;
- manual validation record.

## KU-002 — Supported selection types

Determine the exact Siemens object types for which selection snapshot and source extraction work reliably.

Evidence:

- automated tests where possible;
- a V21 sample-project matrix;
- manual context-menu and source-extraction results.

## KU-003 — Source extraction coverage

Document which block languages and object types can be exported or read by `SelectionSnapshotFactory`, including protected blocks and unsupported objects.

Evidence:

- source-extraction tests;
- representative V21 projects;
- logs showing format and failure behavior.

## KU-004 — UI-host behavior

Validate WPF behavior across supported V21 editions and workstation policies, including dispatcher ownership, focus, modality, scaling, and MessageBox fallback.

Evidence:

- manual UI test matrix;
- dated Add-In logs;
- screenshots or recordings when relevant.

## KU-005 — Runtime compatibility versions

The repository registers runtime IDs and generic minimum-version metadata, but it does not establish a release-tested compatibility matrix for Mimo, OpenCode, or Claude Code.

Evidence:

- exact installed runtime versions;
- adapter command/output tests;
- server-mode health and task tests for OpenCode;
- CLI encoding tests on supported Windows shells.

## KU-006 — TiaMcpServer compatibility

Determine the exact TiaMcpServer version tested with each product release and the corresponding TIA Portal V21 environment.

Evidence:

- `dotnet tool list -g`;
- `tia-mcp doctor`;
- successful end-to-end task evidence;
- upstream release notes when compatibility changes.

Do not document an arbitrary minimum version until the release process enforces or records it.

## KU-007 — Add-In permission reduction

The current `Config.xml` requests `TIA.ReadWrite` while the product workflow is read-only. Determine whether V21 selection capture, source extraction, WPF UI, logging, and Bridge communication remain fully functional with `TIA.ReadOnly`.

This requires a code/package change and host validation, not a documentation-only edit.

Evidence:

- package built with the reduced permission;
- Add-In load and activation;
- action execution for supported selections;
- source extraction;
- WPF and fallback behavior;
- no security exceptions in logs.

## KU-008 — Installation policies

Determine whether target enterprise workstations permit:

- per-user `.NET` global tools;
- writes to `%LOCALAPPDATA%\TiaAgent`;
- writes to the V21 UserAddIns directory;
- execution of external runtime CLIs;
- loopback HTTP communication;
- the current Add-In permission set and signing chain.

Evidence must come from the target workstation policy or deployment owner.

## KU-009 — Release compatibility matrix

Each public release should record:

- product version and channel;
- Windows version;
- TIA Portal V21 update/build;
- Openness and Publisher versions;
- TiaMcpServer version;
- tested runtime and version;
- tested object/language coverage;
- known limitations.

The current workflow publishes artifacts but does not automatically generate this matrix.

## KU-010 — Future write safety

Direct writes are not currently supported. Before any write capability is implemented, validate object-specific preview, concurrency, recovery, compilation, approval, and audit behavior.

No roadmap or upstream MCP capability may be treated as evidence that this product safely supports writes.

## Validation rule

When resolving an item:

1. attach repeatable evidence;
2. record exact versions and environment;
3. update the maintained user and technical documentation;
4. add or update automated tests where possible;
5. create an ADR when the result changes a durable architectural decision.