using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TiaAgent.Bridge.ResponseCenter;
using TiaAgent.Contracts.Bridge;
using Xunit;

namespace TiaAgent.Bridge.Tests;

public sealed class ResponseCenterProcessManagerTests : IDisposable
{
    private readonly string _root = Path.Combine(
        Path.GetTempPath(),
        "tia-agent-rc-process-tests",
        Guid.NewGuid().ToString("N"));

    private readonly Diagnostics.BridgeLogger _logger = new();
    private readonly List<Process> _startedProcesses = new();

    [Fact]
    public void ResolveExecutablePath_ReturnsNull_WhenManifestMissing()
    {
        var path = ResponseCenterProcessManager.ResolveExecutablePath(_root);
        path.Should().BeNull();
    }

    [Fact]
    public void ResolveExecutablePath_UsesActiveVersion()
    {
        Directory.CreateDirectory(_root);
        File.WriteAllText(
            Path.Combine(_root, "current.json"),
            "{\"schemaVersion\":1,\"activeVersion\":\"0.5.0\"}");

        var path = ResponseCenterProcessManager.ResolveExecutablePath(_root);

        path.Should().Be(Path.Combine(
            _root, "versions", "0.5.0", "ResponseCenter", "TiaAgent.ResponseCenter.exe"));
    }

    [Fact]
    public void ParseActiveVersion_ExtractsVersion()
    {
        var version = ResponseCenterProcessManager.ParseActiveVersion(
            "{\"activeVersion\":\"1.2.3-alpha.1\"}");

        version.Should().Be("1.2.3-alpha.1");
    }

    [Fact]
    public void ParseActiveVersion_ReturnsNull_WhenEmpty()
    {
        ResponseCenterProcessManager.ParseActiveVersion("").Should().BeNull();
        ResponseCenterProcessManager.ParseActiveVersion(null!).Should().BeNull();
    }

    [Fact]
    public void BuildArguments_IncludesAllFields()
    {
        var request = CreateRequest("task-abc", "tia-test-instance", "propose");

        var args = ResponseCenterProcessManager.BuildArguments(request);

        args.Should().Contain("--task-id \"task-abc\"");
        args.Should().Contain("--action \"propose\"");
        args.Should().Contain("--tia-instance-id \"tia-test-instance\"");
        args.Should().NotContain("--owner-hwnd");
    }

    [Fact]
    public void BuildArguments_QuotesSpecialCharacters()
    {
        var request = CreateRequest("task-1", "instance \"with quotes\"", "explain");

        var args = ResponseCenterProcessManager.BuildArguments(request);

        args.Should().Contain("--tia-instance-id \"instance \\\"with quotes\\\"\"");
    }

    [Fact]
    public void QuoteArgument_HandlesEmptyString()
    {
        ResponseCenterProcessManager.QuoteArgument("").Should().Be("\"\"");
    }

    [Fact]
    public void QuoteArgument_HandlesBackslashes()
    {
        ResponseCenterProcessManager.QuoteArgument("C:\\path\\to\\file")
            .Should().Be("\"C:\\path\\to\\file\"");
    }

    [Fact]
    public void QuoteArgument_HandlesEmbeddedQuotes()
    {
        ResponseCenterProcessManager.QuoteArgument("say \"hello\"")
            .Should().Be("\"say \\\"hello\\\"\"");
    }

    [Fact]
    public void GetPipeName_SanitizesInstanceId()
    {
        ResponseCenterProcessManager.GetPipeName("tia-123-abc")
            .Should().Be("TiaAgent_RC_tia-123-abc");
    }

    [Fact]
    public void GetPipeName_RemovesSpecialCharacters()
    {
        ResponseCenterProcessManager.GetPipeName("tia@#$%123")
            .Should().Be("TiaAgent_RC_tia123");
    }

    [Fact]
    public void LaunchOrActivate_ReturnsInvalidRequest_WhenTaskIdEmpty()
    {
        using var manager = CreateManager();
        var result = manager.LaunchOrActivate(CreateRequest("", "tia-1", "explain"));

        result.Status.Should().Be(ResponseCenterLaunchStatus.InvalidRequest);
    }

