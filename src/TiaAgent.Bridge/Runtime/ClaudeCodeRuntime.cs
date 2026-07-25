using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TiaAgent.Bridge.Diagnostics;
using TiaAgent.Contracts.Runtime;

namespace TiaAgent.Bridge.Runtime;

/// <summary>
/// Runtime adapter for Claude Code CLI (claude -p --output-format json).
/// Uses non-interactive print mode with MCP configuration for TIA Portal tools.
/// Does not automate the terminal UI or send simulated keyboard input.
/// </summary>
public sealed class ClaudeCodeRuntime : IAgentRuntime, IDisposable
{
    private readonly ProcessRunner _processRunner;
    private readonly BridgeLogger _logger;
    private readonly string? _executable;
    private readonly string? _model;
    private readonly string? _mcpServerCommand;
    private string? _generatedMcpConfigPath;

    private static readonly JsonSerializerOptions s_jsonOptions = new() { WriteIndented = true };

    public string Id => "claude";
    public string DisplayName => "Claude Code CLI";

    /// <summary>
    /// Creates a Claude Code runtime adapter.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="executable">Path to claude executable.</param>
    /// <param name="model">Optional model override.</param>
    /// <param name="mcpServerCommand">The MCP server command (e.g. "tia-mcp"). If set, generates an MCP config file.</param>
    public ClaudeCodeRuntime(BridgeLogger logger, string? executable = null, string? model = null, string? mcpServerCommand = null)
    {
        _logger = logger;
        _processRunner = new ProcessRunner(logger);
        _executable = executable;
        _model = model;
        _mcpServerCommand = mcpServerCommand;
    }

    public async Task<RuntimeAvailabilityResult> CheckAvailabilityAsync(CancellationToken cancellationToken)
    {
        var exeName = _executable ?? "claude";
        var resolved = ResolveProcess(exeName);
        LogResolvedProcess(exeName, resolved, "check-availability");
        try
        {
            var args = resolved.ComposeArguments("--version");
            var result = await _processRunner.RunAsync(
                resolved.FileName, args, null,
                TimeSpan.FromSeconds(10),
                cancellationToken: cancellationToken).ConfigureAwait(false);

            if (result.Success)
            {
                var version = result.StdOut.Trim();
                _logger.Info($"ClaudeCodeRuntime: available, version={version}");

                // Generate MCP config if we have an MCP server command
                if (!string.IsNullOrEmpty(_mcpServerCommand))
                {
                    EnsureMcpConfigGenerated();
                }

                return new RuntimeAvailabilityResult
                {
                    Available = true,
                    Executable = exeName,
                    Version = version,
                    Mode = "cli"
                };
            }

            var error = result.Error ?? $"claude returned exit code {result.ExitCode}: {result.StdErr.Trim()}";
            _logger.Warn($"ClaudeCodeRuntime: not available, error={error}");
            return new RuntimeAvailabilityResult
            {
                Available = false,
                Executable = exeName,
                Mode = "cli",
                Error = error
            };
        }
        catch (Exception ex)
        {
            _logger.Warn($"ClaudeCodeRuntime: executable not found: {ex.Message}");
            return new RuntimeAvailabilityResult
            {
                Available = false,
                Executable = exeName,
                Mode = "cli",
                Error = $"Executable not found: {exeName}. {ex.Message}"
            };
        }
    }

