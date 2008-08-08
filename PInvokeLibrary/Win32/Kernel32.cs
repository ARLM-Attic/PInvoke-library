using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Win32
{
    public static class Kernel32
    {
        [DllImport("coredll", SetLastError = true)]
        public static extern void SetForegroundWindow(IntPtr hwnd);

        [DllImport("coredll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("coredll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
    }
}
