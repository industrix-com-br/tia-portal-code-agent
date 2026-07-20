#if SIEMENS
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using Siemens.Engineering;
using Siemens.Engineering.AddIn;
using Siemens.Engineering.AddIn.Menu;
using TiaAgent.Contracts.Abstractions;

namespace TiaAgent.AddIn.Providers;

public sealed class ProjectTreeProvider : ProjectTreeAddInProvider
{
    protected override IEnumerable<ContextMenuAddIn> GetContextMenuAddIns()
    {
        yield return new TiaAgentContextMenu();
    }
}

public sealed class TiaAgentContextMenu : ContextMenuAddIn
{
    private const string OpenCodeUrl = "http://127.0.0.1:43120";
    private const string McpEndpoint = "http://127.0.0.1:43121/mcp";

    public TiaAgentContextMenu() : base("AI Assistant") { }

    protected override void BuildContextMenuItems(ContextMenuAddInRoot addInRoot)
    {
        var aiSubmenu = addInRoot.Items.AddSubmenu("AI Assistant");

        aiSubmenu.Items.AddActionItem<IEngineeringObject>(
            "Explain selected object",
            (MenuSelectionProvider<IEngineeringObject> selection) =>
                HandleAction("explain", "Explain this PLC block in detail", selection));

        aiSubmenu.Items.AddActionItem<IEngineeringObject>(
            "Review selected object",
            (MenuSelectionProvider<IEngineeringObject> selection) =>
                HandleAction("review", "Review this PLC code for defects and improvements", selection));

        aiSubmenu.Items.AddActionItem<IEngineeringObject>(
            "Propose change",
            (MenuSelectionProvider<IEngineeringObject> selection) =>
                HandleAction("propose", "Propose a change to this PLC block", selection));
    }

