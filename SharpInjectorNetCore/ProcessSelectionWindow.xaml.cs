﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SharpInjectorNetCore.Utilities;
using Process = System.Diagnostics.Process;

namespace SharpInjectorNetCore
{
    /// <summary>
    /// Interaction logic for ProcessSelectionWindow.xaml
    /// </summary>
    ///

    public class ProcessListViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ImageSource Icon { get; set; }
    }

    public partial class ProcessSelectionWindow : Window
    {
        public ProcessSelectionWindow(IReadOnlyCollection<Process> processList = null)
        {
            InitializeComponent();

            if (processList != null && processList.Any())
            {
                foreach (var process in processList)
                {
                    CreateListEntry(process);
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

            Utilities.Globals.InjectProcess = Process.GetProcessById(selectedProcess.Id);
            Utilities.Globals.IsSelectedProcess = true;

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
                // Skip inaccessible system processes
                if (process.Id == 0 || process.Id == 4)
                    continue;

                if (processNameContains != null)
                {
                    if (!process.ProcessName.ToLower().Contains(processNameContains.ToLower()))
                        continue;
                }

                if (!Environment.Is64BitProcess)
                {
                    if (process.IsProcess64Bit(out var isValid) || !isValid)
                        continue;
                }

                CreateListEntry(process);
            }
        }

        private void CreateListEntry(Process proc)
        {
            ImageSource ico = new BitmapImage();

            try
            {
                ico = System.Drawing.Icon.ExtractAssociatedIcon(proc.MainModule?.FileName).ToImageSourceHIcon();
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                // TODO: set to default icon instead
            }

            ProcessListView.Items.Add(new ProcessListViewItem
            {
                Id = proc.Id,
                Name = proc.ProcessName,
                Icon = ico
            });
        }

        #endregion CUSTOM FUNCTIONS
    }
}