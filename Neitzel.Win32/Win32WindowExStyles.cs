using System;

namespace Neitzel.Win32
{
    /// <summary>
    /// Extended window styles.
    /// </summary>
    [Flags]
    public enum Win32WindowExStyles : int
    {
        /// <summary>
        /// 
        /// </summary>
        WS_EX_DLGMODALFRAME = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_NOPARENTNOTIFY = 0x00000004,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_TOPMOST = 0x00000008,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_ACCEPTFILES = 0x00000010,
        /// <summary>
        /// Specifies that a window should not be painted until siblings beneath the window (that were created by the same thread) 
        /// have been painted. You can use it for hit testing - the mouse events will be passed to the other windows underneath the layered window.
        /// </summary>
        WS_EX_TRANSPARENT = 0x00000020,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_MDICHILD = 0x00000040,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_TOOLWINDOW = 0x00000080,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_WINDOWEDGE = 0x00000100,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_CLIENTEDGE = 0x00000200,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_CONTEXTHELP = 0x00000400,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_RIGHT = 0x00001000,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_LEFT = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_RTLREADING = 0x00002000,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_LTRREADING = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_LEFTSCROLLBAR = 0x00004000,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_RIGHTSCROLLBAR = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_CONTROLPARENT = 0x00010000,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_STATICEDGE = 0x00020000,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_APPWINDOW = 0x00040000,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),
        /// <summary>
        /// 
        /// </summary>
        WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),
        /// <summary>
        /// 
        /// </summary>
        WS_EX_LAYERED = 0x00080000,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_NOINHERITLAYOUT = 0x00100000, // Disable inheritance of mirroring by children
        /// <summary>
        /// 
        /// </summary>
        WS_EX_LAYOUTRTL = 0x00400000, // Right to left mirroring
        /// <summary>
        /// 
        /// </summary>
        WS_EX_COMPOSITED = 0x02000000,
        /// <summary>
        /// 
        /// </summary>
        WS_EX_NOACTIVATE = 0x08000000
    }
}