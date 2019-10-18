using System;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SharpInjectorRework.Utilities
{
    public static class Process
    {
        public enum ProcessFilterType
        {
            Valid,
            Invalid
        }

        public static bool IsProcessValid(System.Diagnostics.Process process)
        {
            return process != null && !process.HasExited;
        }

        public static System.Diagnostics.Process[] FilterProcesses(ProcessFilterType filterType, System.Diagnostics.Process[] processList)
        {
            return processList.Where(x => filterType == ProcessFilterType.Valid ? IsProcessValid(x) : !IsProcessValid(x)).ToArray();
        }

        // TODO:
        // - yes this is SHIT but i didnt find any method to check if we have permission to access a process
        public static bool IsProcess64Bit(System.Diagnostics.Process process, out bool isValid)
        {
            isValid = true;

            if (!Environment.Is64BitOperatingSystem)
                return false;

            var isWow64 = false;

            try
            {
                if (!IsWow64Process(process.Handle, out isWow64))
                    throw new Exception();
            }
            catch
            {
                isValid = false;
            }

            return !isWow64;
        }

        public static void GetModule(System.Diagnostics.Process process, string module_name, int timeout_ms, out ProcessModule process_module)
        {
            process_module = null;

            if (!IsProcessValid(process))
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

                    foreach (ProcessModule module in process.Modules)
                        if (module.ModuleName.ToLower().Equals(module_name.ToLower()))
                        {
                            process_module = module;
                            return;
                        }

                    if (Environment.TickCount - start_tickcount > timeout_ms)
                    {
                        Utilities.Messagebox.ShowError($"Timed out while trying to get module '{module_name}' from '{process.ProcessName}'");
                        return;
                    }

                    process.Refresh();
                }
            }
            catch (Exception e)
            {
                Utilities.Messagebox.ShowError($"Failed to get module '{module_name}', {e}");
            }
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);
    }
}