    public async Task<AgentTaskResult> ExecuteAsync(
        AgentTaskRequest request,
        IProgress<AgentTaskEvent>? progress,
        CancellationToken cancellationToken)
    {
        // --- Request validation ---
        var validationError = ValidateRequest(request);
        if (validationError != null)
        {
            _logger.Warn($"ClaudeCodeRuntime: request validation failed: {validationError}");
            return new AgentTaskResult
            {
                Success = false,
                Error = validationError,
                ErrorCode = "INVALID_REQUEST",
                RuntimeId = Id
            };
        }

        var exeName = _executable ?? "claude";
        var resolved = ResolveProcess(exeName);
        LogResolvedProcess(exeName, resolved, "execute");

        // Ensure MCP config exists before building arguments (--mcp-config needs the file)
        if (!string.IsNullOrEmpty(_mcpServerCommand))
        {
            EnsureMcpConfigGenerated();
        }

        var fixedArgs = BuildArguments();
        var hasMcp = !string.IsNullOrEmpty(_generatedMcpConfigPath);
        _logger.Info($"ClaudeCodeRuntime: mcpConfig={_generatedMcpConfigPath ?? "none"}, mcpTransport={(hasMcp ? "stdio" : "none")}, mcpCommand={_mcpServerCommand ?? "none"}, wrapper={resolved.Wrapper}");
        var args = resolved.ComposeArguments(fixedArgs);

        // --- Boundary diagnostics ---
        var promptHash = ProcessRunner.ComputeSha256(request.Prompt!);
        var promptBytes = Encoding.UTF8.GetByteCount(request.Prompt!);
        var promptEndsWithNewline = request.Prompt!.EndsWith('\n');

        _logger.Info($"ClaudeCodeRuntime: task={request.TaskId} action={request.Action} agent={request.AgentId}");
        _logger.Info($"ClaudeCodeRuntime: selection={request.Selection?.Name ?? "none"} ({request.Selection?.ObjectType ?? "none"})");
        _logger.Info($"ClaudeCodeRuntime: prompt length={request.Prompt!.Length} chars, {promptBytes} UTF-8 bytes, hash={promptHash}, endsWithNewline={promptEndsWithNewline}");
        _logger.Info($"ClaudeCodeRuntime: prompt preview start={EscapeForLog(request.Prompt!, 120)}");
        _logger.Info($"ClaudeCodeRuntime: prompt preview end={EscapeForLog(request.Prompt![^120..], 120)}");
        _logger.Info($"ClaudeCodeRuntime: resolved executable={resolved.FileName}, target={resolved.ResolvedTargetPath}, wrapper={resolved.Wrapper}");
        _logger.Info($"ClaudeCodeRuntime: fixed CLI args={Truncate(args, 300)}");
        _logger.Info($"ClaudeCodeRuntime: stdin redirected=True, stdin bytes={promptBytes}");
        _logger.Info($"ClaudeCodeRuntime: session mode=fresh (no --continue/--resume)");
        _logger.Info($"ClaudeCodeRuntime: mcpConfig={_generatedMcpConfigPath ?? "none"}, mcpTransport={(hasMcp ? "stdio" : "none")}");

        var lineProgress = new Progress<string>(line =>
        {
            progress?.Report(new AgentTaskEvent { EventType = "progress", Message = line });
        });

        // --- Execute via stdin transport ---
        var result = await _processRunner.RunAsync(
            resolved.FileName, args, null,
            TimeSpan.FromMinutes(5),
            progress: lineProgress,
            stdinContent: request.Prompt!,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        // --- Post-execution diagnostics ---
        _logger.Info($"ClaudeCodeRuntime: process exit code={result.ExitCode}, stdout length={result.StdOut.Length}, stderr length={result.StdErr.Length}");

        if (result.Cancelled)
        {
            return new AgentTaskResult
            {
                Success = false,
                Error = "Task was cancelled",
                ErrorCode = "TASK_CANCELLED",
                RuntimeId = Id
            };
        }

        if (result.TimedOut)
        {
            return new AgentTaskResult
            {
                Success = false,
                Error = result.Error ?? "Task timed out",
                ErrorCode = "TASK_TIMEOUT",
                RuntimeId = Id
            };
        }

        if (!string.IsNullOrEmpty(result.Error))
        {
            return new AgentTaskResult
            {
                Success = false,
                Error = result.Error,
                ErrorCode = "RUNTIME_EXECUTION_FAILED",
                RuntimeId = Id
            };
        }

        // Parse Claude's JSON output
        var response = ParseClaudeOutput(result.StdOut, result.StdErr);

        // ═══════════════════════════════════════════════════════════════════
        // BOUNDARY 3: After runtime adapter parsing — log code points
        // ═══════════════════════════════════════════════════════════════════
        if (!string.IsNullOrEmpty(response))
        {
            var sampleLen = Math.Min(response.Length, 200);
            var codePointSample = new System.Text.StringBuilder(sampleLen * 7);
            for (int i = 0; i < sampleLen; i++)
            {
                var c = response[i];
                if (c >= 0x20 && c < 0x7F) codePointSample.Append(c);
                else codePointSample.Append($"U+{(int)c:X4} ");
            }
            _logger.Info($"ClaudeCodeRuntime [BOUNDARY 3 - parsed response]: {response.Length} chars, sample: {codePointSample}");
        }

        if (result.ExitCode != 0 && string.IsNullOrEmpty(response))
        {
            return new AgentTaskResult
            {
                Success = false,
                Error = $"claude exited with code {result.ExitCode}. {ProcessRunner.StripAnsiEscapes(result.StdErr.Trim())}",
                ErrorCode = "RUNTIME_NON_ZERO_EXIT",
                RuntimeId = Id
            };
        }

        // --- Response sanity checks ---
        var responseError = ValidateResponse(response, request.Action);
        if (responseError != null)
        {
            _logger.Warn($"ClaudeCodeRuntime: response validation failed: {responseError}");
            return new AgentTaskResult
            {
                Success = false,
                Error = responseError,
                ErrorCode = "RUNTIME_INVALID_RESPONSE",
                RuntimeId = Id,
                RuntimeMode = "cli"
            };
        }

        return new AgentTaskResult
        {
            Success = true,
            Response = response,
            RuntimeId = Id,
            RuntimeMode = "cli"
        };
    }

    public Task CancelAsync(string taskId, CancellationToken cancellationToken)
    {
        _logger.Info($"ClaudeCodeRuntime: cancel requested for task {taskId}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Resolves the executable for process start via <see cref="CommandResolver"/>.
    /// </summary>
    private static ResolvedCommand ResolveProcess(string executable)
        => CommandResolver.Resolve(executable);

    /// <summary>
    /// Logs the resolved executable details for diagnostics.
    /// Includes the resolved FileName, wrapper type, target path, and
    /// console/output encoding to help diagnose encoding issues.
    /// </summary>
    private void LogResolvedProcess(string requested, ResolvedCommand resolved, string context)
    {
        try
        {
            var consoleCodePage = Console.OutputEncoding.CodePage;
            var outputEncodingName = Console.OutputEncoding.EncodingName;
            _logger.Info($"ClaudeCodeRuntime [{context}]: resolved exe={resolved.FileName}, target={resolved.ResolvedTargetPath}, requested={requested}, wrapper={resolved.Wrapper}, consoleCodePage={consoleCodePage}, outputEncoding={outputEncodingName}");
        }
        catch
        {
            // Logging should never crash the process resolution
            _logger.Info($"ClaudeCodeRuntime [{context}]: resolved exe={resolved.FileName}, target={resolved.ResolvedTargetPath}, requested={requested}, wrapper={resolved.Wrapper}");
        }
    }

    /// <summary>
    /// Builds fixed, short command-line arguments for claude.
    /// The dynamic prompt is NOT placed here — it goes through stdin.
    /// </summary>
    private string BuildArguments()
    {
        var sb = new StringBuilder();

        // Non-interactive print mode with a fixed instruction.
        // The real prompt arrives via stdin.
        sb.Append("-p ");
        sb.Append(EscapeShellArg("Process the complete request provided through stdin."));

        // JSON output for machine-readable response
        sb.Append(" --output-format json");

        // MCP configuration for TIA Portal tools
        if (!string.IsNullOrEmpty(_generatedMcpConfigPath))
        {
            sb.Append(" --mcp-config ");
            sb.Append(EscapeShellArg(_generatedMcpConfigPath));
            sb.Append(" --strict-mcp-config");
        }

        // Model override
        if (!string.IsNullOrEmpty(_model))
        {
            sb.Append(" --model ");
            sb.Append(_model);
        }

        // Skip permissions for non-interactive use
        sb.Append(" --dangerously-skip-permissions");

        // No session persistence for one-shot tasks
        sb.Append(" --no-session-persistence");

        return sb.ToString();
    }

    /// <summary>
    /// Parses Claude's JSON output (--output-format json returns a single JSON object).
    /// The response text is in the "result" field.
    /// Does NOT trim or normalize the response — Markdown formatting is data.
    /// </summary>
    private string ParseClaudeOutput(string stdout, string stderr)
    {
        if (string.IsNullOrWhiteSpace(stdout))
            return ProcessRunner.StripAnsiEscapes(stderr);

        try
        {
            using var doc = JsonDocument.Parse(stdout);
            var root = doc.RootElement;

            // Claude --output-format json: { "result": "...", "is_error": false, ... }
            if (root.TryGetProperty("result", out var resultProp) && resultProp.ValueKind == JsonValueKind.String)
            {
                return resultProp.GetString() ?? "";
            }

            // Alternative: check for "content" or "text"
            if (root.TryGetProperty("content", out var contentProp) && contentProp.ValueKind == JsonValueKind.String)
            {
                return contentProp.GetString() ?? "";
            }
        }
        catch (JsonException)
        {
            // Not valid JSON — treat as plain text
        }

        return ProcessRunner.StripAnsiEscapes(stdout);
    }

    /// <summary>
    /// Generates an MCP configuration file for Claude Code pointing to the tia-mcp stdio server.
    /// </summary>
    private void EnsureMcpConfigGenerated()
    {
        if (_generatedMcpConfigPath != null && File.Exists(_generatedMcpConfigPath))
            return;

        try
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var configDir = Path.Combine(localAppData, "TiaAgent");
            Directory.CreateDirectory(configDir);

            _generatedMcpConfigPath = Path.Combine(configDir, "claude-mcp.json");

            // Claude MCP config format
            var config = new
            {
                mcpServers = new
                {
                    tia_portal = new
                    {
                        command = _mcpServerCommand,
                        args = Array.Empty<string>(),
                        type = "stdio"
                    }
                }
            };

            var json = JsonSerializer.Serialize(config, s_jsonOptions);
            File.WriteAllText(_generatedMcpConfigPath, json);
            _logger.Info($"ClaudeCodeRuntime: generated MCP config at {_generatedMcpConfigPath}");
            _logger.Info($"ClaudeCodeRuntime: MCP server command={_mcpServerCommand}, transport=stdio, auth=none (stdio transport)");
        }
        catch (Exception ex)
        {
            _logger.Error("ClaudeCodeRuntime: failed to generate MCP config", ex);
            _generatedMcpConfigPath = null;
        }
    }

    private static string EscapeShellArg(string arg) => RuntimeHelpers.EscapeShellArg(arg);

    private static string? ValidateRequest(AgentTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CorrelationId))
            return "Missing correlation ID";

        if (string.IsNullOrWhiteSpace(request.Action))
            return "Missing action";

        var recognizedActions = new[] { "explain", "review", "propose" };
        var actionLower = request.Action.ToLowerInvariant();
        if (Array.IndexOf(recognizedActions, actionLower) < 0)
            return $"Unrecognized action: '{request.Action}'. Expected one of: explain, review, propose";

        if (string.IsNullOrWhiteSpace(request.AgentId))
            return "Missing agent ID";

        if (string.IsNullOrWhiteSpace(request.Prompt))
            return "Empty prompt — no content to send to Claude";

        if (request.Prompt.Length < 50)
            return $"Prompt too short ({request.Prompt.Length} chars) — expected at least 50 chars from the template";

        return null;
    }

