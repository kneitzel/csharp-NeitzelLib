using System;

namespace Neitzel.Win32
{
    /// <summary>
    /// WindowStyle
    /// </summary>
    public class Win32WindowStyle
    {
        Win32WindowStyles _styles;

        /// <summary>
        /// Create a new instance from WindowsStyles.
        /// </summary>
        /// <param name="wStyles">Styles to use.</param>
        public Win32WindowStyle(Win32WindowStyles wStyles)
        {
            _styles = wStyles;
        }

        /// <summary>
        /// Set the new styles
        /// </summary>
        /// <param name="styles">Window Styles</param>
        /// <param name="value">If true, the style are combined with or. IF false, thes are combined with and.</param>
        public Win32WindowStyles Set(Win32WindowStyles styles, bool value)
        {
            if (value)
                _styles |= styles;
            else
                _styles &= ~styles;
            return _styles;
        }

        /// <summary>
        /// Check Styles
        /// </summary>
        public bool Check(Win32WindowStyles wStyles) { return (_styles & wStyles) == wStyles; }

        /// <summary>
        /// Convert from WindowStyles to WindowStyle
        /// </summary>
        /// <param name="wStyles">WindowStyle to convert.</param>
        /// <returns>Converted WindowStyle</returns>
        public static implicit operator Win32WindowStyle(Win32WindowStyles wStyles) { return new Win32WindowStyle(wStyles); }
        
        /// <summary>
        /// Convert from WindowStyle to WindowStyles
        /// </summary>
        /// <param name="wStyle">WindowStyle to convert.</param>
        /// <returns>Converted WindowStyle</returns>
        public static implicit operator Win32WindowStyles(Win32WindowStyle wStyle) { return wStyle._styles; }
        
        /// <summary>
        /// Convert from a IntPtr to WIndowStyle.
        /// </summary>
        /// <param name="wStyles">IntPtr to convert.</param>
        /// <returns>Converted WindowStyle.</returns>
        public static implicit operator Win32WindowStyle(IntPtr wStyles) { return new Win32WindowStyle((Win32WindowStyles)wStyles); }
        
        /// <summary>
        /// Convert from WindowStyle to IntPtr
        /// </summary>
        /// <param name="wStyles">WindowStyle to convert.</param>
        /// <returns>Converted IntPtr.</returns>
        public static implicit operator IntPtr(Win32WindowStyle wStyles) { return (IntPtr)wStyles._styles; }
        
        /// <summary>
        /// Convert from int to WindowStyle
        /// </summary>
        /// <param name="wStyles">Int value to convert.</param>
        /// <returns>Converted WindowStyle.</returns>
        public static implicit operator Win32WindowStyle(int wStyles) { return new Win32WindowStyle((Win32WindowStyles)wStyles); }
        
        /// <summary>
        /// Convert from WindowStyle to int.
        /// </summary>
        /// <param name="wStyles">WindowStyle to convert.</param>
        /// <returns>Converted int.</returns>
        public static implicit operator int(Win32WindowStyle wStyles) { return (int)wStyles._styles; }
    }
}