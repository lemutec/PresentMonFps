using CommunityToolkit.Mvvm.ComponentModel;
using PresentMonFps;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using Vanara.PInvoke;

namespace PresentMon;

[ObservableObject]
public partial class MaskWindow : Window
{
    [ObservableProperty]
    private string fps = 0.ToString();

    public MaskWindow()
    {
        DataContext = this;
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        nint hWnd = new WindowInteropHelper(this).Handle;
        uint pid = FpsInspector.GetProcessIdByName("YuanShen.exe");
        nint targetHWnd = FpsInspector.GetMainWindowHandle(pid);

        if (pid == 0 || targetHWnd == 0)
        {
            throw new Exception("Process is not running.");
        }

        _ = User32.GetClientRect(hWnd, out RECT rect);
        var succ = User32.SetParent(hWnd, targetHWnd);
        RECT targetRect = default;
        bool isUACRequested = FpsInspector.IsRunAsAdmin();
        bool isTargetUACRequested = false;

        if (succ != 0)
        {
            _ = User32.GetClientRect(targetHWnd, out targetRect);

            float x = DpiHelper.GetScale(targetHWnd).X;
            _ = User32.SetWindowPos(hWnd, IntPtr.Zero, 0, 0, (int)(targetRect.Width * x), (int)(targetRect.Height * x), User32.SetWindowPosFlags.SWP_SHOWWINDOW);
        }
        else
        {
            nint targetProcessHandle = FpsInspector.GetProcessHandle(pid);

            isTargetUACRequested = targetProcessHandle == IntPtr.Zero || FpsInspector.IsRunAsAdmin(targetProcessHandle);
            _ = User32.SetParent(hWnd, IntPtr.Zero);
            Debug.WriteLine($"Failed to SetParent {(isTargetUACRequested ? ", the Administrator UAC is requested" : string.Empty)}.");
        }

        _ = Task.Run(async () =>
        {
            await FpsInspector.StartForeverAsync(new FpsRequest(pid), (result) =>
            {
                if (!isTargetUACRequested || (isTargetUACRequested && isUACRequested))
                {
                    float x = DpiHelper.GetScale(targetHWnd).X;
                    _ = User32.SetWindowPos(hWnd, IntPtr.Zero, 0, 0, (int)(targetRect.Width * x), (int)(targetRect.Height * x), User32.SetWindowPosFlags.SWP_SHOWWINDOW);
                }
                Fps = $"{result.Fps:0}";
            });
        });
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        this.SetLayeredWindow();
        this.SetChildWindow();
        this.HideFromAltTab();
    }
}

file static class MaskWindowExtension
{
    public static void HideFromAltTab(this Window window)
    {
        HideFromAltTab(new WindowInteropHelper(window).Handle);
    }

    public static void HideFromAltTab(nint hWnd)
    {
        int style = User32.GetWindowLong(hWnd, User32.WindowLongFlags.GWL_EXSTYLE);

        style |= (int)User32.WindowStylesEx.WS_EX_TOOLWINDOW;
        _ = User32.SetWindowLong(hWnd, User32.WindowLongFlags.GWL_EXSTYLE, style);
    }

    public static void SetLayeredWindow(this Window window, bool isLayered = true)
    {
        SetLayeredWindow(new WindowInteropHelper(window).Handle, isLayered);
    }

    private static void SetLayeredWindow(nint hWnd, bool isLayered = true)
    {
        int style = User32.GetWindowLong(hWnd, User32.WindowLongFlags.GWL_EXSTYLE);

        if (isLayered)
        {
            style |= (int)User32.WindowStylesEx.WS_EX_TRANSPARENT;
            style |= (int)User32.WindowStylesEx.WS_EX_LAYERED;
        }
        else
        {
            style &= ~(int)User32.WindowStylesEx.WS_EX_TRANSPARENT;
            style &= ~(int)User32.WindowStylesEx.WS_EX_LAYERED;
        }

        _ = User32.SetWindowLong(hWnd, User32.WindowLongFlags.GWL_EXSTYLE, style);
    }

    public static void SetChildWindow(this Window window)
    {
        SetChildWindow(new WindowInteropHelper(window).Handle);
    }

    private static void SetChildWindow(nint hWnd)
    {
        int style = User32.GetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE);

        style |= (int)User32.WindowStyles.WS_CHILD;
        _ = User32.SetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE, style);
    }
}
