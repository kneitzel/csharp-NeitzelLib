using System;
using System.Diagnostics.CodeAnalysis;

namespace Neitzel.Win32
{
    /// <summary>
    /// A windows button.
    /// </summary>
    [CLSCompliant(false)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Win32Button : Win32Window
    {
        #region Constants

        /// <summary>
        /// Button clicked command.
        /// </summary>
        public const int BN_CLICKED = 0;

        /// <summary>
        /// 
        /// </summary>
        public const int BN_PAINT = 1;

        /// <summary>
        /// 
        /// </summary>
        public const int BN_HILITE = 2;

        /// <summary>
        /// 
        /// </summary>
        public const int BN_UNHILITE = 3;

        /// <summary>
        /// 
        /// </summary>
        public const int BN_DISABLE = 4;

        /// <summary>
        /// 
        /// </summary>
        public const int BN_DOUBLECLICKED = 5;

        //public const int BN_PUSHED = BN_HILITE;
        //public const int BN_UNPUSHED = BN_UNHILITE;
        //public const int BN_DBLCLK = BN_DOUBLECLICKED;

        /// <summary>
        /// 
        /// </summary>
        public const int BN_SETFOCUS = 6;

        /// <summary>
        /// 
        /// </summary>
        public const int BN_KILLFOCUS = 7;

        /// <summary>
        /// 
        /// </summary>
        public const int BM_GETCHECK = 0x00F0;

        /// <summary>
        /// 
        /// </summary>
        public const int BM_SETCHECK = 0x00F1;

        /// <summary>
        /// 
        /// </summary>
        public const int BM_GETSTATE = 0x00F2;

        /// <summary>
        /// 
        /// </summary>
        public const int BM_SETSTATE = 0x00F3;

        /// <summary>
        /// 
        /// </summary>
        public const int BM_SETSTYLE = 0x00F4;

        /// <summary>
        /// 
        /// </summary>
        public const int BM_CLICK = 0x00F5;

        /// <summary>
        /// 
        /// </summary>
        public const int BM_GETIMAGE = 0x00F6;

        /// <summary>
        /// 
        /// </summary>
        public const int BM_SETIMAGE = 0x00F7;

        /// <summary>
        /// 
        /// </summary>
        public const int BM_SETDONTCLICK = 0x00F8;

        /// <summary>
        /// Class of a button in windows.
        /// </summary>
        public const string Win32ButtonClass = "Button";

        #endregion

        #region Lifetime

        /// <summary>
        /// Creates a new instance of Win32Button
        /// </summary>
        /// <param name="handle"></param>
        public Win32Button(IntPtr handle) : base(handle)
        {
        }

        #endregion
        /// <summary>
        /// Click the button.
        /// </summary>
        public void Click()
        {
            SendMessage(BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Find a button.
        /// </summary>
        /// <param name="parentHandle">Handle of parent to look in.</param>
        /// <param name="name">Name of the button.</param>
        /// <returns>The button or null if not found.</returns>
        public static Win32Button FindButton(IntPtr parentHandle, string name)
        {
            Win32Window parent = new Win32Window(parentHandle);
            var result = parent.FindChildWindow(Win32ButtonClass, name);
            return new Win32Button(result.Handle);
        }

        /// <summary>
        /// Find a button.
        /// </summary>
        /// <param name="parentHandle">Handle of parent to look in.</param>
        /// <param name="predecessor">Control after that we start looking for the the button.</param>
        /// <param name="name">Name of the button.</param>
        /// <returns>The button or null if not found.</returns>
        public static Win32Button FindButton(IntPtr parentHandle, Win32Window predecessor, string name)
        {
            Win32Window parent = new Win32Window(parentHandle);
            var result = parent.FindChildWindow(predecessor, Win32ButtonClass, name);
            return new Win32Button(result.Handle);
        }
    }
}
