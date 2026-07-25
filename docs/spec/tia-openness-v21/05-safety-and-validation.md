# TIA Portal V21 Safety and Safety Validation API

## Scope and product policy

Safety APIs are exposed through:

- `Siemens.Engineering.Safety`;
- `Siemens.Engineering.SafetyValidation`;
- `Siemens.Engineering.AddIn.Safety`.

This file is an API reference. TIA Portal Code Agent does not currently expose Safety tools, Safety compile extensions, Safety mutation, or Safety download. Safety project content may be explained or reviewed only when it is available through a supported read-oriented selection or external MCP context.

Safety data is not an ordinary CRUD surface. No model-generated text can authorize a Safety operation.

## Safety engineering model

Important V21 types include:

- `SafetyAdministration`;
- `SafetySettings` and `GlobalSettings`;
- `AssignmentOfBlockNumbers`;
- `RuntimeGroup` and its composition;
- `SafetySignature` and `SafetySignatureProvider`;
- `SafetyPrintout` and print options;
- Safety-specific download configurations.

Block-range and settings changes can affect generated blocks, signatures, compile results, and project consistency. They must not be exposed through generic attribute setters.

## Signatures and traceability

Safety signature information can include signature type, value, scope, system version, and state metadata. A signature is engineering evidence, not a simple status flag.

Read-oriented output must preserve scope and freshness and must not imply that a signature was validated when it was merely read.

## Safety compile Add-In workflow API

`Siemens.Engineering.AddIn.Safety` provides a workflow extension chain:

```text
SafetyCompileAddInProvider
  -> SafetyCompileWorkflowAddIn
    -> SafetyCompileWorkflowSupport
      -> SafetyCompileWorkflowItem
        -> Execute / Rollback
```

The current Add-In does not register these providers or execute an LLM inside a Safety compile workflow.

Any future workflow extension must be deterministic, use bounded external communication, surface cancellation, and report partial rollback or validation failure explicitly.

## Safety Validation model

`Siemens.Engineering.SafetyValidation` includes:

- `SafetyValidationAssistant`;
- activation tests and safety functions;
- conditions and condition values;
- device queries;
- trace configuration;
- validation results and state enums;
- activation-test import and printout support.

Availability depends on the installed product, licensing, project configuration, and current state. Returned data must be converted to bounded serializable snapshots.

## Current product boundary

Potential API capabilities such as reading settings, signatures, runtime groups, or activation tests are not documented as supported commands until they are implemented and validated end-to-end.

Explicitly unsupported product behavior includes:

- updating Safety settings;
- changing Safety block ranges;
- importing activation tests;
- registering or executing Safety compile extensions;
- mutating Safety programs;
- downloading Safety programs.

## Requirements before any future Safety operation

A future Safety-related write or deployment workflow would require all of the following:

1. Safety-specific authorization;
2. explicit project and device confirmation outside model text;
3. immutable before-state and signature evidence;
4. deterministic change preview and impact analysis;
5. short-lived, single-use approval;
6. stale-state validation and appropriate exclusive access;
7. Safety compile and validation evidence;
8. signature comparison;
9. complete audit records;
10. no automatic download.

These are design requirements, not implemented features.

## Error handling

Safety diagnostics must distinguish:

- API capability unavailable;
- project not configured for Safety;
- permission or trust failure;
- validation or compile failure;
- stale state;
- external workflow failure;
- user cancellation.

Do not reduce Safety failures to a generic success/failure message.

## Validation sources

Before documenting any Safety capability as supported:

- verify the symbol against the installed V21 Safety assemblies;
- validate licensing and project prerequisites;
- run the workflow in a controlled Safety environment;
- review permissions and failure behavior;
- obtain a separate safety and security review.