    [Fact]
    public void LaunchOrActivate_ReturnsInvalidRequest_WhenTiaInstanceIdEmpty()
    {
        using var manager = CreateManager();
        var result = manager.LaunchOrActivate(CreateRequest("task-1", "", "explain"));

        result.Status.Should().Be(ResponseCenterLaunchStatus.InvalidRequest);
    }

    [Fact]
    public void LaunchOrActivate_ReturnsInvalidRequest_WhenActionEmpty()
    {
        using var manager = CreateManager();
        var result = manager.LaunchOrActivate(CreateRequest("task-1", "tia-1", ""));

        result.Status.Should().Be(ResponseCenterLaunchStatus.InvalidRequest);
    }

    [Fact]
    public void LaunchOrActivate_ReturnsStartupFailure_WhenProcessStartsButNoReadiness()
    {
        var process = StartLongRunningProcess();
        var processOps = new FakeProcessOperations { NextProcess = process };

        using var manager = CreateManager(
            processOps,
            new FakeReadinessListener(null),
            new SequenceActivationClient(false));
        SetupFakeExe();

        var result = manager.LaunchOrActivate(
            CreateRequest("task-1", "tia-no-readiness", "explain"));

        result.Status.Should().Be(ResponseCenterLaunchStatus.StartupFailure);
        result.ErrorMessage.Should().Contain("did not confirm");
        processOps.KilledPids.Should().Contain(process.Id);
    }

    [Fact]
    public void LaunchOrActivate_ReturnsStartedAndVisible_WhenReadinessReceived()
    {
        var process = StartLongRunningProcess();
        var processOps = new FakeProcessOperations { NextProcess = process };
        var request = CreateRequest("task-1", "tia-ready", "explain");
        var readiness = CreateReadiness(request, process.Id, 9999);

        using var manager = CreateManager(
            processOps,
            new FakeReadinessListener(readiness),
            new SequenceActivationClient(false));
        SetupFakeExe();

        var result = manager.LaunchOrActivate(request);

        result.Status.Should().Be(ResponseCenterLaunchStatus.StartedAndVisible);
        result.ProcessId.Should().Be(process.Id);
        result.WindowHandle.Should().Be(9999);
        result.ActivatedExistingInstance.Should().BeFalse();
    }

    [Fact]
    public void LaunchOrActivate_RejectsReadinessForAnotherTask()
    {
        var process = StartLongRunningProcess();
        var processOps = new FakeProcessOperations { NextProcess = process };
        var request = CreateRequest("task-expected", "tia-ready", "explain");
        var wrongRequest = CreateRequest("task-other", "tia-ready", "explain");

        using var manager = CreateManager(
            processOps,
            new FakeReadinessListener(CreateReadiness(wrongRequest, process.Id, 9999)),
            new SequenceActivationClient(false));
        SetupFakeExe();

        var result = manager.LaunchOrActivate(request);

        result.Status.Should().Be(ResponseCenterLaunchStatus.StartupFailure);
        processOps.KilledPids.Should().Contain(process.Id);
    }

    [Fact]
    public void LaunchOrActivate_DeadTrackedInstanceIsReplaced()
    {
        var firstProcess = StartLongRunningProcess();
        var secondProcess = StartLongRunningProcess();
        var processOps = new FakeProcessOperations { NextProcess = firstProcess };
        var firstRequest = CreateRequest("task-first", "tia-dead", "explain");
        var secondRequest = CreateRequest("task-second", "tia-dead", "review");
        var readiness = new CompositeReadinessListener(
            CreateReadiness(firstRequest, firstProcess.Id, 7777),
            CreateReadiness(secondRequest, secondProcess.Id, 8888));

        using var manager = CreateManager(
            processOps,
            readiness,
            new SequenceActivationClient(false, false));
        SetupFakeExe();

        var firstResult = manager.LaunchOrActivate(firstRequest);
        firstResult.Status.Should().Be(ResponseCenterLaunchStatus.StartedAndVisible);

        processOps.SetAlive(firstProcess.Id, false);
        processOps.NextProcess = secondProcess;

        var secondResult = manager.LaunchOrActivate(secondRequest);

        secondResult.Status.Should().Be(ResponseCenterLaunchStatus.StartedAndVisible);
        secondResult.ProcessId.Should().Be(secondProcess.Id);
        processOps.StartCount.Should().Be(2);
    }

