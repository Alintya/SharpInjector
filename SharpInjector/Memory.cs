using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SharpInjector
{
    internal class Memory
    {
        [DllImport("kernel32")]
        private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, UIntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, Int32 dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern Int32 CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        private static extern UIntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, string lpBuffer, UIntPtr nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        private static extern Int32 WaitForSingleObject(IntPtr handle, Int32 milliseconds);

        public enum Method
        {
            Standard,
            ThreadHijacking,
            ManualMap
        }

        public void PrepareInjection(string processName, Method method)
        {
            var failed = new List<string>();

            Int32 processID = GetProcessID(processName);
            if (processID == -1)
            {
                MessageBox.Show("Process not found");
                return;
            }

            IntPtr handleProcess = OpenProcess(0x1F0FFF, 1, processID);
            if (handleProcess == IntPtr.Zero)
            {
                MessageBox.Show("OpenProcess() Failed!");
                return;
            }

            foreach (string dll in Globals.DLL_List)
            {
                try
                {
                    Inject(handleProcess, dll, method);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    failed.Add(dll);
                }
            }

            string text = $"Successfully injected {Globals.DLL_List.Count - failed.Count} dlls {Environment.NewLine}";
            if (failed.Count > 0)
            { 
                text += $"Failed: ";
                failed.ForEach(x => text += $"{x.ToString()}  ");
            }

            MessageBox.Show(text);
        }

        public Int32 GetProcessID(String proc)
        {
            Process[] processList = Process.GetProcessesByName(proc.Remove(proc.Length - 4));
            return processList.Length > 0 ? processList[0].Id : -1;
        }

        private void Inject(IntPtr hProcess, String strDLLName, Method method)
        {
            Int32 lengthWrite = strDLLName.Length + 1;
            IntPtr allocateMemory = VirtualAllocEx(hProcess, (IntPtr)null, (uint)lengthWrite, 0x1000, 0x40);

            IntPtr bytesOut;
            WriteProcessMemory(hProcess, allocateMemory, strDLLName, (UIntPtr)lengthWrite, out bytesOut);

            switch (method)
            {
                case Method.Standard:

                    UIntPtr injector = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                    if (injector == null)
                    {
                        MessageBox.Show("Injector Error! \n");
                        return;
                    }

                    IntPtr handleThread = CreateRemoteThread(hProcess, (IntPtr)null, 0, injector, allocateMemory, 0, out bytesOut);
                    if (handleThread == null)
                    {
                        MessageBox.Show("hThread [ 1 ] Error! \n");
                        return;
                    }

                    int result = WaitForSingleObject(handleThread, 10 * 1000);
                    if (result == 0x00000080L || result == 0x00000102L || result == 0xFFFFFFF)
                    {
                        MessageBox.Show("hThread [ 2 ] Error! \n");
                        if (handleThread != null) CloseHandle(handleThread);
                        return;
                    }

                    if (handleThread != null)
                        CloseHandle(handleThread);

                    break;

                case Method.ManualMap:

                    throw new NotImplementedException();
                    break;
                case Method.ThreadHijacking:

                    throw new NotImplementedException();
                    break;
                default:

                    throw new ArgumentOutOfRangeException("Unsupported injection method");
            }


            Thread.Sleep(1000);

            VirtualFreeEx(hProcess, allocateMemory, (UIntPtr)0, 0x8000);

            return;
        }
    }
}
