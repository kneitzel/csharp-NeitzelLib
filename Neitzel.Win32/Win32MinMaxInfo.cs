using System.Drawing;
using System.Runtime.InteropServices;

namespace Neitzel.Win32
{
    /// <summary>
    /// Struct to hold minimum and maximum information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Win32MinMaxInfo
    {
        /// <summary>
        /// reserved.
        /// </summary>
        public Point ptReserved;

        /// <summary>
        /// Max Size.
        /// </summary>
        public Point ptMaxSize;
        
        /// <summary>
        /// Max Position.
        /// </summary>
        public Point ptMaxPosition;
        
        /// <summary>
        /// Min Track Size.
        /// </summary>
        public Point ptMinTrackSize;
        
        /// <summary>
        /// Max Track Size.
        /// </summary>
        public Point ptMaxTrackSize;
    }
}