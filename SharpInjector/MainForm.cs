using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using MetroFramework;

namespace SharpInjector
{

    public partial class MainForm : Form
    {
        private static Memory MemoryManager => new Memory();

        public MainForm()
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

        private void Header_Title_Text_MouseMove(object sender, MouseEventArgs e)
        {
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

        private void Header_Minimize_Button_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Header_Minimize_Button_MouseEnter(object sender, EventArgs e)
        {
            Header_Minimize_Button.ForeColor = Color.LightGray;
        }

        private void Header_Minimize_Button_MouseLeave(object sender, EventArgs e)
        {
            Header_Minimize_Button.ForeColor = Color.White;
        }

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            //>> Maybe look for a better Solution
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Inject_Button.Style = Globals.SelectedProcess != null && Globals.DLL_List.Any() ? Inject_Button.Style = MetroColorStyle.Green : Inject_Button.Style = MetroColorStyle.Red;
                    Inject_Button.Invoke(new MethodInvoker(() => Inject_Button.Refresh()));

                    Thread.Sleep(100);
                }
            });
        }

        private void Process_Name_Textbox_TextChanged(object sender, EventArgs e)
        {
            if (!Process_Name_Textbox.ContainsFocus)
            {
                Process_Name_Textbox.Text = $@"{Globals.SelectedProcess.ProcessName}.exe";
                Process_Name_Textbox.Style = MetroColorStyle.Green;
            }
            else
            {
                Process_Name_Textbox.Style = MetroColorStyle.Red;
                Globals.SelectedProcess = null;

                if (Process_Name_Textbox.Text.EndsWith(".exe")) 
                {
                    int processID, instanceCount;
                    processID = MemoryManager.GetProcessID(Process_Name_Textbox.Text, out instanceCount);

                    if (processID != -1) 
                    {
                        Process_Name_Textbox.Style = MetroColorStyle.Green;

                        Globals.SelectedProcess = Process.GetProcessById(processID);

                        if (instanceCount > 1) MetroMessageBox.Show(this, $"Found {instanceCount} instances named '{Process_Name_Textbox.Text}', using the one with ID: {processID} \n\nYou might want to use 'Choose Process' if you want to inject it into a different instance", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information, 200);
                    }
                }
            }
            
            Process_Name_Textbox.Refresh();
        }

        private void Process_Name_Textbox_StyleChanged(object sender, EventArgs e)
        {
            MessageBox.Show(Process_Name_Textbox.Style.ToString());
            // TODO use to display process info or "not found" string
        }

        private void Choose_Process_Button_Click(object sender, EventArgs e)
        {
            Choose_Process_Button.Enabled = false;

            ProcessSelectForm processSelectForm = new ProcessSelectForm();
            processSelectForm.Show();
            processSelectForm.FormClosed += ProcessSelectForm_Closed;
        }

        private void ProcessSelectForm_Closed(object sender, FormClosedEventArgs e)
        {
            Choose_Process_Button.Enabled = true;

            if (Globals.SelectedProcess != null)
            {
                Process_Name_Textbox.Text = $@"{Globals.SelectedProcess.ProcessName}.exe";
            }
        }

        private void Add_DLL_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            {
                openFileDialog.Filter = "DLL Files (.dll)|*.dll|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = true;
            }

            DialogResult ? dialogResultOK = openFileDialog.ShowDialog();

            if (dialogResultOK != DialogResult.OK) return;

            foreach (string file in openFileDialog.FileNames)
            {
                if (Globals.DLL_List.Contains(file))
                    continue;

                Globals.DLL_List.Add(file);
                UI_DLL_List.Items.Add(file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal)).Replace("\\", ""));
            }
        }

        private void Remove_DLL_Button_Click(object sender, EventArgs e)
        {
            foreach (string dll in UI_DLL_List.SelectedItems)
            {
                int index = Globals.DLL_List.FindIndex(x => x.Substring(x.LastIndexOf("\\", StringComparison.Ordinal)).Replace("\\", "").Equals(dll));
                if (index != -1)
                {
                    Globals.DLL_List.RemoveAt(index);
                }
            }
            RefreshList();
        }

        private void Clear_DLL_List_Button_Click(object sender, EventArgs e)
        {
            if (Globals.DLL_List.Count == 0)
            {
                MetroMessageBox.Show(this, "DLL List is already empty", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                return;
            }

            Globals.DLL_List.Clear();
            UI_DLL_List.Items.Clear();
        }

        private void RefreshList()
        {
            UI_DLL_List.Items.Clear();

            foreach (string dll in Globals.DLL_List)
            {
                UI_DLL_List.Items.Add(dll.Substring(dll.LastIndexOf("\\", StringComparison.Ordinal)).Replace("\\", ""));
            }
        }

        private void Inject_Button_Click(object sender, EventArgs e)
        {
            if (Process_Name_Textbox.Text == String.Empty || !Process_Name_Textbox.Text.Contains(".exe"))
            {
                MetroMessageBox.Show(this, "No Process selected", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                return;
            }

            if (Globals.DLL_List.Count == 0)
            {
                MetroMessageBox.Show(this, "No DLL found", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                return;
            }

            switch (Inject_Method_Combobox.SelectedIndex)
            {
                case 0:
                    Task.Factory.StartNew(() => MemoryManager.PrepareInjection(Process_Name_Textbox.Text, Memory.Method.Standard));
                    return;
                case 1:
                    Task.Factory.StartNew(() => MemoryManager.PrepareInjection(Process_Name_Textbox.Text, Memory.Method.ManualMap));
                    return;
                case 2:
                    Task.Factory.StartNew(() => MemoryManager.PrepareInjection(Process_Name_Textbox.Text, Memory.Method.ThreadHijacking));
                    return;
                default:
                    throw new Exception("gj m8 you broke the matrix");
            }
        }
    }
}
