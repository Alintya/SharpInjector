using System.Windows.Media;

namespace SharpInjectorRework.Utilities
{
    internal class Globals
    {
        // Info:
        // - will later be moved into the 'InjectionHandler' class
        public static System.Diagnostics.Process InjectProcess = null;

        public static Config Config = new Config();
        public static DllHandler DllHandler = new DllHandler();

        // Info:
        // - not sure if i leave it like this.. should never throw an exception
        public static SolidColorBrush MaterialRedBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCF0434B"));
        public static SolidColorBrush MaterialGreenBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC43F0A9"));
    }
}
