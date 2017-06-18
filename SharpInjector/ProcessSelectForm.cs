using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            Task.Factory.StartNew(() =>
            {
                Process[] processList = Process.GetProcesses();
                foreach (Process process in processList)
                {
                    if (process.Id <= 0)
                        continue;

                    string _Formatted = $"{ process.Id.ToString("X").PadLeft(6, '0') } - { process.ProcessName.ToLower() }";

                    if (!string.IsNullOrEmpty(process.MainWindowTitle)) WindowIDs.Add(_Formatted, process);

                    ProcessIDs.Add(_Formatted, process);
                }

                MethodInvoker listboxInvoker = new MethodInvoker(() =>
                {
                    foreach (var id in ProcessIDs.Keys)
                    {
                        ListBox.Items.Add(id);
                    }
                });

                ListBox.Invoke(listboxInvoker);
            });
        }

        private void SearchTextbox_TextChanged(object sender, EventArgs e)
        {
            List<string> tempList = ProcessIDs.Keys.Where(x => x.Contains(SearchTextbox.Text.ToLower())).ToList();

            ListBox.Items.Clear();

            tempList.ForEach(x => ListBox.Items.Add(x));
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

            if (ProcessIDs.TryGetValue(ListBox.SelectedItem.ToString(), out Globals.SelectedProcess))
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
