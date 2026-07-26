#if SIEMENS
using System.Windows;

namespace TiaAgent.AddIn.Ui;

/// <summary>
/// Native fallback dialogs for validation and startup failures only.
/// Agent task progress and results are always displayed by TiaAgent.ResponseCenter.
/// </summary>
internal static class AddInDialogService
{
    public static void ShowWarning(string message)
    {
        MessageBox.Show(
            message,
            "TIA Agent - Warning",
            MessageBoxButton.OK,
            MessageBoxImage.Warning);
    }

    public static void ShowError(string message)
    {
        MessageBox.Show(
            message,
            "TIA Agent - Error",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
}
#endif
