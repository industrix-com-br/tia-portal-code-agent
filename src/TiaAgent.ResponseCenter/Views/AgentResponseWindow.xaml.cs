using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using TiaAgent.ResponseCenter.Diagnostics;
using TiaAgent.ResponseCenter.ViewModels;

namespace TiaAgent.ResponseCenter.Views;

/// <summary>
/// Code-behind for the Agent Response Center window.
/// UI state remains in the ViewModel; this class only handles WPF document rendering
/// and window lifecycle events.
/// </summary>
public partial class AgentResponseWindow : Window
{
    private readonly AgentResponseViewModel _viewModel;
    private bool _contentRendered;

    /// <summary>True after ContentRendered has fired at least once.</summary>
    public bool HasRenderedContent => _contentRendered;

    public AgentResponseWindow(AgentResponseViewModel viewModel)
    {
        ResponseCenterLogger.Info("InitializeComponent started");
        InitializeComponent();
        ResponseCenterLogger.Info("InitializeComponent completed");

        _viewModel = viewModel;
        DataContext = _viewModel;

        _viewModel.RequestClose += OnViewModelRequestClose;
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;

        Closing += OnWindowClosing;
        Closed += OnWindowClosed;
        SourceInitialized += OnSourceInitialized;
        Loaded += OnWindowLoaded;
        ContentRendered += OnContentRendered;
    }

    /// <summary>
    /// Sets raw response text for rendering.
    /// </summary>
    public void SetResponse(string response)
    {
        RenderResponse(response);
    }

    private void OnSourceInitialized(object? sender, EventArgs e)
    {
        ResponseCenterLogger.Info("Window SourceInitialized");
        var helper = new WindowInteropHelper(this);
        var hwnd = helper.Handle;
        ResponseCenterLogger.Info($"Window handle created: hwnd={hwnd.ToInt64()}");

        EnsureOnScreen();
    }

    private void OnWindowLoaded(object sender, RoutedEventArgs e)
    {
        ResponseCenterLogger.Info("Window Loaded");
    }

    private void OnContentRendered(object? sender, EventArgs e)
    {
        _contentRendered = true;
        ResponseCenterLogger.Info("Window ContentRendered");

        var helper = new WindowInteropHelper(this);
        ResponseCenterLogger.LogWindowState(
            "Window visibility confirmed",
            helper.Handle.ToInt64(),
            IsVisible,
            WindowState.ToString(),
            Left, Top,
            SystemParameters.PrimaryScreenWidth,
            SystemParameters.PrimaryScreenHeight);
    }

    /// <summary>
    /// Validates that the window position intersects at least one active monitor work area.
    /// If the window is completely off-screen, resets it to center of the primary screen.
    /// </summary>
    private void EnsureOnScreen()
    {
        try
        {
            var dpi = VisualTreeHelper.GetDpi(this);

            // Left/Top may be NaN or 0 if WindowStartupLocation=Manual without explicit positioning.
            // In that case, center the window instead of comparing against monitors.
            if (double.IsNaN(Left) || double.IsNaN(Top) || (Left == 0 && Top == 0))
            {
                ResponseCenterLogger.Info("Window position is default (0,0 or NaN) — centering on primary screen");
                Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
                Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
                return;
            }

            var windowLeft = (int)(Left * dpi.DpiScaleX);
            var windowTop = (int)(Top * dpi.DpiScaleY);
            var windowRight = windowLeft + (int)(Width * dpi.DpiScaleX);
            var windowBottom = windowTop + (int)(Height * dpi.DpiScaleY);

            var windowRect = new RECT
            {
                Left = windowLeft,
                Top = windowTop,
                Right = windowRight,
                Bottom = windowBottom
            };

            var intersectsAnyMonitor = false;
            var monitorCount = 0;

            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
                (IntPtr hMonitor, IntPtr _, ref RECT _, IntPtr _) =>
                {
                    monitorCount++;
                    var info = new MONITORINFO { cbSize = Marshal.SizeOf<MONITORINFO>() };
                    if (GetMonitorInfo(hMonitor, ref info))
                    {
                        if (RectanglesOverlap(windowRect, info.rcWork))
                            intersectsAnyMonitor = true;
                    }
                    return true;
                }, IntPtr.Zero);

            if (!intersectsAnyMonitor && monitorCount > 0)
            {
                ResponseCenterLogger.Warn(
                    $"Window is off-screen (pos={Left:F0},{Top:F0}) — resetting to center");
                Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
                Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
            }
        }
        catch (Exception ex)
        {
            ResponseCenterLogger.Warn($"EnsureOnScreen failed: {ex.Message}");
        }
    }

    private static bool RectanglesOverlap(RECT a, RECT b)
    {
        return a.Left < b.Right && a.Right > b.Left && a.Top < b.Bottom && a.Bottom > b.Top;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not (nameof(AgentResponseViewModel.ShowResponse)
            or nameof(AgentResponseViewModel.ResponseContent)))
        {
            return;
        }

        // The Bridge can report completion immediately before publishing the response.
        // Listen to both properties so rendering is correct regardless of event order,
        // including a completed task that returned no content.
        if (_viewModel.ShowResponse)
            RenderResponse(_viewModel.ResponseContent);
    }

    private void RenderResponse(string markdown)
    {
        var displayContent = string.IsNullOrWhiteSpace(markdown)
            ? Strings.EmptyResponseMessage
            : markdown;

        try
        {
            ResponseViewer.Document = MarkdownRenderer.Render(displayContent, ResponseViewer)
                ?? MarkdownRenderer.CreatePlainTextFallback(displayContent, ResponseViewer);
        }
        catch
        {
            ResponseViewer.Document = MarkdownRenderer.CreatePlainTextFallback(displayContent, ResponseViewer);
        }
    }

    private void OnViewModelRequestClose()
    {
        Dispatcher.Invoke(() =>
        {
            _viewModel.RequestClose -= OnViewModelRequestClose;
            Close();
        });
    }

    private void OnWindowClosing(object? sender, CancelEventArgs e)
    {
        // Stop the remote task when the user closes the window during processing.
        if (_viewModel.IsBusy && _viewModel.CancelCommand.CanExecute(null))
            _viewModel.CancelCommand.Execute(null);
    }

    private void OnWindowClosed(object? sender, EventArgs e)
    {
        _viewModel.RequestClose -= OnViewModelRequestClose;
        _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        _viewModel.Dispose();
    }

    private void TechnicalDetailsExpander_Expanded(object sender, RoutedEventArgs e)
    {
        _viewModel.ShowTechnicalDetails = true;
        DetailsToggleText.Text = Strings.HideDetails;
    }

    private void TechnicalDetailsExpander_Collapsed(object sender, RoutedEventArgs e)
    {
        _viewModel.ShowTechnicalDetails = false;
        DetailsToggleText.Text = Strings.ViewDetails;
    }

    #region Win32 Interop

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    private delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

    [DllImport("user32.dll")]
    private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MONITORINFO
    {
        public int cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;
    }

    #endregion
}
