using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using TiaAgent.AddIn.Diagnostics;

namespace TiaAgent.AddIn.Ui;

internal sealed class AssistantExecutionWindow : IAssistantExecutionView, IAssistantExecutionLifetime
{
    private static readonly Regex s_runtimePrefixRegex = new(
        @"^\[Runtime:\s*(.+?)\]\s*\n\s*\n",
        RegexOptions.Compiled);

    private readonly Window _window;
    private readonly TextBlock _headerSubtitle;
    private readonly TextBlock _runtimeLabel;
    private readonly StackPanel _loadingPanel;
    private readonly TextBlock _loadingText;
    private readonly FlowDocumentScrollViewer _viewer;
    private readonly Button _copyButton;
    private readonly CancellationTokenSource _closeCancellation = new();
    private readonly CancellationToken _cancellationToken;
    private string _rawContent = string.Empty;
    private int _isClosed;
    private int _isExecutionComplete;
    private int _isDisposed;

    public AssistantExecutionWindow(string action, string correlationId, string targetObject)
    {
        _cancellationToken = _closeCancellation.Token;
        var actionTitle = GetActionTitle(action);

        var headerGrid = new Grid();
        headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        var headerStack = new StackPanel();
        headerStack.Children.Add(new TextBlock
        {
            Text = "AI Code Agent",
            Foreground = Brushes.White,
            FontSize = 16,
            FontWeight = FontWeights.SemiBold
        });

        _headerSubtitle = new TextBlock
        {
            Text = actionTitle,
            Foreground = new SolidColorBrush(Color.FromArgb(0xCC, 0xFF, 0xFF, 0xFF)),
            FontSize = 12,
            Margin = new Thickness(0, 2, 0, 0)
        };
        headerStack.Children.Add(_headerSubtitle);
        Grid.SetColumn(headerStack, 0);
        headerGrid.Children.Add(headerStack);

        _runtimeLabel = new TextBlock
        {
            Foreground = new SolidColorBrush(Color.FromArgb(0xCC, 0xFF, 0xFF, 0xFF)),
            FontSize = 11,
            VerticalAlignment = VerticalAlignment.Center,
            Visibility = Visibility.Collapsed
        };
        Grid.SetColumn(_runtimeLabel, 1);
        headerGrid.Children.Add(_runtimeLabel);

        var headerBorder = new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(0x00, 0x78, 0xD4)),
            Padding = new Thickness(12, 10, 12, 10),
            Child = headerGrid
        };

        var metadataPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Margin = new Thickness(12, 6, 12, 6)
        };
        metadataPanel.Children.Add(CreateMetadataChip($"Action: {actionTitle}"));
        metadataPanel.Children.Add(CreateMetadataChip($"Target: {targetObject}"));
        metadataPanel.Children.Add(CreateMetadataChip($"ID: {correlationId}"));

        var metadataBorder = new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(0xF8, 0xF8, 0xF8)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(0xE0, 0xE0, 0xE0)),
            BorderThickness = new Thickness(0, 1, 0, 1),
            Padding = new Thickness(4),
            Child = metadataPanel
        };

        _loadingText = new TextBlock
        {
            Text = AssistantExecutionCoordinator.DefaultLoadingMessage,
            FontSize = 14,
            TextAlignment = TextAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 14)
        };

        var progressBar = new ProgressBar
        {
            IsIndeterminate = true,
            Width = 280,
            Height = 8,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        _loadingPanel = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        _loadingPanel.Children.Add(new TextBlock
        {
            Text = "Analyzing code",
            FontSize = 20,
            FontWeight = FontWeights.SemiBold,
            TextAlignment = TextAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 10)
        });
        _loadingPanel.Children.Add(_loadingText);
        _loadingPanel.Children.Add(progressBar);

        _viewer = new FlowDocumentScrollViewer
        {
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            BorderThickness = new Thickness(0),
            Background = Brushes.White,
            MinHeight = 200,
            Visibility = Visibility.Collapsed
        };

        var contentGrid = new Grid();
        contentGrid.Children.Add(_loadingPanel);
        contentGrid.Children.Add(_viewer);

        var buttonPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin = new Thickness(0, 0, 12, 12)
        };

        _copyButton = new Button
        {
            Content = "Copy response",
            Width = 120,
            Height = 32,
            Margin = new Thickness(0, 0, 8, 0),
            IsEnabled = false
        };
        _copyButton.Click += (_, __) => CopyContent();

        var closeButton = new Button
        {
            Content = "Close",
            Width = 100,
            Height = 32,
            IsCancel = true
        };
        closeButton.Click += (_, __) => _window.Close();

        buttonPanel.Children.Add(_copyButton);
        buttonPanel.Children.Add(closeButton);

        var mainGrid = new Grid();
        mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        Grid.SetRow(headerBorder, 0);
        Grid.SetRow(metadataBorder, 1);
        Grid.SetRow(contentGrid, 2);
        Grid.SetRow(buttonPanel, 3);

        mainGrid.Children.Add(headerBorder);
        mainGrid.Children.Add(metadataBorder);
        mainGrid.Children.Add(contentGrid);
        mainGrid.Children.Add(buttonPanel);

        _window = new Window
        {
            Title = "AI Code Agent - " + actionTitle,
            Width = 700,
            Height = 600,
            MinWidth = 500,
            MinHeight = 400,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            ShowInTaskbar = true,
            Topmost = true,
            Content = mainGrid
        };

        _window.Closed += (_, __) =>
        {
            if (Interlocked.Exchange(ref _isClosed, 1) != 0)
                return;

            if (!_closeCancellation.IsCancellationRequested)
                _closeCancellation.Cancel();

            TryDisposeCancellationSource();
        };
    }

    public bool IsClosed => Volatile.Read(ref _isClosed) != 0;

    public CancellationToken CancellationToken => _cancellationToken;

    public void CompleteExecution()
    {
        Interlocked.Exchange(ref _isExecutionComplete, 1);
        TryDisposeCancellationSource();
    }

    public void ShowLoading(string message)
    {
        RunOnDispatcher(() =>
        {
            if (IsClosed)
                return;

            _loadingText.Text = message;
            _loadingPanel.Visibility = Visibility.Visible;
            _viewer.Visibility = Visibility.Collapsed;
            _copyButton.IsEnabled = false;
        });
    }

    public Task ShowAsync()
    {
        if (_window.Dispatcher.CheckAccess())
            return ShowOnDispatcherAsync();

        return InvokeOnDispatcherAsync(ShowOnDispatcherAsync);
    }

    public Task ShowResultAsync(AssistantExecutionResult result)
    {
        if (result == null)
            throw new ArgumentNullException(nameof(result));

        return InvokeOnDispatcherAsync(() =>
        {
            if (IsClosed)
                return Task.CompletedTask;

            var markdown = result.Markdown;
            var runtimeId = result.RuntimeId;
            var runtimeMatch = s_runtimePrefixRegex.Match(markdown);
            if (runtimeMatch.Success)
            {
                runtimeId ??= runtimeMatch.Groups[1].Value;
                markdown = markdown.Substring(runtimeMatch.Length);
            }

            _rawContent = markdown;
            UpdateRuntimeLabel(runtimeId);
            _viewer.Document = RenderMarkdown(markdown);
            _loadingPanel.Visibility = Visibility.Collapsed;
            _viewer.Visibility = Visibility.Visible;
            _copyButton.IsEnabled = true;
            AddInLogger.Info("WPF result updated.");
            return Task.CompletedTask;
        });
    }

    public Task ShowErrorAsync(string message)
    {
        return InvokeOnDispatcherAsync(() =>
        {
            if (IsClosed)
                return Task.CompletedTask;

            _rawContent = message;
            _window.Title = "AI Code Agent - Error";
            _headerSubtitle.Text = "Error";
            _viewer.Document = CreateErrorDocument(message);
            _loadingPanel.Visibility = Visibility.Collapsed;
            _viewer.Visibility = Visibility.Visible;
            _copyButton.IsEnabled = true;
            AddInLogger.Info("WPF error state displayed.");
            return Task.CompletedTask;
        });
    }

    private Task ShowOnDispatcherAsync()
    {
        if (IsClosed || _window.IsVisible)
            return Task.CompletedTask;

        var completion = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        EventHandler? renderedHandler = null;
        EventHandler? closedHandler = null;

        void Complete(bool rendered)
        {
            if (renderedHandler != null)
                _window.ContentRendered -= renderedHandler;
            if (closedHandler != null)
                _window.Closed -= closedHandler;

            if (rendered)
                AddInLogger.Info("WPF window shown.");

            completion.TrySetResult(null);
        }

        renderedHandler = (_, __) => Complete(rendered: true);
        closedHandler = (_, __) => Complete(rendered: false);
        _window.ContentRendered += renderedHandler;
        _window.Closed += closedHandler;
        _window.Show();

        return completion.Task;
    }

    private Task InvokeOnDispatcherAsync(Func<Task> action)
    {
        if (_window.Dispatcher.CheckAccess())
            return action();

        var completion = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        _window.Dispatcher.BeginInvoke(new Action(async () =>
        {
            try
            {
                await action().ConfigureAwait(true);
                completion.TrySetResult(null);
            }
            catch (Exception ex)
            {
                completion.TrySetException(ex);
            }
        }), DispatcherPriority.Normal);
        return completion.Task;
    }

    private void RunOnDispatcher(Action action)
    {
        if (_window.Dispatcher.CheckAccess())
        {
            action();
            return;
        }

        _window.Dispatcher.Invoke(action);
    }

    private void TryDisposeCancellationSource()
    {
        if (Volatile.Read(ref _isClosed) == 0 || Volatile.Read(ref _isExecutionComplete) == 0)
            return;

        if (Interlocked.Exchange(ref _isDisposed, 1) == 0)
            _closeCancellation.Dispose();
    }

    private void UpdateRuntimeLabel(string? runtimeId)
    {
        if (string.IsNullOrWhiteSpace(runtimeId))
        {
            _runtimeLabel.Text = string.Empty;
            _runtimeLabel.Visibility = Visibility.Collapsed;
            return;
        }

        _runtimeLabel.Text = "Runtime: " + runtimeId;
        _runtimeLabel.Visibility = Visibility.Visible;
    }

    private static FlowDocument RenderMarkdown(string markdown)
    {
#if SIEMENS
        if (!string.IsNullOrWhiteSpace(markdown))
        {
            var renderer = new SimpleMarkdownFlowDocumentRenderer();
            var rendered = renderer.Render(markdown);
            if (rendered != null)
                return rendered;
        }
#endif
        return CreatePlainTextDocument(markdown, Brushes.Black);
    }

    private static FlowDocument CreateErrorDocument(string message)
    {
        return CreatePlainTextDocument(message, Brushes.DarkRed);
    }

    private static FlowDocument CreatePlainTextDocument(string content, Brush foreground)
    {
        var document = new FlowDocument
        {
            FontFamily = new FontFamily("Consolas"),
            FontSize = 13,
            Foreground = foreground,
            PagePadding = new Thickness(16)
        };
        document.Blocks.Add(new Paragraph(new Run(content ?? string.Empty))
        {
            Margin = new Thickness(0)
        });
        return document;
    }

    private void CopyContent()
    {
        try
        {
            Clipboard.SetText(_rawContent);
            _copyButton.Content = "Copied!";

            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            timer.Tick += (_, __) =>
            {
                _copyButton.Content = "Copy response";
                timer.Stop();
            };
            timer.Start();
        }
        catch (Exception ex)
        {
            AddInLogger.Warn($"Copy to clipboard failed: {ex.Message}");
            _copyButton.Content = "Copy failed";
        }
    }

    private static Border CreateMetadataChip(string text)
    {
        return new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(0xE8, 0xE8, 0xE8)),
            CornerRadius = new CornerRadius(3),
            Padding = new Thickness(6, 2, 6, 2),
            Margin = new Thickness(0, 0, 6, 0),
            Child = new TextBlock
            {
                Text = text,
                FontSize = 10,
                Foreground = new SolidColorBrush(Color.FromRgb(0x60, 0x60, 0x60)),
                VerticalAlignment = VerticalAlignment.Center
            }
        };
    }

    private static string GetActionTitle(string action)
    {
        return action switch
        {
            "explain" => "Explain selected object",
            "review" => "Review selected object",
            "propose" => "Propose improvements",
            _ => "Analyze selected object"
        };
    }
}
