using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TiaAgent.ResponseCenter.Models;
using TiaAgent.ResponseCenter.Services;

namespace TiaAgent.ResponseCenter.ViewModels;

/// <summary>
/// Main ViewModel for the Agent Response Center window.
/// Drives the UI state machine and coordinates between the BridgeTaskMonitor and the view.
/// </summary>
public sealed class AgentResponseViewModel : INotifyPropertyChanged, IDisposable
{
    private readonly BridgeTaskMonitor _monitor;
    private readonly AgentResponseContext _context;

    // State
    private AgentTaskState _state = AgentTaskState.Created;
    private string _statusMessage = "";
    private string _progressMessage = "";
    private string _responseContent = "";
    private string _runtimeId = "";

    // Derived
    private bool _isBusy;
    private bool _canCancel;
    private bool _canRetry;
    private bool _canClose = true;
    private bool _showProgress;
    private bool _showApproval;
    private bool _showResponse;
    private bool _showError;

    // Minimalist UI additions
    private string _userInput = "";
    private bool _showTechnicalDetails;

    public AgentResponseViewModel(AgentResponseContext context, BridgeTaskMonitor monitor)
    {
        _context = context;
        _monitor = monitor;

        // Wire monitor events
        _monitor.StateChanged += OnStateChanged;
        _monitor.ResponseReceived += OnResponseReceived;
        _monitor.ErrorOccurred += OnErrorOccurred;
        _monitor.PollingError += OnPollingError;

        // Commands
        CancelCommand = new AsyncRelayCommand(ExecuteCancelAsync, () => CanCancel);
        RetryCommand = new RelayCommand(ExecuteRetry, () => CanRetry);
        CopyCommand = new RelayCommand(ExecuteCopy, () => ShowResponse);
        CopyResponseCommand = new RelayCommand(ExecuteCopyResponse, () => ShowResponse);
        CloseCommand = new RelayCommand(ExecuteClose);
        SendCommand = new RelayCommand(ExecuteSend, () => CanSend);
        ToggleTechnicalDetailsCommand = new RelayCommand(ExecuteToggleTechnicalDetails);

        // Set initial context values
        OnPropertyChanged(nameof(ActionDisplay));
        OnPropertyChanged(nameof(ActionId));
        OnPropertyChanged(nameof(ObjectName));
        OnPropertyChanged(nameof(ObjectType));
        OnPropertyChanged(nameof(PlcName));
        OnPropertyChanged(nameof(ProjectName));
        OnPropertyChanged(nameof(CorrelationId));
        OnPropertyChanged(nameof(HeaderDisplay));
    }

    #region Context Properties

    public string ActionDisplay => _context.ActionDisplay;
    public string ActionId => _context.Action;
    public string ObjectName => _context.ObjectName;
    public string ObjectType => _context.ObjectType;
    public string? PlcName => _context.PlcName;
    public string? ProjectName => _context.ProjectName;
    public string? CorrelationId => _context.CorrelationId;

    /// <summary>
    /// Combined header display: action + object name + type.
    /// Examples: "Explain code · OB1 · Main", "Review code · FB12 · ConveyorControl"
    /// </summary>
    public string HeaderDisplay
    {
        get
        {
            var actionDisplay = ActionId.ToLowerInvariant() switch
            {
                "explain" => Strings.ActionExplain,
                "review" => Strings.ActionReview,
                "propose" => Strings.ActionPropose,
                _ => ActionDisplay
            };

            if (!string.IsNullOrEmpty(ObjectName))
            {
                var objectInfo = string.IsNullOrEmpty(ObjectType)
                    ? ObjectName
                    : $"{ObjectName} · {ObjectType}";
                return $"{actionDisplay} · {objectInfo}";
            }

            return actionDisplay;
        }
    }

    #endregion

    #region State Properties

