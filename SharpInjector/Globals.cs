using System.Diagnostics;
using System.Collections.Generic;

namespace SharpInjector
{
    internal class Globals
    {
        public static Process Selected_Process;
        public static int Last_Pid = -1;
        public static List<string> Dll_list = new List<string>();
    }
}
