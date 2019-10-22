using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Bleak;

namespace SharpInjectorNetCore
{
    internal class InjectionManager
    {
        public Process CurrentProcess => _currentProcess;

        private Process _currentProcess;
        private readonly List<Injector> _injectors;


        public InjectionManager()
        {
            _injectors = new List<Injector>();

            _currentProcess.Exited += CurrentProcessOnExited;
        }

        public bool Inject(int procId, string dllPath, InjectionMethod method)
        {
            var injector = new Injector(procId, dllPath, method, InjectionFlags.None);

            var dllBase = injector.InjectDll();

            if (dllBase == IntPtr.Zero)
                return false;

            _injectors.Add(injector);

            return true;
        }

        public bool Inject(int procId, byte[] bytes, InjectionMethod method)
        {
            var injector = new Injector(procId, bytes, method, InjectionFlags.None);

            var dllBase = injector.InjectDll();

            if (dllBase == IntPtr.Zero)
                return false;

            _injectors.Add(injector);

            return true;
        }

        public void EjectAll()
        {
            foreach (var injector in _injectors)
            {
                injector.EjectDll();
            }
        }

        private void CheckCurrentProcess(Int32 id)
        {
            if (_currentProcess == null)
            {
                _currentProcess = Process.GetProcessById(id);
                return;
            }

            if (id != _currentProcess.Id)
            {
                // Eject all?
                ClearInjectors();
            }
        }

        private void ClearInjectors()
        {
            if (_injectors == null) 
                return;

            foreach (var injector in _injectors)
            {
                injector.Dispose();
            }

            _injectors.Clear();
        }

        private void CurrentProcessOnExited(object sender, EventArgs e)
        {
            ClearInjectors();
        }
    }
}
