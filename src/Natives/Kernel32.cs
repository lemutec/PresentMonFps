using System;
using System.Runtime.InteropServices;

namespace PresentMonFps;

internal static class Kernel32
{
    public const int MAX_PATH = 260;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct PROCESSENTRY32
    {
        public uint dwSize;

        public uint cntUsage;

        public uint th32ProcessID;

        public nuint th32DefaultHeapID;

        public uint th32ModuleID;

        public uint cntThreads;

        public uint th32ParentProcessID;

        public int pcPriClassBase;

        public uint dwFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
        public string szExeFile;

        public static readonly PROCESSENTRY32 Default = new() { dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32)) };
    }

    public enum TH32CS : uint
    {
        TH32CS_INHERIT = 0x80000000,

        TH32CS_SNAPHEAPLIST = 0x00000001,

        TH32CS_SNAPMODULE = 0x00000008,

        TH32CS_SNAPMODULE32 = 0x00000010,

        TH32CS_SNAPPROCESS = 0x00000002,

        TH32CS_SNAPTHREAD = 0x00000004,

        TH32CS_SNAPALL = TH32CS_SNAPHEAPLIST | TH32CS_SNAPPROCESS | TH32CS_SNAPTHREAD | TH32CS_SNAPMODULE,
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct HSNAPSHOT
    {
        private readonly IntPtr handle;

        public HSNAPSHOT(IntPtr preexistingHandle) => handle = preexistingHandle;

        public static HSNAPSHOT NULL => new(IntPtr.Zero);

        public bool IsNull => handle == IntPtr.Zero;

        public static explicit operator IntPtr(HSNAPSHOT h) => h.handle;

        public static implicit operator HSNAPSHOT(IntPtr h) => new(h);

        public static bool operator !=(HSNAPSHOT h1, HSNAPSHOT h2) => !(h1 == h2);

        public static bool operator ==(HSNAPSHOT h1, HSNAPSHOT h2) => h1.Equals(h2);

        public override bool Equals(object? obj) => obj is HSNAPSHOT h && handle == h.handle;

        public override int GetHashCode() => handle.GetHashCode();

        public IntPtr DangerousGetHandle() => handle;
    }

    [DllImport("Kernel32.dll", SetLastError = true, ExactSpelling = true)]
    public static extern nint CreateToolhelp32Snapshot(TH32CS dwFlags, [Optional] uint th32ProcessID);

    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool Process32First(HSNAPSHOT hSnapshot, ref PROCESSENTRY32 lppe);

    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool Process32Next(HSNAPSHOT hSnapshot, ref PROCESSENTRY32 lppe);

    [DllImport("Kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool CloseHandle(IntPtr hObject);

    [DllImport("Kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetCurrentProcess();

    public static uint GetProcessIdByName(string processName)
    {
        uint pid = 0;
        PROCESSENTRY32 pe32 = new()
        {
            dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32))
        };

        nint snap = CreateToolhelp32Snapshot(TH32CS.TH32CS_SNAPPROCESS, 0);
        if (snap != IntPtr.Zero)
        {
            if (Process32First(snap, ref pe32))
            {
                do
                {
                    if (pe32.szExeFile.Equals(processName, StringComparison.OrdinalIgnoreCase))
                    {
                        pid = pe32.th32ProcessID;
                        break;
                    }
                } while (Process32Next(snap, ref pe32));
            }
        }

        _ = CloseHandle(snap);
        return pid;
    }
}
