#if SIEMENS
using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace TiaAgent.AddIn.Resources;

/// <summary>
/// Loads and converts the embedded AI icon for TIA Portal context menu items.
/// TIA Portal V21 API uses System.Drawing.Icon for action item icons.
/// The original 48×48 PNG is embedded as a resource and converted to a 16×16 Icon at runtime.
/// </summary>
internal static class AddInIcon
{
    private static Icon? _cachedIcon;
    private static readonly object _lock = new();

    /// <summary>
    /// Gets the 16×16 icon for use in AddActionItemWithIcon calls.
    /// Returns null if the icon cannot be loaded (action items will render without icon).
    /// </summary>
    internal static Icon? GetIcon16x16()
    {
        if (_cachedIcon != null)
            return _cachedIcon;

        lock (_lock)
        {
            if (_cachedIcon != null)
                return _cachedIcon;

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using var stream = assembly.GetManifestResourceStream(
                    "TiaAgent.AddIn.Resources.icons8-ai-48.png");

                if (stream == null)
                    return null;

                using var original = new Bitmap(stream);
                var small = new Bitmap(original, new Size(16, 16));
                _cachedIcon = Icon.FromHandle(small.GetHicon());
            }
            catch
            {
                // Silently fail — icon is cosmetic, must not crash the Add-In
                _cachedIcon = null;
            }
        }

        return _cachedIcon;
    }
}
#endif
