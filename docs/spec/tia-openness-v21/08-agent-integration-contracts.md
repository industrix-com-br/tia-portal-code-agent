# Agent-facing integration contracts for TIA Portal V21

## Purpose

This document describes how the current product connects TIA Portal context to coding-agent runtimes. It does not define an in-repository MCP server or a project-mutation service layer.

## Current topology

```text
TIA Portal V21
  -> TiaAgent.AddIn
    -> selected-object SelectionSnapshot
      -> authenticated loopback Bridge task
        -> Mimo, OpenCode, or Claude Code runtime
          -> external TiaMcpServer / tia-mcp over stdio
            -> TIA Portal Openness
```

Current boundaries:

- `TiaAgent.AddIn` owns the TIA context-menu integration and captures the initiating selection.
- `TiaAgent.Contracts` defines serializable task and runtime contracts.
- `TiaAgent.Bridge` owns task lifecycle, authentication, runtime selection, and adapter execution.
- `TiaMcpServer` is an external dependency that owns additional Openness access exposed through MCP.
- This repository does not implement MCP tool handlers or a second Openness host.

## Selection snapshot contract

The Add-In converts the selected Siemens object into a serializable snapshot before sending it to the Bridge.

The snapshot may include:

- object type and display metadata;
- project and parent context;
- source or exported content when supported;
- the requested action;
- correlation metadata.

Rules:

- live `IEngineeringObject` instances never cross the Add-In boundary;
- snapshots must remain bounded and serializable;
- unsupported source extraction must be represented as an explicit limitation or error;
- project content is untrusted model input;
- absolute paths and unnecessary source content must not be logged.

## Bridge task contract

The Add-In creates a `BridgeTaskRequest` and polls the returned task ID.

The Bridge is responsible for:

- validating authentication;
- assigning and preserving correlation IDs;
- resolving the requested or configured runtime;
- executing the action profile;
- applying cancellation and timeouts;
- returning structured status, result, and error information.

The runtime choice does not change the Add-In contract.

## Current action profiles

| Product action | Action ID | Agent profile |
|---|---|---|
| Explain selected object | `explain` | `tia-explain` |
| Review selected object | `review` | `tia-review` |
| Propose change | `propose` | `tia-change` |

The proposal profile produces recommendations. The current product does not implement a preview, approval, or apply endpoint for TIA project changes.

## MCP boundary

Each supported runtime may invoke `tia-mcp` according to its runtime-specific configuration.

The external MCP package can evolve independently and may expose operations beyond the supported TIA Portal Code Agent workflow. Tool availability in the external MCP server does not expand this product's documented capabilities.

Product documentation must distinguish:

- API or MCP capabilities that exist upstream;
- operations the runtime can technically discover;
- workflows intentionally exposed and validated by this product.

Only the final category is supported product behavior.

## Runtime selection

Selection precedence:

1. runtime override in the task request;
2. `TIA_AGENT_RUNTIME` environment variable;
3. `defaultRuntime` in `%LOCALAPPDATA%\TiaAgent\config.json`;
4. `opencode`.

Supported IDs are `opencode`, `mimo`, and `claude`. Runtime failures are returned explicitly; the Bridge does not silently switch to another runtime.

## Error and cancellation rules

- Every task must preserve a correlation ID.
- Long-running work must execute outside the TIA UI thread.
- Cancellation must propagate to the selected adapter and child process or HTTP request.
- Timeouts must produce a structured terminal task state.
- Runtime output must not be treated as trusted executable instructions.
- Failures must not leave the Add-In waiting indefinitely.

## Current safety boundary

Supported:

- selected-object context capture;
- explanations;
- reviews;
- change proposals;
- read-oriented MCP context gathering;
- diagnostics.

Not supported:

- direct project mutation;
- generic method or attribute invocation;
- PLC download or online control;
- safety modification;
- hardware or network changes;
- deletion;
- unattended project-wide refactoring.

## Future write contract

A future write workflow would require a separately implemented and validated contract with:

- deterministic preview and diff;
- explicit user approval outside model text;
- session, project, object, and content-hash binding;
- short-lived, single-use authorization;
- stale-state detection;
- recoverable previous state;
- compile or consistency validation;
- audit evidence and partial-failure handling.

These are future requirements and must not be interpreted as current MCP tools or Add-In behavior.

## Validation sources

Verify changes against:

- `src/TiaAgent.AddIn/Providers/ProjectTreeProvider.cs`;
- Add-In snapshot and Bridge-client implementations;
- contracts under `src/TiaAgent.Contracts`;
- Bridge runtime registry and adapters;
- `config/opencode.example.json`;
- runtime tests;
- the exact installed `TiaMcpServer` version used for release validation.