using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

using MetroFramework;

namespace SharpInjector.Injection
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
        public void PrepareInjection(string processName, Method method)
        {
            if (Globals.SelectedProcess.Id == -1)
            {
                MetroMessageBox.Show(Form.ActiveForm, "Process not found", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            IntPtr handleProcess = Extra.NativeMethods.OpenProcess(0x1F0FFF, 1, Globals.SelectedProcess.Id);
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
            switch (method)
            {
                case Method.Standard:
                {
                    try
                    {
                        Task.Factory.StartNew(() => Injection.Standard.Inject(hProcess, strDLLName));
                    }
                    catch (Exception)
                    {
                        
                    }

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
        }
    }
}
