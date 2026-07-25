using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace TiaAgent.Contracts.Diagnostics;

/// <summary>
/// Produces bounded, content-safe diagnostics for text and UTF-8 byte payloads.
/// The full payload is never included. Hashes allow adjacent boundaries to be
/// compared without logging production responses.
/// </summary>
public static class TextPayloadDiagnostics
{
    private static readonly UTF8Encoding StrictUtf8 = new(false, true);

    // Only strong multi-character signatures are listed. Individual characters
    // such as Ã and Â are legitimate Unicode and must not be treated as corruption.
    private static readonly string[] KnownMojibakePatterns =
    {
        "ΓÇ",
        "Γå",
        "Γö",
        "≡ƒ",
        "â€",
        "Ãƒ",
        "Ã‚",
        "Â©",
        "Â®",
        "Â ",
        "ðŸ"
    };

    public static string DescribeText(string boundary, string? text, int previewScalarLimit = 32)
    {
        text ??= string.Empty;
        var utf8Bytes = Encoding.UTF8.GetBytes(text);

        return string.Format(
            CultureInfo.InvariantCulture,
            "boundary={0}; utf16Length={1}; scalarCount={2}; utf8Bytes={3}; sha256={4}; replacementCharacter={5}; knownMojibake={6}; escapedPreview=\"{7}\"; codePoints={8}",
            boundary,
            text.Length,
            CountUnicodeScalars(text),
            utf8Bytes.Length,
            ComputeSha256(utf8Bytes),
            text.IndexOf('\uFFFD') >= 0,
            ContainsKnownMojibake(text),
            BuildEscapedPreview(text, previewScalarLimit),
            BuildCodePointPreview(text, previewScalarLimit));
    }

    public static string DescribeUtf8Bytes(string boundary, byte[]? bytes, int hexByteLimit = 32)
    {
        bytes ??= Array.Empty<byte>();
        var strictUtf8Valid = true;
        try
        {
            StrictUtf8.GetString(bytes);
        }
        catch (DecoderFallbackException)
        {
            strictUtf8Valid = false;
        }

        return string.Format(
            CultureInfo.InvariantCulture,
            "boundary={0}; byteCount={1}; sha256={2}; strictUtf8Valid={3}; hexPreview={4}",
            boundary,
            bytes.Length,
            ComputeSha256(bytes),
            strictUtf8Valid,
            BuildHexPreview(bytes, hexByteLimit));
    }

    public static string ComputeUtf8Sha256(string? text)
    {
        return ComputeSha256(Encoding.UTF8.GetBytes(text ?? string.Empty));
    }

    private static string ComputeSha256(byte[] bytes)
    {
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(bytes);
        return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
    }

    private static int CountUnicodeScalars(string text)
    {
        var count = 0;
        for (var i = 0; i < text.Length; i++)
        {
            if (char.IsHighSurrogate(text[i]) &&
                i + 1 < text.Length &&
                char.IsLowSurrogate(text[i + 1]))
            {
                i++;
            }
            count++;
        }
        return count;
    }

    private static bool ContainsKnownMojibake(string text)
    {
        foreach (var pattern in KnownMojibakePatterns)
        {
            if (text.IndexOf(pattern, StringComparison.Ordinal) >= 0)
                return true;
        }
        return false;
    }

    private static string BuildEscapedPreview(string text, int scalarLimit)
    {
        var sb = new StringBuilder();
        var scalarCount = 0;

        for (var i = 0; i < text.Length && scalarCount < scalarLimit; i++, scalarCount++)
        {
            var c = text[i];
            switch (c)
            {
                case '\\': sb.Append("\\\\"); break;
                case '\r': sb.Append("\\r"); break;
                case '\n': sb.Append("\\n"); break;
                case '\t': sb.Append("\\t"); break;
                case '"': sb.Append("\\\""); break;
                default:
                    if (char.IsControl(c))
                    {
                        sb.Append("\\u");
                        sb.Append(((int)c).ToString("X4", CultureInfo.InvariantCulture));
                    }
                    else if (char.IsHighSurrogate(c) &&
                             i + 1 < text.Length &&
                             char.IsLowSurrogate(text[i + 1]))
                    {
                        sb.Append(c);
                        sb.Append(text[++i]);
                    }
                    else
                    {
                        sb.Append(c);
                    }
                    break;
            }
        }

        if (scalarCount < CountUnicodeScalars(text))
            sb.Append('…');

        return sb.ToString();
    }

    private static string BuildCodePointPreview(string text, int scalarLimit)
    {
        var sb = new StringBuilder();
        var scalarCount = 0;

        for (var i = 0; i < text.Length && scalarCount < scalarLimit; i++, scalarCount++)
        {
            if (sb.Length > 0)
                sb.Append(' ');

            int codePoint;
            if (char.IsHighSurrogate(text[i]) &&
                i + 1 < text.Length &&
                char.IsLowSurrogate(text[i + 1]))
            {
                codePoint = char.ConvertToUtf32(text[i], text[i + 1]);
                i++;
            }
            else
            {
                codePoint = text[i];
            }

            sb.Append("U+");
            sb.Append(codePoint.ToString(codePoint > 0xFFFF ? "X6" : "X4", CultureInfo.InvariantCulture));
        }

        if (scalarCount < CountUnicodeScalars(text))
            sb.Append(" …");

        return sb.ToString();
    }

    private static string BuildHexPreview(byte[] bytes, int byteLimit)
    {
        var length = Math.Min(bytes.Length, Math.Max(0, byteLimit));
        if (length == 0)
            return string.Empty;

        var preview = new byte[length];
        Array.Copy(bytes, preview, length);
        var hex = BitConverter.ToString(preview).Replace("-", " ");
        return bytes.Length > length ? hex + " …" : hex;
    }
}
