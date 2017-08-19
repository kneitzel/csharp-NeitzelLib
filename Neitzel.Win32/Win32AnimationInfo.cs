using System.Runtime.InteropServices;

namespace Neitzel.Win32
{
    /// <summary>
    /// ANIMATIONINFO specifies animation effects associated with user actions.
    /// Used with SystemParametersInfo when SPI_GETANIMATION or SPI_SETANIMATION action is specified.
    /// </summary>
    /// <remark>
    /// The uiParam value must be set to (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO)) when using this structure.
    /// </remark>
    [StructLayout(LayoutKind.Sequential)]
    public struct Win32AnimationInfo
    {
        /// <summary>
        /// Creates an AMINMATIONINFO structure.
        /// </summary>
        /// <param name="iMinAnimate">If non-zero and SPI_SETANIMATION is specified, enables minimize/restore animation.</param>
        public Win32AnimationInfo(System.Int32 iMinAnimate)
        {
            cbSize = Marshal.SizeOf(typeof(Win32AnimationInfo));
            this.iMinAnimate = iMinAnimate;
        }

        /// <summary>
        /// Always must be set to Marshal.SizeOf(typeof(ANIMATIONINFO)).
        /// </summary>
        public int cbSize;

        /// <summary>
        /// If non-zero, minimize/restore animation is enabled, otherwise disabled.
        /// </summary>
        public int iMinAnimate;
    }
}