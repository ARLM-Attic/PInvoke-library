using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Win32;

namespace Win32Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr hwnd = User32.FindWindow(null, "Untitled - Notepad");
            if (hwnd != IntPtr.Zero)
                User32.SetWindowText(hwnd, "You are screwed!");
        }
    }
}
