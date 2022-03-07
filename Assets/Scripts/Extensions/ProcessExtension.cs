using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

public static class ProcessExtension
{
    /// <summary>
    /// If <see langword="awaited"/>, pauses execution until this <see cref="Process"/> is finished.
    /// </summary>
    /// <param name="process">The <see cref="Process"/> to be <see langword="awaited"/>.</param>
    /// <param name="cancellationToken"></param>
    public static Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default)
    {
        if (process.HasExited) return Task.CompletedTask;

        var tcs = new TaskCompletionSource<object>();
        process.EnableRaisingEvents = true;
        process.Exited += (sender, args) => tcs.TrySetResult(null);
        if (cancellationToken != default)
            cancellationToken.Register(() => tcs.SetCanceled());

        return process.HasExited ? Task.CompletedTask : tcs.Task;
    }
}
