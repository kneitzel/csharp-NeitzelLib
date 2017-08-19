using System;
using System.Runtime.InteropServices;

namespace Neitzel.Win32
{
    /// <summary>
    /// LV_ITEM
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    unsafe struct Win32ListViewItemData
    {
        /// <summary>
        /// Mask
        /// </summary>
        public Int32 mask;
        /// <summary>
        /// Item
        /// </summary>
        public Int32 iItem;
        /// <summary>
        /// SubItem
        /// </summary>
        public Int32 iSubItem;
        /// <summary>
        /// state
        /// </summary>
        public Int32 state;
        /// <summary>
        /// stateMask
        /// </summary>
        public Int32 stateMask;
        /// <summary>
        /// Text
        /// </summary>
        public char* pszText;
        /// <summary>
        /// TextMax
        /// </summary>
        public Int32 cchTextMax;
        /// <summary>
        /// Image
        /// </summary>
        public Int32 iImage;
        /// <summary>
        /// lParem
        /// </summary>
        public Int32 lParam;
        /// <summary>
        /// Indent
        /// </summary>
        public Int32 iIndent;
    }
}
