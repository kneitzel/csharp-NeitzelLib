using System.Drawing;
using System.Runtime.InteropServices;

namespace Neitzel.Win32
{
    /// <summary>
    /// Rectangle class.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Win32Rectangle
    {
        /// <summary>
        /// Left.
        /// </summary>
        public int left;
        /// <summary>
        /// Top.
        /// </summary>
        public int top;
        /// <summary>
        /// Right.
        /// </summary>
        public int right;
        /// <summary>
        /// Bottom.
        /// </summary>
        public int bottom;

        /// <summary>
        /// Constructor for a new instance.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="top">Top.</param>
        /// <param name="right">Right.</param>
        /// <param name="bottom">Bottom.</param>
        public Win32Rectangle(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        /// <summary>
        /// Constructor for a new instance.
        /// </summary>
        /// <param name="r">Rectangle</param>
        public Win32Rectangle(Rectangle r)
        {
            this.left = r.Left;
            this.top = r.Top;
            this.right = r.Right;
            this.bottom = r.Bottom;
        }

        /// <summary>
        /// Empty Rectangle.
        /// </summary>
        public static Win32Rectangle Empty { get { return new Win32Rectangle(0, 0, 0, 0); } }

        /// <summary>
        /// Height of rectangle.
        /// </summary>
        public int Height { get { return bottom - top; } }

        /// <summary>
        /// Width of rectangle.
        /// </summary>
        public int Width { get { return right - left; } }

        /// <summary>
        /// Size of rectangle.
        /// </summary>
        public Size Size { get { return new Size(this.right - this.left, this.bottom - this.top); } }

        /// <summary>
        /// Location of rectangle.
        /// </summary>
        public Point Location { get { return new Point(left, top); } }

        /// <summary>
        /// Convert Rectangle to RECT
        /// </summary>
        /// <param name="p">Rectangle to convert.</param>
        /// <returns>Converted RECT.</returns>
        public static implicit operator Win32Rectangle(Rectangle p) { return new Win32Rectangle(p); }

        /// <summary>
        /// Convert RECT to Rectangle.
        /// </summary>
        /// <param name="p">RECT to convert.</param>
        /// <returns>Converted Rectangle.</returns>
        public static implicit operator Rectangle(Win32Rectangle p) { return Rectangle.FromLTRB(p.left, p.top, p.right, p.bottom); }

        /// <summary>
        /// Create a RECT instance from a start point together with width and height.
        /// </summary>
        /// <param name="x">Start Point - X coordinate.</param>
        /// <param name="y">Start Point - Y coordinate.</param>
        /// <param name="width">Width of rectangle.</param>
        /// <param name="height">Height of rectangle.</param>
        /// <returns>RECT instance.</returns>
        public static Win32Rectangle FromXYWH(int x, int y, int width, int height) { return new Win32Rectangle(x, y, x + width, y + height); }
    }
}