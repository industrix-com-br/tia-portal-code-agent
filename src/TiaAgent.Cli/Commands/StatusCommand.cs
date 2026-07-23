using System;
using System.IO;
using System.Threading.Tasks;
using TiaAgent.Cli.Supervisor;

namespace TiaAgent.Cli.Commands;

public sealed class StatusOptions
{
    public string? CustomRoot { get; set; }
    public bool Json { get; set; }
}

public static class StatusCommand
{
    public static int Execute(StatusOptions options, TextWriter? stdout = null, TextWriter? stderr = null)
    {
        stdout ??= Console.Out;
        stderr ??= Console.Error;

        return SupervisorEngine.GetStatus(options, stdout, stderr);
    }

    public static async Task<int> ExecuteAsync(StatusOptions options, TextWriter? stdout = null, TextWriter? stderr = null)
    {
        stdout ??= Console.Out;
        stderr ??= Console.Error;

        return await SupervisorEngine.GetStatusAsync(options, stdout, stderr).ConfigureAwait(false);
    }
}
