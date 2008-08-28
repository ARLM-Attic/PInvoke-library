using System;
using System.ComponentModel;

using System.Runtime.InteropServices;

using Win32;

namespace Win32Demo
{
    //http://forums.msdn.microsoft.com/en-US/csharpgeneral/thread/8580a805-383b-4b17-8bd8-514da4a5f3a4
    //http://blogs.msdn.com/oldnewthing/archive/2005/04/18/409205.aspx
    class Program
    {
        static void Main(string[] args)
        {
            
            IntPtr hInstance = Marshal.GetHINSTANCE(typeof(Program).Module);

            string wndClassName = "Win32Demo";
            string wndName = "Win32Demo";

            //Register Window Class
            User32.WNDCLASSEX wcex = new User32.WNDCLASSEX()
            {
                cbSize = Marshal.SizeOf(typeof(User32.WNDCLASSEX)),
                style = User32.CS.CS_HREDRAW | User32.CS.CS_VREDRAW, //User32.CS.CS_GLOBALCLASS 
                lpfnWndProc = MyWndProc,
                cbClsExtra = 0,
                cbWndExtra = 0,
                hInstance = hInstance,
                hIcon = Properties.Resources.Icon1.Handle,
                hbrBackground = (IntPtr)(GDI32.COLOR.COLOR_WINDOW + 1),
                hCursor = User32.LoadCursor(hInstance, User32.IDC.IDC_ARROW),
                lpszClassName = wndClassName,
                lpszMenuName = null,
                hIconSm = IntPtr.Zero
            };

            short atom = User32.RegisterClassEx(ref wcex);

            if (atom == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            //Create Window
            IntPtr hWnd = User32.CreateWindowEx(
                User32.WS_EX.WS_EX_NONE,
                wndClassName,
                wndName,
                User32.WS.WS_OVERLAPPEDWINDOW,
                100, 100, 500, 500,
                IntPtr.Zero,
                IntPtr.Zero,
                hInstance,
                IntPtr.Zero);

            if (hWnd == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error()); //Cannot find window class???

            User32.ShowWindow(hWnd, User32.SW.SW_SHOWNORMAL);
            User32.UpdateWindow(hWnd);

        }

        private static int MyWndProc(IntPtr hwnd, uint msg, uint wParam, uint lParam)
        {
            switch (msg)
            {
                case (int)User32.WM.WM_PAINT:
                    break;

                case (int)User32.WM.WM_DESTROY:
                    User32.PostQuitMessage(0);
                    break;

                default:
                    return User32.DefWindowProc(hwnd, msg, wParam, lParam);

            }

            return 0;
        }
    }
}
