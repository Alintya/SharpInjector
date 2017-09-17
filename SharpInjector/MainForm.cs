using System;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;

using MetroFramework;

namespace SharpInjector
{

    public partial class MainForm : Form
    {
        private static Memory Memory_manager => new Memory();

        public MainForm()
        {
            InitializeComponent();
        }

        #region Custom Design

        private void Header_Background_Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Extra.Imports.ReleaseCapture();
            Extra.Imports.SendMessage(Handle, Extra.Drag.WM_NCLBUTTONDOWN, Extra.Drag.HT_CAPTION, 0);
        }

        private void Header_Line_Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Extra.Imports.ReleaseCapture();
            Extra.Imports.SendMessage(Handle, Extra.Drag.WM_NCLBUTTONDOWN, Extra.Drag.HT_CAPTION, 0);
        }

        private void Header_Title_Text_MouseMove(object sender, MouseEventArgs e)
        {
            Extra.Imports.ReleaseCapture();
            Extra.Imports.SendMessage(Handle, Extra.Drag.WM_NCLBUTTONDOWN, Extra.Drag.HT_CAPTION, 0);
        }

        private void Header_Close_Label_Click(object sender, EventArgs e) => Close();
        private void Header_Close_Label_MouseEnter(object sender, EventArgs e) => Header_Close_Label.ForeColor = Color.LightGray;
        private void Header_Close_Label_MouseLeave(object sender, EventArgs e) => Header_Close_Label.ForeColor = Color.White;
        private void Header_Minimize_Button_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void Header_Minimize_Button_MouseEnter(object sender, EventArgs e) => Header_Minimize_Button.ForeColor = Color.LightGray;
        private void Header_Minimize_Button_MouseLeave(object sender, EventArgs e) => Header_Minimize_Button.ForeColor = Color.White;

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            Inject_Method_Combobox.SelectedIndex = 0;

            //>> Maybe look for a better Solution
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Inject_Button.Style = (Globals.Selected_Process != null && Globals.Dll_list.Any()) ? Inject_Button.Style = MetroColorStyle.Green : Inject_Button.Style = MetroColorStyle.Red;
                    Inject_Button.Invoke(new MethodInvoker(() => Inject_Button.Refresh()));

                    Thread.Sleep(100);
                }
            });
        }

        private void Process_Name_Textbox_TextChanged(object sender, EventArgs e)
        {
            if (!Process_Name_Textbox.ContainsFocus)
            {
                Process_Name_Textbox.Text = Globals.Selected_Process.ProcessName;
                Process_Name_Textbox.Style = MetroColorStyle.Green;
            }
            else
            {
                Process_Name_Textbox.Style = MetroColorStyle.Red;
                Globals.Selected_Process = null;

                if (Process_Name_Textbox.Text.Length > 0)
                {
                    int processID;
                    processID = Memory_manager.GetProcessID($"{ Process_Name_Textbox.Text }.exe", out int instanceCount);

                    if (processID != -1)
                    {
                        Process_Name_Textbox.Style = MetroColorStyle.Green;

                        Globals.Selected_Process = Process.GetProcessById(processID);

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

            ProcessSelectForm process_select_form = new ProcessSelectForm();
            {
                process_select_form.Show();
                process_select_form.FormClosed += ProcessSelectForm_Closed;
            }
        }

        private void ProcessSelectForm_Closed(object sender, FormClosedEventArgs e)
        {
            Choose_Process_Button.Enabled = true;

            if (Globals.Selected_Process != null) Process_Name_Textbox.Text = $@"{Globals.Selected_Process.ProcessName}.exe";
        }

        private void Add_DLL_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            {
                open_file_dialog.Filter = "DLL Files (.dll)|*.dll|All Files (*.*)|*.*";
                open_file_dialog.FilterIndex = 1;
                open_file_dialog.Multiselect = true;
            }

            DialogResult ? dialog_result = open_file_dialog.ShowDialog();

            if (dialog_result != DialogResult.OK) return;

            foreach (string file in open_file_dialog.FileNames)
            {
                if (Globals.Dll_list.Contains(file)) continue;

                Globals.Dll_list.Add(file);

                UI_DLL_List.Items.Add(file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal)).Replace("\\", ""));
            }
        }

        private void Remove_DLL_Button_Click(object sender, EventArgs e)
        {
            foreach (string dll in UI_DLL_List.SelectedItems)
            {
                int index = Globals.Dll_list.FindIndex(x => x.Substring(x.LastIndexOf("\\", StringComparison.Ordinal)).Replace("\\", "").Equals(dll));

                if (index != -1)
                {
                    Globals.Dll_list.RemoveAt(index);
                }
            }
            RefreshList();
        }

        private void Clear_DLL_List_Button_Click(object sender, EventArgs e)
        {
            if (Globals.Dll_list.Count == 0)
            {
                MetroMessageBox.Show(this, "DLL List is already empty", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                return;
            }

            Globals.Dll_list.Clear();
            UI_DLL_List.Items.Clear();
        }

        private void RefreshList()
        {
            UI_DLL_List.Items.Clear();

            foreach (string dll in Globals.Dll_list) UI_DLL_List.Items.Add(dll.Substring(dll.LastIndexOf("\\", StringComparison.Ordinal)).Replace("\\", ""));
        }

        private void Inject_Button_Click(object sender, EventArgs e)
        {
            if (Process_Name_Textbox.Text == String.Empty || Process_Name_Textbox.Text.Length < 0)
            {
                MetroMessageBox.Show(this, "No Process selected", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                return;
            }

            if (Globals.Dll_list.Count == 0)
            {
                MetroMessageBox.Show(this, "No DLL found", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                return;
            }

            if (Inject_Method_Combobox.SelectedIndex < 0)
            {
                MetroMessageBox.Show(this, "Select a Inject Method first", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                return;
            }

            switch (Inject_Method_Combobox.SelectedIndex)
            {
                case 0:
                    Task.Factory.StartNew(() => Memory_manager.Inject(Process_Name_Textbox.Text, Memory.Method.Standard));
                    return;
                case 1:
                    Task.Factory.StartNew(() => Memory_manager.Inject(Process_Name_Textbox.Text, Memory.Method.ManualMap));
                    return;
                case 2:
                    Task.Factory.StartNew(() => Memory_manager.Inject(Process_Name_Textbox.Text, Memory.Method.ThreadHijacking));
                    return;
                default:
                    throw new Exception("gj m8 you broke the matrix");
            }
        }
    }
}
