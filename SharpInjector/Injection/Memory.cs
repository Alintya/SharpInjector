using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MetroFramework;

namespace SharpInjector.Injection
{
    internal class Memory
    {
        #region kernel32 imports

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
        
        #endregion

        public enum Method
        {
            Standard,
            ThreadHijacking,
            ManualMap
        }

        /* TODO remove processName arg */
        public void PrepareInjection(string processName, Method method)
        {
            if (Globals.SelectedProcess.Id == -1)
            {
                MetroMessageBox.Show(Form.ActiveForm, "Process not found", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            IntPtr handleProcess = OpenProcess(0x1F0FFF, 1, Globals.SelectedProcess.Id);
            if (handleProcess == IntPtr.Zero)
            {
                MetroMessageBox.Show(Form.ActiveForm, "OpenProcess() Failed!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            var failed = new List<string>();
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

            string text = $"Successfully injected {Globals.DLL_List.Count - failed.Count} dlls";
            if (failed.Count > 0)
            { 
                text += $"{Environment.NewLine}Failed: ";
                failed.ForEach(x => text += $"{x.ToString()}  ");
            }

            // TODO catch user response in case of error to show log
            MetroMessageBox.Show(Form.ActiveForm, text, "Done", failed.Count > 0 ? MessageBoxButtons.YesNo : MessageBoxButtons.OK, failed.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        public Int32 GetProcessID(String proc, out int instances)
        {
            Process[] processList = Process.GetProcessesByName(proc.Remove(proc.Length - 4));
            instances = processList.Length;
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
                {
                    UIntPtr injector = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                    if (injector == UIntPtr.Zero) 
                    {
                        MetroMessageBox.Show(Form.ActiveForm, "Injector Error!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                        return;
                    }

                    IntPtr handleThread = CreateRemoteThread(hProcess, (IntPtr)null, 0, injector, allocateMemory, 0, out bytesOut);
                    if (handleThread == IntPtr.Zero) 
                    {
                        MetroMessageBox.Show(Form.ActiveForm, "hThread [ 1 ] Error!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                        return;
                    }

                    int result = WaitForSingleObject(handleThread, 10 * 1000);
                    if (result == 0x00000080L || result == 0x00000102L || result == 0xFFFFFFF) 
                    {
                        MetroMessageBox.Show(Form.ActiveForm, "hThread [ 2 ] Error!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                        CloseHandle(handleThread);
                        return;
                    }

                    if (handleThread != null) CloseHandle(handleThread);
                    break;
                }
                case Method.ManualMap:
                {
                    throw new NotImplementedException();
                    break;
                }
                case Method.ThreadHijacking:
                {
                    throw new NotImplementedException();
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException("Unsupported injection method");
            }

            Thread.Sleep(1000);

            VirtualFreeEx(hProcess, allocateMemory, (UIntPtr)0, 0x8000);
        }
    }
}
