using System;

namespace Neitzel.Win32
{
    /// <summary>
    /// Win32 redraw flags.
    /// </summary>
    [Flags]
    public enum Win32RedrawOptions
    {
        /// <summary>
        /// Invalidate.
        /// </summary>
        Invalidate = 0x0001,

        /// <summary>
        /// Internal paint.
        /// </summary>
        InternalPaint = 0x0002,

        /// <summary>
        /// Erase.
        /// </summary>
        Erase = 0x0004,

        /// <summary>
        /// Validate.
        /// </summary>
        Validate = 0x0008,

        /// <summary>
        /// No internal paint.
        /// </summary>
        NoInternalPaint = 0x0010,

        /// <summary>
        /// No erase.
        /// </summary>
        NoErase = 0x0020,

        /// <summary>
        /// No children.
        /// </summary>
        NoChildren = 0x0040,

        /// <summary>
        /// All children.
        /// </summary>
        AllChildren = 0x0080,

        /// <summary>
        /// Update now.
        /// </summary>
        UpdateNow = 0x0100,

        /// <summary>
        /// Erase now.
        /// </summary>
        EraseNow = 0x0200,

        /// <summary>
        /// Frame.
        /// </summary>
        Frame = 0x0400,

        /// <summary>
        /// No frame.
        /// </summary>
        NoFrame = 0x0800
    }
}