    [Fact]
    public void LaunchOrActivate_RecoversExistingInstanceAfterBridgeRestart()
    {
        var request = CreateRequest("task-recovered", "tia-recovered", "review");
        var processOps = new FakeProcessOperations();
        var activation = new SequenceActivationClient(true);

        using var manager = CreateManager(
            processOps,
            new FakeReadinessListener(CreateReadiness(request, 4321, 8765)),
            activation);

        var result = manager.LaunchOrActivate(request);

        result.Status.Should().Be(ResponseCenterLaunchStatus.ActivatedAndVisible);
        result.ActivatedExistingInstance.Should().BeTrue();
        result.ProcessId.Should().Be(4321);
        processOps.StartCount.Should().Be(0);
        activation.NotifyCount.Should().Be(1);
    }

    [Fact]
    public async Task LaunchOrActivate_SerializesConcurrentRequestsPerTiaInstance()
    {
        var process = StartLongRunningProcess();
        var processOps = new FakeProcessOperations { NextProcess = process };
        var request = CreateRequest("task-concurrent", "tia-concurrent", "explain");
        var activation = new SequenceActivationClient(false, true);

        using var manager = CreateManager(
            processOps,
            new FakeReadinessListener(CreateReadiness(request, process.Id, 5555)),
            activation);
        SetupFakeExe();

        var first = Task.Run(() => manager.LaunchOrActivate(request));
        var second = Task.Run(() => manager.LaunchOrActivate(request));
        var results = await Task.WhenAll(first, second);

        processOps.StartCount.Should().Be(1);
        results.Select(result => result.Status).Should().Contain(ResponseCenterLaunchStatus.StartedAndVisible);
        results.Select(result => result.Status).Should().Contain(ResponseCenterLaunchStatus.ActivatedAndVisible);
    }

    [Fact]
    public void LaunchOrActivate_DoesNotReportSuccessBeforeVisibilityConfirmation()
    {
        var process = StartLongRunningProcess();
        var processOps = new FakeProcessOperations { NextProcess = process };

        using var manager = CreateManager(
            processOps,
            new FakeReadinessListener(null),
            new SequenceActivationClient(false));
        SetupFakeExe();

        var result = manager.LaunchOrActivate(
            CreateRequest("task-nosuccess", "tia-nosuccess", "explain"));

        result.Status.Should().Be(ResponseCenterLaunchStatus.StartupFailure);
        result.Status.Should().NotBe(ResponseCenterLaunchStatus.Started);
        result.Status.Should().NotBe(ResponseCenterLaunchStatus.StartedAndVisible);
    }

    [Fact]
    public void LaunchOrActivate_DoesNotReturnLegacyStarted()
    {
        var process = StartLongRunningProcess();
        var processOps = new FakeProcessOperations { NextProcess = process };
        var request = CreateRequest("task-legacy", "tia-legacy", "explain");

        using var manager = CreateManager(
            processOps,
            new FakeReadinessListener(CreateReadiness(request, process.Id, 12345)),
            new SequenceActivationClient(false));
        SetupFakeExe();

        var result = manager.LaunchOrActivate(request);

        result.Status.Should().NotBe(ResponseCenterLaunchStatus.Started);
        result.Status.Should().NotBe(ResponseCenterLaunchStatus.ActivatedExisting);
    }

    private ResponseCenterProcessManager CreateManager(
        IProcessOperations? processOps = null,
        ReadinessListener? readiness = null,
        IResponseCenterActivationClient? activationClient = null)
    {
        return new ResponseCenterProcessManager(
            _logger,
            processOps ?? new FakeProcessOperations(),
            readiness ?? new FakeReadinessListener(null),
            activationClient ?? new SequenceActivationClient(false),
            _root);
    }

