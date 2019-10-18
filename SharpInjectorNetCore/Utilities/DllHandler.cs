using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SharpInjectorNetCore.Utilities
{
    public delegate void DllHandlerDelegate(object source, DllHandlerArgs e);

    public class DllHandlerArgs : EventArgs
    {
        private readonly string _dllName;
        private readonly string _dllPath;

        public DllHandlerArgs(string dllName, string dllPath)
        {
            _dllPath = dllPath;
            _dllName = dllName;
        }

        public string DllName()
            => _dllName;
        public string DllPath()
            => _dllPath;
    }

    public class DllHandler
    {
        public event DllHandlerDelegate OnDllAdd;
        public event DllHandlerDelegate OnDllRemove;

        private readonly Dictionary<string, string> _dlls = new Dictionary<string, string>();

        public void Add(string dllPath)
        {
            var dllName = System.IO.Path.GetFileNameWithoutExtension(dllPath);

            if (_dlls.TryGetValue(dllName ?? throw new InvalidOperationException($"Could not get dll name for dll: {dllPath}"), out var tempPath))
            {
                if (dllPath.Equals(tempPath))
                {
                    Utilities.Messagebox.ShowInfo($"Skipped dll '{dllName}' cause it is already loaded");
                    return;
                }

                var dialogResult = Utilities.Messagebox.ShowError($"dll with name '{dllName}' already exists, do you want to override it?", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.No)
                    return;

                Remove(dllName);
            }

            _dlls.Add(dllName, dllPath);

#if DEBUG
            Utilities.Messagebox.ShowInfo($"Added dll: {dllPath}");
#endif

            OnDllAdd?.Invoke(this, new DllHandlerArgs(dllName, dllPath));
        }

        public void Remove(string dllName)
        {
            if (!_dlls.TryGetValue(dllName, out var dllPath))
            {
                Utilities.Messagebox.ShowError($"dll with name '{dllName}' does not exist");
                return;
            }

            _dlls.Remove(dllName);

#if DEBUG
            Utilities.Messagebox.ShowInfo($"Removed dll: {dllPath}");
#endif

            OnDllRemove?.Invoke(this, new DllHandlerArgs(dllName, dllPath));
        }

        public void RemoveAll()
        {
            // Info:
            // - remove each dll separate so we can trigger the remove event
            var dllsTemp = _dlls.ToArray();
            foreach (var dll in dllsTemp)
                Remove(dll.Key);

            Utilities.Messagebox.ShowInfo("Removed all dlls");
        }

        public string GetPath(string dllName)
        {
            if (_dlls.TryGetValue(dllName, out var dllPath))
                return dllPath;

            Utilities.Messagebox.ShowError($"dll with name '{dllName}' does not exist");

            return string.Empty;

        }

        public IReadOnlyDictionary<string, string> GetDlls()
            => _dlls;
    }
}
