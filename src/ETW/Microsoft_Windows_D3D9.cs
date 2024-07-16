using System;
using static PresentMonFps.AdvApi32;

namespace PresentMonFps.ETW;

internal static class Microsoft_Windows_D3D9
{
    public const string Name = "Microsoft-Windows-D3D9";

    public static readonly Guid GUID = new("783ACA0A-790E-4D7F-8451-AA850511C6B9");

    public static readonly EVENT_DESCRIPTOR_DECL Present_Start = new(0x0001, 0x00, 0x10, 0x00, 0x01, 0x0001, 0x8000000000000002);

    public static readonly EVENT_DESCRIPTOR_DECL Present_Stop = new(0x0002, 0x00, 0x10, 0x00, 0x02, 0x0001, 0x8000000000000002);

    public enum Keyword : ulong
    {
        Events = 0x2,
        Microsoft_Windows_Direct3D9_Analytic = 0x8000000000000000,
    }

    public enum Level : byte
    {
        win_LogAlways = 0x0,
    }

    public enum Channel : byte
    {
        Microsoft_Windows_Direct3D9_Analytic = 0x10,
    };

    public enum D3D9PresentFlags : uint
    {
        D3DPRESENT_DONOTWAIT = 1,
        D3DPRESENT_LINEAR_CONTENT = 2,
        D3DPRESENT_DONOTFLIP = 4,
        D3DPRESENT_FLIPRESTART = 8,
        D3DPRESENT_VIDEO_RESTRICT_TO_MONITOR = 16,
        D3DPRESENT_FORCEIMMEDIATE = 256,
    };
}
