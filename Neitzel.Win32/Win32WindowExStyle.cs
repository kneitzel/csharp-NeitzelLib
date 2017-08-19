using System;

namespace Neitzel.Win32
{
    /// <summary>
    /// WindowExStyle
    /// </summary>
    public class Win32WindowExStyle
    {
        Win32WindowExStyles _styles;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="wStyles">Styles</param>
        public Win32WindowExStyle(Win32WindowExStyles wStyles)
        {
            _styles = wStyles;
        }

        /// <summary>
        /// Set the Styles.
        /// </summary>
        /// <param name="styles">Styles to apply.</param>
        /// <param name="value">Styles are applied with or if true, else with and.</param>
        public Win32WindowExStyles Set(Win32WindowExStyles styles, bool value)
        {
            if (value)
                _styles |= styles;
            else
                _styles &= ~styles;
            return _styles;
        }

        /// <summary>
        /// Check styles.
        /// </summary>
        /// <param name="styles">Styles to check.</param>
        /// <returns>true if styles are equal.</returns>
        public bool Check(Win32WindowExStyles styles) { return (_styles & styles) == styles; }

        /// <summary>
        /// Easy converter
        /// </summary>
        public static implicit operator Win32WindowExStyle(Win32WindowExStyles styles) { return new Win32WindowExStyle(styles); }
        
        /// <summary>
        /// Easy converter
        /// </summary>
        public static implicit operator Win32WindowExStyles(Win32WindowExStyle styles) { return styles._styles; }
        
        /// <summary>
        /// Easy converter
        /// </summary>
        public static implicit operator Win32WindowExStyle(IntPtr styles) { return new Win32WindowExStyle((Win32WindowExStyles)styles); }
        
        /// <summary>
        /// Easy converter
        /// </summary>
        public static implicit operator IntPtr(Win32WindowExStyle styles) { return (IntPtr)styles._styles; }
        
        /// <summary>
        /// Easy converter
        /// </summary>
        public static implicit operator Win32WindowExStyle(int style) { return new Win32WindowExStyle((Win32WindowExStyles)style); }
        
        /// <summary>
        /// Easy converter
        /// </summary>
        public static implicit operator int(Win32WindowExStyle styles) { return (int)styles._styles; }
    }
}