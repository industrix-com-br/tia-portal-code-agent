# TIA Portal V21 Add-In framework

## Scope

The Add-In API is delivered mainly through:

- `Siemens.Engineering.AddIn.Base`;
- `Siemens.Engineering.AddIn.Permissions`;
- `Siemens.Engineering.AddIn.Utilities`;
- `Siemens.Engineering.AddIn.Step7`;
- `Siemens.Engineering.AddIn.Safety`.

An Add-In runs in the TIA Portal extension context. It can contribute context-menu actions and workflow extensions while using the engineering object model supplied by TIA Portal.

This file is an API reference. The current TIA Portal Code Agent implementation uses only the subset described under **Current project implementation**.

## Provider entry points

The base assembly exposes provider classes for major UI locations:

| Provider | UI scope |
|---|---|
| `ProjectTreeAddInProvider` | Project tree |
| `DevicesAndNetworksAddInProvider` | Devices and networks editor/context |
| `ProjectLibraryTreeAddInProvider` | Project library tree |
| `GlobalLibraryTreeAddInProvider` | Global library tree |

VCI providers include:

| Provider | UI scope |
|---|---|
| `VciEditorAddInProvider` | VCI workspace editor |
| `VciImportAddInProvider` | VCI import |
| `VciWorkspaceRepositoryAddInProvider` | VCI repository export |

Specialized providers include:

| Provider | Assembly | Purpose |
|---|---|---|
| `CaxAddInProvider` | `AddIn.Step7` | CAx import/export workflows |
| `SafetyCompileAddInProvider` | `AddIn.Safety` | Safety compile workflows |

A provider returns one or more `ContextMenuAddIn` implementations for its scope. Providers are disposable and their lifetime is controlled by the Add-In framework.

## Context-menu model

`ContextMenuAddIn` is the base extension object. It provides:

- a display name supplied through its constructor;
- `BuildContextMenuItems(ContextMenuAddInRoot)`;
- submenu access through `GetSubmenu()`.

`ContextMenuAddInRoot` exposes its items and default label text. `ChildItemFactory` creates submenus, typed actions, status-aware actions, check boxes, and radio-button items.

The API contains typed `ActionItem<TSelectedObject>` variants for one, two, and three selected-object types. Typed actions are the preferred way to constrain commands to supported engineering objects.

## Selection handling

The menu API supplies `MenuSelectionProvider` variants for one, two, or three selected-object types.

The selection passed to an action is contextual and short-lived. An implementation should:

1. inspect the typed selection;
2. convert live Siemens objects into serializable data while the callback owns them;
3. release the callback quickly;
4. perform HTTP, runtime, or model work outside the TIA UI thread;
5. never retain a `MenuSelectionProvider` or live engineering object as a long-term cache.

Any future mutation would additionally require reacquiring and validating the target immediately before the operation. The current product does not implement that workflow.

## Menu status and state

Status callbacks allow TIA Portal to update whether an item is enabled and how it is displayed.

Relevant types include:

- `MenuStatus`;
- `ActionItemStyle`;
- `CheckBoxActionItemStyle` and `CheckBoxState`;
- `RadioButtonActionItemStyle` and `RadioButtonState`.

Status callbacks must be fast, deterministic, and side-effect free. Do not perform model calls, file I/O, compilation, or deep project scans in a status callback.

## User-feedback APIs

### Progress

`ProgressProvider` displays progress in TIA Portal and exposes update, cancellation, and disposal behavior. It is available for operations that intentionally use TIA-native progress UI.

### Messages and confirmation

`MessageBoxProvider` provides notification and confirmation APIs. Confirmation is required before any future workflow that changes project state or crosses a safety boundary.

### Feedback context

`FeedbackProvider` exposes TIA Portal feedback context. It is UI integration support, not a replacement for product diagnostics.

The current project displays completed agent responses in its own WPF window and falls back to a MessageBox when WPF creation fails.

## Workflow extension model

The base workflow architecture follows:

```text
AddInProvider
  -> WorkflowAddIn
    -> WorkflowSupport
      -> WorkflowItem
        -> Execute / Rollback
```

Core workflow types include `WorkflowContext`, `WorkflowExecutionResult`, and `WorkflowReturnCode`.

The Step7 Add-In assembly specializes this model for CAx workflows. The Safety Add-In assembly specializes it for Safety compile workflows. TIA Portal Code Agent does not currently register CAx, Safety compile, VCI, or other workflow providers.

## External process execution

`Siemens.Engineering.AddIn.Utilities` includes process-execution wrappers and corresponding permissions.

General rules:

- executable paths must come from trusted installation configuration;
- user-controlled strings must not be concatenated into shell commands;
- direct executable invocation is preferable to shell execution;
- stdout and stderr should be bounded and encoded predictably;
- timeouts and cancellation are required;
- model or runtime execution must not block the TIA UI thread.

The current Add-In does not start the coding-agent runtime directly. It communicates with the local Bridge over authenticated loopback HTTP.

## Current project implementation

The shipped project uses:

```text
ProjectTreeProvider
  -> AI Code Agent context menu
    -> SelectionSnapshotFactory
      -> AgentBridgeClient
        -> local TiaAgent.Bridge
          -> selected external runtime
```

Current actions:

- **Explain selected object**;
- **Review selected object**;
- **Propose change**.

Implementation constraints:

- `TiaAgent.AddIn` targets .NET Framework 4.8;
- the Add-In references V21 modular Public API assemblies with `Private=false`;
- live Siemens objects remain inside the callback operation;
- the Bridge request contains serializable contracts only;
- results are displayed through the WPF-first response UI;
- Add-In logging is best-effort and cannot prevent loading;
- no approval or apply workflow exists for project changes.

The exact packaging and permission set are documented in [`../ADDIN_TECHNICAL_SPEC.md`](../ADDIN_TECHNICAL_SPEC.md) and `src/TiaAgent.AddIn/Config.xml`.

## Validation rules

When changing the Add-In:

- verify symbols against the installed V21 assemblies;
- keep menu construction and status callbacks lightweight;
- test selection capture using representative V21 objects;
- test the WPF window and MessageBox fallback inside TIA Portal;
- verify loopback Bridge communication and authentication;
- rebuild and verify the `.addin` package;
- confirm Siemens assemblies are not copied into the artifact;
- update product documentation only for behavior implemented and validated end-to-end.