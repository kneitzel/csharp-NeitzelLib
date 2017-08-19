using System.Diagnostics.CodeAnalysis;

namespace Neitzel.Win32
{
    /// <summary>
    /// 
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Win32ShowWindow
    {
        /// <summary>
        /// 
        /// </summary>
        SW_HIDE = 0,

        /// <summary>
        /// 
        /// </summary>
        SW_SHOWNORMAL = 1,
        
        /// <summary>
        /// 
        /// </summary>
        SW_NORMAL = 1,
        
        /// <summary>
        /// 
        /// </summary>
        SW_SHOWMINIMIZED = 2,
        
        /// <summary>
        /// 
        /// </summary>
        SW_SHOWMAXIMIZED = 3,
        
        /// <summary>
        /// 
        /// </summary>
        SW_MAXIMIZE = 3,
        
        /// <summary>
        /// 
        /// </summary>
        SW_SHOWNOACTIVATE = 4,
        
        /// <summary>
        /// 
        /// </summary>
        SW_SHOW = 5,
        
        /// <summary>
        /// 
        /// </summary>
        SW_MINIMIZE = 6,
        
        /// <summary>
        /// 
        /// </summary>
        SW_SHOWMINNOACTIVE = 7,
        
        /// <summary>
        /// 
        /// </summary>
        SW_SHOWNA = 8,
        
        /// <summary>
        /// 
        /// </summary>
        SW_RESTORE = 9,
        
        /// <summary>
        /// 
        /// </summary>
        SW_SHOWDEFAULT = 10,
        
        /// <summary>
        /// 
        /// </summary>
        SW_FORCEMINIMIZE = 11,
        
        /// <summary>
        /// 
        /// </summary>
        SW_MAX = 11
    }
}