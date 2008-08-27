using System;

using System.Runtime.InteropServices;

using Win32;

namespace Win32Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr hInstance = Marshal.GetHINSTANCE(typeof(Program).Module);

            User32.WNDCLASSEX wcex = new User32.WNDCLASSEX()
            {
                cbSize = Marshal.SizeOf(typeof(User32.WNDCLASSEX)),
                style = User32.CS.CS_HREDRAW | User32.CS.CS_VREDRAW,
                lpfnWndProc = MyWndProc,
                cbClsExtra = 0,
                cbWndExtra = 0,
                hInstance = hInstance,
                hIcon = IntPtr.Zero,
                hbrBackground = IntPtr.Zero,
                hCursor = IntPtr.Zero,
                lpszClassName = "Win32Demo",
                lpszMenuName = null,
                hIconSm = IntPtr.Zero
            };

            User32.RegisterClassEx(ref wcex);




            //        wcex.style = CS_HREDRAW | CS_VREDRAW;
            //wcex.lpfnWndProc = WndProc;
            //wcex.cbClsExtra = 0;
            //wcex.cbWndExtra = 0;
            //wcex.hInstance = hInstance;
            //wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_WIN32APP));
            //wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
            //wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
            //wcex.lpszMenuName = MAKEINTRESOURCE(IDC_WIN32APP);
            //wcex.lpszClassName = szWindowClass;
            //wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));
            

        //     wcex.cbSize = Marshal.SizeOf(typeof(WndClassEx)),

        //wcex.style = (int)ClassStyles.CS_GLOBALCLASS

        //wcex.cbClsExtra = 0;

        //wcex.cbWndExtra = 0,

        //wcex.hbrBackground = IntPtr.Zero,

        //wcex.hCursor = IntPtr.Zero,

        //wcex.hIcon = IntPtr.Zero,

        //wcex.hIconSm = IntPtr.Zero,

        //wcex.lpszClassName = "SharpShellMainClass",

        //wcex.lpszMenuName = null,

        //wcex.hInstance = hInstance,

        //wcex.lpfnWndProc = this.WndProc


        }

        private static int MyWndProc(IntPtr hwnd, uint msg, uint wParam, uint lParam)
        {

            return User32.DefWindowProc(hwnd, msg, wParam, lParam);
        }
    }
}
