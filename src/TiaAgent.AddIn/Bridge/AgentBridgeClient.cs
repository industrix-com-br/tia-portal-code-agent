using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TiaAgent.AddIn.Diagnostics;
using TiaAgent.Contracts.Bridge;
using TiaAgent.Contracts.Diagnostics;

namespace TiaAgent.AddIn.Bridge;

/// <summary>
/// HTTP client for communicating with the TIA Portal Code Agent Bridge.
/// Uses System.Net.Http.HttpClient (available in net48).
/// Manual JSON serialization — no System.Text.Json dependency.
/// </summary>
public sealed class AgentBridgeClient : IAgentBridgeClient, IDisposable
{
    private static readonly Encoding s_strictUtf8 = new UTF8Encoding(false, true);

    private readonly HttpClient _httpClient;
    private readonly AddInConfig _config;
    private readonly bool _ownsHttpClient;

    public AgentBridgeClient(AddInConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(config.BridgeBaseUrl),
            Timeout = TimeSpan.FromSeconds(config.RequestTimeoutSeconds)
        };
        ConfigureAuthentication(_httpClient, config);
        _ownsHttpClient = true;
    }

    public AgentBridgeClient(HttpClient httpClient, AddInConfig config)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _config = config ?? throw new ArgumentNullException(nameof(config));
        ConfigureAuthentication(_httpClient, config);
        _ownsHttpClient = false;
    }

    /// <summary>
    /// Reads HTTP response content as raw bytes and decodes it with strict UTF-8.
    /// Invalid byte sequences fail explicitly instead of introducing U+FFFD.
    /// </summary>
    private static async Task<string> ReadResponseUtf8Async(HttpResponseMessage response)
    {
        var bytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        AddInLogger.Info(TextPayloadDiagnostics.DescribeUtf8Bytes(
            "6.addin.http-response.raw", bytes));

        var charset = response.Content.Headers.ContentType?.CharSet;
        if (!string.IsNullOrWhiteSpace(charset) &&
            !string.Equals(charset, "utf-8", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(charset, "utf8", StringComparison.OrdinalIgnoreCase))
        {
            throw new BridgeTaskException(
                $"Bridge response declared unsupported charset '{charset}'. Expected UTF-8.");
        }

        string decoded;
        try
        {
            decoded = s_strictUtf8.GetString(bytes);
        }
        catch (DecoderFallbackException ex)
        {
            throw new BridgeTaskException("Bridge response contained invalid UTF-8 bytes.", ex);
        }

        AddInLogger.Info(TextPayloadDiagnostics.DescribeText(
            "7.addin.http-response.decoded-json", decoded));
        return decoded;
    }

    private void ConfigureAuthentication(HttpClient client, AddInConfig config)
    {
        if (!string.IsNullOrEmpty(config.AuthToken))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", config.AuthToken);
            AddInLogger.Info($"Bridge auth configured: Bearer token loaded ({TokenFingerprint(config.AuthToken!)})");
        }
        else
        {
            AddInLogger.Warn("Bridge auth token not found — requests to Bridge will be rejected");
        }
    }

    private static string TokenFingerprint(string token)
    {
        if (string.IsNullOrEmpty(token)) return "<empty>";
        if (token.Length > 8)
            return string.Format("{0}...{1} ({2} chars)", token.Substring(0, 4), token.Substring(token.Length - 4), token.Length);
        return string.Format("{0}... ({1} chars)", token.Substring(0, 2), token.Length);
    }

    public void Dispose()
    {
        if (_ownsHttpClient)
            _httpClient.Dispose();
    }

    public async Task<BridgeHealthResponse> CheckHealthAsync(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync("/health", cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var json = await ReadResponseUtf8Async(response).ConfigureAwait(false);
        return ParseHealthResponse(json);
    }

    public async Task<BridgeTaskAccepted> StartTaskAsync(BridgeTaskRequest request, CancellationToken cancellationToken)
    {
        var json = BuildTaskRequestJson(request);

        // Use manually encoded UTF-8 bytes to make the request body unambiguous.
        var jsonBytes = Encoding.UTF8.GetBytes(json);
        var content = new ByteArrayContent(jsonBytes, 0, jsonBytes.Length);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
        {
            CharSet = "utf-8"
        };

        var response = await _httpClient.PostAsync("/v1/tasks", content, cancellationToken).ConfigureAwait(false);
        var responseJson = await ReadResponseUtf8Async(response).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new BridgeTaskException($"Bridge returned {(int)response.StatusCode}: {responseJson}");

        return ParseTaskAccepted(responseJson);
    }

    public async Task<BridgeTaskStatus> GetTaskAsync(string taskId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/v1/tasks/{taskId}", cancellationToken).ConfigureAwait(false);
        var json = await ReadResponseUtf8Async(response).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new BridgeTaskException($"Bridge returned {(int)response.StatusCode}: {json}");

        return ParseTaskStatus(json);
    }

    public async Task CancelTaskAsync(string taskId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsync($"/v1/tasks/{taskId}/cancel", null, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
    }

    #region JSON Serialization (Manual)

    private string BuildTaskRequestJson(BridgeTaskRequest request)
    {
        var sb = new StringBuilder();
        sb.Append('{');

        sb.AppendFormat("\"contractVersion\":\"{0}\"", EscapeJson(request.ContractVersion));
        sb.AppendFormat(",\"correlationId\":\"{0}\"", EscapeJson(request.CorrelationId));
        sb.AppendFormat(",\"action\":\"{0}\"", EscapeJson(request.Action));
        sb.AppendFormat(",\"agentId\":\"{0}\"", EscapeJson(request.AgentId));
        sb.AppendFormat(",\"userMessage\":\"{0}\"", EscapeJson(request.UserMessage));

        if (request.TiaInstance != null)
        {
            sb.Append(",\"tiaInstance\":{");
            sb.AppendFormat("\"processId\":{0}", request.TiaInstance.ProcessId);
            sb.AppendFormat(",\"sessionId\":\"{0}\"", EscapeJson(request.TiaInstance.SessionId));
            sb.AppendFormat(",\"version\":\"{0}\"", EscapeJson(request.TiaInstance.Version));
            sb.Append('}');
        }

        if (request.Project != null)
        {
            sb.Append(",\"project\":{");
            sb.AppendFormat("\"id\":\"{0}\"", EscapeJson(request.Project.Id));
            sb.AppendFormat(",\"name\":\"{0}\"", EscapeJson(request.Project.Name));
            sb.AppendFormat(",\"path\":\"{0}\"", EscapeJson(request.Project.Path));
            sb.Append('}');
        }

        if (request.Selection != null)
        {
            sb.Append(",\"selection\":{");
            sb.AppendFormat("\"name\":\"{0}\"", EscapeJson(request.Selection.Name));
            sb.AppendFormat(",\"objectType\":\"{0}\"", EscapeJson(request.Selection.ObjectType));
            sb.AppendFormat(",\"runtimeType\":\"{0}\"", EscapeJson(request.Selection.RuntimeType));
            sb.AppendFormat(",\"plcName\":\"{0}\"", EscapeJson(request.Selection.PlcName));
            sb.AppendFormat(",\"tiaPath\":\"{0}\"", EscapeJson(request.Selection.TiaPath));
            sb.AppendFormat(",\"language\":\"{0}\"", EscapeJson(request.Selection.Language));

            if (!string.IsNullOrEmpty(request.Selection.Source))
            {
                sb.AppendFormat(",\"source\":\"{0}\"", EscapeJson(request.Selection.Source));
                sb.AppendFormat(",\"sourceFormat\":\"{0}\"", EscapeJson(request.Selection.SourceFormat ?? "xml"));
            }

            sb.Append('}');
        }

        sb.Append('}');
        return sb.ToString();
    }

    private static string EscapeJson(string? value)
    {
        if (value == null) return "";
        return value
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\b", "\\b")
            .Replace("\f", "\\f")
            .Replace("\n", "\\n")
            .Replace("\r", "\\r")
            .Replace("\t", "\\t");
    }

    private static string? ExtractJsonString(string json, string key)
    {
        var search = "\"" + key + "\"";
        var idx = json.IndexOf(search, StringComparison.OrdinalIgnoreCase);
        if (idx < 0) return null;

        idx = json.IndexOf(':', idx + search.Length);
        if (idx < 0) return null;

        idx++;
        while (idx < json.Length && json[idx] == ' ') idx++;

        if (idx >= json.Length) return null;

        if (json[idx] == '"')
        {
            var start = idx + 1;
            var i = start;
            while (i < json.Length)
            {
                if (json[i] == '\\')
                {
                    i += 2;
                    continue;
                }
                if (json[i] == '"')
                    break;
                i++;
            }

            if (i >= json.Length) return null;
            var raw = json.Substring(start, i - start);
            return UnescapeJsonString(raw);
        }

        return null;
    }

    /// <summary>
    /// Unescapes one JSON string value without normalizing its contents.
    /// </summary>
    internal static string UnescapeJsonString(string raw)
    {
        if (string.IsNullOrEmpty(raw))
            return raw;

        var sb = new StringBuilder(raw.Length);
        for (int i = 0; i < raw.Length; i++)
        {
            if (raw[i] == '\\' && i + 1 < raw.Length)
            {
                switch (raw[i + 1])
                {
                    case 'n': sb.Append('\n'); i++; break;
                    case 'r': sb.Append('\r'); i++; break;
                    case 't': sb.Append('\t'); i++; break;
                    case 'b': sb.Append('\b'); i++; break;
                    case 'f': sb.Append('\f'); i++; break;
                    case '"': sb.Append('"'); i++; break;
                    case '\\': sb.Append('\\'); i++; break;
                    case '/': sb.Append('/'); i++; break;
                    case 'u':
                        if (i + 5 < raw.Length)
                        {
                            var hex = raw.Substring(i + 2, 4);
                            if (int.TryParse(hex, System.Globalization.NumberStyles.HexNumber,
                                    System.Globalization.CultureInfo.InvariantCulture, out var codePoint))
                            {
                                if (codePoint >= 0xD800 && codePoint <= 0xDBFF &&
                                    i + 11 < raw.Length && raw[i + 6] == '\\' && raw[i + 7] == 'u')
                                {
                                    var lowHex = raw.Substring(i + 8, 4);
                                    if (int.TryParse(lowHex, System.Globalization.NumberStyles.HexNumber,
                                            System.Globalization.CultureInfo.InvariantCulture, out var lowCode) &&
                                        lowCode >= 0xDC00 && lowCode <= 0xDFFF)
                                    {
                                        var fullCode = 0x10000 + (codePoint - 0xD800) * 0x400 + (lowCode - 0xDC00);
                                        sb.Append(char.ConvertFromUtf32(fullCode));
                                        i += 11;
                                        break;
                                    }
                                }
                                sb.Append((char)codePoint);
                                i += 5;
                            }
                            else
                            {
                                sb.Append(raw[i]);
                            }
                        }
                        else
                        {
                            sb.Append(raw[i]);
                        }
                        break;
                    default:
                        sb.Append(raw[i]);
                        break;
                }
            }
            else
            {
                sb.Append(raw[i]);
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Legacy defense-in-depth helper retained for compatibility with existing tests.
    /// It is not called by the production response path and must not be used to hide
    /// an upstream encoding failure.
    /// </summary>
    internal static string RepairMojibake(string text)
    {
        if (string.IsNullOrEmpty(text) || text.Length < 3)
            return text;

        bool hasHighChars = false;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] >= 0xC0)
            {
                hasHighChars = true;
                break;
            }
        }
        if (!hasHighChars)
            return text;

        try
        {
            var latin1 = Encoding.GetEncoding(28591);
            var bytes = latin1.GetBytes(text);
            var repaired = Encoding.UTF8.GetString(bytes);

            if (repaired != text && !repaired.Contains('�'))
                return repaired;
        }
        catch
        {
            // Compatibility helper only; production does not depend on it.
        }

        return text;
    }

    private static int ExtractJsonInt(string json, string key, int defaultValue = 0)
    {
        var search = "\"" + key + "\"";
        var idx = json.IndexOf(search, StringComparison.OrdinalIgnoreCase);
        if (idx < 0) return defaultValue;

        idx = json.IndexOf(':', idx + search.Length);
        if (idx < 0) return defaultValue;

        idx++;
        while (idx < json.Length && json[idx] == ' ') idx++;

        if (idx >= json.Length) return defaultValue;

        var start = idx;
        while (idx < json.Length && char.IsDigit(json[idx])) idx++;

        if (idx > start && int.TryParse(json.Substring(start, idx - start), out var value))
            return value;

        return defaultValue;
    }

    private static bool ExtractJsonBool(string json, string key, bool defaultValue = false)
    {
        var search = "\"" + key + "\"";
        var idx = json.IndexOf(search, StringComparison.OrdinalIgnoreCase);
        if (idx < 0) return defaultValue;

        idx = json.IndexOf(':', idx + search.Length);
        if (idx < 0) return defaultValue;

        idx++;
        while (idx < json.Length && json[idx] == ' ') idx++;

        if (idx + 4 <= json.Length && json.Substring(idx, 4) == "true")
            return true;
        if (idx + 5 <= json.Length && json.Substring(idx, 5) == "false")
            return false;

        return defaultValue;
    }

    private BridgeHealthResponse ParseHealthResponse(string json)
    {
        return new BridgeHealthResponse
        {
            Status = ExtractJsonString(json, "status") ?? "unknown",
            BridgeVersion = ExtractJsonString(json, "bridgeVersion") ?? "unknown",
            McpConfigured = ExtractJsonBool(json, "mcpConfigured"),
            RuntimeId = ExtractJsonString(json, "runtimeId"),
            RuntimeDisplayName = ExtractJsonString(json, "runtimeDisplayName"),
            RuntimeAvailable = ExtractJsonBool(json, "runtimeAvailable"),
            RuntimeVersion = ExtractJsonString(json, "runtimeVersion"),
            OpenCodeAvailable = ExtractJsonBool(json, "openCodeAvailable") || ExtractJsonBool(json, "runtimeAvailable"),
            OpenCodeVersion = ExtractJsonString(json, "openCodeVersion") ?? ExtractJsonString(json, "runtimeVersion") ?? ""
        };
    }

    private BridgeTaskAccepted ParseTaskAccepted(string json)
    {
        return new BridgeTaskAccepted
        {
            TaskId = ExtractJsonString(json, "taskId") ?? "",
            Status = ExtractJsonString(json, "status") ?? "pending",
            CorrelationId = ExtractJsonString(json, "correlationId") ?? ""
        };
    }

    private BridgeTaskStatus ParseTaskStatus(string json)
    {
        var errorJson = ExtractJsonObject(json, "error");
        BridgeError? error = null;
        if (errorJson != null)
        {
            error = new BridgeError
            {
                Code = ExtractJsonString(errorJson, "code") ?? "",
                Message = ExtractJsonString(errorJson, "message") ?? "",
                Retryable = ExtractJsonBool(errorJson, "retryable")
            };
        }

        var rawResponse = ExtractJsonString(json, "response") ?? "";
        AddInLogger.Info(TextPayloadDiagnostics.DescribeText(
            "8.addin.response-field.extracted", rawResponse));

        return new BridgeTaskStatus
        {
            TaskId = ExtractJsonString(json, "taskId") ?? "",
            Status = ExtractJsonString(json, "status") ?? "",
            Stage = ExtractJsonString(json, "stage") ?? "",
            Message = ExtractJsonString(json, "message") ?? "",
            RuntimeId = ExtractJsonString(json, "runtimeId"),
            RuntimeVersion = ExtractJsonString(json, "runtimeVersion"),
            Response = rawResponse,
            Error = error
        };
    }

    private static string? ExtractJsonObject(string json, string key)
    {
        var search = "\"" + key + "\"";
        var idx = json.IndexOf(search, StringComparison.OrdinalIgnoreCase);
        if (idx < 0) return null;

        idx = json.IndexOf(':', idx + search.Length);
        if (idx < 0) return null;

        idx++;
        while (idx < json.Length && json[idx] == ' ') idx++;

        if (idx >= json.Length || json[idx] != '{') return null;

        var depth = 0;
        var start = idx;
        for (var i = idx; i < json.Length; i++)
        {
            if (json[i] == '{') depth++;
            else if (json[i] == '}')
            {
                depth--;
                if (depth == 0)
                    return json.Substring(start, i - start + 1);
            }
        }

        return null;
    }

    #endregion
}

/// <summary>
/// Exception thrown when a Bridge task operation fails.
/// </summary>
public sealed class BridgeTaskException : Exception
{
    public BridgeTaskException(string message) : base(message) { }
    public BridgeTaskException(string message, Exception inner) : base(message, inner) { }
}
