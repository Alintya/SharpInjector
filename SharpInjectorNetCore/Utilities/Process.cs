using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace SharpInjectorNetCore.Utilities
{
    public static class ProcessExtensions
    {
        public enum ProcessFilterType
        {
            Valid,
            Invalid
        }

        // TODO: catch access denied
        public static bool IsProcessValid(/*System.Diagnostics.Process process*/ this System.Diagnostics.Process proc)
        {
            return proc != null && !proc.HasExited;
        }

        // TODO:
        // - yes this is SHIT but i didnt find any method to check if we have permission to access a process
        public static bool IsProcess64Bit(this System.Diagnostics.Process proc, out bool isValid)
        {
            isValid = true;

            if (!Environment.Is64BitOperatingSystem)
                return false;

            var isWow64 = false;

            try
            {
                if (!IsWow64Process(proc.Handle, out isWow64))
                    throw new Exception();
            }
            catch
            {
                isValid = false;
            }

            return !isWow64;
        }

        public static void GetModule(this System.Diagnostics.Process proc, string module_name, int timeout_ms, out ProcessModule process_module)
        {
            process_module = null;

            if (!IsProcessValid(proc))
            {
                Utilities.Messagebox.ShowError($"Failed to get module '{module_name}', invalid process");
                return;
            }

            var start_tickcount = Environment.TickCount;

            try
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    foreach (ProcessModule module in proc.Modules)
                        if (module.ModuleName.ToLower().Equals(module_name.ToLower()))
                        {
                            process_module = module;
                            return;
                        }

                    if (Environment.TickCount - start_tickcount > timeout_ms)
                    {
                        Utilities.Messagebox.ShowError($"Timed out while trying to get module '{module_name}' from '{proc.ProcessName}'");
                        return;
                    }

                    proc.Refresh();
                }
            }
            catch (Exception e)
            {
                Utilities.Messagebox.ShowError($"Failed to get module '{module_name}', {e}");
            }
        }

        public static System.Diagnostics.Process[] FilterProcesses(ProcessFilterType filterType, System.Diagnostics.Process[] processList)
        {
            return processList.Where(x => filterType == ProcessFilterType.Valid ? IsProcessValid(x) : !IsProcessValid(x)).ToArray();
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);
    }
}
