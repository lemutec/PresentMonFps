﻿using PresentMonFps;
using System;

namespace PresentMon.SampleConsole;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        // Check Available.
        if (!FpsInspector.IsAvailable)
        {
            Console.WriteLine("This library is only available on Windows.");
            return;
        }

        // Simple method to get PID.
        // Fullname is unnecessary.
        uint pid = FpsInspector.GetProcessIdByNameAsync(args.Length > 0 ? args[0] : "YuanShen.exe").GetAwaiter().GetResult();

        // Calculate FPS Once.
        FpsResult result = FpsInspector.StartOnceAsync(new FpsRequest(pid)).GetAwaiter().GetResult();
        Console.WriteLine(result);

        // Calculate FPS Forever.
        int count = default;
        FpsInspector.StartForeverAsync(new FpsRequest(pid), (result) =>
        {
            // Do what you want with the result.
            Console.WriteLine(result);
            if (++count > 99999)
            {
                // You can cancel async task anytime.
                result.IsCanceled = true;
            }
        }).GetAwaiter().GetResult();
    }
}
