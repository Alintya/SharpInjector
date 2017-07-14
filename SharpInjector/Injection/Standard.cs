using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using static SharpInjector.Extra;

using MetroFramework;

namespace SharpInjector.Injection
{
    internal static class Standard
    {
        public static void Inject(IntPtr processHandlePtr, string dll)
        {
            IntPtr injector = NativeMethods.GetProcAddress(NativeMethods.GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            Int32 lengthWrite = dll.Length + 1;
            IntPtr allocateMemory = NativeMethods.VirtualAllocEx(processHandlePtr, (IntPtr)null, (uint)lengthWrite, 0x1000, 0x40);

            uint bytesOut;
            NativeMethods.WriteProcessMemory(processHandlePtr, allocateMemory, dll, lengthWrite, out bytesOut);

            if (injector == IntPtr.Zero)
            {
                MetroMessageBox.Show(Form.ActiveForm, "Injector Error!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            IntPtr handleThread = NativeMethods.CreateRemoteThread(processHandlePtr, (IntPtr)null, 0, injector, allocateMemory, 0, out bytesOut);
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

            Thread.Sleep(1000);

            NativeMethods.VirtualFreeEx(processHandlePtr, allocateMemory, 0, 0x8000);

            NativeMethods.CloseHandle(handleThread);
        }

        public static IntPtr CreateMultiLoadStub(string[] paths, IntPtr hProcess, out IntPtr pModuleBuffer, uint nullmodule = 0)
        {
            IntPtr ptr6;
            pModuleBuffer = IntPtr.Zero;
            IntPtr zero = IntPtr.Zero;
            try
            {
                IntPtr moduleHandleA = NativeMethods.GetModuleHandleA("kernel32.dll");
                IntPtr procAddress = NativeMethods.GetProcAddress(moduleHandleA, "LoadLibraryA");
                IntPtr ptr = NativeMethods.GetProcAddress(moduleHandleA, "GetModuleHandleA");
                if (procAddress == IntPtr.Zero || ptr == IntPtr.Zero)
                {
                    throw new Exception("Unable to find necessary function entry points in the remote process");
                }
                pModuleBuffer = NativeMethods.VirtualAllocEx(hProcess, IntPtr.Zero, ((uint)paths.Length) << 2, 0x3000, 4);
                IntPtr ptr5 = NativeMethods.CreateRemotePointer(hProcess, Encoding.ASCII.GetBytes(string.Join("\0", paths) + "\0"), 4);
                if (pModuleBuffer == IntPtr.Zero || ptr5 == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Unable to allocate memory in the remote process");
                }
                try
                {
                    uint lpNumberOfBytesRead = 0;
                    byte[] array = new byte[paths.Length << 2];
                    for (int i = 0; i < (array.Length >> 2); i++)
                    {
                        BitConverter.GetBytes(nullmodule).CopyTo(array, (int)(i << 2));
                    }
                    NativeMethods.WriteProcessMemory(hProcess, pModuleBuffer, array, array.Length, out lpNumberOfBytesRead);
                    byte[] buffer2 = (byte[])MULTILOAD_STUB.Clone();
                    zero = NativeMethods.VirtualAllocEx(hProcess, IntPtr.Zero, (uint)buffer2.Length, 0x3000, 0x40);
                    if (zero == IntPtr.Zero)
                    {
                        throw new InvalidOperationException("Unable to allocate memory in the remote process");
                    }
                    BitConverter.GetBytes(ptr5.ToInt32()).CopyTo(buffer2, 7);
                    BitConverter.GetBytes(paths.Length).CopyTo(buffer2, 15);
                    BitConverter.GetBytes(pModuleBuffer.ToInt32()).CopyTo(buffer2, 0x18);
                    BitConverter.GetBytes(ptr.Subtract(zero.Add(0x38L)).ToInt32()).CopyTo(buffer2, 0x34);
                    BitConverter.GetBytes(procAddress.Subtract(zero.Add(0x45)).ToInt32()).CopyTo(buffer2, 0x41);
                    if (!(NativeMethods.WriteProcessMemory(hProcess, zero, buffer2, buffer2.Length, out lpNumberOfBytesRead) && (lpNumberOfBytesRead == buffer2.Length)))
                    {
                        throw new Exception("Error creating the remote function stub.");
                    }
                    ptr6 = zero;
                }
                finally
                {
                    NativeMethods.VirtualFreeEx(hProcess, pModuleBuffer, 0, 0x8000);
                    NativeMethods.VirtualFreeEx(hProcess, ptr5, 0, 0x8000);
                    if (zero != IntPtr.Zero)
                    {
                        NativeMethods.VirtualFreeEx(hProcess, zero, 0, 0x8000);
                    }
                    pModuleBuffer = IntPtr.Zero;
                }
            }
            catch (Exception)
            {
                ptr6 = IntPtr.Zero;
            }
            return ptr6;
        }

        static readonly byte[] MULTILOAD_STUB = {
            0x55, 0x8b, 0xec, 0x83, 0xec, 12, 0xb9, 0, 0, 0, 0, 0x89, 12, 0x24, 0xb9, 0,
            0, 0, 0, 0x89, 0x4c, 0x24, 4, 0xb9, 0, 0, 0, 0, 0x89, 0x4c, 0x24, 8,
            0x8b, 0x4c, 0x24, 4, 0x83, 0xf9, 0, 0x74, 0x3a, 0x83, 0xe9, 1, 0x89, 0x4c, 0x24, 4,
            0xff, 0x34, 0x24, 0xe8, 0, 0, 0, 0, 0x83, 0xf8, 0, 0x75, 8, 0xff, 0x34, 0x24,
            0xe8, 0, 0, 0, 0, 0x8b, 0x4c, 0x24, 8, 0x89, 1, 0x83, 0xc1, 4, 0x89, 0x4c,
            0x24, 8, 0x8b, 12, 0x24, 0x8a, 1, 0x83, 0xc1, 1, 60, 0, 0x75, 0xf7, 0x89, 12,
            0x24, 0xeb, 0xbd, 0x8b, 0xe5, 0x5d, 0xc3
        };
        static readonly byte[] MULTIUNLOAD_STUB = {
            0x55, 0x8b, 0xec, 0x83, 0xec, 12, 0xb9, 0, 0, 0, 0, 0x89, 12, 0x24, 0xb9, 0,
            0, 0, 0, 0x89, 0x4c, 0x24, 4, 0x8b, 12, 0x24, 0x8b, 9, 0x83, 0xf9, 0, 0x74,
            0x3a, 0x89, 0x4c, 0x24, 8, 0x8b, 0x4c, 0x24, 4, 0xc7, 1, 0, 0, 0, 0, 0xff,
            0x74, 0x24, 8, 0xe8, 0, 0, 0, 0, 0x83, 0xf8, 0, 0x74, 8, 0x8b, 0x4c, 0x24,
            4, 0x89, 1, 0xeb, 0xea, 0x8b, 12, 0x24, 0x83, 0xc1, 4, 0x89, 12, 0x24, 0x8b, 0x4c,
            0x24, 4, 0x83, 0xc1, 4, 0x89, 0x4c, 0x24, 4, 0xeb, 0xbc, 0x8b, 0xe5, 0x5d, 0xc3
        };
    }
}
