using System;
using static PresentMonFps.AdvApi32;

namespace PresentMonFps.ETW;

internal static class Microsoft_Windows_DXGI
{
    public const string Name = "Microsoft-Windows-DXGI";

    public static readonly Guid GUID = new("CA11C036-0102-4A2D-A6AD-F03CFED5D3C9");

    public static readonly EVENT_DESCRIPTOR_DECL PresentMultiplaneOverlay_Start = new(0x0037, 0x00, 0x10, 0x00, 0x01, 0x000e, 0x8000000000000002);

    public static readonly EVENT_DESCRIPTOR_DECL PresentMultiplaneOverlay_Stop = new(0x0038, 0x00, 0x10, 0x00, 0x02, 0x000e, 0x8000000000000002);

    public static readonly EVENT_DESCRIPTOR_DECL Present_Start = new(0x002a, 0x00, 0x10, 0x00, 0x01, 0x0009, 0x8000000000000002);

    public static readonly EVENT_DESCRIPTOR_DECL Present_Stop = new(0x002b, 0x00, 0x10, 0x00, 0x02, 0x0009, 0x8000000000000002);

    public static readonly EVENT_DESCRIPTOR_DECL ResizeBuffers_Start = new(0x002d, 0x00, 0x10, 0x00, 0x01, 0x0005, 0x8000000000000002);

    public static readonly EVENT_DESCRIPTOR_DECL SwapChain_Start = new(0x000a, 0x00, 0x10, 0x00, 0x01, 0x0002, 0x8000000000000001);

    public enum Keyword : ulong
    {
        Objects = 0x1,
        Events = 0x2,
        JournalEntries = 0x4,
        Microsoft_Windows_DXGI_Analytic = 0x8000000000000000,
        Microsoft_Windows_DXGI_Logging = 0x4000000000000000,
    }

    public enum Level : byte
    {
        win_LogAlways = 0x0,
    }

    public enum Channel : byte
    {
        Microsoft_Windows_DXGI_Analytic = 0x10,
        Microsoft_Windows_DXGI_Logging = 0x11,
    }

    public enum DXGIPresentFlags : uint
    {
        DXGI_PRESENT_TEST = 1,
        DXGI_PRESENT_DO_NOT_SEQUENCE = 2,
        DXGI_PRESENT_RESTART = 4,
        DXGI_PRESENT_DO_NOT_WAIT = 8,
        DXGI_PRESENT_STEREO_PREFER_RIGHT = 16,
        DXGI_PRESENT_STEREO_TEMPORARY_MONO = 32,
        DXGI_PRESENT_RESTRICT_TO_OUTPUT = 64,
    }

    public enum HybridPresentMode : uint
    {
        NOT_HYBRID = 0,
        TWO_COPY_PATH = 1,
        ONE_COPY_PATH_CASO = 2,
    }
}
