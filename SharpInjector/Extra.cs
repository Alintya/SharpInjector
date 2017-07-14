using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SharpInjector
{
    internal class Extra
    {
        public class Customs
        {
            public const int WM_NCLBUTTONDOWN = 0xA1;
            public const int HT_CAPTION = 0x2;

            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
            [DllImport("user32.dll")]
            public static extern bool ReleaseCapture();
        }

        internal static class NativeMethods
        {

            #region kernel32 imports

            [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);

            [DllImport("kernel32")]
            public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

            [DllImport("kernel32.dll")]
            public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, Int32 dwProcessId);

            [DllImport("kernel32.dll")]
            public static extern Int32 CloseHandle(IntPtr hObject);

            [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
            public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);

            [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

            [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, int flProtect);

            [DllImport("kernel32.dll")]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out uint lpNumberOfBytesWritten);
            [DllImport("kernel32.dll")]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, string lpBuffer, int nSize, out uint lpNumberOfBytesWritten);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr GetModuleHandle(string lpModuleName);

            [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
            public static extern Int32 WaitForSingleObject(IntPtr handle, Int32 milliseconds);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern int GetProcessId(IntPtr hProcess);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, int dwThreadId);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint ResumeThread(IntPtr hThread);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint SuspendThread(IntPtr hThread);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool GetThreadContext(IntPtr hThread, ref CONTEXT pContext);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool SetThreadContext(IntPtr hThread, ref CONTEXT pContext);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out uint lpNumberOfBytesRead);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr GetModuleHandleA(string lpModuleName);

            #endregion

            public static bool IsWin64Emulator(Process process)
            {
                if (Environment.OSVersion.Version.Major <= 5 && (Environment.OSVersion.Version.Major != 5 || Environment.OSVersion.Version.Minor < 1)) return false;
                if (!Environment.Is64BitProcess) return false;

                bool retVal;
                return IsWow64Process(process.Handle, out retVal) && retVal;
            }

            public static byte[] ReadRemoteMemory(IntPtr hProc, IntPtr address, uint len)
            {
                byte[] lpBuffer = new byte[len];
                uint lpNumberOfBytesRead = 0;
                if (!(ReadProcessMemory(hProc, address, lpBuffer, lpBuffer.Length, out lpNumberOfBytesRead) && (lpNumberOfBytesRead == len)))
                {
                    lpBuffer = null;
                }
                return lpBuffer;
            }

            public static IntPtr CreateRemotePointer(IntPtr hProcess, byte[] pData, int flProtect)
            {
                IntPtr zero = IntPtr.Zero;
                if ((pData != null) && (hProcess != IntPtr.Zero))
                {
                    zero = VirtualAllocEx(hProcess, IntPtr.Zero, (uint)pData.Length, 0x3000, flProtect);
                    uint lpNumberOfBytesRead = 0;
                    if (((zero != IntPtr.Zero) && WriteProcessMemory(hProcess, zero, pData, pData.Length, out lpNumberOfBytesRead)) && (lpNumberOfBytesRead == pData.Length))
                    {
                        return zero;
                    }
                    if (zero != IntPtr.Zero)
                    {
                        VirtualFreeEx(hProcess, zero, 0, 0x8000);
                        zero = IntPtr.Zero;
                    }
                }
                return zero;
            }

            public struct CONTEXT
            {
                public uint ContextFlags;
                public uint Dr0;
                public uint Dr1;
                public uint Dr2;
                public uint Dr3;
                public uint Dr6;
                public uint Dr7;
                public FLOATING_SAVE_AREA FloatSave;
                public uint SegGs;
                public uint SegFs;
                public uint SegEs;
                public uint SegDs;
                public uint Edi;
                public uint Esi;
                public uint Ebx;
                public uint Edx;
                public uint Ecx;
                public uint Eax;
                public uint Ebp;
                public uint Eip;
                public uint SegCs;
                public uint EFlags;
                public uint Esp;
                public uint SegSs;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x200)]
                public byte[] ExtendedRegisters;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct FLOATING_SAVE_AREA
            {
                public uint ControlWord;
                public uint StatusWord;
                public uint TagWord;
                public uint ErrorOffset;
                public uint ErrorSelector;
                public uint DataOffset;
                public uint DataSelector;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
                public byte[] RegisterArea;
                public uint Cr0NpxState;
            }
        }
    }

    public static class Win32Ptr
    {
        public static IntPtr Add(this IntPtr ptr, long val)
        {
            return new IntPtr(ptr.ToInt32() + ((int)val));
        }

        public static IntPtr Add(this IntPtr ptr, IntPtr val)
        {
            return new IntPtr(ptr.ToInt32() + val.ToInt32());
        }

        public static bool Compare(this IntPtr ptr, long value)
        {
            return (ptr.ToInt64() == value);
        }

        public static IntPtr Create(long value)
        {
            return new IntPtr((int)value);
        }

        public static bool IsNull(this IntPtr ptr)
        {
            return (ptr == IntPtr.Zero);
        }

        public static bool IsNull(this UIntPtr ptr)
        {
            return (ptr == UIntPtr.Zero);
        }

        public static IntPtr Subtract(this IntPtr ptr, long val)
        {
            return new IntPtr((int)(ptr.ToInt64() - val));
        }

        public static IntPtr Subtract(this IntPtr ptr, IntPtr val)
        {
            return new IntPtr((int)(ptr.ToInt64() - val.ToInt64()));
        }
    }
}
