using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpInjector
{

    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        private string ProcessName = String.Empty;
        public List<string> DLL_List { get; set; }



        public Form1()
        {
            InitializeComponent();

            this.StyleManager = metroStyleManager1;

            //this.StyleManager.Style = MetroFramework.MetroColorStyle.Black;
            this.StyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;

            DLL_List = new List<string>();
        }

        #region DLLImports

        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(
          IntPtr hProcess,
          IntPtr lpThreadAttributes,
          uint dwStackSize,
          UIntPtr lpStartAddress, // raw Pointer into remote process
          IntPtr lpParameter,
          uint dwCreationFlags,
          out IntPtr lpThreadId
        );

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
            UInt32 dwDesiredAccess,
            Int32 bInheritHandle,
            Int32 dwProcessId
        );

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(
        IntPtr hObject
        );

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            UIntPtr dwSize,
            uint dwFreeType
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern UIntPtr GetProcAddress(
            IntPtr hModule,
            string procName
        );

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            uint dwSize,
            uint flAllocationType,
            uint flProtect
        );

        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            string lpBuffer,
            UIntPtr nSize,
            out IntPtr lpNumberOfBytesWritten
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(
            string lpModuleName
        );

        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        internal static extern Int32 WaitForSingleObject(
            IntPtr handle,
            Int32 milliseconds
        );

        #endregion

        public Int32 GetProcessId(String proc)
        {
            Process[] procList;
            procList = Process.GetProcessesByName(proc.Remove(proc.Length - 4));
            if (procList.Length > 0)
                return procList[0].Id;
            else
                return -1;
        }

        public void InjectDLL(IntPtr hProcess, String strDLLName)
        {
            IntPtr bytesout;

            // Length of string containing the DLL file name +1 byte padding
            Int32 lenWrite = strDLLName.Length + 1;
            // Allocate memory within the virtual address space of the target process
            IntPtr allocMem = (IntPtr)VirtualAllocEx(hProcess, (IntPtr)null, (uint)lenWrite, 0x1000, 0x40); //allocation pour WriteProcessMemory

            // Write DLL file name to allocated memory in target process
            WriteProcessMemory(hProcess, allocMem, strDLLName, (UIntPtr)lenWrite, out bytesout);
            // Function pointer "Injector"
            UIntPtr injector = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (injector == null)
            {
                MessageBox.Show(" Injector Error! \n ");
                // return failed
                return;
            }

            // Create thread in target process and store handle in hThread
            IntPtr hThread = CreateRemoteThread(hProcess, (IntPtr)null, 0, injector, allocMem, 0, out bytesout);
            // Make sure thread handle is valid
            if (hThread == null)
            {
                // incorrect thread handle, return failed
                MessageBox.Show(" hThread [ 1 ] Error! \n ");
                return;
            }

            int result = WaitForSingleObject(hThread, 10 * 1000);
            // check for timeout
            if (result == 0x00000080L || result == 0x00000102L || result == 0xFFFFFFF)
            {
                // Thread timed out
                MessageBox.Show(" hThread [ 2 ] Error! \n ");
                // Make sure thread handle is valid before closing... prevents crashes.
                if (hThread != null)
                    CloseHandle(hThread);

                return;
            }

            Thread.Sleep(1000);
            // Clear up allocated space ( Allocmem )
            VirtualFreeEx(hProcess, allocMem, (UIntPtr)0, 0x8000);
            // Make sure thread handle is valid before closing... prevents crashes.
            if (hThread != null)
            {
                CloseHandle(hThread);
            }

            return;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Int32 ProcID;

            if (ProcessName == String.Empty || !ProcessName.Contains(".exe"))
            {
                MessageBox.Show(this, "Process name is missing .exe extension or empty");
                return;
            }
                
            if (metroToggle1.Checked)
            {
                bool timedOut = false;
                System.Windows.Forms.Timer timeout = new System.Windows.Forms.Timer();
                timeout.Interval = 60000;
                timeout.Tick += (x, y) =>
                {
                    timeout.Stop();
                    timedOut = true;
                };

                timeout.Start();

                // Throw error msgbbox if process not found
                do
                {
                    ProcID = GetProcessId(ProcessName);
                    Thread.Sleep(500);
                } while (ProcID < 0 && !timedOut);
            }
            else
            {
                ProcID = GetProcessId(ProcessName);
            }

            if (ProcID >= 0)
            {
                IntPtr hProcess = OpenProcess(0x1F0FFF, 1, ProcID);
                if (hProcess == IntPtr.Zero)
                {
                    MessageBox.Show("OpenProcess() Failed!");
                    return;
                }

                foreach (string dll in DLL_List)
                {
                    try
                    {
                        InjectDLL(hProcess, dll);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }
                    MessageBox.Show(this, "Successful");
                }
                    
     
            }
            else
                MessageBox.Show(this, "Process not found");
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "EXE Files (.exe)|*.exe|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            // Call the ShowDialog method to show the dialog box.
            DialogResult? userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                this.metroTextBox1.Text = openFileDialog1.SafeFileName;
                ProcessName = openFileDialog1.SafeFileName;
            }
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }

        private void autoInjectCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void addDLLButton_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "DLL Files (.dll)|*.dll|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            // Call the ShowDialog method to show the dialog box.
            DialogResult? userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                //DLL_List.AddRange(openFileDialog1.FileNames);
                foreach (string f in openFileDialog1.FileNames)
                {
                    if (DLL_List.Contains(f))
                        continue;
                    DLL_List.Add(f);
                    metroListView1.Items.Add(f.Substring(f.LastIndexOf("\\")).Replace("\\", ""));
                }
            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!metroTextBox1.Text.Contains(".exe"))
            {
                metroTextBox1.BackColor = Color.Red;
                return;
            }

            metroTextBox1.BackColor = GetProcessId(metroTextBox1.Text) < 0 ? Color.Red : Color.White;

            ProcessName = metroTextBox1.Text;
        }

        private void metroLabel3_Click(object sender, EventArgs e)
        {

        }

        private void clearButton_Click(object sender, EventArgs e)
        {

        }

        private void removeDLLButton_Click(object sender, EventArgs e)
        {

        }
    }
}
