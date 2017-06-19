using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MetroFramework;
using Color = System.Drawing.Color;
using HorizontalAlignment = System.Windows.Forms.HorizontalAlignment;
using Size = System.Drawing.Size;

namespace SharpInjector
{
    public struct ProcessContainer
    {
        public string Name;
        public Process Process;
        public int ImageIndex;
        public bool HasWindow;
        public bool IsWow64;
    }

    public partial class ProcessSelectForm : Form
    {
        private enum Filter
        {
            Window,
            None
        }

        private List<ProcessContainer> ProcessIDs = new List<ProcessContainer>();
        private ImageList ImgList = new ImageList { ImageSize = new Size(24, 24) };

        private static Thread Form_Loading_Thread { get; set; }

        public ProcessSelectForm()
        {
            InitializeComponent();
        }

        #region Custom Design

        private void Header_Background_Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Extra.Drag.ReleaseCapture();
            Extra.Drag.SendMessage(Handle, Extra.Drag.WM_NCLBUTTONDOWN, Extra.Drag.HT_CAPTION, 0);
        }

        private void Header_Line_Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Extra.Drag.ReleaseCapture();
            Extra.Drag.SendMessage(Handle, Extra.Drag.WM_NCLBUTTONDOWN, Extra.Drag.HT_CAPTION, 0);
        }

        private void Header_Title_Label_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Extra.Drag.ReleaseCapture();
            Extra.Drag.SendMessage(Handle, Extra.Drag.WM_NCLBUTTONDOWN, Extra.Drag.HT_CAPTION, 0);
        }

        private void Header_Close_Label_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Header_Close_Label_MouseEnter(object sender, EventArgs e)
        {
            Header_Close_Label.ForeColor = Color.LightGray;
        }

        private void Header_Close_Label_MouseLeave(object sender, EventArgs e)
        {
            Header_Close_Label.ForeColor = Color.White;
        }

        private void Header_Minimize_Label_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Header_Minimize_Label_MouseEnter(object sender, EventArgs e)
        {
            Header_Minimize_Label.ForeColor = Color.LightGray;
        }

        private void Header_Minimize_Label_MouseLeave(object sender, EventArgs e)
        {
            Header_Minimize_Label.ForeColor = Color.White;
        }

        #endregion

        private void ProcessSelectForm_Load(object sender, EventArgs e)
        {
            ImgList.ColorDepth = ColorDepth.Depth32Bit;

            Process_ListView.Columns.Add("", Process_ListView.Width - 20, HorizontalAlignment.Left);
            Process_ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
            Process_ListView.SmallImageList = ImgList;

            Task.Factory.StartNew(() =>
            {
                Form_Loading_Thread = Thread.CurrentThread;

                foreach (Process process in Process.GetProcesses())
                {
                    if (process.Id <= 0)
                        continue;

                    try
                    {
                        ImgList.Images.Add(Icon.ExtractAssociatedIcon(process.MainModule.FileName));

                        bool Is64b = Extra.NativeMethods.IsWin64Emulator(process);
                        ProcessIDs.Add(new ProcessContainer
                        {
                            HasWindow = !string.IsNullOrEmpty(process.MainWindowTitle),
                            ImageIndex = ImgList.Images.Count - 1,
                            // TODO offer config to show as hex
                            Name = $"{process.Id/*.PadLeft(6, '0')*/:D5} - ({(Is64b ? "x64" : "x86")}) - {process.ProcessName.ToLower()}",
                            // format specifier faster as PadLeft?
                            Process = process,
                            IsWow64 = Is64b
                        });
                    }
                    catch (Win32Exception accessDeniedException)
                    {
                        //ImgList.Images.Add(Resources._default);
                    }
                }

                Process_ListView.Invoke(new MethodInvoker(() => RefreshList(Filter.None, ProcessIDs)));

                Invoke((MethodInvoker)(() =>
                {
                    Window_List_Button.Enabled = true;
                    Process_List_Button.Enabled = true;
                }));

                Form_Loading_Thread = null;
            });
        }

        private void Process_ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Process_ListView.GetItemAt(e.X, e.Y).Name.Length == 0) return;

            Select_Button_Click(sender, e);
        }

        private void SearchTextbox_TextChanged(object sender, EventArgs e)
        {
            RefreshList(Filter.None, ProcessIDs.Where(x => x.Name.Contains(SearchTextbox.Text.ToLower())).ToList());
        }

        private void Process_List_Button_Click(object sender, EventArgs e)
        {
            if (ProcessIDs.Count == 0)
            {
                MetroMessageBox.Show(this, "ProcessID List is empty", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }
            RefreshList(Filter.None, ProcessIDs);
        }

        private void Window_List_Button_Click(object sender, EventArgs e)
        {
            if (ProcessIDs.Count(x => x.HasWindow) == 0)
            {
                MetroMessageBox.Show(this, "No Windows found", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }
            RefreshList(Filter.Window, ProcessIDs);
        }

        private void Select_Button_Click(object sender, EventArgs e)
        {
            if (Process_ListView.SelectedItems.Count < 1)
            {
                MetroMessageBox.Show(this, "Select something first", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            var selectedProcess = ProcessIDs.FirstOrDefault(x => x.Name == Process_ListView.SelectedItems[0].Name);
            if (selectedProcess.Process == null)
            {
                MetroMessageBox.Show(this, "Could not find your selected Process", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            Globals.SelectedProcess = selectedProcess.Process;
            Close();
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProcessSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form_Loading_Thread != null && Form_Loading_Thread.IsAlive) Form_Loading_Thread.Abort();
        }

        private void RefreshList(Filter filter, List<ProcessContainer> list)
        {
            if (Process_ListView.Items.Count > 0) Process_ListView.Items.Clear();

            list.ForEach(x =>
            {
                if (filter == Filter.None) 
                {
                    ListViewItem listViewItem = new ListViewItem
                    {
                        Name = x.Name,
                        ImageIndex = x.ImageIndex,
                        Text = x.Name
                    };
                    Process_ListView.Items.Add(listViewItem);
                }

                if (filter == Filter.Window && x.HasWindow)
                {
                    ListViewItem listViewItem = new ListViewItem
                    {
                        Name = x.Name,
                        ImageIndex = x.ImageIndex,
                        Text = x.Name
                    };
                    Process_ListView.Items.Add(listViewItem);
                }
            });
        }
    }
}
