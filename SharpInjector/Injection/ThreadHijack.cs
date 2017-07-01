using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SharpInjector.Extra;

namespace SharpInjector.Injection
{
    internal static class ThreadHijack
    {
        public static void Inject(IntPtr processHandlePtr, string dll)
        {
            
        }

        private static readonly byte[] REDIRECT_STUB = { 0x9c, 0x60, 0xe8, 0, 0, 0, 0, 0x61, 0x9d, 0xe9, 0, 0, 0, 0 };

        public static IntPtr Inject(string dllPath, IntPtr hProcess)
        {
            IntPtr[] ptrArray = InjectAll(new [] { dllPath }, hProcess);
            if (ptrArray != null)
            {
                
            }
            return ptrArray != null && ptrArray.Length > 0 ? ptrArray[0] : IntPtr.Zero;
        }

        public static IntPtr[] InjectAll(string[] dllPaths, IntPtr hProcess)
        {
            Exception exception;
            try
            {
                if (hProcess == IntPtr.Zero)
                {
                    throw new ArgumentException("Invalid process handle.", "hProcess");
                }
                int processId = NativeMethods.GetProcessId(hProcess);
                if (processId == 0)
                {
                    throw new ArgumentException("Provided handle doesn't have sufficient permissions to inject", "hProcess");
                }
                Process processById = Process.GetProcessById(processId);
                if (processById.Threads.Count == 0)
                {
                    throw new Exception("Target process has no targetable threads to hijack.");
                }
                ProcessThread thread = SelectOptimalThread(processById);
                IntPtr ptr = NativeMethods.OpenThread(0x1a, false, thread.Id);
                if (ptr == IntPtr.Zero)
                {
                    throw new Exception("Unable to obtain a handle for the remote thread.");
                }
                IntPtr zero = IntPtr.Zero;
                IntPtr lpAddress = IntPtr.Zero;
                IntPtr ptr4 = Standard.CreateMultiLoadStub(dllPaths, hProcess, out zero, 1);
                IntPtr[] ptrArray = null;
                if (ptr4 != IntPtr.Zero)
                {
                    if (NativeMethods.SuspendThread(ptr) == uint.MaxValue)
                    {
                        throw new Exception("Unable to suspend the remote thread");
                    }
                    try
                    {
                        uint lpNumberOfBytesRead;
                        NativeMethods.CONTEXT pContext = new NativeMethods.CONTEXT
                        {
                            ContextFlags = 0x10001
                        };
                        if (!NativeMethods.GetThreadContext(ptr, ref pContext))
                        {
                            throw new InvalidOperationException("Cannot get the remote thread's context");
                        }
                        byte[] array = REDIRECT_STUB;
                        IntPtr ptr5 = NativeMethods.VirtualAllocEx(hProcess, IntPtr.Zero, (uint)array.Length, 0x3000, 0x40);
                        if (ptr5 == IntPtr.Zero)
                        {
                            throw new InvalidOperationException("Unable to allocate memory in the remote process.");
                        }
                        BitConverter.GetBytes(ptr4.Subtract(ptr5.Add(7L)).ToInt32()).CopyTo(array, 3);
                        BitConverter.GetBytes(pContext.Eip - (uint)ptr5.Add(array.Length).ToInt32()).CopyTo(array, array.Length - 4);
                        if (!(NativeMethods.WriteProcessMemory(hProcess, ptr5, array, array.Length, out lpNumberOfBytesRead) && lpNumberOfBytesRead == array.Length))
                        {
                            throw new InvalidOperationException("Unable to write stub to the remote process.");
                        }
                        pContext.Eip = (uint)ptr5.ToInt32();
                        NativeMethods.SetThreadContext(ptr, ref pContext);
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ptrArray = null;
                        NativeMethods.VirtualFreeEx(hProcess, zero, 0, 0x8000);
                        NativeMethods.VirtualFreeEx(hProcess, ptr4, 0, 0x8000);
                        NativeMethods.VirtualFreeEx(hProcess, lpAddress, 0, 0x8000);
                    }
                    NativeMethods.ResumeThread(ptr);

                    Thread.Sleep(100);
                    ptrArray = new IntPtr[dllPaths.Length];
                    byte[] buffer2 = NativeMethods.ReadRemoteMemory(hProcess, zero, (uint)dllPaths.Length << 2);
                    if (buffer2 != null)
                    {
                        for (int i = 0; i < ptrArray.Length; i++)
                        {
                            ptrArray[i] = Win32Ptr.Create(BitConverter.ToInt32(buffer2, i << 2));
                        }
                    }

                    NativeMethods.CloseHandle(ptr);
                }
                return ptrArray;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                return null;
            }
        }

        private static ProcessThread SelectOptimalThread(Process target)
        {
            return target.Threads[0];
        }
    }
}
