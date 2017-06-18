using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpInjector
{

    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        private static Memory MemoryManager => new Memory();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Process_Name_Textbox_TextChanged(object sender, EventArgs e)
        {
            if (Process_Name_Textbox.Text == string.Empty || !Process_Name_Textbox.Text.Contains(".exe"))
            {
                Process_Name_Textbox.BackColor = Color.Red;
                return;
            }
            Process_Name_Textbox.BackColor = MemoryManager.GetProcessID(Process_Name_Textbox.Text) < 0 ? Color.Red : Color.White;
        }

        private void Choose_Process_Button_Click(object sender, EventArgs e)
        {
            ProcessSelectForm processSelectForm = new ProcessSelectForm();
            processSelectForm.Show();
            processSelectForm.FormClosed += ProcessSelectForm_Closed;
        }

        private void ProcessSelectForm_Closed(object sender, FormClosedEventArgs e)
        {
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

            if (dialogResultOK == DialogResult.OK)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    if (Globals.DLL_List.Contains(file))
                        continue;

                    Globals.DLL_List.Add(file);
                    UI_DLL_List.Items.Add(file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal)).Replace("\\", ""));
                }
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
                MessageBox.Show(this, "DLL List is already empty");
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
                MessageBox.Show(this, "Process name is missing .exe extension or empty");
                return;
            }

            if (Globals.DLL_List.Count == 0)
            {
                MessageBox.Show(this, "No DLL found");
                return;
            }

            int _TestingValue = 0;
            switch (_TestingValue)
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
            }
        }
    }
}
