using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;
using PresentMonFps.ETW;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace PresentMonFps;

public static class FpsInspector
{
    public const string SessionName = "PresentMon-FpsInspector";
    public const string Present = "Present";

    public static bool IsAvailable => Environment.OSVersion.Platform == PlatformID.Win32NT;

    public static void StopTraceSession()
    {
        AdvApi32.EVENT_TRACE_PROPERTIES properties = new();
        properties.Wnode.BufferSize = (uint)Marshal.SizeOf<AdvApi32.EVENT_TRACE_PROPERTIES>();
        _ = AdvApi32.ControlTrace(0, SessionName, ref properties, AdvApi32.EVENT_TRACE_CONTROL.EVENT_TRACE_CONTROL_STOP);
    }

    public static uint GetProcessIdByName(string processName)
    {
        return Kernel32.GetProcessIdByName(processName);
    }

    public static async Task<uint> GetProcessIdByNameAsync(string processName)
    {
        return await Task.Run(() => Kernel32.GetProcessIdByName(processName));
    }

    public static async Task<FpsResult> StartOnceAsync(FpsRequest request)
    {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT)
        {
            throw new OperationCanceledException($"For now only Windows is supported, detected platform is {Environment.OSVersion.Platform}.");
        }

        if (request.TargetPid == 0)
        {
            throw new ArgumentException(nameof(FpsRequest.TargetPid));
        }

        TaskCompletionSource<FpsResult> tcs = new();
        FpsResult result = new();
        Vector<ulong> presentTimestamps = new(100);
        FpsCalculator fps = new();
        int pid = (int)request.TargetPid;
        Guid dxgKrnlGuid = Microsoft_Windows_DxgKrnl.GUID;
        TraceEventID presentEventId = (TraceEventID)Microsoft_Windows_DxgKrnl.Present_Info.Id;

        void OnDynamicAll(TraceEvent data)
        {
            if (data.ProcessID != pid)
            {
                return;
            }

            /// <see cref="Present"/>
            /// <see cref="Microsoft_Windows_DxgKrnl.Name"/>
            if (data.ProviderGuid == dxgKrnlGuid)
            {
                if (data.ID == presentEventId)
                {
                    DateTime timestamp = data.TimeStamp;
                    fps.Calculate(timestamp.Ticks);
                }
            }
        }

        await Task.Run(() =>
        {
            using TraceEventSession session = new(SessionName);

            session.Source.Dynamic.All += OnDynamicAll;
            session.EnableProvider(Microsoft_Windows_DxgKrnl.GUID);

            _ = Task.Run(() =>
            {
                session.Source.Process();
            });

            SpinWait.SpinUntil(() =>
            {
                Thread.Sleep(request.PeriodMillisecond);
                return fps.Fps != 0d;
            }, 5000);

            session.Source.Dynamic.All -= OnDynamicAll;
            session.Source.StopProcessing();

            result.Fps = fps.Fps;
            tcs.SetResult(result);
        });

        return await tcs.Task;
    }

    public static async Task StartForeverAsync(FpsRequest request, Action<FpsResult>? callback = null, CancellationToken? token = null)
    {
        while (!(token?.IsCancellationRequested ?? false))
        {
            FpsResult result = await StartOnceAsync(request);

            callback?.Invoke(result);
            if (result.IsCanceled)
            {
                break;
            }
        }
    }
}

public sealed class FpsRequest(uint targetPid)
{
    public uint TargetPid { get; set; } = targetPid;
    public int PeriodMillisecond { get; set; } = 100;

    public FpsRequest() : this(default)
    {
    }
}

[DebuggerDisplay("{ToString()}")]
public sealed class FpsResult
{
    /// <summary>
    /// Only used for <see cref="FpsInspector.StartForeverAsync"/>.
    /// </summary>
    public bool IsCanceled { get; set; } = false;

    public double Fps { get; set; } = default;

    public FpsResult()
    {
    }

    public override string ToString()
    {
        return $"FPS: {Fps}";
    }
}
