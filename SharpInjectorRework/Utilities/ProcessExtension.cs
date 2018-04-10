using System.Linq;
using System.Diagnostics;

namespace SharpInjectorRework.Utilities
{
    internal class ProcessExtension
    {
        public bool IsProcessValid(Process process)
            => process != null && !process.HasExited;

        public Process[] FilterValidProcesses(Process[] process_list)
            => process_list.Where(IsProcessValid).ToArray();

        public bool GetModule(Process process, string module_name, out ProcessModule module_handle)
        {
            module_handle = null;

            if (!IsProcessValid(process))
            {
                Globals.MessageboxExtension.ShowError($"failed to get module '{module_name}', invalid process");
                return false;
            }

            foreach (ProcessModule process_module in process.Modules)
            {
                if (process_module == null)
                    continue;

                var module_name_lower = module_name.ToLower();
                var process_module_name_lower = process_module.ModuleName.ToLower();

                if (!process_module_name_lower.Equals(module_name_lower))
                    continue;

                module_handle = process_module;

                return true;
            }

            Globals.MessageboxExtension.ShowError($"failed to get module '{module_name}', module was not found");

            return false;
        }
    }
}
