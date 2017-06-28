using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using static SharpInjector.Extra;

namespace SharpInjector.Injection
{
    internal static class Standard
    {
        public static void Inject(IntPtr hProcess, string dll)
        {
            UIntPtr injector = NativeMethods.GetProcAddress(NativeMethods.GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            Int32 lengthWrite = dll.Length + 1;
            IntPtr allocateMemory = Extra.NativeMethods.VirtualAllocEx(hProcess, (IntPtr)null, (uint)lengthWrite, 0x1000, 0x40);

            IntPtr bytesOut;
            Extra.NativeMethods.WriteProcessMemory(hProcess, allocateMemory, dll, (UIntPtr)lengthWrite, out bytesOut);

            if (injector == UIntPtr.Zero)
            {
                MetroMessageBox.Show(Form.ActiveForm, "Injector Error!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            IntPtr handleThread = NativeMethods.CreateRemoteThread(hProcess, (IntPtr)null, 0, injector, allocateMemory, 0, out bytesOut);
            if (handleThread == IntPtr.Zero)
            {
                MetroMessageBox.Show(Form.ActiveForm, "hThread [ 1 ] Error!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            int result = NativeMethods.WaitForSingleObject(handleThread, 10 * 1000);
            if (result == 0x00000080L || result == 0x00000102L || result == 0xFFFFFFF)
            {
                MetroMessageBox.Show(Form.ActiveForm, "hThread [ 2 ] Error!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                NativeMethods.CloseHandle(handleThread);
                return;
            }

            if (handleThread != null) NativeMethods.CloseHandle(handleThread);

            Thread.Sleep(1000);

            NativeMethods.VirtualFreeEx(hProcess, allocateMemory, (UIntPtr)0, 0x8000);
        }
        

    }
}
