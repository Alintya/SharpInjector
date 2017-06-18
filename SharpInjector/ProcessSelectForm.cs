using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;

namespace SharpInjector
{
    public struct ProcessContainer
    {
        public string Name;
        public Process Process;
        public int ImageIndex;
        public bool HasWindow;
    }

    public partial class ProcessSelectForm : MetroFramework.Forms.MetroForm
    {

        //Dictionary<string, Process> ProcessIDs = new Dictionary<string, Process> { };
        //Dictionary<string, Process> WindowIDs = new Dictionary<string, Process> { };
        List<ProcessContainer> ProcessIds = new List<ProcessContainer>();

        private ImageList imgList = new ImageList { ImageSize = new Size(24, 24) };

        public ProcessSelectForm()
        {
            InitializeComponent();
        }

        private void ProcessSelectForm_Load(object sender, EventArgs e)
        {
            metroListView1.Columns.Add("", metroListView1.Width - 20, HorizontalAlignment.Left);

            metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
            metroListView1.HeaderStyle = ColumnHeaderStyle.None;

            metroListView1.SmallImageList = imgList;

            Task.Factory.StartNew(() =>
            {
                foreach (Process process in Process.GetProcesses())
                {

                    if (process.Id <= 0/* || IsWow64Process(process.MainWindowHandle)*/)
                        continue;

                    try
                    {
                        Icon ico = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
                        imgList.Images.Add(ico.ToBitmap());
                    }
                    catch (Exception)
                    {

                    }

                    string _Formatted = $"{ process.Id.ToString().PadLeft(6, '0') } - { process.ProcessName.ToLower() }";

                    ProcessIds.Add(new ProcessContainer
                    {
                        HasWindow = !(string.IsNullOrEmpty(process.MainWindowTitle)),
                        ImageIndex = imgList.Images.Count - 1,
                        Name = _Formatted,
                        Process = process
                    });

                }

                // Wait for List to get populated
                Invoke((MethodInvoker)
                    (() =>
                        {
                            // TODO Check if window is still there (got opened and immediately closed again
                            Window_List_Button.Enabled = true;
                            Process_List_Button.Enabled = true;
                        }
                    )
                );

                void ListboxInvoker()
                {
                    foreach (var entry in ProcessIds)
                    {
                        var lvi = new ListViewItem
                        {
                            Name = entry.Name,
                            ImageIndex = entry.ImageIndex,
                            Text = entry.Name
                        };
                        metroListView1.Items.Add(lvi);
                    }
                }

                metroListView1.Invoke((MethodInvoker) ListboxInvoker);

            });
        }

        private void SearchTextbox_TextChanged(object sender, EventArgs e)
        {
            List<ProcessContainer> tempList = ProcessIds.Where(x => x.Name.Contains(SearchTextbox.Text.ToLower())).ToList();

            metroListView1.Items.Clear();

            tempList.ForEach(x => metroListView1.Items.Add(new ListViewItem
            {
                Name = x.Name,
                ImageIndex = x.ImageIndex,
                Text = x.Name
            }));
        }

        private void Process_List_Button_Click(object sender, EventArgs e)
        {
            if (ProcessIds.Count == 0)
            {
                MetroMessageBox.Show(this, "ProcessIDs List is empty", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            if (metroListView1.Items.Count > 0) metroListView1.Items.Clear();

            foreach (var entry in ProcessIds)
            {
                metroListView1.Items.Add(new ListViewItem
                {
                    Name = entry.Name,
                    ImageIndex = entry.ImageIndex,
                    Text = entry.Name
                });
            }
        }

        private void Window_List_Button_Click(object sender, EventArgs e)
        {
            if (ProcessIds.Count(x => x.HasWindow) == 0)
            {
                MetroMessageBox.Show(this, "No Windows found", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            if (metroListView1.Items.Count > 0) metroListView1.Items.Clear();

            foreach (var entry in ProcessIds.Where(x => x.HasWindow))
            {
                metroListView1.Items.Add(new ListViewItem
                {
                    Name = entry.Name,
                    ImageIndex = entry.ImageIndex,
                    Text = entry.Name
                });
            }
        }

        private void Select_Button_Click(object sender, EventArgs e)
        {
            
            if (metroListView1.SelectedItems.Count < 1)
            {
                MetroMessageBox.Show(this, "Select something first", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            var selectedProcess = ProcessIds.FirstOrDefault(x => x.Name == metroListView1.SelectedItems[0].Name);
            if (selectedProcess.Process != null)
            {
                Globals.SelectedProcess = selectedProcess.Process;
                Close();
            }
            else
            {
                MetroMessageBox.Show(this, "Could not find your selected Process", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
            }
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    internal static class NativeMethods
    {
        public static bool Is64Bit(Process process)
        {
            if (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") == "x86")
                return false;

            bool isWow64;
            if (!IsWow64Process(process.Handle, out isWow64))
                throw new Win32Exception();
            return !isWow64;
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);
    }
}
