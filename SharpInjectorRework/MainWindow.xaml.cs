using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Input;
using SharpInjectorRework.Utilities;

namespace SharpInjectorRework
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            var compileDateTime = Utilities.Assembly.GetLinkerTime();
            this.HeaderLabel.Content = $"SharpInjector [{compileDateTime.ToShortDateString()} - {compileDateTime.ToShortTimeString()}]";

            Utilities.Globals.DllHandler.OnDllAdd += OnDllAdd;
            Utilities.Globals.DllHandler.OnDllRemove += OnDllRemove;
        }

        #region UI EVENTS

        private void AddDllButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".dll",
                Filter = "Dll Files (.dll)|*.dll",
                Multiselect = true
            };

            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != true)
            {
                Utilities.Messagebox.ShowError("failed to add dll");
                return;
            }

            foreach (var fileName in openFileDialog.FileNames)
                Utilities.Globals.DllHandler.Add(fileName);
        }

        private void RemoveSelectedItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DllsListBox.Items.Count <= 0)
            {
                Utilities.Messagebox.ShowError("dlls listbox is empty");
                return;
            }

            var selectedItem = this.DllsListBox.SelectedItem;
            if (selectedItem == null)
            {
                Utilities.Messagebox.ShowError("no dll is selected");
                return;
            }

            Globals.DllHandler.Remove(selectedItem.ToString());
        }

        private void RemoveAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (DllsListBox.Items.Count <= 0)
            {
                Utilities.Messagebox.ShowError("dlls listbox is empty");
                return;
            }

            Globals.DllHandler.RemoveAll();
        }

        private void SelectProcessButton_Click(object sender, RoutedEventArgs e)
        {
            this.OpenProcessSelectionWindow();
        }

        private void InjectSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Utilities.Globals.ProcessHandler.IsProcessValid(Utilities.Globals.InjectProcess))
            {
                Utilities.Messagebox.ShowError("no process selected or process has exited");
                return;
            }

            if (this.DllsListBox.Items.Count <= 0)
            {
                Utilities.Messagebox.ShowError("dlls listbox is empty");
                return;
            }

            var selectedItem = this.DllsListBox.SelectedItem;
            if (selectedItem == null)
            {
                Utilities.Messagebox.ShowError("no dll is selected");
                return;
            }

            // TODO:
            // - inject
        }

        private void InjectAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Utilities.Globals.ProcessHandler.IsProcessValid(Utilities.Globals.InjectProcess))
            {
                Utilities.Messagebox.ShowError("no process selected or process has exited");
                return;
            }

            if (this.DllsListBox.Items.Count <= 0)
            {
                Utilities.Messagebox.ShowError("dlls listbox is empty");
                return;
            }

            // TODO:
            // - we may need to put the items into a new array depending on when we remove the dlls that got injected from the listbox
            foreach (var dll in this.DllsListBox.Items)
            {
                // TODO:
                // - inject
            }
        }

        private void ProcessTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Utilities.Globals.InjectProcess = null;

            var found_process_list = Process.GetProcessesByName(ProcessTextBox.Text);
            if (found_process_list.Any())
            {
                var found_process_list_count = found_process_list.Length;
                if (found_process_list_count == 1)
                {
                    Utilities.Globals.InjectProcess = found_process_list.FirstOrDefault();
                }
                else if (Utilities.Messagebox.ShowInfo($"found '{found_process_list_count}' processes, do you want to open the process selection?", 
                             MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.OpenProcessSelectionWindow();
                }
            }

            var new_color = Utilities.Globals.ProcessHandler.IsProcessValid(Utilities.Globals.InjectProcess)
                ? Utilities.Globals.MaterialGreenBrush
                : Utilities.Globals.MaterialRedBrush;
            if (!this.ProcessTextBox.BorderBrush.Equals(new_color))
                this.ProcessTextBox.BorderBrush = new_color;
        }

        #endregion UI EVENTS

        #region CUSTOM UI EVENTS

        private void Headerbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void ExitLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.Close();
        }

        private void MinimizeLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.WindowState = WindowState.Minimized;
        }

        private void HeaderLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        #endregion CUSTOM UI EVENTS

        #region CUSTOM EVENTS

        private void OnDllAdd(object source, DllHandlerArgs e)
        {
            this.DllsListBox.Items.Add(e.DllName());
        }

        private void OnDllRemove(object source, DllHandlerArgs e)
        {
            this.DllsListBox.Items.Remove(e.DllName());
        }

        private void ProcessSelectionWindow_Closed(object sender, EventArgs e)
        {
            if (!this.SelectProcessButton.IsEnabled)
                this.SelectProcessButton.IsEnabled = true;

            // Info:
            // - cant think of anything better atm
            if (this.ProcessTextBox.Text != Globals.InjectProcess.ProcessName)
                this.ProcessTextBox.Text = Globals.InjectProcess.ProcessName;
        }

        #endregion CUSTOM EVENTS

        #region CUSTOM FUNCTIONS

        public void OpenProcessSelectionWindow(Process[] process_list = null)
        {
            if (SelectProcessButton.IsEnabled)
                SelectProcessButton.IsEnabled = false;

            var processSelectionWindow = new ProcessSelectionWindow(process_list);
            processSelectionWindow.Show();
            processSelectionWindow.Closed += ProcessSelectionWindow_Closed;
        }

        #endregion CUSTOM FUNCTIONS
    }
}
