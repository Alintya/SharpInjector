using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SharpInjectorNetCore.Utilities
{
    internal static class Messagebox
    {
        public static MessageBoxResult ShowError(string message, MessageBoxButton buttons = MessageBoxButton.OK)
        {
            return MessageBox.Show(message, "Error", buttons, MessageBoxImage.Error);
        }

        public static MessageBoxResult ShowInfo(string message, MessageBoxButton buttons = MessageBoxButton.OK)
        {
            return MessageBox.Show(message, "Info", buttons, MessageBoxImage.Information);
        }

        public static MessageBoxResult ShowWarning(string message, MessageBoxButton buttons = MessageBoxButton.OK)
        {
            return MessageBox.Show(message, "Warning", buttons, MessageBoxImage.Warning);
        }
    }
}