    private void SetupFakeExe()
    {
        Directory.CreateDirectory(_root);
        File.WriteAllText(
            Path.Combine(_root, "current.json"),
            "{\"activeVersion\":\"1.0.0\"}");

        var exeDir = Path.Combine(_root, "versions", "1.0.0", "ResponseCenter");
        Directory.CreateDirectory(exeDir);
        File.WriteAllText(Path.Combine(exeDir, "TiaAgent.ResponseCenter.exe"), "fake");
    }

    private Process StartLongRunningProcess()
    {
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = "/c ping 127.0.0.1 -n 60",
            CreateNoWindow = true,
            UseShellExecute = false
        })!;

        _startedProcesses.Add(process);
        return process;
    }

    private static LaunchResponseCenterRequest CreateRequest(
        string taskId,
        string tiaInstanceId,
        string action)
    {
        return new LaunchResponseCenterRequest
        {
            TaskId = taskId,
            TiaInstanceId = tiaInstanceId,
            Action = action
        };
    }

    private static ResponseCenterReadinessInfo CreateReadiness(
        LaunchResponseCenterRequest request,
        int processId,
        long windowHandle)
    {
        return new ResponseCenterReadinessInfo
        {
            ProcessId = processId,
            WindowHandle = windowHandle,
            TaskId = request.TaskId,
            TiaInstanceId = request.TiaInstanceId,
            IsVisible = true,
            WindowState = "Normal"
        };
    }

    public void Dispose()
    {
        foreach (var process in _startedProcesses)
        {
            try
            {
                if (!process.HasExited)
                    process.Kill();
            }
            catch
            {
                // Process already stopped.
            }
            finally
            {
                process.Dispose();
            }
        }

        if (Directory.Exists(_root))
            Directory.Delete(_root, recursive: true);
    }
}

internal sealed class FakeProcessOperations : IProcessOperations
{
    private readonly ConcurrentDictionary<int, bool> _alive = new();
    private int _startCount;

    public Process? NextProcess { get; set; }
    public List<int> KilledPids { get; } = new();
    public int StartCount => _startCount;

    public void SetAlive(int pid, bool alive)
    {
        _alive[pid] = alive;
    }

    public Process? Start(ProcessStartInfo startInfo)
    {
        Interlocked.Increment(ref _startCount);
        var process = NextProcess;
        if (process != null)
            _alive[process.Id] = true;
        return process;
    }

    public bool IsAlive(int processId)
    {
        return _alive.TryGetValue(processId, out var alive) && alive;
    }

    public void Kill(int processId)
    {
        lock (KilledPids)
        {
            KilledPids.Add(processId);
        }
        _alive[processId] = false;
    }
}

internal sealed class SequenceActivationClient : IResponseCenterActivationClient
{
    private readonly Queue<bool> _results;
    private readonly object _lock = new();

    public SequenceActivationClient(params bool[] results)
    {
        _results = new Queue<bool>(results);
    }

    public int NotifyCount { get; private set; }

    public bool Notify(LaunchResponseCenterRequest request)
    {
        lock (_lock)
        {
            NotifyCount++;
            return _results.Count > 0 && _results.Dequeue();
        }
    }
}

internal sealed class FakeReadinessListener : ReadinessListener
{
    private readonly ResponseCenterReadinessInfo? _result;

    public FakeReadinessListener(ResponseCenterReadinessInfo? readinessResult)
        : base(new Diagnostics.BridgeLogger())
    {
        _result = readinessResult;
    }

    public override Task<ResponseCenterReadinessInfo?> WaitForReadinessAsync(
        string instanceId,
        TimeSpan timeout)
    {
        return Task.FromResult(_result);
    }
}

internal sealed class CompositeReadinessListener : ReadinessListener
{
    private readonly Queue<ResponseCenterReadinessInfo?> _results;
    private readonly object _lock = new();

    public CompositeReadinessListener(params ResponseCenterReadinessInfo?[] results)
        : base(new Diagnostics.BridgeLogger())
    {
        _results = new Queue<ResponseCenterReadinessInfo?>(results);
    }

    public override Task<ResponseCenterReadinessInfo?> WaitForReadinessAsync(
        string instanceId,
        TimeSpan timeout)
    {
        lock (_lock)
        {
            return Task.FromResult(_results.Count > 0 ? _results.Dequeue() : null);
        }
    }
}