    private static string? ValidateResponse(string? response, string action)
    {
        if (string.IsNullOrWhiteSpace(response))
            return "Empty response from Claude";

        var trimmed = response.Trim();

        // Detect generic greeting
        if (trimmed.StartsWith("Hi!", StringComparison.OrdinalIgnoreCase) ||
            trimmed.Contains("I'm Claude", StringComparison.OrdinalIgnoreCase) ||
            trimmed.Contains("ready to help", StringComparison.OrdinalIgnoreCase))
        {
            return "Response is a generic greeting — prompt was likely not received";
        }

        // Detect empty-message warnings
        if (trimmed.Contains("message came through empty", StringComparison.OrdinalIgnoreCase) ||
            trimmed.Contains("message got cut off", StringComparison.OrdinalIgnoreCase))
        {
            return "Response indicates empty or truncated input — prompt was not delivered correctly";
        }

        return null;
    }

    private static string EscapeForLog(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return "(empty)";

        var sb = new StringBuilder(Math.Min(text.Length, maxLength));
        foreach (var c in text)
        {
            if (sb.Length >= maxLength) break;
            switch (c)
            {
                case '\n': sb.Append("\\n"); break;
                case '\r': sb.Append("\\r"); break;
                case '\t': sb.Append("\\t"); break;
                case '\\' when sb.Length < maxLength - 2: sb.Append("\\\\"); break;
                case '"' when sb.Length < maxLength - 2: sb.Append("\\\""); break;
                default:
                    if (char.IsControl(c))
                    {
                        if (sb.Length < maxLength - 4)
                            sb.Append($"\\x{(int)c:X2}");
                    }
                    else
                    {
                        sb.Append(c);
                    }
                    break;
            }
        }
        if (text.Length > maxLength) sb.Append("...");
        return sb.ToString();
    }

    private static string Truncate(string s, int maxLength)
    {
        if (s == null) return "";
        return s.Length <= maxLength ? s : string.Concat(s.AsSpan(0, maxLength), "...");
    }

    public void Dispose()
    {
        _processRunner.Dispose();

        // Clean up generated MCP config
        if (_generatedMcpConfigPath != null && File.Exists(_generatedMcpConfigPath))
        {
            try { File.Delete(_generatedMcpConfigPath); } catch { }
        }
    }
}
