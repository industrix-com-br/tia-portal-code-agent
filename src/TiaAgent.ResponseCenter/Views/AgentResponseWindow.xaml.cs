using System;
using System.ComponentModel;
using System.Windows;
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

    public AgentResponseWindow(AgentResponseViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        DataContext = _viewModel;

        _viewModel.RequestClose += OnViewModelRequestClose;
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;

        Closing += OnWindowClosing;
        Closed += OnWindowClosed;
    }

    /// <summary>
    /// Sets raw response text for rendering.
    /// </summary>
    public void SetResponse(string response)
    {
        RenderResponse(response);
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not (nameof(AgentResponseViewModel.ShowResponse)
            or nameof(AgentResponseViewModel.ResponseContent)))
        {
            return;
        }

        // The Bridge reports completion immediately before publishing the response.
        // Listen to both properties so rendering is correct regardless of event order.
        if (_viewModel.ShowResponse && !string.IsNullOrEmpty(_viewModel.ResponseContent))
            RenderResponse(_viewModel.ResponseContent);
    }

    private void RenderResponse(string markdown)
    {
        try
        {
            ResponseViewer.Document = MarkdownRenderer.Render(markdown)
                ?? MarkdownRenderer.CreatePlainTextFallback(markdown);
        }
        catch
        {
            ResponseViewer.Document = MarkdownRenderer.CreatePlainTextFallback(markdown);
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
}
