using System.Windows;

namespace SharpInjectorRework.Utilities
{
    internal class MessageboxExtension
    {
        public MessageBoxResult ShowError(string message, MessageBoxButton buttons = MessageBoxButton.OK)
            => MessageBox.Show(message, "Error", buttons, MessageBoxImage.Error);

        public MessageBoxResult ShowInfo(string message, MessageBoxButton buttons = MessageBoxButton.OK)
            => MessageBox.Show(message, "Info", buttons, MessageBoxImage.Information);

        public MessageBoxResult ShowWarning(string message, MessageBoxButton buttons = MessageBoxButton.OK)
            => MessageBox.Show(message, "Warning", buttons, MessageBoxImage.Warning);
    }
}
