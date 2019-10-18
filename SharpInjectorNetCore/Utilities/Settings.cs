using System;
using System.Collections.Generic;
using System.Text;

namespace SharpInjectorNetCore.Utilities
{
    internal static class Settings
    {
        public static class AutoInject
        {
            public static bool Enabled = false;
            public static string Dll { get; set; }
            public static string Process { get; set; }
            public static int Method { get; set; }
        }
    }
}
