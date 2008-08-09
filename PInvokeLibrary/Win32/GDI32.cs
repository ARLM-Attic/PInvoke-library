using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using System.Runtime.InteropServices;

namespace Win32
{
    public static partial class GDI32
    {
#if PocketPC
        private const string DllName = "coredll.dll";
#else
        private const string DllName = "gdi32.dll";
#endif

        #region StretchBlt
        [DllImport(DllName)]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest,
             IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
             TernaryRasterOperations dwRop);
        #endregion

        #region BitBlt
        [DllImport(DllName)]
        public static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
                IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        //public static void DrawControlToBitMap(Control srcControl, Bitmap destBitmap, Rectangle destBounds)
        //{
        //    using (Graphics srcGraph = srcControl.CreateGraphics())
        //    {
        //        IntPtr srcHdc = srcGraph.GetHdc();
        //        User32.SendMessage(srcControl.Handle, User32.WM.WM_PRINT, (int)srcHdc, 30);

        //        using (Graphics destGraph = Graphics.FromImage(destBitmap))
        //        {
        //            IntPtr destHdc = destGraph.GetHdc();

        //            //BitBlt(destHdc, destBounds.X, destBounds.Y, destBounds.Width, destBounds.Height,
        //            //    srcHdc, 0, 0, TernaryRasterOperations.SRCCOPY);

        //            User32.SendMessage(srcControl.Handle, User32.WM.WM_PRINT, (int)destHdc, 30);

        //            destGraph.ReleaseHdc(destHdc);
        //        }

        //        srcGraph.ReleaseHdc(srcHdc);
        //    }
        //}

        /// <summary>
        /// Only print the current active form?! WM_PRINT and PrintWindow is not supported on CE.
        /// </summary>
        /// <param name="srcControl"></param>
        /// <param name="destBitmap"></param>
        /// <param name="destBounds"></param>
        public static void DrawControlToBitMap(Control srcControl, Bitmap destBitmap, Rectangle destBounds)
        {
            using (Graphics srcGraph = srcControl.CreateGraphics())
            {
                IntPtr srcHdc = srcGraph.GetHdc();
                User32.SendMessage(srcControl.Handle, User32.WM.WM_PRINT, (int)srcHdc, 30);

                using (Graphics destGraph = Graphics.FromImage(destBitmap))
                {
                    IntPtr destHdc = destGraph.GetHdc();
                    BitBlt(destHdc, destBounds.X, destBounds.Y, destBounds.Width, destBounds.Height,
                        srcHdc, 0, 0, TernaryRasterOperations.SRCCOPY);
                    destGraph.ReleaseHdc(destHdc);
                }

                srcGraph.ReleaseHdc(srcHdc);
            }
        }

        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020, /* dest = source*/
            SRCPAINT = 0x00EE0086, /* dest = source OR dest*/
            SRCAND = 0x008800C6, /* dest = source AND dest*/
            SRCINVERT = 0x00660046, /* dest = source XOR dest*/
            SRCERASE = 0x00440328, /* dest = source AND (NOT dest )*/
            NOTSRCCOPY = 0x00330008, /* dest = (NOT source)*/
            NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
            MERGECOPY = 0x00C000CA, /* dest = (source AND pattern)*/
            MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest*/
            PATCOPY = 0x00F00021, /* dest = pattern*/
            PATPAINT = 0x00FB0A09, /* dest = DPSnoo*/
            PATINVERT = 0x005A0049, /* dest = pattern XOR dest*/
            DSTINVERT = 0x00550009, /* dest = (NOT dest)*/
            BLACKNESS = 0x00000042, /* dest = BLACK*/
            WHITENESS = 0x00FF0062, /* dest = WHITE*/
        }

        public enum PRF
        {
            PRF_CHECKVISIBLE = 1,
            PRF_NONCLIENT = 2,
            PRF_CLIENT = 4,
            PRF_ERASEBKGND = 8,
            PRF_CHILDREN = 16,
            PRF_OWNED = 32
        }

        #endregion

        [DllImport(DllName)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport(DllName)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport(DllName)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);


        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left_, int top_, int right_, int bottom_)
            {
                Left = left_;
                Top = top_;
                Right = right_;
                Bottom = bottom_;
            }

            public void Shift(int x, int y)
            {
                Left += x;
                Right += x;
                Top += y;
                Bottom += y;
            }
        }
    }
}
