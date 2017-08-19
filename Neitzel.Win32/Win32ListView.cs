using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Neitzel.Win32
{
    /// <summary>
    /// Wrapper for the CListView Windows common control.
    /// </summary>
    [CLSCompliant(false)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Win32ListView : Win32Window
    {
        #region Constants

        #region Messages

        /// <summary>
        /// Start of LVM Range
        /// </summary>
        public const uint LVM_FIRST = 0x1000;

        /// <summary>
        /// This message retrieves the background color of a list-view control.
        /// </summary>
        public const uint LVM_GETBKCOLOR = LVM_FIRST + 0;

        /// <summary>
        /// This message sets the background color of a list-view control.
        /// </summary>
        public const uint LVM_SETBKCOLOR = LVM_FIRST + 1;

        /// <summary>
        /// LVM_GETIMAGELIST
        /// </summary>
        public const uint LVM_GETIMAGELIST = LVM_FIRST + 2;

        /// <summary>
        /// LVM_SETIMAGELIST
        /// </summary>
        public const uint LVM_SETIMAGELIST = LVM_FIRST + 3;

        /// <summary>
        /// LVM_GETITEMCOUNT
        /// </summary>
        public const uint LVM_GETITEMCOUNT = LVM_FIRST + 4;

        /// <summary>
        /// LVM_GETITEMA
        /// </summary>
        public const uint LVM_GETITEMA = LVM_FIRST + 5;

        /// <summary>
        /// LVM_GETITEMW
        /// </summary>
        public const uint LVM_GETITEMW = LVM_FIRST + 75;

        /// <summary>
        /// LVM_SETITEMA
        /// </summary>
        public const uint LVM_SETITEMA = LVM_FIRST + 6;

        /// <summary>
        /// LVM_SETITEMW
        /// </summary>
        public const uint LVM_SETITEMW = LVM_FIRST + 76;

        /// <summary>
        /// LVM_INSERTITEMA
        /// </summary>
        public const uint LVM_INSERTITEMA = LVM_FIRST + 7;

        /// <summary>
        /// LVM_INSERTITEMW
        /// </summary>
        public const uint LVM_INSERTITEMW = LVM_FIRST + 77;

        /// <summary>
        /// LVM_DELETEITEM
        /// </summary>
        public const uint LVM_DELETEITEM = LVM_FIRST + 8;

        /// <summary>
        /// LVM_DELETEALLITEMS
        /// </summary>
        public const uint LVM_DELETEALLITEMS = LVM_FIRST + 9;


        /// <summary>
        /// LVM_GETCALLBACKMASK
        /// </summary>
        public const uint LVM_GETCALLBACKMASK = LVM_FIRST + 10;

        /// <summary>
        /// LVM_SETCALLBACKMASK
        /// </summary>
        public const uint LVM_SETCALLBACKMASK = LVM_FIRST + 11;

        /// <summary>
        /// LVM_GETNEXTITEM
        /// </summary>
        public const uint LVM_GETNEXTITEM = LVM_FIRST + 12;

        /// <summary>
        /// LVM_FINDITEMA
        /// </summary>
        public const uint LVM_FINDITEMA = (LVM_FIRST + 13);

        /// <summary>
        /// LVM_FINDITEMW
        /// </summary>
        public const uint LVM_FINDITEMW = (LVM_FIRST + 83);

        /// <summary>
        /// LVM_GETITEMRECT
        /// </summary>
        public const uint LVM_GETITEMRECT = (LVM_FIRST + 14);

        /// <summary>
        /// LVM_SETITEMPOSITION
        /// </summary>
        public const uint LVM_SETITEMPOSITION = (LVM_FIRST + 15);

        /// <summary>
        /// LVM_GETITEMPOSITION
        /// </summary>
        public const uint LVM_GETITEMPOSITION = (LVM_FIRST + 16);

        /// <summary>
        /// LVM_GETSTRINGWIDTHA
        /// </summary>
        public const uint LVM_GETSTRINGWIDTHA = (LVM_FIRST + 17);

        /// <summary>
        /// LVM_GETSTRINGWIDTHW
        /// </summary>
        public const uint LVM_GETSTRINGWIDTHW = (LVM_FIRST + 87);

        /// <summary>
        /// LVM_HITTEST
        /// </summary>
        public const uint LVM_HITTEST = (LVM_FIRST + 18);

        /// <summary>
        /// LVM_ENSUREVISIBLE
        /// </summary>
        public const uint LVM_ENSUREVISIBLE = (LVM_FIRST + 19);

        /// <summary>
        /// LVM_SCROLL
        /// </summary>
        public const uint LVM_SCROLL = (LVM_FIRST + 20);

        /// <summary>
        /// LVM_REDRAWITEMS
        /// </summary>
        public const uint LVM_REDRAWITEMS = (LVM_FIRST + 21);

        /// <summary>
        /// LVA_DEFAULT
        /// </summary>
        public const uint LVA_DEFAULT = 0x0000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVA_ALIGNLEFT = 0x0001;

        /// <summary>
        /// LVA_ALIGNLEFT
        /// </summary>
        public const uint LVA_ALIGNTOP = 0x0002;

        /// <summary>
        /// LVA_SNAPTOGRID
        /// </summary>
        public const uint LVA_SNAPTOGRID = 0x0005;

        /// <summary>
        /// LVM_ARRANGE
        /// </summary>
        public const uint LVM_ARRANGE = (LVM_FIRST + 22);

        /// <summary>
        /// LVM_EDITLABELA
        /// </summary>
        public const uint LVM_EDITLABELA = (LVM_FIRST + 23);

        /// <summary>
        /// LVM_EDITLABELW
        /// </summary>
        public const uint LVM_EDITLABELW = (LVM_FIRST + 118);

        #region LVNI constants

        /// <summary>
        /// LVNI_ALL
        /// </summary>
        public const uint LVNI_ALL = 0x0000;

        /// <summary>
        /// LVNI_FOCUSED
        /// </summary>
        public const uint LVNI_FOCUSED = 0x0001;

        /// <summary>
        /// LVNI_SELECTED
        /// </summary>
        public const uint LVNI_SELECTED = 0x0002;

        /// <summary>
        /// LVNI_CUT
        /// </summary>
        public const uint LVNI_CUT = 0x0004;

        /// <summary>
        /// LVNI_DROPHILITED
        /// </summary>
        public const uint LVNI_DROPHILITED = 0x0008;

        /// <summary>
        /// LVNI_STATEMASK
        /// </summary>
        public const uint LVNI_STATEMASK = (LVNI_FOCUSED | LVNI_SELECTED | LVNI_CUT | LVNI_DROPHILITED);

        /// <summary>
        /// LVNI_VISIBLEORDER
        /// </summary>
        public const uint LVNI_VISIBLEORDER = 0x0010;

        /// <summary>
        /// LVNI_PREVIOUS
        /// </summary>
        public const uint LVNI_PREVIOUS = 0x0020;

        /// <summary>
        /// LVNI_VISIBLEONLY
        /// </summary>
        public const uint LVNI_VISIBLEONLY = 0x0040;

        /// <summary>
        /// LVNI_SAMEGROUPONLY
        /// </summary>
        public const uint LVNI_SAMEGROUPONLY = 0x0080;

        /// <summary>
        /// LVNI_ABOVE
        /// </summary>
        public const uint LVNI_ABOVE = 0x0100;

        /// <summary>
        /// LVNI_BELOW
        /// </summary>
        public const uint LVNI_BELOW = 0x0200;

        /// <summary>
        /// LVNI_TOLEFT
        /// </summary>
        public const uint LVNI_TOLEFT = 0x0400;

        /// <summary>
        /// LVNI_TORIGHT
        /// </summary>
        public const uint LVNI_TORIGHT = 0x0800;

        /// <summary>
        /// LVNI_DIRECTIONMASK
        /// </summary>
        public const uint LVNI_DIRECTIONMASK = (LVNI_ABOVE | LVNI_BELOW | LVNI_TOLEFT | LVNI_TORIGHT);

        #endregion

        #region LVFI Constants

        /// <summary>
        /// LVFI_PARAM
        /// </summary>
        public const uint LVFI_PARAM = 0x0001;

        /// <summary>
        /// LVFI_STRING
        /// </summary>
        public const uint LVFI_STRING = 0x0002;

        /// <summary>
        /// LVFI_SUBSTRING
        /// </summary>
        public const uint LVFI_SUBSTRING = 0x0004; // Same as LVFI_PARTIAL

        /// <summary>
        /// LVFI_PARTIAL
        /// </summary>
        public const uint LVFI_PARTIAL = 0x0008;

        /// <summary>
        /// LVFI_WRAP
        /// </summary>
        public const uint LVFI_WRAP = 0x0020;

        /// <summary>
        /// LVFI_NEARESTXY
        /// </summary>
        public const uint LVFI_NEARESTXY = 0x0040;

        #endregion

        #region LVHT Constants

        /// <summary>
        /// LVHT_NOWHERE
        /// </summary>
        public const uint LVHT_NOWHERE = 0x00000001;

        /// <summary>
        /// LVHT_ONITEMICON
        /// </summary>
        public const uint LVHT_ONITEMICON = 0x00000002;

        /// <summary>
        /// LVHT_ONITEMLABEL
        /// </summary>
        public const uint LVHT_ONITEMLABEL = 0x00000004;

        /// <summary>
        /// LVHT_ONITEMSTATEICON
        /// </summary>
        public const uint LVHT_ONITEMSTATEICON = 0x00000008;

        /// <summary>
        /// LVHT_ONITEM
        /// </summary>
        public const uint LVHT_ONITEM = (LVHT_ONITEMICON | LVHT_ONITEMLABEL | LVHT_ONITEMSTATEICON);

        /// <summary>
        /// LVHT_ABOVE
        /// </summary>
        public const uint LVHT_ABOVE = 0x00000008;

        /// <summary>
        /// LVHT_BELOW
        /// </summary>
        public const uint LVHT_BELOW = 0x00000010;

        /// <summary>
        /// LVHT_TORIGHT
        /// </summary>
        public const uint LVHT_TORIGHT = 0x00000020;

        /// <summary>
        /// LVHT_TOLEFT
        /// </summary>
        public const uint LVHT_TOLEFT = 0x00000040;

        /// <summary>
        /// LVHT_EX_GROUP_HEADER
        /// </summary>
        public const uint LVHT_EX_GROUP_HEADER = 0x10000000;

        /// <summary>
        /// LVHT_EX_GROUP_FOOTER
        /// </summary>
        public const uint LVHT_EX_GROUP_FOOTER = 0x20000000;

        /// <summary>
        /// LVHT_EX_GROUP_COLLAPSE
        /// </summary>
        public const uint LVHT_EX_GROUP_COLLAPSE = 0x40000000;

        /// <summary>
        /// LVHT_EX_GROUP_BACKGROUND
        /// </summary>
        public const uint LVHT_EX_GROUP_BACKGROUND = 0x80000000;

        /// <summary>
        /// LVHT_EX_GROUP_STATEICON
        /// </summary>
        public const uint LVHT_EX_GROUP_STATEICON = 0x01000000;

        /// <summary>
        /// LVHT_EX_GROUP_SUBSETLINK
        /// </summary>
        public const uint LVHT_EX_GROUP_SUBSETLINK = 0x02000000;

        /// <summary>
        /// LVHT_EX_GROUP
        /// </summary>
        public const uint LVHT_EX_GROUP =
            (LVHT_EX_GROUP_BACKGROUND | LVHT_EX_GROUP_COLLAPSE | LVHT_EX_GROUP_FOOTER | LVHT_EX_GROUP_HEADER |
             LVHT_EX_GROUP_STATEICON | LVHT_EX_GROUP_SUBSETLINK);

        /// <summary>
        /// LVHT_EX_ONCONTENTS
        /// </summary>
        public const uint LVHT_EX_ONCONTENTS = 0x04000000; // On item AND not on the background

        /// <summary>
        /// LVHT_EX_FOOTER
        /// </summary>
        public const uint LVHT_EX_FOOTER = 0x08000000;

        #endregion

        /// <summary>
        /// LVCF_FMT
        /// </summary>
        public const uint LVCF_FMT = 0x0001;

        /// <summary>
        /// LVCF_WIDTH
        /// </summary>
        public const uint LVCF_WIDTH = 0x0002;

        /// <summary>
        /// LVCF_TEXT
        /// </summary>
        public const uint LVCF_TEXT = 0x0004;

        /// <summary>
        /// LVCF_SUBITEM
        /// </summary>
        public const uint LVCF_SUBITEM = 0x0008;

        /// <summary>
        /// LVCF_IMAGE
        /// </summary>
        public const uint LVCF_IMAGE = 0x0010;

        /// <summary>
        /// LVCF_ORDER
        /// </summary>
        public const uint LVCF_ORDER = 0x0020;

        /// <summary>
        /// LVCF_MINWIDTH
        /// </summary>
        public const uint LVCF_MINWIDTH = 0x0040;

        /// <summary>
        /// LVCF_DEFAULTWIDTH
        /// </summary>
        public const uint LVCF_DEFAULTWIDTH = 0x0080;

        /// <summary>
        /// LVCF_IDEALWIDTH
        /// </summary>
        public const uint LVCF_IDEALWIDTH = 0x0100;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_LEFT = 0x0000; // Same as HDF_LEFT

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_RIGHT = 0x0001; // Same as HDF_RIGHT

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_CENTER = 0x0002; // Same as HDF_CENTER

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_JUSTIFYMASK = 0x0003; // Same as HDF_JUSTIFYMASK

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_IMAGE = 0x0800; // Same as HDF_IMAGE

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_BITMAP_ON_RIGHT = 0x1000; // Same as HDF_BITMAP_ON_RIGHT

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_COL_HAS_IMAGES = 0x8000; // Same as HDF_OWNERDRAW

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_FIXED_WIDTH = 0x00100; // Can't resize the column; same as HDF_FIXEDWIDTH

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_NO_DPI_SCALE = 0x40000; // If not set, CCM_DPISCALE will govern scaling up fixed width

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_FIXED_RATIO = 0x80000; // Width will augment with the row height

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_LINE_BREAK = 0x100000; // Move to the top of the next list of columns

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_FILL = 0x200000; // Fill the remainder of the tile area. Might have a title.

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_WRAP = 0x400000; // This sub-item can be wrapped.

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_NO_TITLE = 0x800000; // This sub-item doesn't have an title.

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_TILE_PLACEMENTMASK = (LVCFMT_LINE_BREAK | LVCFMT_FILL);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCFMT_SPLITBUTTON = 0x1000000; // Column is a split button; same as HDF_SPLITBUTTON

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETCOLUMNA = (LVM_FIRST + 25);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETCOLUMNW = (LVM_FIRST + 95);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETCOLUMNA = (LVM_FIRST + 26);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETCOLUMNW = (LVM_FIRST + 96);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_INSERTCOLUMNA = (LVM_FIRST + 27);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_INSERTCOLUMNW = (LVM_FIRST + 97);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_DELETECOLUMN = (LVM_FIRST + 28);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETCOLUMNWIDTH = (LVM_FIRST + 29);

        /// <summary>
        /// 
        /// </summary>
        public const int LVSCW_AUTOSIZE = -1;

        /// <summary>
        /// 
        /// </summary>
        public const int LVSCW_AUTOSIZE_USEHEADER = -2;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETCOLUMNWIDTH = (LVM_FIRST + 30);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETHEADER = (LVM_FIRST + 31);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_CREATEDRAGIMAGE = (LVM_FIRST + 33);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETVIEWRECT = (LVM_FIRST + 34);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETTEXTCOLOR = (LVM_FIRST + 35);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETTEXTCOLOR = (LVM_FIRST + 36);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETTEXTBKCOLOR = (LVM_FIRST + 37);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETTEXTBKCOLOR = (LVM_FIRST + 38);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETTOPINDEX = (LVM_FIRST + 39);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETCOUNTPERPAGE = (LVM_FIRST + 40);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETORIGIN = (LVM_FIRST + 41);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_UPDATE = (LVM_FIRST + 42);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETITEMSTATE = (LVM_FIRST + 43);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETITEMSTATE = (LVM_FIRST + 44);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETITEMTEXTA = (LVM_FIRST + 45);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETITEMTEXTW = (LVM_FIRST + 115);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETITEMTEXTA = (LVM_FIRST + 46);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETITEMTEXTW = (LVM_FIRST + 116);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVSICF_NOINVALIDATEALL = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVSICF_NOSCROLL = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETITEMCOUNT = (LVM_FIRST + 47);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SORTITEMS = (LVM_FIRST + 48);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETITEMPOSITION32 = (LVM_FIRST + 49);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETSELECTEDCOUNT = (LVM_FIRST + 50);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETITEMSPACING = (LVM_FIRST + 51);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETISEARCHSTRINGA = (LVM_FIRST + 52);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETISEARCHSTRINGW = (LVM_FIRST + 117);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETICONSPACING = (LVM_FIRST + 53);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54); // optional wParam == mask

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 55);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_GRIDLINES = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_SUBITEMIMAGES = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_CHECKBOXES = 0x00000004;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_TRACKSELECT = 0x00000008;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_HEADERDRAGDROP = 0x00000010;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_FULLROWSELECT = 0x00000020; // applies to report mode only

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_ONECLICKACTIVATE = 0x00000040;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_TWOCLICKACTIVATE = 0x00000080;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_FLATSB = 0x00000100;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_REGIONAL = 0x00000200;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_INFOTIP = 0x00000400; // listview does InfoTips for you

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_UNDERLINEHOT = 0x00000800;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_UNDERLINECOLD = 0x00001000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_MULTIWORKAREAS = 0x00002000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_LABELTIP = 0x00004000;
            // listview unfolds partly hidden labels if it does not have infotip text

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_BORDERSELECT = 0x00008000; // border selection style instead of highlight

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_DOUBLEBUFFER = 0x00010000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_HIDELABELS = 0x00020000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_SINGLEROW = 0x00040000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_SNAPTOGRID = 0x00080000; // Icons automatically snap to grid.

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_SIMPLESELECT = 0x00100000;
            // Also changes overlay rendering to top right for icon mode.

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_JUSTIFYCOLUMNS = 0x00200000;
            // Icons are lined up in columns that use up the whole view area.

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_TRANSPARENTBKGND = 0x00400000;
            // Background is painted by the parent via WM_PRINTCLIENT

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_TRANSPARENTSHADOWTEXT = 0x00800000;
            // Enable shadow text on transparent backgrounds only  =(useful with bitmaps);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_AUTOAUTOARRANGE = 0x01000000;
            // Icons automatically arrange if no icon positions have been set

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_HEADERINALLVIEWS = 0x02000000; // Display column header in all view modes

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_AUTOCHECKSELECT = 0x08000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_AUTOSIZECOLUMNS = 0x10000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_COLUMNSNAPPOINTS = 0x40000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVS_EX_COLUMNOVERFLOW = 0x80000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETSUBITEMRECT = (LVM_FIRST + 56);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SUBITEMHITTEST = (LVM_FIRST + 57);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETCOLUMNORDERARRAY = (LVM_FIRST + 58);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETCOLUMNORDERARRAY = (LVM_FIRST + 59);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETHOTITEM = (LVM_FIRST + 60);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETHOTITEM = (LVM_FIRST + 61);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETHOTCURSOR = (LVM_FIRST + 62);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETHOTCURSOR = (LVM_FIRST + 63);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_APPROXIMATEVIEWRECT = (LVM_FIRST + 64);

        /// <summary>
        /// 
        /// </summary>
        public const uint LV_MAX_WORKAREAS = 16;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETWORKAREAS = (LVM_FIRST + 65);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETWORKAREAS = (LVM_FIRST + 70);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETNUMBEROFWORKAREAS = (LVM_FIRST + 73);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETSELECTIONMARK = (LVM_FIRST + 66);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETSELECTIONMARK = (LVM_FIRST + 67);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETHOVERTIME = (LVM_FIRST + 71);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETHOVERTIME = (LVM_FIRST + 72);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETTOOLTIPS = (LVM_FIRST + 74);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETTOOLTIPS = (LVM_FIRST + 78);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SORTITEMSEX = (LVM_FIRST + 81);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVBKIF_SOURCE_NONE = 0x00000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVBKIF_SOURCE_HBITMAP = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVBKIF_SOURCE_URL = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVBKIF_SOURCE_MASK = 0x00000003;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVBKIF_STYLE_NORMAL = 0x00000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVBKIF_STYLE_TILE = 0x00000010;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVBKIF_STYLE_MASK = 0x00000010;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVBKIF_FLAG_TILEOFFSET = 0x00000100;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVBKIF_TYPE_WATERMARK = 0x10000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVBKIF_FLAG_ALPHABLEND = 0x20000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETBKIMAGEA = (LVM_FIRST + 68);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETBKIMAGEW = (LVM_FIRST + 138);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETBKIMAGEA = (LVM_FIRST + 69);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETBKIMAGEW = (LVM_FIRST + 139);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETSELECTEDCOLUMN = (LVM_FIRST + 140);

        /// <summary>
        /// 
        /// </summary>
        public const uint LV_VIEW_ICON = 0x0000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LV_VIEW_DETAILS = 0x0001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LV_VIEW_SMALLICON = 0x0002;

        /// <summary>
        /// 
        /// </summary>
        public const uint LV_VIEW_LIST = 0x0003;

        /// <summary>
        /// 
        /// </summary>
        public const uint LV_VIEW_TILE = 0x0004;

        /// <summary>
        /// 
        /// </summary>
        public const uint LV_VIEW_MAX = 0x0004;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETVIEW = (LVM_FIRST + 142);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETVIEW = (LVM_FIRST + 143);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_NONE = 0x00000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_HEADER = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_FOOTER = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_STATE = 0x00000004;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_ALIGN = 0x00000008;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_GROUPID = 0x00000010;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_SUBTITLE = 0x00000100; // pszSubtitle is valid

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_TASK = 0x00000200; // pszTask is valid

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_DESCRIPTIONTOP = 0x00000400; // pszDescriptionTop is valid

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_DESCRIPTIONBOTTOM = 0x00000800; // pszDescriptionBottom is valid

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_TITLEIMAGE = 0x00001000; // iTitleImage is valid

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_EXTENDEDIMAGE = 0x00002000; // iExtendedImage is valid

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_ITEMS = 0x00004000; // iFirstItem and cItems are valid

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_SUBSET = 0x00008000; // pszSubsetTitle is valid

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGF_SUBSETITEMS = 0x00010000;
            // readonly, cItems holds count of items in visible subset, iFirstItem is valid

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGS_NORMAL = 0x00000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGS_COLLAPSED = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGS_HIDDEN = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGS_NOHEADER = 0x00000004;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGS_COLLAPSIBLE = 0x00000008;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGS_FOCUSED = 0x00000010;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGS_SELECTED = 0x00000020;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGS_SUBSETED = 0x00000040;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGS_SUBSETLINKFOCUSED = 0x00000080;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGA_HEADER_LEFT = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGA_HEADER_CENTER = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGA_HEADER_RIGHT = 0x00000004; // Don't forget to validate exclusivity

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGA_FOOTER_LEFT = 0x00000008;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGA_FOOTER_CENTER = 0x00000010;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGA_FOOTER_RIGHT = 0x00000020; // Don't forget to validate exclusivity

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_INSERTGROUP = (LVM_FIRST + 145);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETGROUPINFO = (LVM_FIRST + 147);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETGROUPINFO = (LVM_FIRST + 149);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_REMOVEGROUP = (LVM_FIRST + 150);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_MOVEGROUP = (LVM_FIRST + 151);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETGROUPCOUNT = (LVM_FIRST + 152);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETGROUPINFOBYINDEX = (LVM_FIRST + 153);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_MOVEITEMTOGROUP = (LVM_FIRST + 154);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGGR_GROUP = 0; // Entire expanded group

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGGR_HEADER = 1; // Header only  =(collapsed group);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGGR_LABEL = 2; // Label only

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGGR_SUBSETLINK = 3; // subset link only

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETGROUPRECT = (LVM_FIRST + 98);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGMF_NONE = 0x00000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGMF_BORDERSIZE = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGMF_BORDERCOLOR = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVGMF_TEXTCOLOR = 0x00000004;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETGROUPMETRICS = (LVM_FIRST + 155);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETGROUPMETRICS = (LVM_FIRST + 156);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_ENABLEGROUPVIEW = (LVM_FIRST + 157);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SORTGROUPS = (LVM_FIRST + 158);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_INSERTGROUPSORTED = (LVM_FIRST + 159);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_REMOVEALLGROUPS = (LVM_FIRST + 160);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_HASGROUP = (LVM_FIRST + 161);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETGROUPSTATE = (LVM_FIRST + 92);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETFOCUSEDGROUP = (LVM_FIRST + 93);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVTVIF_AUTOSIZE = 0x00000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVTVIF_FIXEDWIDTH = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVTVIF_FIXEDHEIGHT = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVTVIF_FIXEDSIZE = 0x00000003;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVTVIF_EXTENDED = 0x00000004;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVTVIM_TILESIZE = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVTVIM_COLUMNS = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVTVIM_LABELMARGIN = 0x00000004;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETTILEVIEWINFO = (LVM_FIRST + 162);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETTILEVIEWINFO = (LVM_FIRST + 163);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETTILEINFO = (LVM_FIRST + 164);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETTILEINFO = (LVM_FIRST + 165);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVIM_AFTER = 0x00000001; // TRUE = insert After iItem, otherwise before

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETINSERTMARK = (LVM_FIRST + 166);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETINSERTMARK = (LVM_FIRST + 167);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_INSERTMARKHITTEST = (LVM_FIRST + 168);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETINSERTMARKRECT = (LVM_FIRST + 169);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETINSERTMARKCOLOR = (LVM_FIRST + 170);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETINSERTMARKCOLOR = (LVM_FIRST + 171);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETINFOTIP = (LVM_FIRST + 173);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETSELECTEDCOLUMN = (LVM_FIRST + 174);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_ISGROUPVIEWENABLED = (LVM_FIRST + 175);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETOUTLINECOLOR = (LVM_FIRST + 176);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETOUTLINECOLOR = (LVM_FIRST + 177);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_CANCELEDITLABEL = (LVM_FIRST + 179);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_MAPINDEXTOID = (LVM_FIRST + 180);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_MAPIDTOINDEX = (LVM_FIRST + 181);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_ISITEMVISIBLE = (LVM_FIRST + 182);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETEMPTYTEXT = (LVM_FIRST + 204);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETFOOTERRECT = (LVM_FIRST + 205);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVFF_ITEMCOUNT = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETFOOTERINFO = (LVM_FIRST + 206);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETFOOTERITEMRECT = (LVM_FIRST + 207);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVFIF_TEXT = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVFIF_STATE = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVFIS_FOCUSED = 0x0001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETFOOTERITEM = (LVM_FIRST + 208);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETITEMINDEXRECT = (LVM_FIRST + 209);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_SETITEMINDEXSTATE = (LVM_FIRST + 210);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVM_GETNEXTITEMINDEX = (LVM_FIRST + 211);

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCDI_ITEM = 0x00000000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCDI_GROUP = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCDRF_NOSELECT = 0x00010000;

        /// <summary>
        /// 
        /// </summary>
        public const uint LVCDRF_NOGROUPFRAME = 0x00020000;

        #endregion

        #region LVIF Constants

        /// <summary>
        /// LVIF_TEXT
        /// </summary>
        public const int LVIF_TEXT = 0x00000001;

        /// <summary>
        /// LVIF_IMAGE
        /// </summary>
        public const int LVIF_IMAGE = 0x00000002;

        /// <summary>
        /// LVIF_PARAM
        /// </summary>
        public const int LVIF_PARAM = 0x00000004;

        /// <summary>
        /// LVIF_STATE
        /// </summary>
        public const int LVIF_STATE = 0x00000008;

        /// <summary>
        /// LVIF_INDENT
        /// </summary>
        public const int LVIF_INDENT = 0x00000010;

        /// <summary>
        /// LVIF_NORECOMPUTE
        /// </summary>
        public const int LVIF_NORECOMPUTE = 0x00000800;

        /// <summary>
        /// LVIF_GROUPID
        /// </summary>
        public const int LVIF_GROUPID = 0x00000100;

        /// <summary>
        /// LVIF_COLUMNS
        /// </summary>
        public const int LVIF_COLUMNS = 0x00000200;

        #endregion

        #endregion

        #region Lifetime

        /// <summary>
        /// Get a Win32ListView from a Handle
        /// </summary>
        public Win32ListView(IntPtr handle)
            : base(handle)
        {
        }

        /// <summary>
        /// Operator to get a Win32ListView from a handle
        /// </summary>
        /// <param name="handle">Handle of window</param>
        /// <returns>Win32ListView object</returns>
        public static implicit operator Win32ListView(IntPtr handle)
        {
            return new Win32ListView(handle);
        }

        /// <summary>
        /// Operator to get a Win32ListView from a handle
        /// </summary>
        /// <param name="handle">handle of window</param>
        /// <returns>Win32ListView object</returns>
        public static implicit operator Win32ListView(int handle)
        {
            return new Win32ListView((IntPtr) handle);
        }

        /// <summary>
        /// Operator to get an Handle from a Win32ListView
        /// </summary>
        /// <param name="window">Win32ListView object</param>
        /// <returns>Handle</returns>
        public static implicit operator IntPtr(Win32ListView window)
        {
            return window.Handle;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the ListView Item with creating a buffer in the adressspace of the window process.
        /// </summary>
        /// <param name="item">Number of the item to get. First element is 0.</param>
        /// <returns>Text of the given item</returns>
        public string GetListViewItem(int item)
        {
            return GetListViewItem(item, 0);
        }

        /// <summary>
        /// Get the ListView Item with creating a buffer in the adressspace of the window process.
        /// </summary>
        /// <param name="item">Number of the item to get. First element is 0.</param>
        /// <param name="subitem">Subitem, First element is 0.</param>
        /// <returns>Text of the given item</returns>
        public string GetListViewItem(int item, int subitem)
        {
            const int dwBufferSize = 2048;

            string retval;
            IntPtr hProcess = IntPtr.Zero;
            IntPtr lpRemoteBuffer = IntPtr.Zero;
            IntPtr lpLocalBuffer = IntPtr.Zero;

            try
            {
                var lvItem = new Win32ListViewItemData();
                lpLocalBuffer = Marshal.AllocHGlobal(dwBufferSize);
                // Get the process id owning the window 

                if (ThreadId == 0)
                    throw new ArgumentException("hWnd");

                // Open the process with all access 
                hProcess = UnsafeNativeMethods.OpenProcess(Win32ProcessAccessType.AllAccess, false, ProcessId);
                if (hProcess == IntPtr.Zero)
                    throw new ApplicationException("Failed to access process!");

                // Allocate a buffer in the remote process 
                lpRemoteBuffer = UnsafeNativeMethods.VirtualAllocEx(hProcess, IntPtr.Zero, dwBufferSize, Win32MemoryAllocation.Commit, Win32MemoryProtection.PAGE_READWRITE);
                if (lpRemoteBuffer == IntPtr.Zero)
                    throw new SystemException("Failed to allocate memory in remote process");

                // Fill in the LVITEM struct, this is in your own process 
                // Set the pszText member to somewhere in the remote buffer, 
                // For the example I used the address imediately following the LVITEM stuct 
                lvItem.mask = LVIF_TEXT;
                lvItem.iItem = item;
                lvItem.iSubItem = subitem;
                unsafe
                {
                    lvItem.pszText = (char*)(lpRemoteBuffer.ToInt32() + Marshal.SizeOf(typeof(Win32ListViewItemData)));
                }
                lvItem.cchTextMax = 500;

                // Copy the local LVITEM to the remote buffer 
                IntPtr bytesWrittenOrRead;
                var bSuccess = UnsafeNativeMethods.WriteProcessMemory(hProcess, lpRemoteBuffer, ref lvItem, Marshal.SizeOf(typeof(Win32ListViewItemData)), out bytesWrittenOrRead);
                if (!bSuccess)
                    throw new SystemException("Failed to write to process memory");

                // Send the message to the remote window with the address of the remote buffer 
                SendMessage(LVM_GETITEMW, IntPtr.Zero, lpRemoteBuffer);

                // Read the struct back from the remote process into local buffer 
                bSuccess = UnsafeNativeMethods.ReadProcessMemory(hProcess, lpRemoteBuffer, lpLocalBuffer, new IntPtr(dwBufferSize), out bytesWrittenOrRead);

                if (!bSuccess)
                    throw new SystemException("Failed to read from process memory");

                // At this point the lpLocalBuffer contains the returned LV_ITEM structure 
                // the next line extracts the text from the buffer into a managed string 
                retval = Marshal.PtrToStringUni((IntPtr)(lpLocalBuffer.ToInt32() + Marshal.SizeOf(typeof(Win32ListViewItemData))));

            }
            finally
            {
                if (lpLocalBuffer != IntPtr.Zero)
                    Marshal.FreeHGlobal(lpLocalBuffer);
                if (lpRemoteBuffer != IntPtr.Zero)
                    UnsafeNativeMethods.VirtualFreeEx(hProcess, lpRemoteBuffer, 0, Win32MemoryAllocation.Release);
                if (hProcess != IntPtr.Zero)
                    UnsafeNativeMethods.CloseHandle(hProcess);
            }
            return retval;
        }

        /// <summary>
        /// Get the number of items in the ListView
        /// </summary>
        public int ItemCount => SendMessage(LVM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero).ToInt32();

        #endregion
    }
}
