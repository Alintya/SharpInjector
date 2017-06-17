using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SharpInjector
{

    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        #region DLL IMPORTS

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

        private string ProcessName = String.Empty;

        private List<string> DLL_List { get; set; }

        public Form1()
        {
            InitializeComponent();

            DLL_List = new List<string>();
        }

        private Int32 GetProcessID(String proc)
        {
            Process[] _ProcessList = Process.GetProcessesByName(proc.Remove(proc.Length - 4));
            return _ProcessList.Length > 0 ? _ProcessList[0].Id : -1; 
        }

        private void InjectDLL(IntPtr hProcess, String strDLLName)
        {
            Int32 _LengthWrite = strDLLName.Length + 1;
            IntPtr _AllocateMemory = VirtualAllocEx(hProcess, (IntPtr)null, (uint)_LengthWrite, 0x1000, 0x40);

            IntPtr _BytesOut;
            WriteProcessMemory(hProcess, _AllocateMemory, strDLLName, (UIntPtr)_LengthWrite, out _BytesOut);

            UIntPtr _Injector = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            if (_Injector == null)
            {
                MessageBox.Show(" Injector Error! \n "); return;
            }

            IntPtr _HandleThread = CreateRemoteThread(hProcess, (IntPtr)null, 0, _Injector, _AllocateMemory, 0, out _BytesOut);
            if (_HandleThread == null)
            {
                MessageBox.Show(" hThread [ 1 ] Error! \n "); return;
            }

            int _Result = WaitForSingleObject(_HandleThread, 10 * 1000);
            if (_Result == 0x00000080L || _Result == 0x00000102L || _Result == 0xFFFFFFF)
            {
                MessageBox.Show(" hThread [ 2 ] Error! \n ");
                if (_HandleThread != null) CloseHandle(_HandleThread);  return;
            }

            Thread.Sleep(1000);

            VirtualFreeEx(hProcess, _AllocateMemory, (UIntPtr)0, 0x8000);

            if (_HandleThread != null) CloseHandle(_HandleThread); return;
        }

        private void InjectButton_Click(object sender, EventArgs e)
        {
            if (ProcessName == String.Empty || !ProcessName.Contains(".exe"))
            {
                MessageBox.Show(this, "Process name is missing .exe extension or empty"); return;
            }

            Int32 _ProcessID = 0;
            if (AutoInjectSwitch.Checked)
            {
                bool _TimeOut = false;

                System.Windows.Forms.Timer _Timer = new System.Windows.Forms.Timer();
                _Timer.Interval = 60000;

                _Timer.Tick += (x, y) =>
                {
                    _Timer.Stop();
                    _TimeOut = true;
                };

                _Timer.Start();

                while (_ProcessID < 0 && !_TimeOut)
                {
                    _ProcessID = GetProcessID(ProcessName);
                    Thread.Sleep(500);
                }
            }
            else
            {
                _ProcessID = GetProcessID(ProcessName);
            }

            if (_ProcessID >= 0)
            {
                IntPtr _HandleProcess = OpenProcess(0x1F0FFF, 1, _ProcessID);
                if (_HandleProcess == IntPtr.Zero)
                {
                    MessageBox.Show("OpenProcess() Failed!"); return;
                }

                foreach (string _DLL in DLL_List)
                {
                    try
                    {
                        InjectDLL(_HandleProcess, _DLL);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception); throw;
                    }
                    MessageBox.Show(this, "Successful");
                }
            }
            else
            {
                MessageBox.Show(this, "Process not found");
            }
        }

        private void ChooseProcessButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog _OpenFileDialog = new OpenFileDialog();
            {
                _OpenFileDialog.Filter = "EXE Files (.exe)|*.exe|All Files (*.*)|*.*";
                _OpenFileDialog.FilterIndex = 1;
                _OpenFileDialog.Multiselect = true;
            }

            DialogResult ? _DialogResultOK = _OpenFileDialog.ShowDialog();

            if (_DialogResultOK == DialogResult.OK)
            {
                ProcessNameTextbox.Text = _OpenFileDialog.SafeFileName;
                ProcessName = _OpenFileDialog.SafeFileName;
            }
        }

        private void AddDLLButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog _OpenFileDialog = new OpenFileDialog();
            {
                _OpenFileDialog.Filter = "DLL Files (.dll)|*.dll|All Files (*.*)|*.*";
                _OpenFileDialog.FilterIndex = 1;
                _OpenFileDialog.Multiselect = true;
            }

            DialogResult ? _DialogResultOK = _OpenFileDialog.ShowDialog();

            if (_DialogResultOK == DialogResult.OK)
            {
                foreach (string _File in _OpenFileDialog.FileNames)
                {
                    if (DLL_List.Contains(_File))
                        continue;

                    DLL_List.Add(_File);
                    DLLList.Items.Add(_File.Substring(_File.LastIndexOf("\\")).Replace("\\", ""));
                }
            }
        }

        private void ProcessNameTextbox_TextChanged(object sender, EventArgs e)
        {
            if (!ProcessNameTextbox.Text.Contains(".exe"))
            {
                ProcessNameTextbox.BackColor = Color.Red;
                return;
            }

            ProcessNameTextbox.BackColor = GetProcessID(ProcessNameTextbox.Text) < 0 ? Color.Red : Color.White;

            ProcessName = ProcessNameTextbox.Text;
        }
    }
}
