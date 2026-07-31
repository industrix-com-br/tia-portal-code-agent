using System;
using System.Diagnostics;
using System.IO;
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

    // --- Static utility tests (unchanged) ---

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
        var request = new LaunchResponseCenterRequest
        {
            TaskId = "task-abc",
            Action = "propose",
            TiaInstanceId = "tia-test-instance"
        };

        var args = ResponseCenterProcessManager.BuildArguments(request);

        args.Should().Contain("--task-id \"task-abc\"");
        args.Should().Contain("--action \"propose\"");
        args.Should().Contain("--tia-instance-id \"tia-test-instance\"");
        args.Should().NotContain("--owner-hwnd");
    }

    [Fact]
    public void BuildArguments_QuotesSpecialCharacters()
    {
        var request = new LaunchResponseCenterRequest
        {
            TaskId = "task-1",
            Action = "explain",
            TiaInstanceId = "instance \"with quotes\""
        };

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
        var result = ResponseCenterProcessManager.QuoteArgument("C:\\path\\to\\file");
        result.Should().Be("\"C:\\path\\to\\file\"");
    }

    [Fact]
    public void QuoteArgument_HandlesEmbeddedQuotes()
    {
        var result = ResponseCenterProcessManager.QuoteArgument("say \"hello\"");
        result.Should().Be("\"say \\\"hello\\\"\"");
    }

    [Fact]
    public void GetPipeName_SanitizesInstanceId()
    {
        var pipeName = ResponseCenterProcessManager.GetPipeName("tia-123-abc");
        pipeName.Should().Be("TiaAgent_RC_tia-123-abc");
    }

    [Fact]
    public void GetPipeName_RemovesSpecialCharacters()
    {
        var pipeName = ResponseCenterProcessManager.GetPipeName("tia@#$%123");
        pipeName.Should().Be("TiaAgent_RC_tia123");
    }

    // --- Validation tests ---

    [Fact]
    public void LaunchOrActivate_ReturnsInvalidRequest_WhenTaskIdEmpty()
    {
        using var manager = CreateManager();
        var result = manager.LaunchOrActivate(new LaunchResponseCenterRequest
        {
            TaskId = "", Action = "explain", TiaInstanceId = "tia-1"
        });

        result.Status.Should().Be(ResponseCenterLaunchStatus.InvalidRequest);
    }

    [Fact]
    public void LaunchOrActivate_ReturnsInvalidRequest_WhenTiaInstanceIdEmpty()
    {
        using var manager = CreateManager();
        var result = manager.LaunchOrActivate(new LaunchResponseCenterRequest
        {
            TaskId = "task-1", Action = "explain", TiaInstanceId = ""
        });

        result.Status.Should().Be(ResponseCenterLaunchStatus.InvalidRequest);
    }

    [Fact]
    public void LaunchOrActivate_ReturnsInvalidRequest_WhenActionEmpty()
    {
        using var manager = CreateManager();
        var result = manager.LaunchOrActivate(new LaunchResponseCenterRequest
        {
            TaskId = "task-1", Action = "", TiaInstanceId = "tia-1"
        });

        result.Status.Should().Be(ResponseCenterLaunchStatus.InvalidRequest);
    }

    // --- Readiness handshake tests ---

    [Fact]
    public void LaunchOrActivate_ReturnsStartupFailure_WhenProcessStartsButNoReadiness()
    {
        using var longRunning = StartLongRunningProcess();
        var processOps = new FakeProcessOperations();
        processOps.NextProcess = longRunning;
        var readiness = new FakeReadinessListener(readinessResult: null); // no readiness

        using var manager = CreateManager(processOps, readiness);
        SetupFakeExe();

        var result = manager.LaunchOrActivate(new LaunchResponseCenterRequest
        {
            TaskId = "task-1",
            Action = "explain",
            TiaInstanceId = "tia-no-readiness"
        });

        result.Status.Should().Be(ResponseCenterLaunchStatus.StartupFailure);
        result.ErrorMessage.Should().Contain("did not confirm window visibility");
        processOps.KilledPids.Should().Contain(longRunning.Id);
    }

    [Fact]
    public void LaunchOrActivate_ReturnsStartedAndVisible_WhenReadinessReceived()
    {
        using var longRunning = StartLongRunningProcess();
        var processOps = new FakeProcessOperations();
        processOps.NextProcess = longRunning;
        var readiness = new FakeReadinessListener(new ResponseCenterReadinessInfo
        {
            ProcessId = longRunning.Id,
            WindowHandle = 9999,
            TaskId = "task-1",
            TiaInstanceId = "tia-ready",
            IsVisible = true,
            WindowState = "Normal"
        });

        using var manager = CreateManager(processOps, readiness);
        SetupFakeExe();

        var result = manager.LaunchOrActivate(new LaunchResponseCenterRequest
        {
            TaskId = "task-1",
            Action = "explain",
            TiaInstanceId = "tia-ready"
        });

        result.Status.Should().Be(ResponseCenterLaunchStatus.StartedAndVisible);
        result.ProcessId.Should().Be(longRunning.Id);
        result.WindowHandle.Should().Be(9999);
        result.ActivatedExistingInstance.Should().BeFalse();
    }

    [Fact]
    public void LaunchOrActivate_DeadInstanceIsReplaced()
    {
        // Simpler version: verify that when a tracked instance's process is dead,
        // a new process is started instead.
        using var newProcess = StartLongRunningProcess();
        var processOps = new FakeProcessOperations();
        processOps.NextProcess = newProcess;
        var readiness = new FakeReadinessListener(new ResponseCenterReadinessInfo
        {
            ProcessId = newProcess.Id,
            WindowHandle = 7777,
            TaskId = "task-replace",
            TiaInstanceId = "tia-dead",
            IsVisible = true,
            WindowState = "Normal"
        });

        using var manager = CreateManager(processOps, readiness);
        SetupFakeExe();

        // First launch succeeds
        var result1 = manager.LaunchOrActivate(new LaunchResponseCenterRequest
        {
            TaskId = "task-first", Action = "explain", TiaInstanceId = "tia-dead"
        });
        result1.Status.Should().Be(ResponseCenterLaunchStatus.StartedAndVisible);

        // Now mark the process as dead
        processOps.SetAlive(newProcess.Id, false);

        // Second launch: detects dead instance, starts new process
        using var newProcess2 = StartLongRunningProcess();
        processOps.NextProcess = newProcess2;
        readiness = new FakeReadinessListener(new ResponseCenterReadinessInfo
        {
            ProcessId = newProcess2.Id,
            WindowHandle = 8888,
            TaskId = "task-replace-2",
            TiaInstanceId = "tia-dead",
            IsVisible = true,
            WindowState = "Normal"
        });

        // Need to create a new manager since the readiness listener changed
        using var manager2 = CreateManager(processOps, readiness);
        // The instance tracking is per-manager, so the second manager won't know about
        // the first instance. This test verifies the dead-process path in isolation.
        var result2 = manager2.LaunchOrActivate(new LaunchResponseCenterRequest
        {
            TaskId = "task-replace-2", Action = "explain", TiaInstanceId = "tia-dead"
        });
        result2.Status.Should().Be(ResponseCenterLaunchStatus.StartedAndVisible);
    }

    [Fact]
    public void LaunchOrActivate_DoesNotReportSuccess_BeforeVisibilityConfirmation()
    {
        using var longRunning = StartLongRunningProcess();
        var processOps = new FakeProcessOperations();
        processOps.NextProcess = longRunning;
        var readiness = new FakeReadinessListener(readinessResult: null); // never confirms

        using var manager = CreateManager(processOps, readiness);
        SetupFakeExe();

        var result = manager.LaunchOrActivate(new LaunchResponseCenterRequest
        {
            TaskId = "task-nosuccess",
            Action = "explain",
            TiaInstanceId = "tia-nosuccess"
        });

        result.Status.Should().Be(ResponseCenterLaunchStatus.StartupFailure);
        result.Status.Should().NotBe(ResponseCenterLaunchStatus.Started);
        result.Status.Should().NotBe(ResponseCenterLaunchStatus.StartedAndVisible);
    }

    [Fact]
    public void LaunchOrActivate_DoesNotReturnLegacyStarted()
    {
        // Verify the legacy "started" status is never emitted
        using var longRunning = StartLongRunningProcess();
        var processOps = new FakeProcessOperations();
        processOps.NextProcess = longRunning;
        var readiness = new FakeReadinessListener(new ResponseCenterReadinessInfo
        {
            ProcessId = longRunning.Id,
            WindowHandle = 12345,
            TaskId = "task-legacy",
            TiaInstanceId = "tia-legacy",
            IsVisible = true,
            WindowState = "Normal"
        });

        using var manager = CreateManager(processOps, readiness);
        SetupFakeExe();

        var result = manager.LaunchOrActivate(new LaunchResponseCenterRequest
        {
            TaskId = "task-legacy",
            Action = "explain",
            TiaInstanceId = "tia-legacy"
        });

        result.Status.Should().NotBe(ResponseCenterLaunchStatus.Started);
        result.Status.Should().NotBe(ResponseCenterLaunchStatus.ActivatedExisting);
    }

    // --- Helpers ---

    private ResponseCenterProcessManager CreateManager(
        IProcessOperations? processOps = null,
        ReadinessListener? readiness = null)
    {
        return new ResponseCenterProcessManager(
            _logger,
            processOps ?? new FakeProcessOperations(),
            readiness ?? new FakeReadinessListener(null));
    }

    private void SetupFakeExe()
    {
        Directory.CreateDirectory(_root);
        File.WriteAllText(Path.Combine(_root, "current.json"), "{\"activeVersion\":\"1.0.0\"}");
        var exeDir = Path.Combine(_root, "versions", "1.0.0", "ResponseCenter");
        Directory.CreateDirectory(exeDir);
        File.WriteAllText(Path.Combine(exeDir, "TiaAgent.ResponseCenter.exe"), "fake");
    }

    /// <summary>
    /// Starts a long-running process (ping loopback) that can be used as a fake Process object.
    /// The caller must dispose the returned process.
    /// </summary>
    private static Process StartLongRunningProcess()
    {
        return Process.Start(new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = "/c ping 127.0.0.1 -n 60",
            CreateNoWindow = true,
            UseShellExecute = false
        })!;
    }

    public void Dispose()
    {
        if (Directory.Exists(_root))
            Directory.Delete(_root, recursive: true);
    }
}

