using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SharpInjector
{
    internal class Extra
    {
        public class Drag
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
            [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);

            public static bool IsWin64Emulator(Process process)
            {
                if ((Environment.OSVersion.Version.Major > 5) || ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1))) 
                {
                    bool retVal;
                    return IsWow64Process(process.Handle, out retVal) && retVal;
                }
                return false;
            }
        }
    }
}
