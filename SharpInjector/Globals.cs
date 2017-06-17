using System.Collections.Generic;
using System.Diagnostics;

namespace SharpInjector
{
    internal class Globals
    {
        public static Process Selected_Process = new Process();
        public static List<string> DLL_List = new List<string>();
    }
}
