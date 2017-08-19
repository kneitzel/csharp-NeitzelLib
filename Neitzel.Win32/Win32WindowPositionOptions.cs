using System;

namespace Neitzel.Win32
{
    /// <summary>
    /// Win32 window position options.
    /// </summary>
    [Flags]
    public enum Win32WindowPositionOptions
    {
        /// <summary>
        /// No size.
        /// </summary>
        NoSize = 0x0001,

        /// <summary>
        /// No move.
        /// </summary>
        NoMove = 0x0002,

        /// <summary>
        /// No Z-order.
        /// </summary>
        NoZOrder = 0x0004,

        /// <summary>
        /// No redraw.
        /// </summary>
        NoRedraw = 0x0008,

        /// <summary>
        /// No activate.
        /// </summary>
        NoActivate = 0x0010,

        /// <summary>
        /// Frame changed.
        /// </summary>
        /// <remarks>
        /// Send WM_NCCALCSIZE when the frame is changed.
        /// </remarks>
        FrameChanged = 0x0020,

        /// <summary>
        /// Show.
        /// </summary>
        Show = 0x0040,

        /// <summary>
        /// Hide.
        /// </summary>
        Hide = 0x0080,

        /// <summary>
        /// No copy bits.
        /// </summary>
        NoCopyBits = 0x0100,

        /// <summary>
        /// No owner z-order.
        /// </summary>
        NoOwnerZOrder = 0x0200,

        /// <summary>
        /// No send changing.
        /// </summary>
        /// <remarks>
        /// Don't send WM_WINDOWPOSCHANGING.
        /// </remarks>
        NoSendChanging = 0x0400,

        /// <summary>
        /// Draw frame.
        /// </summary>
        DrawFrame = FrameChanged,

        /// <summary>
        /// No re-position.
        /// </summary>
        NoReposition = NoOwnerZOrder,

        /// <summary>
        /// Defer erase.
        /// </summary>
        DeferErase = 0x2000,

        /// <summary>
        /// Asynchronous window positioning.
        /// </summary>
        Asynchronous = 0x4000
    }
}