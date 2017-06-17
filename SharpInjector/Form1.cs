using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpInjector
{

    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private List<string> DLL_List { get; set; }

        private Memory MemoryManager => new Memory();

        public Form1()
        {
            InitializeComponent();

            DLL_List = new List<string>();
        }

        private void InjectButton_Click(object sender, EventArgs e)
        {
            if (ProcessNameTextbox.Text == String.Empty || !ProcessNameTextbox.Text.Contains(".exe"))
            {
                MessageBox.Show(this, "Process name is missing .exe extension or empty");
                return;
            }

            if (DLL_List.Count == 0)
            {
                MessageBox.Show(this, "No DLL found");
                return;
            }

            Task.Factory.StartNew(() => MemoryManager.PrepareInjection(ProcessNameTextbox.Text, DLL_List));
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
            ProcessNameTextbox.BackColor = MemoryManager.GetProcessID(ProcessNameTextbox.Text) < 0 ? Color.Red : Color.White;
        }
    }
}
