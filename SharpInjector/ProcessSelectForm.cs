using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace SharpInjector
{
    public partial class ProcessSelectForm : MetroFramework.Forms.MetroForm
    {
        Dictionary<string, Process> ProcessIDs = new Dictionary<string, Process> { };
        Dictionary<string, Process> WindowIDs = new Dictionary<string, Process> { };

        public ProcessSelectForm()
        {
            InitializeComponent();
        }

        private void ProcessSelectForm_Load(object sender, EventArgs e)
        {
            Process[] _ProcessList = Process.GetProcesses();
            foreach (Process _Process in _ProcessList)
            {
                if (_Process.Id == 0)
                    continue;

                string _Formated = string.Format("{0} - {1}", _Process.Id.ToString().PadLeft(8, '0'), _Process.ProcessName);
                switch (!string.IsNullOrEmpty(_Process.MainWindowTitle))
                {
                    case true:
                        WindowIDs.Add(_Formated, _Process);
                        break;
                    case false:
                        ProcessIDs.Add(_Formated, _Process);
                        break;
                }
            }
        }

        private void Process_List_Button_Click(object sender, EventArgs e)
        {
            if (ProcessIDs.Count == 0)
            {
                MessageBox.Show(this, "ProcessIDs List is empty");
                return;
            }

            if (ListBox.Items.Count > 0) ListBox.Items.Clear();

            foreach (var _ID in ProcessIDs.Keys)
            {
                ListBox.Items.Add(_ID);
            }
        }

        private void Window_List_Button_Click(object sender, EventArgs e)
        {
            if (WindowIDs.Count == 0)
            {
                MessageBox.Show(this, "WindowIDs List is empty");
                return;
            }

            if (ListBox.Items.Count > 0) ListBox.Items.Clear();

            foreach (var _ID in WindowIDs.Keys)
            {
                ListBox.Items.Add(_ID);
            }
        }

        private void Select_Button_Click(object sender, EventArgs e)
        {
            if (ListBox.SelectedItem == null)
            {
                MessageBox.Show(this, "Select something first");
                return;
            }
            if (ProcessIDs.TryGetValue(ListBox.SelectedItem.ToString(), out Globals.Selected_Process) || WindowIDs.TryGetValue(ListBox.SelectedItem.ToString(), out Globals.Selected_Process))
            {
                Close();
            }
            else
            {
                MessageBox.Show(this, "Could not find your selected Process");
                return;
            }
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
