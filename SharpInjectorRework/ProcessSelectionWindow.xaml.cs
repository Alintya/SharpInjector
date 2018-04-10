using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Controls;

namespace SharpInjectorRework
{
    /// <summary>
    /// Interaktionslogik für ProcessSelectionWindow.xaml
    /// </summary>
    public partial class ProcessSelectionWindow : Window
    {
        public ProcessSelectionWindow(Process[] process_list = null)
        {
            InitializeComponent();

            if (process_list != null && process_list.Any())
            {
                // TODO:
            }
        }

        #region UI EVENTS

        private void SelectProcessButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO:
        }

        #endregion UI EVENTS

        #region CUSTOM UI EVENTS

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

        #endregion CUSTOM UI EVENTS
    }
}
