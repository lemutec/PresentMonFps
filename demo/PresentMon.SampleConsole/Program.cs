using PresentMonFps;
using System;

namespace PresentMon.SampleConsole;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        if (!FpsInspector.IsAvailable)
        {
            Console.WriteLine("This library is only available on Windows x64 and Administrator Permission.");
            return;
        }

        uint pid = FpsInspector.GetProcessIdByNameAsync(args.Length > 0 ? args[0] : "vsedit.exe").GetAwaiter().GetResult();

        // Once
        FpsResult result = FpsInspector.StartOnceAsync(new FpsRequest(pid)).GetAwaiter().GetResult();
        Console.WriteLine(result);

        // Forever
        FpsInspector.StartForeverAsync(new FpsRequest(pid), Console.WriteLine, null!).GetAwaiter().GetResult();
    }
}
