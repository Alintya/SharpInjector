﻿using System;
using System.Runtime.InteropServices;

namespace SharpInjector
{
    internal class Extra
    {
        public class Drag
        {
            public const int WM_NCLBUTTONDOWN = 0xA1;
            public const int HT_CAPTION = 0x2;

            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
            [DllImport("user32.dll")]
            public static extern bool ReleaseCapture();
        }
    }
}
