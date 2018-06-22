using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using SharpInjectorRework.Utilities;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using Process = System.Diagnostics.Process;

namespace SharpInjectorRework
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var compileDateTime = Assembly.GetLinkerTime();
            HeaderLabel.Content = $"SharpInjector [{compileDateTime.ToShortDateString()} - {compileDateTime.ToShortTimeString()}]";

            Globals.DllHandler.OnDllAdd += OnDllAdd;
            Globals.DllHandler.OnDllRemove += OnDllRemove;

            if (Globals.Config.Load("config") && Utilities.Settings.AutoInject.Enabled)
            {
                // TODO:
                // - check if our process in settings is valid and place into the processtextbox
                // - check if our dll in settings is valid and place into dllslistbox
                // - get inject mode & set injectmodecombobox accordingly

                if (!(new ButtonAutomationPeer(InjectSelectedButton).GetPattern(PatternInterface.Invoke) is IInvokeProvider inject_selected_button))
                {
                    Utilities.Messagebox.ShowError("failed to press inject button programmatically");
                }
                else
                {
                    inject_selected_button.Invoke();
                }
            }
        }

        #region UI EVENTS

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
                Globals.DllHandler.Add(fileName);
        }

        private void RemoveSelectedItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (DllsListBox.Items.Count <= 0)
            {
                Utilities.Messagebox.ShowError("dlls listbox is empty");
                return;
            }

            var selectedItem = DllsListBox.SelectedItem;
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
            OpenProcessSelectionWindow();
        }

        private void InjectSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Utilities.Process.IsProcessValid(Globals.InjectProcess))
            {
                Utilities.Messagebox.ShowError("no process selected or process has exited");
                return;
            }

            if (DllsListBox.Items.Count <= 0)
            {
                Utilities.Messagebox.ShowError("dlls listbox is empty");
                return;
            }

            var selectedItem = DllsListBox.SelectedItem;
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
            if (!Utilities.Process.IsProcessValid(Globals.InjectProcess))
            {
                Utilities.Messagebox.ShowError("no process selected or process has exited");
                return;
            }

            if (DllsListBox.Items.Count <= 0)
            {
                Utilities.Messagebox.ShowError("dlls listbox is empty");
                return;
            }

            // TODO:
            // - we may need to put the items into a new array depending on when we remove the dlls that got injected from the listbox
            // - 1: remove them directly after they successfully injected
            // - 2: remove the ones that injected successfully after we finished injecting all of them (extra work? -> tracking)
            // - 3: just call remove all after after we finished injecting all of them
            foreach (var dll in DllsListBox.Items)
            {
                // TODO:
                // - inject
            }
        }

        private void ProcessTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Globals.InjectProcess = null;

            var processList = new List<Process>();

            foreach (var process in Process.GetProcessesByName(ProcessTextBox.Text))
            {
                if (!Environment.Is64BitProcess)
                {
                    if (Utilities.Process.IsProcess64Bit(process, out var isValid) || !isValid)
                        continue;
                }

                processList.Add(process);
            }

            if (processList.Any())
            {
                var foundProcessListCount = processList.Count;
                if (foundProcessListCount == 1)
                {
                    Globals.InjectProcess = processList.FirstOrDefault();
                }
                else if (Utilities.Messagebox.ShowInfo($"found '{foundProcessListCount}' processes, do you want to open the process selection?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    OpenProcessSelectionWindow(processList);
                }
            }

            var newColor = Utilities.Process.IsProcessValid(Globals.InjectProcess)
                ? Globals.MaterialGreenBrush
                : Globals.MaterialRedBrush;
            if (!ProcessTextBox.BorderBrush.Equals(newColor))
                ProcessTextBox.BorderBrush = newColor;
        }

        #endregion UI EVENTS

        #region CUSTOM EVENTS

        private void OnDllAdd(object source, DllHandlerArgs e)
        {
            DllsListBox.Items.Add(e.DllName());
        }

        private void OnDllRemove(object source, DllHandlerArgs e)
        {
            DllsListBox.Items.Remove(e.DllName());
        }

        private void ProcessSelectionWindow_Closed(object sender, EventArgs e)
        {
            if (!SelectProcessButton.IsEnabled)
                SelectProcessButton.IsEnabled = true;

            // Info:
            // - cant think of anything better atm
            if (Globals.InjectProcess != null && ProcessTextBox.Text != Globals.InjectProcess.ProcessName)
                ProcessTextBox.Text = Globals.InjectProcess.ProcessName;
        }

        #endregion CUSTOM EVENTS

        #region CUSTOM FUNCTIONS

        public void OpenProcessSelectionWindow(List<Process> processList = null)
        {
            if (SelectProcessButton.IsEnabled)
                SelectProcessButton.IsEnabled = false;

            var processSelectionWindow = new ProcessSelectionWindow(processList);
            processSelectionWindow.Show();
            processSelectionWindow.Closed += ProcessSelectionWindow_Closed;
        }

        #endregion CUSTOM FUNCTIONS
    }
}
