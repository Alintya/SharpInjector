using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using SharpInjectorRework.Utilities;
using Process = System.Diagnostics.Process;

namespace SharpInjectorRework
{
    public class ProcessListViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Interaktionslogik für ProcessSelectionWindow.xaml
    /// </summary>
    public partial class ProcessSelectionWindow : Window
    {
        public ProcessSelectionWindow(IReadOnlyCollection<Process> processList = null)
        {
            InitializeComponent();

            if (processList != null && processList.Any())
            {
                foreach (var process in processList)
                {
                    ProcessListView.Items.Add(new ProcessListViewItem
                    {
                        Id = process.Id,
                        Name = process.ProcessName
                    });
                }
            }
            else
            {
                PopulateProcessListView();
            }
        }

        #region UI EVENTS

        private void Headerbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ExitLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                Close();
        }

        private void MinimizeLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                WindowState = WindowState.Minimized;
        }

        private void HeaderLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void SelectProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessListView.SelectedItems.Count < 1)
            {
                Utilities.Messagebox.ShowError("select a process first");
                return;
            }

            if (!(ProcessListView.SelectedItems[0] is ProcessListViewItem selectedProcess))
            {
                Utilities.Messagebox.ShowError("selected process is invalid");
                return;
            }

            Globals.InjectProcess = Process.GetProcessById(selectedProcess.Id);

            Close();
        }

        private void ApplySearchFilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!SearchTextBox.Text.Any())
            {
                Utilities.Messagebox.ShowError("filter textbox is empty");
                return;
            }

            PopulateProcessListView(SearchTextBox.Text);
        }

        private void RefreshProcessListButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateProcessListView();
        }

        #endregion UI EVENTS

        #region CUSTOM FUNCTIONS

        public void PopulateProcessListView(string processNameContains = null)
        {
            if (ProcessListView.Items.Count > 0)
                ProcessListView.Items.Clear();

            foreach (var process in Process.GetProcesses())
            {
                if (processNameContains != null)
                {
                    if (!process.ProcessName.ToLower().Contains(processNameContains.ToLower()))
                        continue;
                }

                if (!Environment.Is64BitProcess)
                {
                    if (Utilities.Process.IsProcess64Bit(process, out var isValid) || !isValid)
                        continue;
                }

                ProcessListView.Items.Add(new ProcessListViewItem
                {
                    Id = process.Id,
                    Name = process.ProcessName
                });
            }
        }

        #endregion CUSTOM FUNCTIONS
    }
}