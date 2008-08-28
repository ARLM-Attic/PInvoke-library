using System;
using System.Runtime.InteropServices;
using System.Text;


namespace Win32
{
    public static partial class Shell32
    {
#if PocketPC
        private const string Shell32Dll = "coredll.dll";
#else
        private const string Shell32Dll = "user32.dll";
#endif

        #region dll imports

        //http://msdn.microsoft.com/en-us/library/aa456040.aspx
        [DllImport("aygshell.dll")]
        public static extern int SHDeviceLockAndPrompt();

        [DllImport(Shell32Dll)]
        public static extern int SHGetSpecialFolderPath(
            IntPtr hwndOwner, ref string lpszPath, CSIDL nFolder, bool fCreate);

        [DllImport("aygshell")]
        public static extern void SHSendBackToFocusWindow(
             uint uMsg, uint wp, int lp);

        #endregion

        #region enums & structs
        public enum CSIDL
        {
            CSIDL_DESKTOP = 0x0000, //Not supported on Smartphone.
            CSIDL_FAVORITES = 0x0006, //The file system directory that serves as a common repository for the user's favorite items.
            CSIDL_FONTS = 0x0014, //The virtual folder that contains fonts.
            CSIDL_PERSONAL = 0x0005, //The file system directory that serves as a common repository for documents.
            CSIDL_PROGRAM_FILES = 0x0026, //The program files folder.
            CSIDL_PROGRAMS = 0x0002, //The file system directory that contains the user's program groups, which are also file system directories.
            CSIDL_STARTUP = 0x0007, //The file system directory that corresponds to the user's Startup program group. The system starts these programs when a device is powered on.
            CSIDL_WINDOWS = 0x0024  //The Windows folder.
        }
        #endregion

        #region wrappers
        #endregion

        #region SIP

        public enum SIPF : uint
        {
            SIPF_OFF = 0x0000,

            SIPF_ON = 0x0001,

            SIPF_DOCKED = 0x00000002,

            SIPF_LOCKED = 0x00000004
        }

        //public struct RECT
        //{
        //    public int left;
        //    public int top;
        //    public int right;
        //    public int bottom;
        //}

        [DllImport(Shell32Dll)]
        public extern static uint SipGetInfo(SIPINFO pSipInfo);

        [DllImport(Shell32Dll)]
        public extern static uint SipSetInfo(SIPINFO pSipInfo);

        [DllImport(Shell32Dll)]
        public extern static void SipShowIM(SIPF dwFlag);

        public class SIPINFO
        {
            public SIPINFO()
            {
                cbSize = (uint)Marshal.SizeOf(this);
            }

            public uint cbSize;

            public SIPF fdwFlags;

            public GDI32.RECT rcVisibleDesktop;

            public GDI32.RECT rcSipRect;

            public uint dwImDataSize;

            public IntPtr pvImData;
        }

        // SipStatus return values
        public const uint SIP_STATUS_UNAVAILABLE = 0;
        public const uint SIP_STATUS_AVAILABLE = 1;

        [DllImport(Shell32Dll)]
        public extern static uint SipStatus();
        #endregion

        #region
        [DllImport("aygshell.dll")]
        public static extern uint SHFullScreen(IntPtr hwndRequester, SHFS dwState);

        public enum SHFS : uint
        {
            SHFS_SHOWTASKBAR = 0x0001,
            SHFS_HIDETASKBAR = 0x0002,
            SHFS_SHOWSIPBUTTON = 0x0004,
            SHFS_HIDESIPBUTTON = 0x0008,
            SHFS_SHOWSTARTICON = 0x0010,
            SHFS_HIDESTARTICON = 0x0020
        }

        #endregion

        public static string GetSpecialFolderPath(CSIDL id)
        {
            string path = string.Empty;
            SHGetSpecialFolderPath((IntPtr)0, ref path, id, false);
            return path;
        }
    }
}

