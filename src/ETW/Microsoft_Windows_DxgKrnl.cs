﻿using System;
using static PresentMonFps.AdvApi32;

namespace PresentMonFps.ETW;

internal static class Microsoft_Windows_DxgKrnl
{
    public const string Name = "Microsoft-Windows-DxgKrnl";

    public static readonly Guid GUID = new("802EC45A-1E99-4B83-9920-87C98277BA9D");

    public static readonly EVENT_DESCRIPTOR_DECL AdapterAllocation_DCStart = new(0x0023, 0x03, 0x11, 0x00, 0x03, 0x0015, 0x4000000000000040);

    public static readonly EVENT_DESCRIPTOR_DECL AdapterAllocation_Start = new(0x0021, 0x03, 0x11, 0x00, 0x01, 0x0015, 0x4000000000000040);

    public static readonly EVENT_DESCRIPTOR_DECL AdapterAllocation_Stop = new(0x0022, 0x03, 0x11, 0x00, 0x02, 0x0015, 0x4000000000000040);

    public static readonly EVENT_DESCRIPTOR_DECL BlitCancel_Info = new(0x01f5, 0x00, 0x11, 0x04, 0x00, 0x0135, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL Blit_Info = new(0x00a6, 0x00, 0x11, 0x04, 0x00, 0x0067, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL Context_DCStart = new(0x0020, 0x00, 0x11, 0x00, 0x03, 0x0014, 0x4000000000000840);

    public static readonly EVENT_DESCRIPTOR_DECL Context_Start = new(0x001e, 0x00, 0x11, 0x00, 0x01, 0x0014, 0x4000000000000840);

    public static readonly EVENT_DESCRIPTOR_DECL Context_Stop = new(0x001f, 0x00, 0x11, 0x00, 0x02, 0x0014, 0x4000000000000840);

    public static readonly EVENT_DESCRIPTOR_DECL Device_DCStart = new(0x001d, 0x02, 0x11, 0x00, 0x03, 0x0013, 0x4000000000000840);

    public static readonly EVENT_DESCRIPTOR_DECL Device_Start = new(0x001b, 0x02, 0x11, 0x00, 0x01, 0x0013, 0x4000000000000840);

    public static readonly EVENT_DESCRIPTOR_DECL Device_Stop = new(0x001c, 0x02, 0x11, 0x00, 0x02, 0x0013, 0x4000000000000840);

    public static readonly EVENT_DESCRIPTOR_DECL DmaPacket_Info = new(0x00b1, 0x01, 0x11, 0x00, 0x00, 0x0008, 0x4000000000000001);

    public static readonly EVENT_DESCRIPTOR_DECL DmaPacket_Info_2 = new(0x01c2, 0x00, 0x11, 0x00, 0x00, 0x0008, 0x4000000000000001);

    public static readonly EVENT_DESCRIPTOR_DECL DmaPacket_Info_3 = new(0x01c3, 0x00, 0x11, 0x00, 0x00, 0x0008, 0x4000000000000001);

    public static readonly EVENT_DESCRIPTOR_DECL DmaPacket_Start = new(0x00af, 0x01, 0x11, 0x00, 0x01, 0x0008, 0x4000000000000001);

    public static readonly EVENT_DESCRIPTOR_DECL FlipMultiPlaneOverlay_Info = new(0x00fc, 0x00, 0x11, 0x00, 0x00, 0x008f, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL Flip_Info = new(0x00a8, 0x00, 0x11, 0x00, 0x00, 0x0003, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL HSyncDPCMultiPlane_Info = new(0x017e, 0x02, 0x11, 0x00, 0x00, 0x00e6, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL HwQueue_DCStart = new(0x01a8, 0x00, 0x11, 0x00, 0x03, 0x00ff, 0x4000000000000840);

    public static readonly EVENT_DESCRIPTOR_DECL HwQueue_Start = new(0x01a6, 0x00, 0x11, 0x00, 0x01, 0x00ff, 0x4000000000000840);

    public static readonly EVENT_DESCRIPTOR_DECL IndependentFlip_Info = new(0x010a, 0x03, 0x11, 0x00, 0x00, 0x0097, 0x4000000000000001);

    public static readonly EVENT_DESCRIPTOR_DECL MMIOFlipMultiPlaneOverlay3_Info = new(0x0182, 0x08, 0x11, 0x00, 0x00, 0x00ea, 0x4000000000000001);

    public static readonly EVENT_DESCRIPTOR_DECL MMIOFlipMultiPlaneOverlay_Info = new(0x0103, 0x03, 0x11, 0x00, 0x00, 0x0090, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL MMIOFlip_Info = new(0x0074, 0x00, 0x11, 0x00, 0x00, 0x0011, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL NodeMetadata_Info = new(0x00fa, 0x00, 0x11, 0x00, 0x00, 0x008d, 0x4000000000000001);

    public static readonly EVENT_DESCRIPTOR_DECL PresentHistoryDetailed_Start = new(0x00d7, 0x02, 0x11, 0x00, 0x01, 0x007e, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL PresentHistory_Info = new(0x00ac, 0x02, 0x11, 0x00, 0x00, 0x0006, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL PresentHistory_Start = new(0x00ab, 0x02, 0x11, 0x00, 0x01, 0x0006, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL Present_Info = new(0x00b8, 0x01, 0x11, 0x00, 0x00, 0x006b, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL QueuePacket_Start = new(0x00b2, 0x01, 0x11, 0x00, 0x01, 0x0009, 0x4000000000000001);

    public static readonly EVENT_DESCRIPTOR_DECL QueuePacket_Start_2 = new(0x00f4, 0x01, 0x11, 0x00, 0x01, 0x0009, 0x4000000000000001);

    public static readonly EVENT_DESCRIPTOR_DECL QueuePacket_Start_3 = new(0x00f5, 0x02, 0x11, 0x00, 0x01, 0x0009, 0x4000000000000001);

    public static readonly EVENT_DESCRIPTOR_DECL QueuePacket_Stop = new(0x00b4, 0x01, 0x11, 0x00, 0x02, 0x0009, 0x4000000000000001);

    public static readonly EVENT_DESCRIPTOR_DECL VSyncDPCMultiPlane_Info = new(0x0111, 0x04, 0x11, 0x00, 0x00, 0x009f, 0x4000000008000001);

    public static readonly EVENT_DESCRIPTOR_DECL VSyncDPC_Info = new(0x0011, 0x00, 0x11, 0x00, 0x00, 0x000b, 0x4000000008000001);

    public enum Keyword : ulong
    {
        Base = 0x1,
        Profiler = 0x2,
        References = 0x4,
        ForceVsync = 0x8,
        Patch = 0x10,
        Cdd = 0x20,
        Resource = 0x40,
        Memory = 0x80,
        Dxgkrnl_StatusChangeNotify = 0x100,
        DxgKrnl_Power = 0x200,
        DriverEvents = 0x400,
        LongHaul = 0x800,
        StablePower = 0x1000,
        DefaultOverride = 0x2000,
        HistoryBuffer = 0x4000,
        GPUScheduler = 0x8000,
        DxgKrnl = 0x10000,
        DxgKrnl_WDI = 0x20000,
        Miracast = 0x40000,
        IndirectSwapChain = 0x80000,
        GPUVA = 0x100000,
        VidMmWorkerThread = 0x200000,
        Diagnostics = 0x400000,
        VirtualGpu = 0x800000,
        AdapterLock = 0x1000000,
        MixedReality = 0x2000000,
        HardwareSchedulingLog = 0x4000000,
        Present = 0x8000000,
        DxgKrnl_Int = 0x10000000,
        PerfData = 0x20000000,
        AzureTriageLogging = 0x40000000,
        win_ResponseTime = 0x1000000000000,
        Microsoft_Windows_DxgKrnl_Diagnostic = 0x8000000000000000,
        Microsoft_Windows_DxgKrnl_Performance = 0x4000000000000000,
        Microsoft_Windows_DxgKrnl_Power = 0x2000000000000000,
        Microsoft_Windows_DxgKrnl_Contention = 0x1000000000000000,
        Microsoft_Windows_DxgKrnl_Admin = 0x800000000000000,
        Microsoft_Windows_DxgKrnl_Operational = 0x400000000000000,
    };

    public enum Level : byte
    {
        win_LogAlways = 0x0,
        win_Error = 0x2,
        win_Informational = 0x4,
    };

    public enum Channel : byte
    {
        Microsoft_Windows_DxgKrnl_Diagnostic = 0x10,
        Microsoft_Windows_DxgKrnl_Performance = 0x11,
        Microsoft_Windows_DxgKrnl_Power = 0x12,
        Microsoft_Windows_DxgKrnl_Contention = 0x13,
        Microsoft_Windows_DxgKrnl_Admin = 0x14,
        Microsoft_Windows_DxgKrnl_Operational = 0x15,
    };

    public enum AllocationFlags : uint
    {
        CpuVisible = 1,
        PermanentSysMem = 2,
        Cached = 4,
        Protected = 8,
        ExistingSysMem = 16,
        ExistingKernelSysMem = 32,
        FromEndOfSegment = 64,
        Swizzled = 128,
        Overlay = 256,
        Capture = 512,
        UseAlternateVA = 1024,
        SynchronousPaging = 2048,
        LinkMirrored = 4096,
        LinkInstanced = 8192,
        CrossAdapter = 1048576,
        SwapChainBackBuffer = 2097152,
        SectionSupplied = 4194304,
        CddCached = 8388608,
        CddWriteCombined = 16777216,
        DoDPrimary = 33554432,
        CddBitmap = 67108864,
        PinnedBackingStore = 134217728,
        PagingBuffer = 268435456,
        Shareable = 536870912,
        Primary = 1073741824,
        ManagedPrimary = 2147483648,
    };

    public enum ColorSpaceType : uint
    {
        D3DDDI_COLOR_SPACE_RGB_FULL_G22_NONE_P709 = 0,
        D3DDDI_COLOR_SPACE_RGB_FULL_G10_NONE_P709 = 1,
        D3DDDI_COLOR_SPACE_RGB_STUDIO_G22_NONE_P709 = 2,
        D3DDDI_COLOR_SPACE_RGB_STUDIO_G22_NONE_P2020 = 3,
        D3DDDI_COLOR_SPACE_RESERVED = 4,
        D3DDDI_COLOR_SPACE_YCBCR_FULL_G22_NONE_P709_X601 = 5,
        D3DDDI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P601 = 6,
        D3DDDI_COLOR_SPACE_YCBCR_FULL_G22_LEFT_P601 = 7,
        D3DDDI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P709 = 8,
        D3DDDI_COLOR_SPACE_YCBCR_FULL_G22_LEFT_P709 = 9,
        D3DDDI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P2020 = 10,
        D3DDDI_COLOR_SPACE_YCBCR_FULL_G22_LEFT_P2020 = 11,
        D3DDDI_COLOR_SPACE_RGB_FULL_G2084_NONE_P2020 = 12,
        D3DDDI_COLOR_SPACE_YCBCR_STUDIO_G2084_LEFT_P2020 = 13,
    };

    public enum ContextFlags : uint
    {
        SystemContext = 1,
        PagingContext = 2,
        CompanionContext = 4,
    };

    public enum D3DFormat : uint
    {
        D3DDDIFMT_UNKNOWN = 0,
        D3DDDIFMT_R8G8B8 = 20,
        D3DDDIFMT_A8R8G8B8 = 21,
        D3DDDIFMT_X8R8G8B8 = 22,
        D3DDDIFMT_R5G6B5 = 23,
        D3DDDIFMT_X1R5G5B5 = 24,
        D3DDDIFMT_A1R5G5B5 = 25,
        D3DDDIFMT_A4R4G4B4 = 26,
        D3DDDIFMT_R3G3B2 = 27,
        D3DDDIFMT_A8 = 28,
        D3DDDIFMT_A8R3G3B2 = 29,
        D3DDDIFMT_X4R4G4B4 = 30,
        D3DDDIFMT_A2B10G10R10 = 31,
        D3DDDIFMT_A8B8G8R8 = 32,
        D3DDDIFMT_X8B8G8R8 = 33,
        D3DDDIFMT_G16R16 = 34,
        D3DDDIFMT_A2R10G10B10 = 35,
        D3DDDIFMT_A16B16G16R16 = 36,
        D3DDDIFMT_A8P8 = 40,
        D3DDDIFMT_P8 = 41,
        D3DDDIFMT_L8 = 50,
        D3DDDIFMT_A8L8 = 51,
        D3DDDIFMT_A4L4 = 52,
        D3DDDIFMT_V8U8 = 60,
        D3DDDIFMT_L6V5U5 = 61,
        D3DDDIFMT_X8L8V8U8 = 62,
        D3DDDIFMT_Q8W8V8U8 = 63,
        D3DDDIFMT_V16U16 = 64,
        D3DDDIFMT_W11V11U10 = 65,
        D3DDDIFMT_A2W10V10U10 = 67,
        D3DDDIFMT_D16_LOCKABLE = 70,
        D3DDDIFMT_D32 = 71,
        D3DDDIFMT_S1D15 = 72,
        D3DDDIFMT_D15S1 = 73,
        D3DDDIFMT_S8D24 = 74,
        D3DDDIFMT_D24S8 = 75,
        D3DDDIFMT_X8D24 = 76,
        D3DDDIFMT_D24X8 = 77,
        D3DDDIFMT_X4S4D24 = 78,
        D3DDDIFMT_D24X4S4 = 79,
        D3DDDIFMT_D16 = 80,
        D3DDDIFMT_L16 = 81,
        D3DDDIFMT_D32F_LOCKABLE = 82,
        D3DDDIFMT_D24FS8 = 83,
        D3DDDIFMT_D32_LOCKABLE = 84,
        D3DDDIFMT_S8_LOCKABLE = 85,
        D3DDDIFMT_G8R8 = 91,
        D3DDDIFMT_R8 = 92,
        D3DDDIFMT_VERTEXDATA = 100,
        D3DDDIFMT_INDEX16 = 101,
        D3DDDIFMT_INDEX32 = 102,
        D3DDDIFMT_Q16W16V16U16 = 110,
        D3DDDIFMT_R16F = 111,
        D3DDDIFMT_G16R16F = 112,
        D3DDDIFMT_A16B16G16R16F = 113,
        D3DDDIFMT_R32F = 114,
        D3DDDIFMT_G32R32F = 115,
        D3DDDIFMT_A32B32G32R32F = 116,
        D3DDDIFMT_CxV8U8 = 117,
        D3DDDIFMT_A1 = 118,
        D3DDDIFMT_A2B10G10R10_XR_BIAS = 119,
        D3DDDIFMT_PICTUREPARAMSDATA = 150,
        D3DDDIFMT_MACROBLOCKDATA = 151,
        D3DDDIFMT_RESIDUALDIFFERENCEDATA = 152,
        D3DDDIFMT_DEBLOCKINGDATA = 153,
        D3DDDIFMT_INVERSEQUANTIZATIONDATA = 154,
        D3DDDIFMT_SLICECONTROLDATA = 155,
        D3DDDIFMT_BITSTREAMDATA = 156,
        D3DDDIFMT_MOTIONVECTORBUFFER = 157,
        D3DDDIFMT_FILMGRAINBUFFER = 158,
        D3DDDIFMT_DXVA_RESERVED9 = 159,
        D3DDDIFMT_DXVA_RESERVED10 = 160,
        D3DDDIFMT_DXVA_RESERVED11 = 161,
        D3DDDIFMT_DXVA_RESERVED12 = 162,
        D3DDDIFMT_DXVA_RESERVED13 = 163,
        D3DDDIFMT_DXVA_RESERVED14 = 164,
        D3DDDIFMT_DXVA_RESERVED15 = 165,
        D3DDDIFMT_DXVA_RESERVED16 = 166,
        D3DDDIFMT_DXVA_RESERVED17 = 167,
        D3DDDIFMT_DXVA_RESERVED18 = 168,
        D3DDDIFMT_DXVA_RESERVED19 = 169,
        D3DDDIFMT_DXVA_RESERVED20 = 170,
        D3DDDIFMT_DXVA_RESERVED21 = 171,
        D3DDDIFMT_DXVA_RESERVED22 = 172,
        D3DDDIFMT_DXVA_RESERVED23 = 173,
        D3DDDIFMT_DXVA_RESERVED24 = 174,
        D3DDDIFMT_DXVA_RESERVED25 = 175,
        D3DDDIFMT_DXVA_RESERVED26 = 176,
        D3DDDIFMT_DXVA_RESERVED27 = 177,
        D3DDDIFMT_DXVA_RESERVED28 = 178,
        D3DDDIFMT_DXVA_RESERVED29 = 179,
        D3DDDIFMT_DXVA_RESERVED30 = 180,
        D3DDDIFMT_DXVA_RESERVED31 = 181,
        D3DDDIFMT_BINARYBUFFER = 199,
        D3DDDIFMT_MULTI2_ARGB8 = 827606349,
        D3DDDIFMT_DXT1 = 827611204,
        D3DDDIFMT_DXT2 = 844388420,
        D3DDDIFMT_YUY2 = 844715353,
        D3DDDIFMT_DXT3 = 861165636,
        D3DDDIFMT_DXT4 = 877942852,
        D3DDDIFMT_DXT5 = 894720068,
        D3DDDIFMT_G8R8_G8B8 = 1111970375,
        D3DDDIFMT_R8G8_B8G8 = 1195525970,
        D3DDDIFMT_UYVY = 1498831189,
    };

    public enum D3DKMT_PRESENTFLAGS : uint
    {
        Blt = 1,
        ColorFill = 2,
        Flip = 4,
        FlipDoNotFlip = 8,
        FlipWithNoWait = 16,
        SrcColorKey = 512,
        DstColorKey = 1024,
        LinearToSrgb = 2048,
        Rotate = 8192,
        PresentToBitmap = 16384,
        RedirectedFlip = 32768,
        RedirectedBlt = 65536,
        FlipStereo = 131072,
        PresentHistoryTokenOnly = 2097152,
        CrossAdapter = 67108864,
    };

    public enum DXGK_ENGINE : uint
    {
        OTHER = 0,
        _3D = 1,
        VIDEO_DECODE = 2,
        VIDEO_ENCODE = 3,
        VIDEO_PROCESSING = 4,
        SCENE_ASSEMBLY = 5,
        COPY = 6,
        OVERLAY = 7,

        // Manually added:
        CRYPTO = 8,
    };

    public enum DeviceClientType : uint
    {
        DXGDEVICECLIENT_LEGACYUSER = 0,
        DXGDEVICECLIENT_USER = 1,
        DXGDEVICECLIENT_CDD = 2,
    };

    public enum DisplayRotation : uint
    {
        D3DDDI_ROTATION_IDENTITY = 1,
        D3DDDI_ROTATION_90 = 2,
        D3DDDI_ROTATION_180 = 3,
        D3DDDI_ROTATION_270 = 4,
    };

    public enum DmaInterruptType : uint
    {
        DXGK_INTERRUPT_DMA_COMPLETED = 1,
        DXGK_INTERRUPT_DMA_PREEMPTED = 2,
        DXGK_INTERRUPT_DMA_FAULTED = 4,
        DXGK_INTERRUPT_DMA_PAGE_FAULTED = 9,
    };

    public enum DmaPacketType : uint
    {
        DXGKETW_CLIENT_RENDER_BUFFER = 0,
        DXGKETW_CLIENT_PAGING_BUFFER = 1,
        DXGKETW_SYSTEM_PAGING_BUFFER = 2,
        DXGKETW_SYSTEM_PREEMTION_BUFFER = 3,
    };

    public enum DmaPageFaultFlags : uint
    {
        Write = 1,
        FenceInvalid = 2,
        AdapterResetRequired = 4,
        EngineResetRequired = 8,
        FatalHardwareError = 16,
    };

    public enum FlipEntryStatus : uint
    {
        FlipWaitVSync = 5,
        FlipWaitComplete = 11,
        FlipWaitPassive = 13,
        FlipWaitPost = 14,
        FlipWaitHSync = 15,
    };

    public enum FlipmodeType : uint
    {
        DXGKETW_FLIPMODE_NO_DEVICE = 0,
        DXGKETW_FLIPMODE_IMMEDIATE = 1,
        DXGKETW_FLIPMODE_VSYNC_HW_FLIP_QUEUE = 2,
        DXGKETW_FLIPMODE_VSYNC_SW_FLIP_QUEUE = 3,
        DXGKETW_FLIPMODE_VSYNC_BUILT_IN_WAIT = 4,
        DXGKETW_FLIPMODE_IMMEDIATE_SW_FLIP_QUEUE = 5,
    };

    public enum HDRMetaDataTypeEnum : uint
    {
        None = 0,
        HDR10 = 1,
        HDR10Plus = 2,
    };

    public enum MultiPlaneOverlayAttributesFlags : uint
    {
        VerticalFlip = 1,
        HorizontalFlip = 2,
    };

    public enum MultiPlaneOverlayBlend : uint
    {
        Opaque = 0,
        AlphaBlend = 1,
    };

    public enum PresentFlags : uint
    {
        Blt = 1,
        ColorFill = 2,
        Flip = 4,
        FlipWithNoWait = 8,
        SrcColorKey = 16,
        DstColorKey = 32,
        LinearToSrgb = 64,
        Rotate = 128,
    };

    public enum PresentModel : uint
    {
        D3DKMT_PM_UNINITIALIZED = 0,
        D3DKMT_PM_REDIRECTED_GDI = 1,
        D3DKMT_PM_REDIRECTED_FLIP = 2,
        D3DKMT_PM_REDIRECTED_BLT = 3,
        D3DKMT_PM_REDIRECTED_VISTABLT = 4,
        D3DKMT_PM_SCREENCAPTUREFENCE = 5,
        D3DKMT_PM_REDIRECTED_GDI_SYSMEM = 6,
        D3DKMT_PM_REDIRECTED_COMPOSITION = 7,
        D3DKMT_PM_SURFACECOMPLETE = 8,

        // Added manually
        D3DKMT_PM_FLIPMANAGER = 9,
    };

    public enum QuantumStatus : uint
    {
        VIDSCH_QUANTUM_READY = 0,
        VIDSCH_QUANTUM_RUNNING = 1,
        VIDSCH_QUANTUM_EXPIRED = 2,
        VIDSCH_QUANTUM_PROCESSED_EXPIRE = 3,
    };

    public enum QueuePacketType : uint
    {
        DXGKETW_RENDER_COMMAND_BUFFER = 0,
        DXGKETW_DEFERRED_COMMAND_BUFFER = 1,
        DXGKETW_SYSTEM_COMMAND_BUFFER = 2,
        DXGKETW_MMIOFLIP_COMMAND_BUFFER = 3,
        DXGKETW_WAIT_COMMAND_BUFFER = 4,
        DXGKETW_SIGNAL_COMMAND_BUFFER = 5,
        DXGKETW_DEVICE_COMMAND_BUFFER = 6,
        DXGKETW_SOFTWARE_COMMAND_BUFFER = 7,
        DXGKETW_PAGING_COMMAND_BUFFER = 8,
    };

    public enum SetVidPnSourceAddressFlags : uint
    {
        ModeChange = 1,
        FlipImmediate = 2,
        FlipOnNextVSync = 4,
    };

    public enum SetVidPnSourceAddressInputFlags : uint
    {
        FlipStereo = 1,
        FlipStereoTemporaryMono = 2,
        FlipStereoPreferRight = 4,
        RetryAtLowerIrql = 8,
    };

    public enum SetVidPnSourceAddressOutputFlags : uint
    {
        PrePresentNeeded = 1,
        HwFlipQueueDrainNeeded = 2,
    };

    public enum UsageFlags : uint
    {
        PrivateFormat = 1,
        Swizzled = 2,
        MipMap = 4,
        Cube = 8,
        Volume = 16,
        Vertex = 32,
        Index = 64,
    };
}
