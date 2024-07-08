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
