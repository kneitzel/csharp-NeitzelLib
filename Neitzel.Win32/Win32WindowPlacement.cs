using System.Runtime.InteropServices;

namespace Neitzel.Win32
{
    /// <summary>
    /// Information about window placement.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Win32WindowPlacement
    {
        /// <summary>
        /// length
        /// </summary>
        public int length;

        /// <summary>
        /// flags
        /// </summary>
        public int flags;
        
        /// <summary>
        /// showCmd
        /// </summary>
        public int showCmd;
        
        /// <summary>
        /// Min Position x
        /// </summary>
        public int ptMinPosition_x;
        
        /// <summary>
        /// Min Position y
        /// </summary>
        public int ptMinPosition_y;
        
        /// <summary>
        /// Max Position X
        /// </summary>
        public int ptMaxPosition_x;
        
        /// <summary>
        ///  Max Position Y
        /// </summary>
        public int ptMaxPosition_y;
        
        /// <summary>
        /// Normal position left.
        /// </summary>
        public int rcNormalPosition_left;
        
        /// <summary>
        /// Normal position top.
        /// </summary>
        public int rcNormalPosition_top;
        
        /// <summary>
        /// Normal Position right.
        /// </summary>
        public int rcNormalPosition_right;
        
        /// <summary>
        /// Normal Position bottom.
        /// </summary>
        public int rcNormalPosition_bottom;
    }
}