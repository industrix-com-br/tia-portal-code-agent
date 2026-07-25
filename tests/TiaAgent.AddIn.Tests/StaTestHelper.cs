using System;
using System.Threading;

namespace TiaAgent.AddIn.Tests;

/// <summary>
/// Helper for running WPF-dependent tests on STA threads.
/// xUnit runs tests on MTA threads by default, which breaks WPF control creation.
/// </summary>
internal static class StaTestHelper
{
    /// <summary>
    /// Runs the given action on an STA thread, blocking until complete.
    /// Propagates any exception from the action.
    /// </summary>
    public static void RunOnSta(Action action)
    {
        Exception? threadException = null;
        var thread = new Thread(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                threadException = ex;
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();

        if (threadException != null)
        {
            throw new AggregateException("Test action failed on STA thread", threadException);
        }
    }

    /// <summary>
    /// Runs the given function on an STA thread and returns the result.
    /// Propagates any exception from the function.
    /// </summary>
    public static T RunOnSta<T>(Func<T> func)
    {
        Exception? threadException = null;
        T? result = default;
        var thread = new Thread(() =>
        {
            try
            {
                result = func();
            }
            catch (Exception ex)
            {
                threadException = ex;
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();

        if (threadException != null)
        {
            throw new AggregateException("Test action failed on STA thread", threadException);
        }

        return result!;
    }
}
