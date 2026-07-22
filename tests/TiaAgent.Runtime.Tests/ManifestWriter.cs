namespace TiaAgent.Runtime.Tests;

/// <summary>
/// Provides atomic, concurrency-safe file writes for the runtime manifest.
///
/// Each call writes content to a unique temporary file, then replaces
/// the destination under an in-process lock with bounded retries for
/// transient Windows file-access errors.
/// </summary>
public static class ManifestWriter
{
    private static readonly SemaphoreSlim ReplaceLock = new(1, 1);
    private const int MaxRetries = 5;
    private const int InitialRetryMs = 10;

    /// <summary>
    /// Atomically writes <paramref name="content"/> to <paramref name="destinationPath"/>.
    ///
    /// The write is performed in three stages:
    /// 1. Content is written to a unique temporary file (concurrent-safe).
    /// 2. The temporary file replaces the destination under a serialized lock.
    /// 3. Any leftover temporary file is cleaned up.
    /// </summary>
    public static void WriteAtomic(string destinationPath, string content)
    {
        var directory = Path.GetDirectoryName(destinationPath)
            ?? throw new ArgumentException("Destination path has no directory.", nameof(destinationPath));

        var tempPath = Path.Combine(
            directory,
            $"{Path.GetFileName(destinationPath)}.tmp.{Guid.NewGuid():N}");

        try
        {
            // Stage 1: write to a unique temp file — concurrent, no lock needed.
            File.WriteAllText(tempPath, content);

            // Stage 2: serialize the final replace so only one writer
            // touches the destination at a time.
            ReplaceLock.Wait();
            try
            {
                ReplaceWithRetry(tempPath, destinationPath);
                // Move succeeded — mark tempPath as consumed so the
                // finally block does not attempt to delete it.
                tempPath = null!;
            }
            finally
            {
                ReplaceLock.Release();
            }
        }
        finally
        {
            // Stage 3: clean up temp file if the move did not complete.
            TryDelete(tempPath);
        }
    }

    private static void ReplaceWithRetry(string source, string destination)
    {
        for (var attempt = 0; ; attempt++)
        {
            try
            {
                File.Move(source, destination, overwrite: true);
                return;
            }
            catch (Exception ex) when (IsTransient(ex) && attempt < MaxRetries)
            {
                Thread.Sleep(InitialRetryMs * (1 << attempt));
            }
        }
    }

    private static bool IsTransient(Exception ex) =>
        ex is IOException or UnauthorizedAccessException;

    private static void TryDelete(string? path)
    {
        if (path is null)
            return;

        try
        {
            File.Delete(path);
        }
        catch
        {
            // Best-effort cleanup — do not mask the original exception.
        }
    }
}
