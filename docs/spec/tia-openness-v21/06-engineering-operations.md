# TIA Portal V21 engineering operations

## Scope

This file summarizes V21 APIs for compile, import/export, compare, cross-reference, CAx transfer, and download.

These are Siemens API capabilities, not current TIA Portal Code Agent commands. The current product exposes read-oriented analysis and change proposals only. It does not compile, import, export, mutate, save, or download a TIA project as a user-facing workflow.

## Compilation

`Siemens.Engineering.Compiler.ICompilable` and `CompileProvider` expose compile operations. `CompilerResult` includes state, error and warning counts, and hierarchical messages.

Any future compile integration must preserve the full message hierarchy, target identity, duration, and explicit capability status. Compilation must not be presented as successful based only on the absence of an exception.

## SIMATIC ML and document exchange

Typed domain objects expose import and export methods with explicit options. Step7 also supports document-oriented exchange for selected objects and languages.

Reference file-handling rules:

- use a per-operation temporary directory;
- normalize and validate paths;
- do not accept unrestricted destinations from model output;
- hash generated or consumed artifacts;
- bound retention and logging;
- remove temporary files on cancellation or failure where possible.

The current product does not expose import or export commands. Source extraction performed for a selected object is an internal read-context operation and must report unsupported formats accurately.

## Compare

The V21 compare API exposes result states and a hierarchical result tree. Compare can support future review and validation, but it must not automatically choose which side overwrites the other.

Online compare is outside the current product boundary.

## Cross-reference

`Siemens.Engineering.CrossReference` exposes source, reference, location, access-type, filter, and result objects.

Cross-reference queries can be expensive. A future read integration must accept a bounded scope and result limit and must return truncation and capability information.

## CAx transfer

`Siemens.Engineering.Cax.CaxProvider` exposes device- and project-level export/import with merge options and structured transfer results.

CAx import is a broad project mutation. TIA Portal Code Agent does not expose CAx transfer. Any future implementation would require deterministic preview, explicit merge policy, approval, recovery, and complete result evidence.

## Download

`Siemens.Engineering.Download.DownloadProvider` exposes device download and configuration decisions involving module state, online/offline differences, protection, initialization, HMI components, user-management data, and Safety content.

Download is deployment, not ordinary engineering editing. It is explicitly unsupported by the current product.

A future download workflow would require, at minimum:

- dedicated authorization;
- exact target and interface confirmation;
- complete configuration preview;
- explicit handling of protection credentials outside model text;
- no guessed choices;
- complete result-message capture;
- no automatic module start;
- separation from project mutation and save.

## Future operation state model

A future mutating or deployment operation would need a state model similar to:

```text
requested
  -> target resolved
  -> capability checked
  -> preview generated
  -> explicit approval
  -> stale-state validation
  -> exclusive access where required
  -> operation executed
  -> compile or validation
  -> committed
  -> optional explicit save
  -> completed
```

This is a safety design reference, not an implemented Bridge task flow.

## Error taxonomy for future integrations

Stable operation errors should distinguish session, project, object, capability, version, permission, trust, cancellation, import/export, compile, compare, CAx, download, and Safety restrictions.

Original exception types and technical details belong in protected diagnostics, not in a stable public error code or unfiltered model response.

## Current product rule

Do not add any command, feature description, or example implying support for compile, mutation, online compare, CAx transfer, save, or download until the complete workflow exists in the Add-In and Bridge and has been validated against a controlled V21 environment.