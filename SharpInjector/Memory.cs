using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

using MetroFramework;

namespace SharpInjector
{
    internal class Memory
    {
        public enum Method
        {
            Standard,
            ThreadHijacking,
            ManualMap
        }

        /* TODO remove processName arg */
        public void Inject(Method method)
        {
            if (Globals.Selected_Process.Id == -1)
            {
                MetroMessageBox.Show(Form.ActiveForm, "Process not found", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            IntPtr process_handle = Extra.Imports.OpenProcess(0x1F0FFF, 1, Globals.Selected_Process.Id);
            if (process_handle == IntPtr.Zero)
            {
                MetroMessageBox.Show(Form.ActiveForm, "Cannot open handle to process", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            List<string> failed_injections = new List<string>();

            foreach (string dll in Globals.Dll_list)
            {
                try
                {
                    Inject(process_handle, dll, method);
                }
                catch /*(Exception exception)*/
                {
                    failed_injections.Add(dll);
                }
            }

            Globals.Last_Pid = Globals.Selected_Process.Id;

            string text = $"Successfully injected {Globals.Dll_list.Count - failed_injections.Count} dlls {Environment.NewLine}";

            if (failed_injections.Count > 0)
            {
                text += "Failed: ";
                failed_injections.ForEach(x => text += $"{x.ToString()}  ");
            }

            // TODO catch user response in case of error to show log
            // MetroMessageBox.Show(Form.ActiveForm, text, "Done", failed_injections.Count > 0 ? MessageBoxButtons.YesNo : MessageBoxButtons.OK, failed_injections.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            //MessageBox.Show(Form.ActiveForm, text, "Done", failed_injections.Count > 0 ? MessageBoxButtons.YesNo : MessageBoxButtons.OK, failed_injections.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        public Int32 GetProcessID(String proc, out int instances)
        {
            Process[] processList = Process.GetProcessesByName(proc.Remove(proc.Length - 4));

            instances = processList.Length;

            return processList.Length > 0 ? processList[0].Id : -1;
        }

        private void Inject(IntPtr hProcess, string strDLLName, Method method)
        {
            int length_write = strDLLName.Length + 1;

            IntPtr allocated_memory = Extra.Imports.VirtualAllocEx(hProcess, IntPtr.Zero, (uint)length_write, 0x1000, 0x40);

            Extra.Imports.WriteProcessMemory(hProcess, allocated_memory, strDLLName, (UIntPtr)length_write, out IntPtr bytesOut);

            switch (method)
            {
                case Method.Standard:
                {
                    UIntPtr injector = Extra.Imports.GetProcAddress(Extra.Imports.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                    if (injector == UIntPtr.Zero)
                    {
                        MetroMessageBox.Show(Form.ActiveForm, "Injector Error!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                        return;
                    }

                    IntPtr thread_handle = Extra.Imports.CreateRemoteThread(hProcess, IntPtr.Zero, 0, injector, allocated_memory, 0, out bytesOut);
                    if (thread_handle == IntPtr.Zero)
                    {
                        MetroMessageBox.Show(Form.ActiveForm, "hThread [1] Error!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                        return;
                    }

                    int result = Extra.Imports.WaitForSingleObject(thread_handle, 10 * 1000);
                    if (result == 0x00000080L || result == 0x00000102L || result == 0xFFFFFFF)
                    {
                        MetroMessageBox.Show(Form.ActiveForm, "hThread [2] Error!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);

                        Extra.Imports.CloseHandle(thread_handle);

                        return;
                    }

                    if (thread_handle != null) Extra.Imports.CloseHandle(thread_handle);

                    break;
                }
                case Method.ManualMap:
                {
                    throw new NotImplementedException("Not Implemented!");
                }
                case Method.ThreadHijacking:
                {
                    throw new NotImplementedException("Not Implemented!");
                }
                default:
                    throw new ArgumentOutOfRangeException("Unsupported injection method");
            }

            Thread.Sleep(1000);

            Extra.Imports.VirtualFreeEx(hProcess, allocated_memory, (UIntPtr)0, 0x8000);
        }
    }
}
