using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PresentMonFps;

internal static class AdvApi32
{
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool GetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, uint TokenInformationLength, out uint ReturnLength);

    public const uint TOKEN_QUERY = 0x0008;
    public const int TokenElevation = 20;

    public enum TOKEN_INFORMATION_CLASS
    {
        TokenUser = 1,
        TokenGroups,
        TokenPrivileges,
        TokenOwner,
        TokenPrimaryGroup,
        TokenDefaultDacl,
        TokenSource,
        TokenType,
        TokenImpersonationLevel,
        TokenStatistics,
        TokenRestrictedSids,
        TokenSessionId,
        TokenGroupsAndPrivileges,
        TokenSessionReference,
        TokenSandBoxInert,
        TokenAuditPolicy,
        TokenOrigin,
        TokenElevationType,
        TokenLinkedToken,
        TokenElevation,
        TokenHasRestrictions,
        TokenAccessInformation,
        TokenVirtualizationAllowed,
        TokenVirtualizationEnabled,
        TokenIntegrityLevel,
        TokenUIAccess,
        TokenMandatoryPolicy,
        TokenLogonSid
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EVENT_TRACE_PROPERTIES
    {
        public WNODE_HEADER Wnode;
        public uint BufferSize;
        public uint MinimumBuffers;
        public uint MaximumBuffers;
        public uint MaximumFileSize;
        public uint LogFileMode;
        public uint FlushTimer;
        public uint EnableFlags;
        public int AgeLimit;
        public int NumberOfBuffers;
        public uint FreeBuffers;
        public uint EventsLost;
        public uint BuffersWritten;
        public uint LogBuffersLost;
        public uint RealTimeBuffersLost;
        public nint LoggerThreadId;
        public uint LogFileNameOffset;
        public uint LoggerNameOffset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WNODE_HEADER
    {
        public uint BufferSize;
        public uint ProviderId;
        public ulong HistoricalContext;
        public ulong TimeStamp;
        public Guid Guid;
        public uint ClientContext;
        public uint Flags;
    }

    public enum EVENT_TRACE_CONTROL
    {
        EVENT_TRACE_CONTROL_QUERY = 0,

        EVENT_TRACE_CONTROL_STOP = 1,

        EVENT_TRACE_CONTROL_UPDATE = 2,

        EVENT_TRACE_CONTROL_FLUSH = 3,

        EVENT_TRACE_CONTROL_INCREMENT_FILE = 4,
    }

    public struct EVENT_DESCRIPTOR_DECL(ushort id, byte version, byte channel, byte level, byte opcode, ushort task, ulong keyword)
    {
        public ushort Id = id;
        public byte Version = version;
        public byte Channel = channel;
        public byte Level = level;
        public byte Opcode = opcode;
        public ushort Task = task;
        public ulong Keyword = keyword;
    }

    [Obsolete]
    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern uint StopTrace(ulong sessionHandle, string sessionName, ref EVENT_TRACE_PROPERTIES properties);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern uint ControlTrace(ulong sessionHandle, string sessionName, ref EVENT_TRACE_PROPERTIES properties, EVENT_TRACE_CONTROL ControlCode);

    public static bool IsRunAsAdmin()
    {
        nint tokenHandle = IntPtr.Zero;
        try
        {
            if (!OpenProcessToken(Kernel32.GetCurrentProcess(), TOKEN_QUERY, out tokenHandle))
            {
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }

            GetTokenInformation(tokenHandle, TOKEN_INFORMATION_CLASS.TokenElevation, IntPtr.Zero, 0, out uint tokenInfoLength);
            nint tokenInfo = Marshal.AllocHGlobal((int)tokenInfoLength);

            try
            {
                if (GetTokenInformation(tokenHandle, TOKEN_INFORMATION_CLASS.TokenElevation, tokenInfo, tokenInfoLength, out _))
                {
                    int elevation = Marshal.ReadInt32(tokenInfo);
                    return elevation != 0;
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                Marshal.FreeHGlobal(tokenInfo);
            }
        }
        finally
        {
            if (tokenHandle != IntPtr.Zero)
            {
                Kernel32.CloseHandle(tokenHandle);
            }
        }
    }
}
