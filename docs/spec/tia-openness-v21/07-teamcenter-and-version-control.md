# TIA Portal V21 Teamcenter Gateway and version-control APIs

## Scope

`Siemens.Engineering.TeamcenterGateway` exposes integration with Teamcenter for TIA projects and global libraries. The V21 Add-In API also contains version-control integration points.

This file is an API reference. TIA Portal Code Agent does not currently implement Teamcenter search, download, locking, check-in, revision management, metadata changes, or VCI workflows.

## Teamcenter Gateway surface

Important types include:

- `TeamcenterConnectionProvider`;
- `TcGatewayConnectionInfo`;
- `TcGatewaySearchAndDownloadProvider`;
- `TcGatewayWorkflowProvider`;
- `TcGatewayLockProvider`;
- item, revision, property, dataset, cache, and search-result models;
- `TcGatewayException` and error callbacks.

Dataset types distinguish TIA project and TIA library data. Any future serialized model must keep Teamcenter item/revision identity separate from local TIA project identity.

## Domain capabilities

The V21 API surface includes:

- connection inspection;
- item and revision search;
- project or library download;
- workflow-related item metadata;
- lock handling;
- mapped and custom properties;
- local cache behavior.

The existence of these APIs does not make them supported agent or product actions.

## Locking and concurrency

Teamcenter locks and TIA project exclusive access are separate boundaries. A future workflow changing a Teamcenter-managed project would need to coordinate both systems and preserve the relationship between local project state and Teamcenter revision state.

A TIA transaction is not a substitute for Teamcenter locking, revision control, or check-in policy.

## Version-control Add-In surface

`Siemens.Engineering.AddIn.VersionControl` includes provider and workflow families for VCI workspace, editor, repository, and import integration. These are distinct from the current `ProjectTreeAddInProvider` context menu.

TIA Portal Code Agent does not register VCI or Teamcenter Add-In providers.

## Current product boundary

The current product does not expose even read-only Teamcenter commands. Teamcenter context may be discussed only when it is already present in a supported selected-object snapshot or external runtime context.

Unsupported product behavior includes:

- Teamcenter searches or downloads;
- lock acquisition or release;
- revision creation;
- property updates;
- check-in or workflow execution;
- VCI import, export, or repository actions.

## Future requirements

Any future Teamcenter integration would require:

- explicit connection and identity validation;
- bounded search and property results;
- dedicated authorization;
- lock-state and stale-revision validation;
- deterministic preview for metadata or content changes;
- explicit approval;
- coordinated TIA and Teamcenter recovery behavior;
- complete audit evidence;
- controlled handling of cache and downloaded files.

These are future design constraints, not implemented tools.

## Error handling

A future integration should preserve Teamcenter callback messages, exception type, item and revision identifiers, cache options, operation IDs, and the TIA project state reached before failure.

## Validation sources

Before documenting Teamcenter or VCI support:

- verify the exact V21 assembly and licensed product availability;
- test the workflow against a controlled Teamcenter environment;
- validate lock and recovery semantics;
- review credentials and local cache handling;
- implement the Add-In and Bridge workflow end-to-end.