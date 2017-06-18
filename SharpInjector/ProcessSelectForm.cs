using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using SharpInjector.Properties;
using System.Threading;

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
        private List<ProcessContainer> ProcessIDs = new List<ProcessContainer>();
        private ImageList ImgList = new ImageList { ImageSize = new Size(24, 24) };

        private static Thread Form_Loading_Thread { get; set; }

        public ProcessSelectForm()
        {
            InitializeComponent();
        }

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

                    // TODO: Bessere methode finden & berechtigungs kacke fixen (x32/x64)
                    try
                    {
                        ImgList.Images.Add(Icon.ExtractAssociatedIcon(process.MainModule.FileName).ToBitmap());
                    }
                    catch (Exception)
                    {
                        ImgList.Images.Add(Resources._default);
                    }

                    ProcessIDs.Add(new ProcessContainer
                    {
                        HasWindow = !string.IsNullOrEmpty(process.MainWindowTitle),
                        ImageIndex = ImgList.Images.Count - 1,
                        Name = $"{ process.Id.ToString().PadLeft(6, '0') } - { process.ProcessName.ToLower() }",
                        Process = process
                    });
                }

                void listboxInvoker()
                {
                    ProcessIDs.ForEach(x =>
                    {
                        ListViewItem listViewItem = new ListViewItem
                        {
                            Name = x.Name,
                            ImageIndex = x.ImageIndex,
                            Text = x.Name
                        };
                        Process_ListView.Items.Add(listViewItem);
                    });
                }

                Process_ListView.Invoke((MethodInvoker)listboxInvoker);

                Invoke((MethodInvoker)(() =>
                {
                    Window_List_Button.Enabled = true;
                    Process_List_Button.Enabled = true;
                }));
            });
        }

        private void SearchTextbox_TextChanged(object sender, EventArgs e)
        {
            List<ProcessContainer> tempList = ProcessIDs.Where(x => x.Name.Contains(SearchTextbox.Text.ToLower())).ToList();

            Process_ListView.Items.Clear();

            tempList.ForEach(x =>
            {
                ListViewItem listViewItem = new ListViewItem
                {
                    Name = x.Name,
                    ImageIndex = x.ImageIndex,
                    Text = x.Name
                };
                Process_ListView.Items.Add(listViewItem);
            });
        }

        private void Process_List_Button_Click(object sender, EventArgs e)
        {
            if (ProcessIDs.Count == 0)
            {
                MetroMessageBox.Show(this, "ProcessIDs List is empty", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            if (Process_ListView.Items.Count > 0) Process_ListView.Items.Clear();

            ProcessIDs.ForEach(x =>
            {
                ListViewItem listViewItem = new ListViewItem
                {
                    Name = x.Name,
                    ImageIndex = x.ImageIndex,
                    Text = x.Name
                };
                Process_ListView.Items.Add(listViewItem);
            });
        }

        private void Window_List_Button_Click(object sender, EventArgs e)
        {
            if (ProcessIDs.Count(x => x.HasWindow) == 0)
            {
                MetroMessageBox.Show(this, "No Windows found", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 115);
                return;
            }

            if (Process_ListView.Items.Count > 0) Process_ListView.Items.Clear();

            ProcessIDs.ForEach(x =>
            {
                if (x.HasWindow)
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
            Form_Loading_Thread.Abort();
        }
    }
}
