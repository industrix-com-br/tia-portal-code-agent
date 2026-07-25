# Runtime Configuration

The Bridge supports three interchangeable coding-agent runtimes. Runtime selection does not change the Add-In or the task contract.

## Supported runtimes

| Runtime | ID | Supported mode |
|---|---|---|
| Mimo CLI | `mimo` | CLI |
| OpenCode | `opencode` | Server or CLI |
| Claude Code CLI | `claude` | CLI |

The runtime executable must be available on `PATH` or configured with an explicit path.

## Configuration file

Path:

```text
%LOCALAPPDATA%\TiaAgent\config.json
```

Example:

```json
{
  "defaultRuntime": "opencode",
  "updateChannel": "stable",
  "runtimes": {
    "mimo": {
      "enabled": true,
      "executable": "mimo",
      "mode": "cli"
    },
    "opencode": {
      "enabled": true,
      "executable": "opencode",
      "mode": "server",
      "serverUrl": "http://127.0.0.1:43120"
    },
    "claude": {
      "enabled": true,
      "executable": "claude",
      "mode": "cli"
    }
  }
}
```

Implemented fields:

- `defaultRuntime` — runtime used when no request or environment override exists; default `opencode`.
- `updateChannel` — `stable`, `rc`, `beta`, or `alpha`; default `stable`.
- `runtimes.<id>.enabled` — whether the runtime is selectable.
- `runtimes.<id>.executable` — executable name or path.
- `runtimes.<id>.mode` — `server` or `cli`, limited by the selected adapter.
- `runtimes.<id>.serverUrl` — HTTP endpoint used by server mode.
- `runtimes.<id>.environment` — extra process environment values. This field is supported by the configuration model but is not exposed by the current `tia-agent config set` command.

## Selection precedence

1. `runtime` in the task request;
2. `TIA_AGENT_RUNTIME` environment variable;
3. `defaultRuntime` in `config.json`;
4. `opencode`.

The Bridge does not silently fall back to another runtime. An unavailable selected runtime produces an explicit error.

## CLI configuration

```powershell
tia-agent runtime list
tia-agent runtime use opencode --mode server
tia-agent runtime use claude --mode cli
tia-agent runtime doctor
tia-agent runtime doctor claude
tia-agent runtime status
```

Configuration values can also be managed directly:

```powershell
tia-agent config list
tia-agent config get defaultRuntime
tia-agent config set defaultRuntime claude
tia-agent config set runtimes.claude.executable C:\tools\claude.cmd
tia-agent config set runtimes.opencode.mode server
tia-agent config set runtimes.opencode.serverUrl http://127.0.0.1:43120
```

The current `config set` implementation supports `enabled`, `executable`, `mode`, and `serverUrl` for runtime entries.

## Execution modes

### CLI mode

The Bridge starts a runtime process for each task. Mimo and Claude support only this mode in the current registry. OpenCode may also use CLI mode.

### Server mode

OpenCode may run as a local HTTP server. The Runtime Supervisor starts the server and waits for health before publishing the runtime manifest.

All services must remain bound to loopback.

## MCP integration

Each runtime is configured to invoke the external `TiaMcpServer` integration through stdio. This repository does not implement a second TIA Portal MCP server.

The upstream MCP package may expose read and write tools. The current TIA Portal Code Agent product workflow supports reads, reviews, and change proposals only. The Add-In does not implement a user approval and apply workflow, so direct project writes must not be documented as supported behavior.

## Runtime manifest

The supervisor publishes:

```text
%LOCALAPPDATA%\TiaAgent\runtime\runtime.json
```

The manifest records the active Bridge endpoint, selected runtime, process metadata, and status. Consumers must still call a health endpoint because the file may outlive a failed process.

## Adding a runtime adapter

A new adapter requires:

1. an `IAgentRuntime` implementation under `src/TiaAgent.Bridge/Runtime/`;
2. registration in the Bridge startup code;
3. compatibility metadata in `RuntimeCompatibilityRegistry`;
4. supervisor changes when a server process is required;
5. runtime and task tests;
6. updates to this document and [CLI.md](CLI.md).

Adapters must support cancellation and bounded timeouts, avoid shell execution when possible, preserve output encoding, and return structured task results.

## Diagnostics

```powershell
tia-agent runtime doctor
tia-agent doctor --verbose
tia-agent status
```

See [TROUBLESHOOTING.md](TROUBLESHOOTING.md) for log paths and common failures.