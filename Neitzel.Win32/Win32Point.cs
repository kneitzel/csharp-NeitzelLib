using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Neitzel.Win32
{
    /// <summary>
    /// Win32 point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Win32Point
    {
        /// <summary>
        /// X coordinate.
        /// </summary>
        public short x;
        /// <summary>
        /// Y Coordinate
        /// </summary>
        public short y;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public Win32Point(short x, short y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Converts a <see cref="Win32Point"/> to a <see cref="Point"/>.
        /// </summary>
        public static Point ToPoint(Win32Point value)
        {
            return new Point(value.x, value.y);
        }

        /// <summary>
        /// Converts a <see cref="Point" /> to a <see cref="Win32Point"/>.
        /// </summary>
        public static Win32Point FromPoint(Point value)
        {
            return new Win32Point(Convert.ToInt16(value.X), Convert.ToInt16(value.Y));
        }

        /// <summary>
        /// Implicitly converts a <see cref="Win32Point"/> to a <see cref="Point"/>.
        /// </summary>
        public static implicit operator Point(Win32Point value)
        {
            return ToPoint(value);
        }

        /// <summary>
        /// Implicitly converts a 
        /// </summary>
        public static implicit operator Win32Point(Point value)
        {
            return FromPoint(value);
        }
    }
}