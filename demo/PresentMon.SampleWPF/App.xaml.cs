using System.Windows;
using Vanara.PInvoke;

namespace PresentMon;

public partial class App : Application
{
    static App()
    {
        _ = SHCore.SetProcessDpiAwareness(SHCore.PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
    }
}
