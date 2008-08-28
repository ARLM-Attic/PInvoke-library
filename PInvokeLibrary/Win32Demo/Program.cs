using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

using Win32;

namespace Win32Demo
{
    /// <summary>
    /// Mimics a Win32 program using C#.  
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr hInstance = Marshal.GetHINSTANCE(typeof(Program).Module);

            string wndClassName = "Win32Demo";
            string wndName = "Win32 in C#";

            MyRegisterClass(hInstance, wndClassName);

            InitInstance(hInstance, wndClassName, wndName);

            //Message loop
            User32.MSG msg = new User32.MSG();
            while (User32.GetMessage(out msg, IntPtr.Zero, 0, 0))
            {
                User32.TranslateMessage(ref msg);
                User32.DispatchMessage(ref msg);
            }
        }

        private static void MyRegisterClass(IntPtr hInstance, string wndClassName)
        {
            //Register window class
            User32.WNDCLASSEX wcex = new User32.WNDCLASSEX()
            {
                cbSize = Marshal.SizeOf(typeof(User32.WNDCLASSEX)),
                style = User32.CS.CS_HREDRAW | User32.CS.CS_VREDRAW, 
                lpfnWndProc = MyWndProc,
                cbClsExtra = 0,
                cbWndExtra = 0,
                hInstance = hInstance,
                hIcon = Properties.Resources.Icon1.Handle,
                hbrBackground = (IntPtr)(GDI32.COLOR.COLOR_WINDOW + 1),
                hCursor = User32.LoadCursor(IntPtr.Zero, User32.IDC.IDC_ARROW),
                lpszClassName = wndClassName,
                lpszMenuName = null,
                hIconSm = IntPtr.Zero
            };

            short atom = User32.RegisterClassEx(ref wcex);

            if (atom == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        private static void InitInstance(IntPtr hInstance, string wndClassName, string wndName)
        {
            //Create window
            IntPtr hWnd = User32.CreateWindowEx(
                User32.WS_EX.WS_EX_NONE,
                wndClassName,
                wndName,
                User32.WS.WS_OVERLAPPEDWINDOW,
                User32.CW_USEDEFAULT, User32.CW_USEDEFAULT, 
                User32.CW_USEDEFAULT, User32.CW_USEDEFAULT,
                IntPtr.Zero,
                IntPtr.Zero,
                hInstance,
                IntPtr.Zero);

            if (hWnd == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error()); //Cannot find window class???

            User32.ShowWindow(hWnd, User32.SW.SW_SHOWNORMAL);
            User32.UpdateWindow(hWnd);
        }

        private static int MyWndProc(IntPtr hWnd, uint msg, uint wParam, uint lParam)
        {
            switch (msg)
            {
                case (int)User32.WM.WM_PAINT:

                    GDI32.RECT rect = new GDI32.RECT();
                    User32.GetClientRect(hWnd, out rect);

                    GDI32.PAINTSTRUCT ps = new GDI32.PAINTSTRUCT();
                    IntPtr hdc = User32.BeginPaint(hWnd, out ps);

                    User32.DrawText(hdc, "Hello, World!", -1, ref rect, 
                        User32.DT.DT_CENTER | User32.DT.DT_VCENTER | User32.DT.DT_SINGLELINE);

                    User32.EndPaint(hWnd, out ps);

                    break;

                case (int)User32.WM.WM_DESTROY:
                    User32.PostQuitMessage(0);
                    break;

                default:
                    return User32.DefWindowProc(hWnd, msg, wParam, lParam);
            }

            return 0;
        }
    }
}
