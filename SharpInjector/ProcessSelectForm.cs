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

            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList)
            {
                if (process.Id <= 0)
                    continue;

                string formatted = $"{process.Id.ToString("X").PadLeft(4, '0')} - {process.ProcessName}";
                switch (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    case true:
                        WindowIDs.Add(formatted, process);
                        break;
                    case false:
                        ProcessIDs.Add(formatted, process);
                        break;
                    default:
                        throw new Exception("Congratz, you broke the Matrix");
                }
                /*
                // Fucking strange bug: listbox has like 100 duplicates on first fill without this
                ListBox.Items.Clear();
                foreach (var id in ProcessIDs.Keys)
                {
                    ListBox.Items.Add(id);
                }
                */
                // Default Listbox to processes
                Process_List_Button_Click(this, new EventArgs());
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

            foreach (var id in ProcessIDs.Keys)
            {
                ListBox.Items.Add(id);
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

            foreach (var id in WindowIDs.Keys)
            {
                ListBox.Items.Add(id);
            }
        }

        private void Select_Button_Click(object sender, EventArgs e)
        {
            if (ListBox.SelectedItem == null)
            {
                MessageBox.Show(this, "Select something first");
                return;
            }
            if (ProcessIDs.TryGetValue(ListBox.SelectedItem.ToString(), out Globals.SelectedProcess) || WindowIDs.TryGetValue(ListBox.SelectedItem.ToString(), out Globals.SelectedProcess))
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