    public AgentTaskState State
    {
        get => _state;
        private set => SetProperty(ref _state, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public string ProgressMessage
    {
        get => _progressMessage;
        private set => SetProperty(ref _progressMessage, value);
    }

    public string ResponseContent
    {
        get => _responseContent;
        private set => SetProperty(ref _responseContent, value);
    }

    public string RuntimeId
    {
        get => _runtimeId;
        private set => SetProperty(ref _runtimeId, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        private set
        {
            if (SetProperty(ref _isBusy, value))
            {
                OnPropertyChanged(nameof(ShowLoading));
                OnPropertyChanged(nameof(ShowTextInput));
            }
        }
    }

    public bool CanCancel
    {
        get => _canCancel;
        private set
        {
            if (SetProperty(ref _canCancel, value))
                OnPropertyChanged(nameof(CancelButtonText));
        }
    }

    public bool CanRetry
    {
        get => _canRetry;
        private set => SetProperty(ref _canRetry, value);
    }

    public bool CanClose
    {
        get => _canClose;
        private set => SetProperty(ref _canClose, value);
    }

    public bool ShowProgress
    {
        get => _showProgress;
        private set => SetProperty(ref _showProgress, value);
    }

    public bool ShowApproval
    {
        get => _showApproval;
        private set => SetProperty(ref _showApproval, value);
    }

    public bool ShowResponse
    {
        get => _showResponse;
        private set
        {
            if (SetProperty(ref _showResponse, value))
            {
                OnPropertyChanged(nameof(ShowCopyButton));
                OnPropertyChanged(nameof(ShowTextInput));
            }
        }
    }

    public bool ShowError
    {
        get => _showError;
        private set => SetProperty(ref _showError, value);
    }

    public string CancelButtonText => Strings.ButtonCancel;

    #endregion

    #region Minimalist UI Properties

    /// <summary>Shows loading state when busy and not showing response/error.</summary>
    public bool ShowLoading => IsBusy && !ShowResponse && !ShowError;

    /// <summary>Shows text input when not busy and not showing error.</summary>
    public bool ShowTextInput => !IsBusy && !ShowError;

    /// <summary>Shows copy button when response is visible.</summary>
    public bool ShowCopyButton => ShowResponse && !string.IsNullOrEmpty(ResponseContent);

    /// <summary>User input from the text box (for future conversational follow-up).</summary>
    public string UserInput
    {
        get => _userInput;
        set => SetProperty(ref _userInput, value);
    }

    /// <summary>Whether the Send button can be executed.</summary>
    public bool CanSend => !string.IsNullOrWhiteSpace(UserInput) && !IsBusy;

    /// <summary>Whether technical details are expanded in error state.</summary>
    public bool ShowTechnicalDetails
    {
        get => _showTechnicalDetails;
        set => SetProperty(ref _showTechnicalDetails, value);
    }

    /// <summary>Technical details text for the expander.</summary>
    public string TechnicalDetails => ErrorDetails?.TechnicalDetails ?? "";

    #endregion

    #region Child ViewModels

    private ErrorDetailsViewModel? _errorDetails;
    public ErrorDetailsViewModel? ErrorDetails
    {
        get => _errorDetails;
        private set
        {
            if (SetProperty(ref _errorDetails, value))
                OnPropertyChanged(nameof(TechnicalDetails));
        }
    }

    private ApprovalPreviewViewModel? _approvalPreview;
    public ApprovalPreviewViewModel? ApprovalPreview
    {
        get => _approvalPreview;
        private set => SetProperty(ref _approvalPreview, value);
    }

    #endregion

    #region Commands

    public ICommand CancelCommand { get; }
    public ICommand RetryCommand { get; }
    public ICommand CopyCommand { get; }
    public ICommand CopyResponseCommand { get; }
    public ICommand CloseCommand { get; }
    public ICommand SendCommand { get; }
    public ICommand ToggleTechnicalDetailsCommand { get; }

    #endregion

    #region Public Methods

    /// <summary>Starts monitoring the task.</summary>
    public void StartMonitoring()
    {
        _monitor.Start();
    }

    #endregion

    #region Command Handlers

    private async Task ExecuteCancelAsync()
    {
        StatusMessage = "Cancelling…";
        CanCancel = false;
        await _monitor.CancelAsync().ConfigureAwait(false);
    }

    private void ExecuteRetry()
    {
        // Reset state and restart
        ShowError = false;
        ShowResponse = false;
        ShowProgress = true;
        ShowApproval = false;
        IsBusy = true;
        CanRetry = false;
        CanCancel = true;
        ErrorDetails = null;
        ResponseContent = "";
        StatusMessage = Strings.StatusSubmitting;
        ProgressMessage = Strings.ProgressSubmitting;

        // Re-create monitor with same context
        _monitor.StateChanged += OnStateChanged;
        _monitor.ResponseReceived += OnResponseReceived;
        _monitor.ErrorOccurred += OnErrorOccurred;
        _monitor.PollingError += OnPollingError;
        _monitor.Start();
    }

    private void ExecuteCopy()
    {
        try
        {
            if (!string.IsNullOrEmpty(ResponseContent))
            {
                Clipboard.SetText(ResponseContent);
            }
        }
        catch
        {
            // Clipboard access can fail in some environments
        }
    }

    private void ExecuteCopyResponse()
    {
        ExecuteCopy();
    }

    private void ExecuteSend()
    {
        // Placeholder for future conversational follow-up
        // For now, just clear the input
        UserInput = "";
    }

    private void ExecuteToggleTechnicalDetails()
    {
        ShowTechnicalDetails = !ShowTechnicalDetails;
        OnPropertyChanged(nameof(TechnicalDetails));
    }

    private void ExecuteClose()
    {
        _monitor.Stop();
        RequestClose?.Invoke();
    }

    #endregion

    #region Monitor Event Handlers

    private void OnStateChanged(AgentTaskState state, string? stage, string? message)
    {
        // Marshal to UI thread
        Application.Current?.Dispatcher.Invoke(() =>
        {
            State = state;

            switch (state)
            {
                case AgentTaskState.Created:
                    StatusMessage = Strings.StatusCreated;
                    break;

                case AgentTaskState.Submitting:
                    StatusMessage = Strings.StatusSubmitting;
                    ProgressMessage = Strings.ProgressSubmitting;
                    IsBusy = true;
                    ShowProgress = true;
                    CanCancel = false;
                    CanRetry = false;
                    break;

                case AgentTaskState.Queued:
                    StatusMessage = Strings.StatusQueued;
                    ProgressMessage = FormatStageMessage(stage, Strings.ProgressQueued);
                    IsBusy = true;
                    ShowProgress = true;
                    CanCancel = true;
                    CanRetry = false;
                    break;

                case AgentTaskState.Running:
                    StatusMessage = Strings.StatusRunning;
                    ProgressMessage = FormatStageMessage(stage, Strings.ProgressDefault);
                    IsBusy = true;
                    ShowProgress = true;
                    CanCancel = true;
                    CanRetry = false;
                    break;

                case AgentTaskState.WaitingForApproval:
                    StatusMessage = Strings.StatusWaitingForApproval;
                    ProgressMessage = message ?? "";
                    IsBusy = false;
                    ShowProgress = false;
                    ShowApproval = true;
                    CanCancel = false;
                    CanRetry = false;
                    ApprovalPreview ??= new ApprovalPreviewViewModel();
                    ApprovalPreview.IsExpanded = true;
                    break;

                case AgentTaskState.Completed:
                    StatusMessage = Strings.StatusCompleted;
                    IsBusy = false;
                    ShowProgress = false;
                    CanCancel = false;
                    CanRetry = false;
                    ShowResponse = true;
                    break;

                case AgentTaskState.Failed:
                    StatusMessage = Strings.StatusFailed;
                    ProgressMessage = "";
                    IsBusy = false;
                    ShowProgress = false;
                    CanCancel = false;
                    CanRetry = true;
                    break;

                case AgentTaskState.Cancelled:
                    StatusMessage = Strings.StatusCancelled;
                    ProgressMessage = "";
                    IsBusy = false;
                    ShowProgress = false;
                    CanCancel = false;
                    CanRetry = false;
                    break;

                case AgentTaskState.Disconnected:
                    StatusMessage = Strings.StatusDisconnected;
                    ProgressMessage = "";
                    IsBusy = false;
                    ShowProgress = false;
                    CanCancel = false;
                    CanRetry = true;
                    break;
            }
        });
    }

    private void OnResponseReceived(string response)
    {
        Application.Current?.Dispatcher.Invoke(() =>
        {
            ResponseContent = response;
            ShowResponse = true;
        });
    }

    private void OnErrorOccurred(string? userMessage, string? errorCode, string? technicalMessage, bool retryable)
    {
        Application.Current?.Dispatcher.Invoke(() =>
        {
            ErrorDetails = ErrorDetailsViewModel.FromBridgeError(
                userMessage, errorCode, technicalMessage, retryable, _context.CorrelationId);
            ShowError = true;
            CanRetry = retryable;
        });
    }

    private void OnPollingError(string message)
    {
        // Transient polling errors are logged but not shown to the user
        // unless they accumulate (which triggers Disconnected state)
    }

    #endregion

    #region Events

    /// <summary>Raised when the window should close.</summary>
    public event Action? RequestClose;

    #endregion

    #region Helpers

    private static string FormatStageMessage(string? stage, string fallback)
    {
        if (string.IsNullOrEmpty(stage))
            return fallback;

        return stage.ToLowerInvariant() switch
        {
            "resolving_runtime" => "Connecting to AI agent…",
            "checking_availability" => "Verifying AI agent availability…",
            "building_prompt" => "Preparing your request…",
            "executing" => "The AI agent is working on your request…",
            "collecting_result" => "Collecting the response…",
            _ => stage
        };
    }

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    public void Dispose()
    {
        _monitor.StateChanged -= OnStateChanged;
        _monitor.ResponseReceived -= OnResponseReceived;
        _monitor.ErrorOccurred -= OnErrorOccurred;
        _monitor.PollingError -= OnPollingError;
        _monitor.Dispose();
    }
}

#region Command Implementations

/// <summary>Simple synchronous relay command.</summary>
public sealed class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;
    public void Execute(object? parameter) => _execute();
}

/// <summary>Simple async relay command.</summary>
public sealed class AsyncRelayCommand : ICommand
{
    private readonly Func<Task> _execute;
    private readonly Func<bool>? _canExecute;
    private bool _isExecuting;

    public AsyncRelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter) => !_isExecuting && (_canExecute?.Invoke() ?? true);

    public async void Execute(object? parameter)
    {
        if (_isExecuting) return;
        _isExecuting = true;
        try
        {
            await _execute().ConfigureAwait(false);
        }
        finally
        {
            _isExecuting = false;
        }
    }
}

#endregion
