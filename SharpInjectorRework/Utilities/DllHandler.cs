using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace SharpInjectorRework.Utilities
{
    public delegate void DllHandlerEventHandler(object source, DllHandlerArgs e);

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

    internal class DllHandler
    {
        public event DllHandlerEventHandler OnDllAdd;
        public event DllHandlerEventHandler OnDllRemove;

        private readonly Dictionary<string, string> _dlls = new Dictionary<string, string>();

        public void Add(string dllPath)
        {
            var dllName = System.IO.Path.GetFileNameWithoutExtension(dllPath);

            if (_dlls.TryGetValue(dllName ?? throw new InvalidOperationException($"could not get dll name for dll: {dllPath}"), out var tempPath))
            {
                if (dllPath.Equals(tempPath))
                {
                    Utilities.Messagebox.ShowInfo($"skipped dll '{dllName}' cause it is already loaded");
                    return;
                }

                var dialogResult = Utilities.Messagebox.ShowError(
                    $"dll with name '{dllName}' already exists, do you want to override it?", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.No)
                    return;

                _dlls.Remove(dllName);

                OnDllRemove?.Invoke(this, new DllHandlerArgs(dllName, dllPath));
            }

            _dlls.Add(dllName, dllPath);

            OnDllAdd?.Invoke(this, new DllHandlerArgs(dllName, dllPath));
        }

        public void Remove(string dllName)
        {
            if (!_dlls.TryGetValue(dllName, out var dllPath))
            {
                Utilities.Messagebox.ShowError($"dll with name '{dllName}' doesnt exist");
                return;
            }

            _dlls.Remove(dllName);

            OnDllRemove?.Invoke(this, new DllHandlerArgs(dllName, dllPath));
        }

        public void RemoveAll()
        {
            // Info:
            // - remove each dll separate so we can trigger the remove event
            var dllsTemp = _dlls.ToArray();
            foreach (var dll in dllsTemp)
                this.Remove(dll.Key);

            Utilities.Messagebox.ShowInfo("removed all dlls");
        }

        public string GetPath(string dllName)
        {
            if (_dlls.TryGetValue(dllName, out var dllPath))
                return dllPath;

            Utilities.Messagebox.ShowError($"dll with name '{dllName}' doesnt exist");

            return string.Empty;

        }

        public Dictionary<string, string> GetDlls()
            => _dlls;
    }
}