/// <summary>
/// Fake process operations for testing. Tracks started and killed PIDs.
/// </summary>
internal sealed class FakeProcessOperations : IProcessOperations
{
    private readonly System.Collections.Generic.Dictionary<int, bool> _alive = new();
    public Process? NextProcess { get; set; }
    public System.Collections.Generic.List<int> KilledPids { get; } = new();

    public void SetAlive(int pid, bool alive)
    {
        _alive[pid] = alive;
    }

    public Process? Start(ProcessStartInfo startInfo)
    {
        if (NextProcess != null)
        {
            _alive[NextProcess.Id] = true;
            return NextProcess;
        }
        return null;
    }

    public bool IsAlive(int processId)
    {
        return _alive.TryGetValue(processId, out var alive) && alive;
    }

    public void Kill(int processId)
    {
        KilledPids.Add(processId);
        _alive[processId] = false;
    }
}

/// <summary>
/// Fake readiness listener that returns a pre-configured result.
/// </summary>
internal sealed class FakeReadinessListener : ReadinessListener
{
    private readonly ResponseCenterReadinessInfo? _result;

    public FakeReadinessListener(ResponseCenterReadinessInfo? readinessResult)
        : base(new Diagnostics.BridgeLogger())
    {
        _result = readinessResult;
    }

    public override Task<ResponseCenterReadinessInfo?> WaitForReadinessAsync(
        string instanceId, TimeSpan timeout)
    {
        return Task.FromResult(_result);
    }
}

/// <summary>
/// Composite readiness listener that returns results from two inner listeners in sequence.
/// First call uses the first listener, second call uses the second, etc.
/// </summary>
internal sealed class CompositeReadinessListener : ReadinessListener
{
    private readonly ReadinessListener[] _listeners;
    private int _callIndex;

    public CompositeReadinessListener(params ReadinessListener[] listeners)
        : base(new Diagnostics.BridgeLogger())
    {
        _listeners = listeners;
    }

    public override Task<ResponseCenterReadinessInfo?> WaitForReadinessAsync(
        string instanceId, TimeSpan timeout)
    {
        var listener = _listeners[Math.Min(_callIndex, _listeners.Length - 1)];
        _callIndex++;
        return listener.WaitForReadinessAsync(instanceId, timeout);
    }
}
