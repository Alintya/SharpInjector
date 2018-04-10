using System.Diagnostics;
using System.Windows.Media;

namespace SharpInjectorRework.Utilities
{
    internal class Globals
    {
        public static Process InjectProcess = null;
        public static DllHandler DllHandler = new DllHandler();
        public static ProcessHandler ProcessHandler = new ProcessHandler();

        // Info:
        // - not sure if i leave it like this.. should never throw an exception
        public static SolidColorBrush MaterialRedBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCF0434B"));
        public static SolidColorBrush MaterialGreenBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC43F0A9"));
    }
}
