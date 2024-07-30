using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;
using PresentMonFps.ETW;
using PresentMonFps.Natives;
using System;
using System.Diagnostics;
using System.Linq;
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

    public static nint GetMainWindowHandle(uint processId)
    {
        nint mainWindowHandle = IntPtr.Zero;

        Process? p = Process.GetProcesses().Where(p => p.Id == processId).FirstOrDefault();

        if (p != null)
        {
            _ = User32.EnumWindows((hWnd, lParam) =>
            {
                _ = User32.GetWindowThreadProcessId(hWnd, out uint windowProcessId);
                if (windowProcessId == processId && User32.IsWindowVisible(hWnd))
                {
                    mainWindowHandle = hWnd;
                    return false;
                }
                return true;
            }, IntPtr.Zero);
        }
        return mainWindowHandle;
    }

    public static nint GetProcessHandle(uint processId)
    {
        try
        {
            nint processHandle = IntPtr.Zero;

            Process? p = Process.GetProcesses().Where(p => p.Id == processId).FirstOrDefault();

            if (p != null)
            {
                return p.Handle;
            }

            return processHandle;
        }
        catch (Exception e)
        {
            _ = e.Message;
        }

        return IntPtr.Zero;
    }

    public static async Task<uint> GetProcessIdByNameAsync(string processName)
    {
        return await Task.Run(() => Kernel32.GetProcessIdByName(processName));
    }

    public static async Task<FpsResult> StartOnceAsync(FpsRequest request)
    {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT)
        {
            throw new FpsInspectorException($"For now only Windows is supported, detected platform is {Environment.OSVersion.Platform}.");
        }

        if (request.TargetPid == 0)
        {
            throw new FpsInspectorException($"Target Pid {nameof(FpsRequest.TargetPid)} is not supported.");
        }

        try
        {
            TaskCompletionSource<FpsResult> tcs = new();
            FpsResult result = new();
            FpsCalculator fps = new();
            int pid = (int)request.TargetPid;
            TraceEventID presentEventId = (TraceEventID)Microsoft_Windows_DxgKrnl.Present_Info.Id;

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

            void OnDynamicAll(TraceEvent data)
            {
                if (data.ProcessID != pid)
                {
                    return;
                }

                /// <see cref="Present"/>
                /// <see cref="Microsoft_Windows_DxgKrnl.Name"/>
                if (data.ProviderGuid == Microsoft_Windows_DxgKrnl.GUID)
                {
                    if (data.ID == presentEventId)
                    {
                        DateTime timestamp = data.TimeStamp;
                        fps.Calculate(timestamp.Ticks);
                    }
                }
            }
        }
        catch (Exception e)
        {
            throw new FpsInspectorException(e.Message);
        }
    }

    public static async Task StartForeverAsync(FpsRequest request, Action<FpsResult>? callback = null, CancellationToken? token = null)
    {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT)
        {
            throw new FpsInspectorException($"For now only Windows is supported, detected platform is {Environment.OSVersion.Platform}.");
        }

        if (request.TargetPid == 0)
        {
            throw new FpsInspectorException($"Target Pid {nameof(FpsRequest.TargetPid)} is not supported.");
        }

        try
        {
            FpsResult result = new();
            FpsCalculator fps = new();
            int pid = (int)request.TargetPid;
            TraceEventID presentEventId = (TraceEventID)Microsoft_Windows_DxgKrnl.Present_Info.Id;

            using TraceEventSession session = new(SessionName);

            fps.FpsReceived += OnFpsReceived;
            session.Source.Dynamic.All += OnDynamicAll;
            session.EnableProvider(Microsoft_Windows_DxgKrnl.GUID);

            Task processTask = Task.Factory.StartNew(session.Source.Process, TaskCreationOptions.LongRunning);
            Task consumeTask = Task.Run(() =>
            {
                while (!(token?.IsCancellationRequested ?? false))
                {
                    Thread.Sleep(request.PeriodMillisecond);
                    if (result.IsCanceled)
                    {
                        break;
                    }
                }
            });

            _ = await Task.WhenAny(processTask, consumeTask);

            fps.FpsReceived -= OnFpsReceived;
            session.Source.Dynamic.All -= OnDynamicAll;
            session.Source.StopProcessing();

            return;

            void OnDynamicAll(TraceEvent data)
            {
                if (data.ProcessID != pid)
                {
                    return;
                }

                /// <see cref="Present"/>
                /// <see cref="Microsoft_Windows_DxgKrnl.Name"/>
                if (data.ProviderGuid == Microsoft_Windows_DxgKrnl.GUID)
                {
                    if (data.ID == presentEventId)
                    {
                        DateTime timestamp = data.TimeStamp;
                        fps.Calculate(timestamp.Ticks);
                    }
                }
            }

            void OnFpsReceived(double fps)
            {
                result.Fps = fps;
                callback?.Invoke(result);
            }
        }
        catch (Exception e)
        {
            throw new FpsInspectorException(e.Message);
        }
    }

    public static bool IsRunAsAdmin()
    {
        return AdvApi32.IsRunAsAdmin();
    }

    public static bool IsRunAsAdmin(nint hWnd)
    {
        return AdvApi32.IsRunAsAdmin(hWnd);
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
public sealed class FpsResult(double fps)
{
    /// <summary>
    /// Only used for <see cref="FpsInspector.StartForeverAsync"/>.
    /// </summary>
    public bool IsCanceled { get; set; } = false;

    public double Fps { get; set; } = fps;

    public FpsResult() : this(default)
    {
    }

    public override string ToString() => $"FPS: {Fps}";
}
