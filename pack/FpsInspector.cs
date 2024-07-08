using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PresentMonFps;

public static class FpsInspector
{
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

        if (!Environment.Is64BitProcess)
        {
            throw new OperationCanceledException("You need 64-bit .NET to use this library.");
        }

        if (request.TargetPid == 0)
        {
            throw new ArgumentException(nameof(FpsRequest.TargetPid));
        }

        TaskCompletionSource<FpsResult> tcs = new();
        FpsResult result = new();

        await Task.Run(async () =>
        {
            {
                PresentMon.EventTracerExitCodes res = PresentMon.StartEventRecording((int)request.TargetPid, request.ArraySize);
                if (res != PresentMon.EventTracerExitCodes.STATUS_OK)
                {
                    throw new FpsInspectorException("Unable to start event tracing session", res);
                }
            }

            await Task.Delay(request.PeriodMillisecond);

            {
                PresentMon.EventTracerExitCodes res = PresentMon.StopEventRecording();
                if (res != PresentMon.EventTracerExitCodes.STATUS_OK)
                {
                    throw new FpsInspectorException("Unable to stop fliprate capturing", res);
                }
            }

            {
                int sampleCount = default;

                {
                    PresentMon.EventTracerExitCodes res = PresentMon.GetDataCount(ref sampleCount);
                    if (res != PresentMon.EventTracerExitCodes.STATUS_OK || sampleCount <= 0)
                    {
                        throw new FpsInspectorException("Unable get fliprate count", res);
                    }
                }

                {
                    double[] timeArr = new double[sampleCount];
                    PresentMon.EventScores[] fliprateArr = new PresentMon.EventScores[sampleCount];

                    unsafe
                    {
                        fixed (double* tsBuf = timeArr)
                        {
                            fixed (PresentMon.EventScores* scoresBuf = fliprateArr)
                            {
                                PresentMon.EventTracerExitCodes res = PresentMon.GetData(sampleCount, tsBuf, scoresBuf);
                                if (res != PresentMon.EventTracerExitCodes.STATUS_OK)
                                {
                                    throw new FpsInspectorException("Unable get fliprate data", res);
                                }

                                result.EventScores = fliprateArr;
                            }
                        }
                    }
                }
            }

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
        }
    }
}

public sealed class FpsRequest(uint targetPid)
{
    public uint TargetPid { get; set; } = targetPid;
    public int ArraySize { get; set; } = 86400 * 60;
    public int PeriodMillisecond { get; set; } = 100;

    public FpsRequest() : this(default)
    {
    }
}

[DebuggerDisplay("{ToString()}")]
public sealed class FpsResult(PresentMon.EventScores[] eventScores)
{
    public PresentMon.EventScores[] EventScores { get; internal set; } = eventScores;

    public float Fps => EventScores.Length > 0 ? (float)(EventScores.Sum(es => es.fps) / EventScores.Length) : 0f;

    public FpsResult() : this([])
    {
    }

    public override string ToString()
    {
        return $"FPS: {Fps}";
    }
}
