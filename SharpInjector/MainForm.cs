using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpInjector
{

    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        private Memory MemoryManager => new Memory();

        public MainForm()
        {
            InitializeComponent();
        }

        private void InjectButton_Click(object sender, EventArgs e)
        {
            if (ProcessNameTextbox.Text == String.Empty || !ProcessNameTextbox.Text.Contains(".exe"))
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
                    Task.Factory.StartNew(() => MemoryManager.PrepareInjection(ProcessNameTextbox.Text, Memory.Method.Standard));
                    return;
                case 1:
                    Task.Factory.StartNew(() => MemoryManager.PrepareInjection(ProcessNameTextbox.Text, Memory.Method.ManualMap));
                    return;
                case 2:
                    Task.Factory.StartNew(() => MemoryManager.PrepareInjection(ProcessNameTextbox.Text, Memory.Method.ThreadHijacking));
                    return;
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
                    if (Globals.DLL_List.Contains(_File))
                        continue;

                    Globals.DLL_List.Add(_File);
                    UI_DLL_List.Items.Add(_File.Substring(_File.LastIndexOf("\\")).Replace("\\", ""));
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
            ProcessNameTextbox.BackColor = MemoryManager.GetProcessID(ProcessNameTextbox.Text) < 0 ? Color.Red : Color.White;
        }

        private void ClearDLLListButton_Click(object sender, EventArgs e)
        {
            if (Globals.DLL_List.Count == 0)
            {
                MessageBox.Show(this, "DLL List is already empty");
                return;
            }

            Globals.DLL_List.Clear();
            UI_DLL_List.Items.Clear();
        }

        private void RemoveDLLButton_Click(object sender, EventArgs e)
        {
            foreach (string _DLL in UI_DLL_List.SelectedItems)
            {
                int _Index = Globals.DLL_List.FindIndex(x => x.Substring(x.LastIndexOf("\\")).Replace("\\", "").Equals(_DLL));
                if (_Index != -1)
                {
                    Globals.DLL_List.RemoveAt(_Index);
                }
            }
            RefreshList();
        }

        private void RefreshList()
        {
            UI_DLL_List.Items.Clear();

            foreach (string _DLL in Globals.DLL_List)
            {
                UI_DLL_List.Items.Add(_DLL.Substring(_DLL.LastIndexOf("\\")).Replace("\\", ""));
            }
        }
    }
}