    private void HandleAction(string action, string taskDescription, MenuSelectionProvider<IEngineeringObject> selection)
    {
        try
        {
            Log("HandleAction: " + action);

            var objects = selection.GetSelection();
            var enumerator = objects.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                MessageBox.Show("No object selected.", "TIA Agent", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedObj = enumerator.Current;
            var objName = selectedObj.ToString() ?? "Unknown";
            var objType = selectedObj.GetType().Name;
            Log("Selected: " + objName + " (" + objType + ")");

            // Try OpenCode first, fallback to direct MCP
            string result;
            if (IsOpenCodeRunning())
            {
                Log("Using OpenCode agent runtime");
                result = RunViaOpenCode(action, taskDescription, objName, objType);
            }
            else
            {
                Log("OpenCode not available, using direct MCP");
                result = RunViaMcp(objName, objType);
            }

            Log("Result length: " + result.Length);
            MessageBox.Show(result, "TIA Agent - " + action, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            Log("ERROR: " + ex.ToString());
            MessageBox.Show("Error: " + ex.Message, "TIA Agent", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool IsOpenCodeRunning()
    {
        try
        {
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
            var resp = client.GetAsync(OpenCodeUrl + "/health").GetAwaiter().GetResult();
            return resp.IsSuccessStatusCode;
        }
        catch { return false; }
    }

    private string RunViaOpenCode(string action, string taskDescription, string objName, string objType)
    {
        using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) };

        // 1. Create session
        var sessionBody = "{\"correlationId\":\"tia-" + Guid.NewGuid().ToString("N").Substring(0, 8) + "\",\"tiaSessionId\":\"tia-sim-001\",\"projectId\":\"sim-project-001\",\"defaultAgent\":\"tia-explain\"}";
        var sessionResp = PostJson(client, OpenCodeUrl + "/api/sessions", sessionBody);
        var session = JsonSerializer.Deserialize<JsonElement>(sessionResp);
        var sessionId = session.GetProperty("sessionId").GetString();
        Log("OpenCode session: " + sessionId);

        // 2. Start task
        var taskBody = "{\"sessionId\":\"" + sessionId + "\",\"correlationId\":\"tia-" + Guid.NewGuid().ToString("N").Substring(0, 8) + "\",\"agentId\":\"tia-explain\",\"message\":\"" + taskDescription + " - Object: " + objName + " (" + objType + ")\",\"selectionToken\":\"sel-001\"}";
        var taskResp = PostJson(client, OpenCodeUrl + "/api/sessions/" + sessionId + "/tasks", taskBody);
        var task = JsonSerializer.Deserialize<JsonElement>(taskResp);
        var taskId = task.GetProperty("taskId").GetString();
        Log("OpenCode task: " + taskId);

        // 3. Watch for completion
        var watchReq = new HttpRequestMessage(HttpMethod.Get, OpenCodeUrl + "/api/tasks/" + taskId + "/events");
        watchReq.Headers.Add("Accept", "text/event-stream");
        var watchResp = client.SendAsync(watchReq, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult();

        var sb = new StringBuilder();
        using var stream = watchResp.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
        using var reader = new StreamReader(stream);
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrEmpty(line)) continue;
            if (line.StartsWith("data: ", StringComparison.Ordinal))
            {
                var evt = JsonSerializer.Deserialize<JsonElement>(line.Substring(6));
                var evtType = evt.GetProperty("eventType").GetString();
                var msg = evt.TryGetProperty("message", out var m) ? m.GetString() : "";
                Log("Event: " + evtType + " - " + msg);

                if (evtType == "completed")
                {
                    sb.AppendLine(msg);
                    break;
                }
                else if (evtType == "tool_call")
                {
                    sb.AppendLine("[Tool] " + msg);
                }
                else if (evtType == "progress")
                {
                    sb.AppendLine("[...] " + msg);
                }
            }
        }

        return sb.Length > 0 ? sb.ToString() : "Task completed (no output)";
    }

    private string RunViaMcp(string objName, string objType)
    {
        using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
        var sessionId = InitializeMcpSession(client);
        var response = CallMcpTool(client, sessionId, "tia_get_current_context", "{}");
        return "[MCP Direct] Object: " + objName + " (" + objType + ")\n\nContext:\n" + response;
    }

    private string InitializeMcpSession(HttpClient client)
    {
        var initBody = "{\"jsonrpc\":\"2.0\",\"id\":1,\"method\":\"initialize\",\"params\":{\"protocolVersion\":\"2025-03-26\",\"capabilities\":{},\"clientInfo\":{\"name\":\"tia-addin\",\"version\":\"0.1.0\"}}}";
        var initReq = new HttpRequestMessage(HttpMethod.Post, McpEndpoint)
        {
            Content = new StringContent(initBody, Encoding.UTF8, "application/json")
        };
        initReq.Headers.Add("Accept", "application/json, text/event-stream");
        var initResp = client.SendAsync(initReq).GetAwaiter().GetResult();
        var sessionId = initResp.Headers.Contains("Mcp-Session-Id")
            ? string.Join("", initResp.Headers.GetValues("Mcp-Session-Id"))
            : "";

        var notifBody = "{\"jsonrpc\":\"2.0\",\"method\":\"notifications/initialized\"}";
        var notifReq = new HttpRequestMessage(HttpMethod.Post, McpEndpoint)
        {
            Content = new StringContent(notifBody, Encoding.UTF8, "application/json")
        };
        notifReq.Headers.Add("Accept", "application/json, text/event-stream");
        notifReq.Headers.Add("Mcp-Session-Id", sessionId);
        client.SendAsync(notifReq).GetAwaiter().GetResult();
        return sessionId;
    }

    private string CallMcpTool(HttpClient client, string sessionId, string toolName, string args)
    {
        var body = "{\"jsonrpc\":\"2.0\",\"id\":2,\"method\":\"tools/call\",\"params\":{\"name\":\"" + toolName + "\",\"arguments\":" + args + "}}";
        var req = new HttpRequestMessage(HttpMethod.Post, McpEndpoint)
        {
            Content = new StringContent(body, Encoding.UTF8, "application/json")
        };
        req.Headers.Add("Accept", "application/json, text/event-stream");
        req.Headers.Add("Mcp-Session-Id", sessionId);
        var resp = client.SendAsync(req).GetAwaiter().GetResult();
        var content = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        foreach (var line in content.Split('\n'))
        {
            if (line.StartsWith("data: ", StringComparison.Ordinal))
            {
                return line.Substring(6);
            }
        }
        return content;
    }

    private string PostJson(HttpClient client, string url, string json)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        var resp = client.SendAsync(req).GetAwaiter().GetResult();
        return resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
    }

    private void Log(string message)
    {
        try
        {
            var logPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TiaAgent", "debug.log");
            var dir = System.IO.Path.GetDirectoryName(logPath);
            if (!System.IO.Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);
            System.IO.File.AppendAllText(logPath, DateTime.Now.ToString("HH:mm:ss") + " " + message + Environment.NewLine);
        }
        catch { }
    }
}
#endif
