using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Neitzel.Win32
{
    /// <summary>
    /// Native Win32 Window
    /// </summary>
    [CLSCompliant(false)]
    public class Win32Window : IWin32Window
    {
        #region Constructors/Operators

        /// <summary>
        /// Create a new instance from a handle.
        /// </summary>
        /// <param name="handle">Handle of a window.</param>
        public Win32Window(IntPtr handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Compares 2 CWindow objects
        /// </summary>
        /// <param name="a">first CWindow</param>
        /// <param name="b">second CWindow</param>
        /// <returns>true if instances are equal, else false</returns>
        public static bool operator ==(Win32Window a, Win32Window b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (ReferenceEquals(a, null)|| ReferenceEquals(b,null))
                return false;
            return a.Handle == b.Handle;
        }

        /// <summary>
        /// Compares this object with another by value.
        /// </summary>
        public override bool Equals(object obj)
        {
            // Compare nullability and type
            var other = obj as Win32Window;
            if (ReferenceEquals(other, null))
                return false;

            // Compare values
            return other.Handle == Handle;
        }


        /// <summary>
        /// Compares 2 CWindow objects
        /// </summary>
        /// <param name="win1">first CWindow</param>
        /// <param name="win2">second CWindow</param>
        /// <returns>true if instances are not equal, else false</returns>
        public static bool operator !=(Win32Window win1, Win32Window win2)
        {
            return !(win1 == win2);
        }

        /// <summary>
        /// Compares a CWindow with a Handle
        /// </summary>
        /// <param name="win1">CWindow</param>
        /// <param name="win2">Handle</param>
        /// <returns>true if equal</returns>
        public static bool operator ==(Win32Window win1, IntPtr win2)
        {
            if (ReferenceEquals(win1, null))
                return false;
            return win1.Handle == win2;
        }

        /// <summary>
        /// Compares a CWindow with a Handle
        /// </summary>
        /// <param name="win1">CWindow</param>
        /// <param name="win2">Handle</param>
        /// <returns>true if equal</returns>
        public static bool operator !=(Win32Window win1, IntPtr win2)
        {
            return !(win1 == win2);
        }


        /// <summary>
        /// Get the HashCode. Only the Handle is used to calculate the hash code because other Properties must be retrieved first.
        /// </summary>
        /// <returns>HashCode.</returns>
        public override int GetHashCode() => Handle.GetHashCode();

        #endregion

        #region Static
        /// <summary>
        /// Gets/sets the window at the top of the Z order.
        /// </summary>
        public static Win32Window TopWindow
        {
            get
            {
                return new Win32Window(UnsafeNativeMethods.GetTopWindow(IntPtr.Zero));
            }
            set
            {
                value?.BringToFront();
            }
        }

        /// <summary>
        /// Gets/sets whether windows animation, when minimizing and maximizing, is enabled.
        /// </summary>
        public static bool StateChangeAnimation
        {
            get
            {
                var info = new Win32AnimationInfo(0);
                UnsafeNativeMethods.SystemParametersInfo(Win32Constants.SPI_GETANIMATION, info.cbSize, ref info, 0);
                return info.iMinAnimate != 0;
            }
            set
            {
                var info = new Win32AnimationInfo(value ? 1 : 0);
                UnsafeNativeMethods.SystemParametersInfo(Win32Constants.SPI_SETANIMATION, info.cbSize, ref info, 0);
            }
        }

        /// <summary>
        /// Gets/sets the currently active window.
        /// </summary>
        public static Win32Window ActiveWindow
        {
            get
            {
                return new Win32Window(UnsafeNativeMethods.GetForegroundWindow());
            }
            set
            {
                value?.ForceActivate();
            }
        }

        /// <summary>
        /// Gets/sets the window that has the keyboard focus.
        /// </summary>
        public static Win32Window FocusedWindow
        {
            get
            {
                return new Win32Window(UnsafeNativeMethods.GetFocus());
            }
            set
            {
                value?.Focus();
            }
        }

        /// <summary>
        /// Gets the window (if any) that has captured the mouse.
        /// </summary>
        public static Win32Window Captured => new Win32Window(UnsafeNativeMethods.GetCapture());

        /// <summary>
        /// Gets the desktop window. The desktop window covers the entire screen. 
        /// The desktop window is the area on top of which other windows are painted. 
        /// </summary>
        public static Win32Window DesktopWindow => new Win32Window(UnsafeNativeMethods.GetDesktopWindow());

        /// <summary>
        /// Gets the collection of all the top-level windows on the screen.
        /// </summary>
        public static Collection<Win32Window> TopLevelWindows
        {
            get
            {
                var list = new Collection<Win32Window>();
                var listHandle = GCHandle.Alloc(list);
                try
                {
                    UnsafeNativeMethods.EnumWindows_(EnumChildrenProc, GCHandle.ToIntPtr(listHandle));
                }
                finally
                {
                    if (listHandle.IsAllocated)
                        listHandle.Free();
                }
                return list;
            }
        }
        /// <summary>
        /// Retrieves the window at a specified location.
        /// </summary>
        public static Win32Window FromPoint(Point pt)
        {
            return new Win32Window(UnsafeNativeMethods.WindowFromPoint(pt));
        }
        /// <summary>
        /// Retrieves the top-level window whose class name and window name match the specified strings. 
        /// This function does not search child windows. This function does not perform a case-sensitive search.
        /// </summary>
        public static Win32Window FindWindow(string className, string windowName)
        {
            return new Win32Window(UnsafeNativeMethods.FindWindow_(className, windowName));
        }

        /// <summary>
        /// Retrieves the child windows whose class name and window name match the specified strings, 
        /// beginning with the one following the specified child window.
        /// This function does not perform a case-sensitive search.
        /// </summary>
        public static Win32Window FindWindow(Win32Window windowAfter, string className, string windowName)
        {
            // Validate arguments
            if (windowAfter == null)
                throw new ArgumentNullException(nameof(windowAfter));

            return new Win32Window(UnsafeNativeMethods.FindWindowEx(IntPtr.Zero, windowAfter.Handle, className, windowName));
        }

        /// <summary>
        /// Retrieves the collection of the top-level windows contained within the specified thread.
        /// </summary>
        public static Collection<Win32Window> GetThreadWindows(uint threadId)
        {
            var list = new Collection<Win32Window>();
            var listHandle = GCHandle.Alloc(list);
            try
            {
                EnumThreadWindows(threadId, EnumChildrenProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return list;
        }

        #endregion

        #region Window Relations

        /// <summary>
        /// Gets/sets window's parent or owner, and changes the parent window. For more information, see 
        /// GetParent function in MSDN
        /// </summary>
        public Win32Window Parent
        {
            get
            {
                return new Win32Window(UnsafeNativeMethods.GetParent(Handle));
            }
            set
            {
                if (value != null)
                    UnsafeNativeMethods.SetParent(Handle, value.Handle);
            }
        }

        /// <summary>
        /// Gets owner window, if any. For more information, see "Window Features"/"Owned Windows", GetWindow in MSDN.
        /// </summary>
        public Win32Window Owner => GetNextWindow(Win32GetWindowCommand.Owner);

        /// <summary>
        /// Retrieves the root window by walking the chain of parent windows. See Control.TopLevelControl in MSDN also. 
        /// </summary>
        public Win32Window TopLevelWindow => new Win32Window(UnsafeNativeMethods.GetAncestor(Handle, Win32WindowAncestor.Root));

        /// <summary>
        /// Gets a value indicating whether the window is a top-level window.
        /// </summary>
        public bool TopLevel => TopLevelWindow.Handle == Handle;

        /// <summary>
        /// If current window is a top-level window, checks if it is at the top of the Z order.
        /// If the window is a child window, checks if it is at the top of it parent's Z order.
        /// </summary>
        public bool IsTopWindow => UnsafeNativeMethods.GetTopWindow(Parent.Handle) == Handle;

        /// <summary>
        /// Gets or sets a value indicating whether the window should be displayed as a topmost window.
        /// </summary>
        public bool Topmost
        {
            get
            {
                return ExStyles.Check(Win32WindowExStyles.WS_EX_TOPMOST);
            }
            set
            {
                UnsafeNativeMethods.SetWindowPos(Handle, (value ? Win32WindowPositionAfter.HWND_TOPMOST : Win32WindowPositionAfter.HWND_NOTOPMOST), 0, 0, 0, 0,
                    Win32WindowPositionOptions.NoSize | Win32WindowPositionOptions.NoMove | Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the window contains one or more child windows.
        /// </summary>
        public bool HasChildren => GetNextWindow(Win32GetWindowCommand.Child) != IntPtr.Zero;

        /// <summary>
        /// Gets the collection of windows contained within the window. It contains only immediate child windows.
        /// </summary>
        public Collection<Win32Window> Children
        {
            get
            {
                var list = new Collection<Win32Window>();
                var listHandle = GCHandle.Alloc(list);
                try
                {
                    EnumChildWindows(EnumChildrenProc, GCHandle.ToIntPtr(listHandle));
                }
                catch
                {
                    // Can be ignored.
                }

                // Release listHandle if required
                if (listHandle.IsAllocated)
                    listHandle.Free();

                var cur = 0;
                while (cur < list.Count)
                    if (UnsafeNativeMethods.GetAncestor(list[cur].Handle, Win32WindowAncestor.Parent) != Handle)
                        list.RemoveAt(cur);
                    else
                        cur++;
                return list;
            }
        }

        /// <summary>
        /// Gets the child window at the top of the Z order, if this window is a parent window; 
        /// otherwise, returns null-window. Examines only child windows of this window (does not examine descendant windows).
        /// </summary>
        public Win32Window TopChild => GetNextWindow(Win32GetWindowCommand.Child);

        /// <summary>
        /// Gets the collection of the top-level windows contained within the window's thread.
        /// </summary>
        public Collection<Win32Window> ThreadWindows => GetThreadWindows(ThreadId);

        /// <summary>
        /// Sets the control as the top-level control.
        /// </summary>
        public void SetTopLevel()
        {
            Parent = new Win32Window(IntPtr.Zero);
        }

        /// <summary>
        /// Retrieves a value indicating whether the specified window is a child of this window.
        /// </summary>
        public bool Contains(Win32Window wind)
        {
            return UnsafeNativeMethods.IsChild(Handle, wind.Handle);
        }

        /// <summary>
        /// Retrieves the child window at a specified location.
        /// </summary>
        /// <param name="pt">Point to find window at.</param>
        /// <param name="skipValue">Determining whether to ignore child controls of a certain type.</param>
        public Win32Window GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
        {
            Win32Window descendant = new Win32Window(UnsafeNativeMethods.ChildWindowFromPointEx(Handle, pt, (int)skipValue));
            if (descendant.Handle != Handle)
                return descendant;
            return null;
        }

        /// <summary>
        /// Retrieves the child window at a specified location.
        /// </summary>
        public Win32Window GetChildAtPoint(Point pt)
        {
            return GetChildAtPoint(pt, GetChildAtPointSkip.None);
        }

        /// <summary>
        /// Retrieves the window whose class name and window name match the specified strings. 
        /// The function searches child windows, beginning with the one following the specified child window. 
        /// This function does not perform a case-sensitive search. 
        /// </summary>
        /// <param name="childAfter">The search begins with the next child window in the Z order. The child window must be a direct child window, not just a descendant window. </param>
        /// <param name="className">Class Name to look for.</param>
        /// <param name="windowName">Window Name to look for.</param>
        public Win32Window FindChildWindow(Win32Window childAfter, string className, string windowName)
        {
            // validate arguments
            if (childAfter == null)
                throw new ArgumentNullException(nameof(childAfter));

            return new Win32Window(UnsafeNativeMethods.FindWindowEx(Handle, childAfter.Handle, className, windowName));
        }
        /// <summary>
        /// Find a child window with the given classname and window name.
        /// </summary>
        /// <param name="className">Class name.</param>
        /// <param name="windowName">Window name.</param>
        public Win32Window FindChildWindow(string className, string windowName)
        {
            return new Win32Window(UnsafeNativeMethods.FindWindowEx(Handle, IntPtr.Zero, className, windowName));
        }

        /// <summary>
        /// Retrieve a child window of teh given classname.
        /// </summary>
        /// <param name="className">Clasname to look for</param>
        /// <returns>First found child window of that class or null if none found.</returns>
        public Win32Window FindChildWindow(string className)
        {
            // Go through all children
            foreach (Win32Window child in Children)
            {
                // Check the child
                if (child.ClassName.Equals(className, StringComparison.Ordinal))
                    return child;

                // Check the children of the child
                Win32Window result = child.FindChildWindow(className);

                // Return the window if it was found.
                if (result != null)
                    return result;
            }

            // Return null if we was unable to find the Window.
            return null;
        }

        /// <summary>
        /// Retrieves a window that has the specified relationship (Z-Order or owner) to this window. 
        /// </summary>
        /// <param name="command">Specifies the relationship between this window and the window which is retrieved.</param>
        public Win32Window GetNextWindow(Win32GetWindowCommand command)
        {
            return new Win32Window(UnsafeNativeMethods.GetWindow(Handle, (int)command));
        }

        /// <summary>
        /// Inserts this window after the specified in the Z order.
        /// </summary>
        /// <param name="predecessor">A window to precede the positioned window in the Z order.</param>
        public void InsertAfter(Win32Window predecessor)
        {
            // validate
            if (predecessor == null) throw new ArgumentNullException(nameof(predecessor));

            UnsafeNativeMethods.SetWindowPos(Handle, (Win32WindowPositionAfter)predecessor.Handle, 0, 0, 0, 0, Win32WindowPositionOptions.NoSize | Win32WindowPositionOptions.NoMove |
                Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
        }

        /// <summary>
        /// Places the window at the top of the Z order.
        /// </summary>
        public bool BringToFront()
        {
            return UnsafeNativeMethods.SetWindowPos(Handle, Win32WindowPositionAfter.HWND_TOP, 0, 0, 0, 0, Win32WindowPositionOptions.NoSize | Win32WindowPositionOptions.NoMove |
                Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
        }

        /// <summary>
        /// Places the window at the bottom of the Z order. If if current window is topmost window, 
        /// the window loses its topmost status and is placed at the bottom of all other windows.
        /// </summary>
        public void SendToBack()
        {
            UnsafeNativeMethods.SetWindowPos(Handle, Win32WindowPositionAfter.HWND_BOTTOM, 0, 0, 0, 0, Win32WindowPositionOptions.NoSize | Win32WindowPositionOptions.NoMove |
                Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
        }

        #endregion

        #region Coordinats
        /// <summary>
        /// Gets or sets the size and location of the window on the Windows desktop.
        /// </summary>
        public Rectangle DesktopBounds
        {
            get
            {
                Win32Rectangle rect = new Win32Rectangle();
                UnsafeNativeMethods.GetWindowRect(Handle, out rect);
                return rect;
            }
            set
            {
                Point locat = Parent.PointToClient(value.Location);
                UnsafeNativeMethods.SetWindowPos(Handle, 0, locat.X, locat.Y, value.Width, value.Height, Win32WindowPositionOptions.NoZOrder |
                    Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }
        /// <summary>
        /// Gets or sets the location of the window on the Windows desktop.
        /// </summary>
        public Point DesktopLocation
        {
            get
            {
                return DesktopBounds.Location;
            }
            set
            {
                Point locat = Parent.PointToClient(value);
                UnsafeNativeMethods.SetWindowPos(Handle, 0, locat.X, locat.Y, 0, 0, Win32WindowPositionOptions.NoZOrder | Win32WindowPositionOptions.NoSize |
                    Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }
        /// <summary>
        /// Gets or sets the size and location of the window including its nonclient elements, in pixels, relative to the parent window.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                var parent = Parent;
                if (parent != IntPtr.Zero)
                {
                    Win32Rectangle rect = DesktopBounds;
                    return new Rectangle(parent.PointToClient(rect.Location), rect.Size);
                }
                return DesktopBounds;
            }
            set
            {
                UnsafeNativeMethods.SetWindowPos(Handle, 0, value.X, value.Y, value.Width, value.Height, Win32WindowPositionOptions.NoZOrder | Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }
        /// <summary>
        /// Gets or sets the size of the window.
        /// </summary>
        public Size Size
        {
            get { return Bounds.Size; }
            set
            {
                UnsafeNativeMethods.SetWindowPos(Handle, 0, 0, 0, value.Width, value.Height, Win32WindowPositionOptions.NoZOrder | Win32WindowPositionOptions.NoMove | Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }
        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of the window relative to the upper-left corner of its parent.
        /// </summary>
        public Point Location
        {
            get
            {
                return Bounds.Location;
            }
            set
            {
                UnsafeNativeMethods.SetWindowPos(Handle, 0, value.X, value.Y, 0, 0, Win32WindowPositionOptions.NoZOrder | Win32WindowPositionOptions.NoSize | Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }
        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        public int Width
        {
            get { return Bounds.Width; }
            set
            {
                UnsafeNativeMethods.SetWindowPos(Handle, 0, 0, 0, value, Bounds.Height, Win32WindowPositionOptions.NoZOrder | Win32WindowPositionOptions.NoMove | Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }
        /// <summary>
        /// Gets or sets the height of the window.
        /// </summary>
        public int Height
        {
            get { return Bounds.Height; }
            set
            {
                UnsafeNativeMethods.SetWindowPos(Handle, 0, 0, 0, Bounds.Width, value, Win32WindowPositionOptions.NoZOrder | Win32WindowPositionOptions.NoMove | Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }
        /// <summary>
        /// Gets/sets the distance, in pixels, between the top edge of the window and the top edge of its parent's client area.
        /// </summary>
        public int Top
        {
            get { return Bounds.Top; }
            set
            {
                UnsafeNativeMethods.SetWindowPos(Handle, 0, Bounds.Left, value, 0, 0, Win32WindowPositionOptions.NoZOrder | Win32WindowPositionOptions.NoSize | Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }
        /// <summary>
        /// Gets/sets the distance, in pixels, between the bottom edge of the window and the top edge of its parent's client area.
        /// </summary>
        public int Bottom
        {
            get { return Bounds.Bottom; }
            set
            {
                Rectangle bounds = Bounds;
                UnsafeNativeMethods.SetWindowPos(Handle, 0, 0, 0, bounds.Width, value - bounds.Top, Win32WindowPositionOptions.NoZOrder | Win32WindowPositionOptions.NoMove | Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }
        /// <summary>
        /// Gets/sets or sets the distance, in pixels, between the left edge of the window and the left edge of its parent's client area.
        /// </summary>
        public int Left
        {
            get { return Bounds.Left; }
            set
            {
                UnsafeNativeMethods.SetWindowPos(Handle, 0, value, Bounds.Top, 0, 0, Win32WindowPositionOptions.NoZOrder | Win32WindowPositionOptions.NoSize | Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }
        /// <summary>
        /// Gets/sets the distance, in pixels, between the right edge of the window and the left edge of its parent's client area.
        /// </summary>
        public int Right
        {
            get { return Bounds.Right; }
            set
            {
                Rectangle bounds = Bounds;
                UnsafeNativeMethods.SetWindowPos(Handle, 0, 0, 0, value - bounds.Left, bounds.Height, Win32WindowPositionOptions.NoZOrder | Win32WindowPositionOptions.NoMove | Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
            }
        }
        /// <summary>
        /// Gets the rectangle that represents the client area of the window.
        /// </summary>
        public Rectangle ClientRectangle
        {
            get
            {
                Win32Rectangle rect = new Win32Rectangle();
                UnsafeNativeMethods.GetClientRect(Handle, out rect);
                return rect;
            }
        }
        /// <summary>
        /// Gets/sets the size of the client area of the window.
        /// </summary>
        public Size ClientSize
        {
            get { return ClientRectangle.Size; }
            set
            {
                Win32Rectangle rect = new Win32Rectangle(0, 0, value.Width, value.Height);
                UnsafeNativeMethods.AdjustWindowRect(ref rect, Styles, Menu != IntPtr.Zero);
                Size = rect.Size;
            }
        }
        /// <summary>
        /// Gets the maximum size the window can be resized to. Be careful with this property, check IsHung first.
        /// </summary>
        public Size MaximumSize
        {
            get
            {
                Win32MinMaxInfo inf = new Win32MinMaxInfo();
                UnsafeNativeMethods.SendMessage(Handle, Win32Constants.WM_GETMINMAXINFO, IntPtr.Zero, ref inf);
                return (Size)inf.ptMaxTrackSize;
            }
        }
        /// <summary>
        /// Gets the minimum size the window can be resized to. Be careful with this property, check IsHung first.
        /// </summary>
        public Size MinimumSize
        {
            get
            {
                Win32MinMaxInfo inf = new Win32MinMaxInfo();
                UnsafeNativeMethods.SendMessage(Handle, Win32Constants.WM_GETMINMAXINFO, IntPtr.Zero, ref inf);
                return (Size)inf.ptMinTrackSize;
            }
        }
        /// <summary>
        /// Gets the size of the window when it is maximized. Be careful with this property, check IsHung first.
        /// </summary>
        public Rectangle MaximizedBounds
        {
            get
            {
                var info = new Win32MinMaxInfo();
                UnsafeNativeMethods.SendMessage(Handle, Win32Constants.WM_GETMINMAXINFO, IntPtr.Zero, ref info);
                return new Rectangle(info.ptMaxPosition, (Size)info.ptMaxSize);
            }
        }
        /// <summary>
        /// Gets/sets the location and size of the window in its normal window state.
        /// </summary>
        public Rectangle RestoreBounds
        {
            get
            {
                var placement = new Win32WindowPlacement();
                placement.length = Marshal.SizeOf(placement);
                var returnCode = UnsafeNativeMethods.GetWindowPlacement(Handle, ref placement);
                if (returnCode != Win32Constants.NO_ERROR)
                    throw new Win32Exception(returnCode, SafeNativeMethods.FormatMessage(returnCode));
                return new Rectangle(placement.rcNormalPosition_left, placement.rcNormalPosition_top,
                    placement.rcNormalPosition_right - placement.rcNormalPosition_left, placement.rcNormalPosition_bottom - placement.rcNormalPosition_top);
            }
            set
            {
                var placement = new Win32WindowPlacement();
                placement.length = Marshal.SizeOf(placement);
                placement.rcNormalPosition_left = value.Left;
                placement.rcNormalPosition_top = value.Top;
                placement.rcNormalPosition_right = value.Left + value.Width;
                placement.rcNormalPosition_bottom = value.Top + value.Height;
                if (!UnsafeNativeMethods.SetWindowPlacement(Handle, ref placement))
                {
                    var returnCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(returnCode, SafeNativeMethods.FormatMessage(returnCode));
                }
            }
        }
        /// <summary>
        /// Computes the location of the specified screen point into client coordinates.
        /// </summary>
        public Point PointToClient(Point screenPoint)
        {
            Win32Point pt = screenPoint;
            if (!UnsafeNativeMethods.ScreenToClient(Handle, ref pt))
            {
                var returnCode = Marshal.GetLastWin32Error();
                throw new Win32Exception(returnCode, SafeNativeMethods.FormatMessage(returnCode));
            }
            return pt;
        }
        /// <summary>
        /// Computes the location of the specified client point into screen coordinates.
        /// </summary>
        public Point PointToScreen(Point clientPoint)
        {
            Win32Point pt = clientPoint;
            if (!UnsafeNativeMethods.ClientToScreen(Handle, ref pt))
            {
                var returnCode = Marshal.GetLastWin32Error();
                throw new Win32Exception(returnCode, SafeNativeMethods.FormatMessage(returnCode));
            }
            return pt;
        }
        /// <summary>
        /// Computes the size and location of the specified screen rectangle in client coordinates.
        /// </summary>
        public Rectangle RectangleToClient(Rectangle screenRect)
        {
            Win32Rectangle rect = screenRect;
            var returnCode = UnsafeNativeMethods.MapWindowPoints(IntPtr.Zero, Handle, ref rect, 2);
            if (returnCode != Win32Constants.NO_ERROR)
                throw new Win32Exception(returnCode, SafeNativeMethods.FormatMessage(returnCode));
            return rect;
        }
        /// <summary>
        /// Computes the size and location of the specified client rectangle in screen coordinates.
        /// </summary>
        public Rectangle RectangleToScreen(Rectangle clientRect)
        {
            Win32Rectangle rect = clientRect;
            var returnCode = UnsafeNativeMethods.MapWindowPoints(Handle, IntPtr.Zero, ref rect, 2);
            if (returnCode != Win32Constants.NO_ERROR)
                throw new Win32Exception(returnCode, SafeNativeMethods.FormatMessage(returnCode));
            return rect;
        }

        #endregion

        #region State
        /// <summary>
        /// Gets/sets the window's visible state. Implements the same as Form.WindowState.
        /// Sometimes it differs from real window state. For changing current state use WindowState.
        /// </summary>
        public FormWindowState WindowVisibleState
        {
            get
            {
                var placement = new Win32WindowPlacement();
                placement.length = Marshal.SizeOf(placement);
                var returnCode = UnsafeNativeMethods.GetWindowPlacement(Handle, ref placement);
                if (returnCode != Win32Constants.NO_ERROR)
                    throw new Win32Exception(returnCode, SafeNativeMethods.FormatMessage(returnCode));
                if (placement.showCmd == (int)Win32ShowWindow.SW_SHOWMINIMIZED)
                    return FormWindowState.Minimized;
                if (placement.showCmd == (int)Win32ShowWindow.SW_SHOWMAXIMIZED)
                    return FormWindowState.Maximized;
                return FormWindowState.Normal;

            }
            set
            {
                var placement = new Win32WindowPlacement();
                placement.length = Marshal.SizeOf(placement);
                var returnCode = UnsafeNativeMethods.GetWindowPlacement(Handle, ref placement);
                if (returnCode != Win32Constants.NO_ERROR)
                    throw new Win32Exception(returnCode, SafeNativeMethods.FormatMessage(returnCode));
                switch (value)
                {
                    case FormWindowState.Maximized:
                        UnsafeNativeMethods.ShowWindow(Handle, Win32ShowWindow.SW_MAXIMIZE);
                        break;
                    case FormWindowState.Minimized:
                        UnsafeNativeMethods.ShowWindow(Handle, Win32ShowWindow.SW_MINIMIZE);
                        break;
                    case FormWindowState.Normal:
                        UnsafeNativeMethods.ShowWindow(Handle, Win32ShowWindow.SW_NORMAL);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets/sets the window's state. See WindowVisibleState also.
        /// </summary>
        public FormWindowState WindowState
        {
            get { return WindowVisibleState; }
            set
            {
                switch (value)
                {
                    case FormWindowState.Maximized:
                        SysCommand(Win32SystemControl.SC_MAXIMIZE, 0);
                        break;
                    case FormWindowState.Minimized:
                        SysCommand(Win32SystemControl.SC_MINIMIZE, 0);
                        break;
                    case FormWindowState.Normal:
                        RestoreToMaximized = false;
                        SysCommand(Win32SystemControl.SC_RESTORE, 0);
                        break;
                }
            }
        }
        /// <summary>
        /// Gets/sets a value indicating whether the window is maximized in restored state.
        /// This setting is only valid the next time the window is restored. It does not change the default restoration behavior. 
        /// This property is only valid when the window is minimized.
        /// </summary>
        public bool RestoreToMaximized
        {
            get
            {
                var placement = new Win32WindowPlacement();
                placement.length = Marshal.SizeOf(placement);
                var returnCode = UnsafeNativeMethods.GetWindowPlacement(Handle, ref placement);
                if (returnCode != Win32Constants.NO_ERROR)
                    throw new Win32Exception(returnCode, SafeNativeMethods.FormatMessage(returnCode));
                if (placement.showCmd != (int)Win32ShowWindow.SW_SHOWMINIMIZED || placement.flags != Win32Constants.WPF_RESTORETOMAXIMIZED)
                    return false;
                return true;
            }
            set
            {
                var placement = new Win32WindowPlacement();
                placement.length = Marshal.SizeOf(placement);
                var returnCode = UnsafeNativeMethods.GetWindowPlacement(Handle, ref placement);
                if (returnCode != Win32Constants.NO_ERROR)
                    throw new Win32Exception(returnCode, SafeNativeMethods.FormatMessage(returnCode));
                if (placement.showCmd != (int)Win32ShowWindow.SW_SHOWMINIMIZED)
                    return;
                if (value)
                    placement.flags |= Win32Constants.WPF_RESTORETOMAXIMIZED;
                else
                    placement.flags &= ~Win32Constants.WPF_RESTORETOMAXIMIZED;
                if (!UnsafeNativeMethods.SetWindowPlacement(Handle, ref placement))
                {
                    returnCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(returnCode, SafeNativeMethods.FormatMessage(returnCode));
                }
            }
        }
        /// <summary>
        /// Minimizes the window.
        /// </summary>
        public void Minimize()
        {
            WindowState = FormWindowState.Minimized;
        }
        /// <summary>
        /// Restores the window from minimized to its previous state.
        /// </summary>
        public void Restore()
        {
            if (WindowState != FormWindowState.Minimized)
                return;

            WindowState = RestoreToMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        #endregion

        #region Visibility
        /// <summary>
        /// Gets/sets a value indicating whether the opacity of the window can be adjusted.
        /// </summary>
        public bool AllowTransparency
        {
            get
            {
                return ExStyles.Check(Win32WindowExStyles.WS_EX_LAYERED);
            }
            set
            {
                if (AllowTransparency == value)
                    return;
                SetExStyle(Win32WindowExStyles.WS_EX_LAYERED, value);
            }
        }
        /// <summary>
        /// Gets/sets the color that will represent transparent areas of the window.
        /// </summary>
        public Color TransparencyKey
        {
            get
            {
                int col;
                byte t1; int flags;
                UnsafeNativeMethods.GetLayeredWindowAttributes(Handle, out col, out t1, out flags);
                if ((flags & Win32Constants.LWA_COLORKEY) == Win32Constants.LWA_COLORKEY)
                    return ColorTranslator.FromWin32(col);
                return Color.Empty;
            }
            set
            {
                SetLayeredAttributesInternal(ColorTranslator.ToWin32(value), 0, Win32Constants.LWA_COLORKEY);
            }
        }
        /// <summary>
        /// Gets/sets the opacity level of the window.
        /// </summary>
        public double Opacity
        {
            get
            {
                byte opac;
                int t1, flags;
                UnsafeNativeMethods.GetLayeredWindowAttributes(Handle, out t1, out opac, out flags);
                if ((flags & Win32Constants.LWA_ALPHA) == Win32Constants.LWA_ALPHA)
                    return (double)opac / 255;
                return 1;
            }
            set
            {
                if (value > 1.0)
                    value = 1.0;
                else if (value < 0.0)
                    value = 0.0;
                byte opacity = (byte)(0xff * value);
                SetLayeredAttributesInternal(0, opacity, Win32Constants.LWA_ALPHA);
            }
        }
        /// <summary>
        /// Gets/sets a value indicating whether the window and is visible to user.
        /// </summary>
        public bool Visible
        {
            get
            {
                return UnsafeNativeMethods.IsWindowVisible(Handle);
            }
            set
            {
                if (value)
                    Show();
                else
                    Hide();
            }
        }
        /// <summary>
        /// Gets/sets the window region associated with the window.
        /// </summary>
        public Region Region
        {
            get
            {
                IntPtr hRgn = UnsafeNativeMethods.CreateRectRgn(0, 0, 0, 0);
                UnsafeNativeMethods.GetWindowRgn(Handle, hRgn);
                Win32Rectangle rect = new Win32Rectangle();
                if (UnsafeNativeMethods.GetRgnBox(hRgn, out rect) == 1)
                    return null;
                return Region.FromHrgn(hRgn);
            }
            set
            {
                if (value == null) throw new ArgumentNullException();
                IntPtr hrgn = value.GetHrgn(Graphics.FromHdc(UnsafeNativeMethods.GetDC(Handle)));
                UnsafeNativeMethods.SetWindowRgn(Handle, hrgn, true);
            }
        }
        /// <summary>
        /// Gets/sets weather the window is considered to be transparent. Can be used for hit testing or for drawing underlayered windows.
        /// </summary>
        public bool Transparent
        {//If Transparent specified and no layout attributes, then flags are set to LWA_ALPHA and alpha to opaque.
            get
            {
                return ExStyles.Check(Win32WindowExStyles.WS_EX_TRANSPARENT);
            }
            set
            {
                SetExStyle(Win32WindowExStyles.WS_EX_TRANSPARENT, value);
                SetLayeredAttributesInternal(0, 0, 0);
            }
        }
        /// <summary>
        /// Conceals the control from the user.
        /// </summary>
        public bool Hide()
        {
            return UnsafeNativeMethods.ShowWindow(Handle, Win32ShowWindow.SW_HIDE);
        }
        /// <summary>
        /// Displays the window. 
        /// </summary>
        public bool Show()
        {
            return Show(true);
        }
        /// <summary>
        /// Displays the window. 
        /// </summary>
        /// <param name="activate">Whether the window have to be activated. </param>
        public bool Show(bool activate)
        {
            if (activate)
                return UnsafeNativeMethods.ShowWindow(Handle, Win32ShowWindow.SW_SHOW);
            else
                return UnsafeNativeMethods.ShowWindow(Handle, Win32ShowWindow.SW_SHOWNA);
        }
        /// <summary>
        /// Causes the window to redraw the invalidated regions within its client area.
        /// </summary>
        public bool Update()
        {
            return UnsafeNativeMethods.UpdateWindow(Handle);
        }
        /// <summary>
        /// Invalidates a specific region of the window and causes a paint message to be sent to the window.
        /// </summary>
        public void Invalidate(bool invalidateChildren)
        {
            if (invalidateChildren)
                UnsafeNativeMethods.RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, Win32RedrawOptions.Erase | Win32RedrawOptions.Invalidate | Win32RedrawOptions.AllChildren);
            else
                UnsafeNativeMethods.InvalidateRect(Handle, IntPtr.Zero, Opacity != 1);
        }
        /// <summary>
        /// Invalidates the window and causes a paint message to be sent to the window.
        /// </summary>
        public void Invalidate()
        {
            Invalidate(false);
        }
        /// <summary>
        /// Forces the window to invalidate its client area and immediately redraw itself and any child controls.
        /// </summary>
        public void Refresh()
        {
            Invalidate(true);
            Update();
        }
        /// <summary>
        /// Sets window's layered attributes in according to the specified values an Transparent state.
        /// For more information, see SetLayeredWindowAttributes in MSDN.
        /// </summary>
        private bool SetLayeredAttributesInternal(int crKey, byte bAlpha, int dwFlags)
        {
            int key; byte alpha; int flags;
            bool changed = false;
            UnsafeNativeMethods.GetLayeredWindowAttributes(Handle, out key, out alpha, out flags);
            if (((dwFlags & Win32Constants.LWA_ALPHA) == Win32Constants.LWA_ALPHA))
            {
                changed = true;
                alpha = bAlpha;
                if (alpha < 255)
                    flags |= Win32Constants.LWA_ALPHA;
                else
                    flags &= ~Win32Constants.LWA_ALPHA;
            }
            if (((dwFlags & Win32Constants.LWA_COLORKEY) == Win32Constants.LWA_COLORKEY))
            {
                changed = true;
                key = crKey;
                if (key != 0)
                    flags |= Win32Constants.LWA_COLORKEY;
                else
                    flags &= ~Win32Constants.LWA_COLORKEY;
            }
            if (flags == 0 && !Transparent)
                AllowTransparency = false;
            else
                AllowTransparency = true;
            if (flags == 0 && Transparent)
            {//If Transparent specified and no layout attributes, then set flags to LWA_ALPHA and alpha to opaque.
                changed = true;
                alpha = 255;
                flags = Win32Constants.LWA_ALPHA;
            }
            else
                if (alpha == 255 && ((flags & Win32Constants.LWA_ALPHA) == Win32Constants.LWA_ALPHA))
                    flags &= ~Win32Constants.LWA_ALPHA;
            if (changed)
                return UnsafeNativeMethods.SetLayeredWindowAttributes(Handle, key, alpha, flags);
            else
                return true;
        }

        #endregion

        #region Styles

        /// <summary>
        /// Get/Set current window styles. Remember, that changing the object, returned with this property does't change window's styles.
        /// Set: Styles just set, but don't apply. Call UpdateStyles() to apply them to the window.
        /// </summary>
        public Win32WindowStyle Styles
        {
            get
            {
                return GetWindowLong(Handle, Win32Constants.GWL_STYLE);
            }
            set
            {
                SetWindowLong(Handle, Win32Constants.GWL_STYLE, value);
            }
        }
        /// <summary>
        /// Get/Set current window extended styles. Remember, that changing the object, returned with this property does't change window's styles.
        /// Set: Styles just set, but don't apply. Call UpdateExStyles() to apply them to the window.
        /// </summary>
        public Win32WindowExStyle ExStyles
        {
            get
            {
                return (Win32WindowExStyles)GetWindowLong(Handle, Win32Constants.GWL_EXSTYLE);
            }
            set
            {
                SetWindowLong(Handle, Win32Constants.GWL_EXSTYLE, value);
            }
        }
        /// <summary>
        /// Forces the assigned styles to be reapplied to the window.
        /// </summary>
        public void UpdateStyles()
        {
            UnsafeNativeMethods.SetWindowPos(Handle, 0, 0, 0, 0, 0, Win32WindowPositionOptions.FrameChanged | Win32WindowPositionOptions.NoZOrder |
                Win32WindowPositionOptions.NoSize | Win32WindowPositionOptions.NoMove | Win32WindowPositionOptions.NoActivate | Win32WindowPositionOptions.NoOwnerZOrder);
        }
        /// <summary>
        /// Retrieves the value of the specified window styles for the window.
        /// </summary>
        /// <param name="styles">Checked styles. Could be specified more than one.</param>
        public bool GetStyle(Win32WindowStyles styles)
        {
            return Styles.Check(styles);
        }
        /// <summary>
        /// Sets the specified styles to the specified value and applies them.
        /// </summary>
        /// <param name="styles">Styles to set. Could be specified more than one.</param>
        /// <param name="value">true to set specified styles to the window; false - to reset. </param>
        public void SetStyle(Win32WindowStyles styles, bool value)
        {
            SetStyle(styles, value, true);
        }
        /// <summary>
        /// Sets the specified styles to the specified value.
        /// </summary>
        /// <param name="styles">Styles to set. Could be specified more than one.</param>
        /// <param name="value">true to set specified styles to the window; false - to reset. </param>
        /// <param name="applyStyles">true to apply specified styles to the window; false - just to set.</param>
        public void SetStyle(Win32WindowStyles styles, bool value, bool applyStyles)
        {
            Styles = Styles.Set(styles, value);
            if (applyStyles)
                UpdateStyles();
        }
        /// <summary>
        /// Forces the assigned extended styles to be reapplied to the window.
        /// </summary>
        public void UpdateExStyles()
        {
            UpdateStyles();
        }
        /// <summary>
        /// Retrieves the value of the specified window extended styles for the window.
        /// </summary>
        /// <param name="styles">Checked styles. Could be specified more than one.</param>
        public bool GetExStyle(Win32WindowExStyles styles)
        {
            return ExStyles.Check(styles);
        }
        /// <summary>
        /// Sets the specified extended styles to the specified value and applies them.
        /// </summary>
        /// <param name="styles">Styles to set. Could be specified more than one.</param>
        /// <param name="value">true to set specified styles to the window; false - to reset.</param>
        public void SetExStyle(Win32WindowExStyles styles, bool value)
        {
            SetExStyle(styles, value, true);
        }
        /// <summary>
        /// Sets the specified styles to the specified value.
        /// </summary>
        /// <param name="styles">Styles to set. Could be specified more than one.</param>
        /// <param name="value">true to set specified styles to the window; false - to reset.</param>
        /// <param name="applyStyles">true to apply specified styles to the window; false - just to set.</param>
        public void SetExStyle(Win32WindowExStyles styles, bool value, bool applyStyles)
        {
            ExStyles = ExStyles.Set(styles, value);
            if (applyStyles)
                UpdateExStyles();
        }

        #endregion

        #region Window Info

        /// <summary>
        /// Gets the window handle.
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// Determines whether this object identifies an existing window.
        /// </summary>
        public bool Exists => UnsafeNativeMethods.IsWindow(Handle);

        /// <summary>
        /// Gets/sets the text associated with this window. Be careful with this property, check IsHung first.
        /// </summary>
        public string TextUnsafe
        {
            get
            {
                var textLength = (int)UnsafeNativeMethods.SendMessage(Handle, Win32Constants.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                var text = new StringBuilder(textLength + 1);
                UnsafeNativeMethods.SendMessage(Handle, Win32Constants.WM_GETTEXT, (IntPtr)text.Capacity, text);
                return text.ToString();
            }
            set
            {
                UnsafeNativeMethods.SendMessage(Handle, Win32Constants.WM_SETTEXT, IntPtr.Zero, new StringBuilder(value));
            }
        }

        /// <summary>
        /// Gets/sets the text associated with this window. 
        /// This property cann't get/set the text of the most window types. See TextUnsafe also.
        /// </summary>
        public string Text
        {
            get
            {
                StringBuilder text = new StringBuilder(260);
                int lengthRead = UnsafeNativeMethods.GetWindowText(Handle, text, text.Capacity);
                if (lengthRead > 0)
                    return text.ToString();
                else
                    return String.Empty;
            }
            set
            {
                UnsafeNativeMethods.SetWindowText(Handle, value);
            }
        }

        /// <summary>
        /// Gets/sets a handle to the menu assigned to the window.
        /// </summary>
        public IntPtr Menu
        {
            get
            {
                IntPtr pt = UnsafeNativeMethods.GetMenu(Handle);
                if (UnsafeNativeMethods.IsMenu(pt))
                    return pt;
                else
                    return IntPtr.Zero;
            }
            set
            {
                if (UnsafeNativeMethods.IsMenu(value))
                    UnsafeNativeMethods.SetMenu(Handle, value);
            }
        }

        /// <summary>
        /// Gets/sets a value indicating whether the window can respond to user interaction.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return UnsafeNativeMethods.IsWindowEnabled(Handle);
            }
            set
            {
                UnsafeNativeMethods.EnableWindow(Handle, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the window is displayed modally.
        /// </summary>
        public bool Modal
        {
            get
            {
                Win32WindowStyle styles = Styles;
                if (styles.Check(Win32WindowStyles.WS_DISABLED) || !styles.Check(Win32WindowStyles.WS_VISIBLE))
                    return false;
                Win32Window res = new Win32Window(IntPtr.Zero);
                GCHandle hRes = GCHandle.Alloc(res);
                try
                {
                    EnumThreadWindows(EnumModalProc, GCHandle.ToIntPtr(hRes));
                }
                catch
                {
                    // can be ignored
                }

                // Free hres if required.
                if (hRes.IsAllocated)
                    hRes.Free();
                return res.Handle == Handle;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the window can receive focus.
        /// </summary>
        public bool CanFocus => (Visible && Enabled);

        /// <summary>
        /// Gets a value indicating whether the window has input focus.
        /// </summary>
        public bool Focused => UnsafeNativeMethods.GetFocus() == Handle;

        /// <summary>
        /// Gets a value indicating whether the window, or one of its child windows, currently has the input focus.
        /// </summary>
        public bool ContainsFocus
        {
            get
            {
                IntPtr focus = UnsafeNativeMethods.GetFocus();
                if (focus == IntPtr.Zero)
                    return false;
                return ((focus == Handle) || UnsafeNativeMethods.IsChild(Handle, focus));
            }
        }

        /// <summary>
        /// Gets/sets a value indicating whether the window has captured the mouse.
        /// </summary>
        public bool Capture
        {
            get
            {
                return UnsafeNativeMethods.GetCapture() == Handle;
            }
            set
            {
                if (Capture != value)
                {
                    if (value)
                    {
                        UnsafeNativeMethods.SetCapture(Handle);
                    }
                    else
                    {
                        UnsafeNativeMethods.ReleaseCapture();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the identifier of the thread that created this window.
        /// </summary>
        public uint ThreadId
        {
            get
            {
                uint prId;
                return UnsafeNativeMethods.GetWindowThreadProcessId(Handle, out prId);
            }
        }

        /// <summary>
        /// Gets the identifier of the process that created the window. 
        /// </summary>
        public uint ProcessId
        {
            get
            {
                uint prId;
                UnsafeNativeMethods.GetWindowThreadProcessId(Handle, out prId);
                return prId;
            }
        }

        /// <summary>
        /// Determines whether the specified window is a native Unicode window. For more information, see IsWindowUnicode in MSDN.
        /// </summary>
        public bool IsUnicodeWindow => UnsafeNativeMethods.IsWindowUnicode(Handle);

        /// <summary>
        /// Determine if Microsoft Windows considers that a specified application is not responding.
        /// For more information, see IsHungAppWindow in MSDN.
        /// </summary>
        public bool IsHung => UnsafeNativeMethods.IsHungAppWindow(Handle);

        /// <summary>
        /// Gets the name of the class to which the specified window belongs.
        /// For more information, see GetClassName in MSDN.
        /// </summary>
        public string ClassName
        {
            get
            {
                StringBuilder cName = new StringBuilder(261);
                int len = UnsafeNativeMethods.GetClassName(Handle, cName, 260);
                return cName.ToString(0, len);
            }
        }

        /// <summary>
        /// Gets a string that specifies the window type.
        /// For more information, see RealClassName in MSDN.
        /// </summary>
        public string RealClassName
        {
            get
            {
                StringBuilder cName = new StringBuilder(261);
                UnsafeNativeMethods.RealGetWindowClass(Handle, cName, 260);
                return cName.ToString();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the window is displayed in the Windows taskbar. 
        /// </summary>
        public bool ShowInTaskbar => ExStyles.Check(Win32WindowExStyles.WS_EX_APPWINDOW);

        /// <summary>
        /// Test if an icon is correct.
        /// </summary>
        /// <param name="ic">icon to check.</param>
        /// <returns>null if it is not an icon with a height and width greater 0.</returns>
        private Icon TestIcon(Icon ic)
        {
            if (ic != null)
                if (ic.Height > 0 && ic.Width > 0)
                    return ic;
            return null;
        }

        /// <summary>
        /// Gets the large icon for the window. Be careful with this property, check IsHung first.
        /// </summary>
        public Icon Icon
        {
            get
            {
                IntPtr icon = UnsafeNativeMethods.SendMessage(Handle, Win32Constants.WM_GETICON, (IntPtr)Win32Constants.ICON_BIG, IntPtr.Zero);
                if (icon != IntPtr.Zero)
                    return TestIcon(Icon.FromHandle(icon));
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets the small icon for the window. Be careful with this property, check IsHung first.
        /// </summary>
        public Icon SmallIcon
        {
            get
            {
                IntPtr icon = UnsafeNativeMethods.SendMessage(Handle, Win32Constants.WM_GETICON, (IntPtr)Win32Constants.ICON_SMALL, IntPtr.Zero);
                if (icon != IntPtr.Zero)
                    return TestIcon(Icon.FromHandle(icon));
                else
                {
                    Version osVersion = Environment.OSVersion.Version;
                    if (osVersion.Major >= 5 && osVersion.Minor >= 1)//minimum operation system for ICON_SMALL2 is WinXP.
                        icon = UnsafeNativeMethods.SendMessage(Handle, Win32Constants.WM_GETICON, (IntPtr)Win32Constants.ICON_SMALL2, IntPtr.Zero);
                    if (icon != IntPtr.Zero)
                        return TestIcon(Icon.FromHandle(icon));
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets an icon associated with the window class. For more information, see GCL_HICON in MSDN.
        /// </summary>
        public Icon ClassIcon
        {
            get
            {
                IntPtr icon = GetClassLongPtr(Handle, Win32Constants.GCL_HICON);
                if (icon != IntPtr.Zero)
                    return TestIcon(Icon.FromHandle(icon));
                return null;
            }
        }

        /// <summary>
        /// Gets a small icon associated with the window class.For more information, see GCL_HICONSM in MSDN.
        /// </summary>
        public Icon SmallClassIcon
        {
            get
            {
                IntPtr icon = GetClassLongPtr(Handle, Win32Constants.GCL_HICONSM);
                if (icon != IntPtr.Zero)
                    return TestIcon(Icon.FromHandle(icon));
                return null;
            }
        }

        /// <summary>
        /// Get the ControlID (GetDlgCtrlID)
        /// </summary>
        public int ControlID => UnsafeNativeMethods.GetDlgCtrlID(Handle);

        #endregion

        #region Other Functions

        /// <summary>
        /// Get the ControlID of an item form the given class.
        /// </summary>
        /// <param name="className">Name of Class that we are looking for.</param>
        /// <returns>ControlID or 0 if we cannot find it.</returns>
        public int FindControl(string className)
        {
            // Check if this is the Control we are looking for.
            if (ClassName.Equals(className, StringComparison.Ordinal))
                return ControlID;

            foreach (Win32Window child in Children)
            {
                // Check the child for the Control.
                int ctrl = child.FindControl(className);

                // Return COntrol if we found it.
                if (ctrl != 0)
                    return ctrl;
            }

            // Return 0 if we was unable to find the control.
            return 0;
        }

        /// <summary>
        /// Closes the window. 
        /// </summary>
        public void Close()
        {
            SysCommand(Win32SystemControl.SC_CLOSE, 0);
        }

        /// <summary>
        /// Activates the form and gives it focus. 
        /// Cann't activate window in some cases, for example, when a popup window is shown. For more information, see SetForegroundWindow in MSDN.
        /// If you want to activate window in any case use ForceActivate();
        /// </summary>
        public bool Activate()
        {
            return UnsafeNativeMethods.SetForegroundWindow(Handle);
        }

        /// <summary>
        /// Activates the window and gives it focus in any case. See Activate() also.
        /// </summary>
        public void ForceActivate()
        {
            if (!UnsafeNativeMethods.SetForegroundWindow(Handle))
            {
                var topLevel = TopLevelWindow;
                var animation = StateChangeAnimation;
                if (animation)
                    StateChangeAnimation = false;
                topLevel.Minimize();
                topLevel.Restore();
                if (animation)
                    StateChangeAnimation = true;
                UnsafeNativeMethods.SetForegroundWindow(Handle);
            }
        }

        /// <summary>
        /// Sets input focus to the control.
        /// </summary>
        public bool Focus()
        {
            if (CanFocus)
                UnsafeNativeMethods.SetFocus(Handle);
            return Focused;
        }

        /// <summary>
        /// Creates the Graphics for the window. 
        /// </summary>
        public Graphics CreateGraphics()
        {
            return Graphics.FromHwnd(Handle);
        }
        private bool SysCommand(Win32SystemControl wParam, int lParam)
        {
            return UnsafeNativeMethods.PostMessage(Handle, Win32Constants.WM_SYSCOMMAND, new IntPtr((int)wParam), new IntPtr(lParam));
        }

        #endregion

        #region Adopted Windows Functions

        /// <summary>
        /// Send a Message to this window.
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="wParam">wParam</param>
        /// <param name="lParam">lParam</param>
        /// <returns>Return value.</returns>
        public IntPtr SendMessage(uint msg, IntPtr wParam, IntPtr lParam)
        {
            return UnsafeNativeMethods.SendMessage(Handle, msg, wParam, lParam);
        }

        /// <summary>
        /// Send a Message to this window.
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="wParam">wParam</param>
        /// <param name="lParam">lParam</param>
        /// <returns>Return value.</returns>
        public IntPtr SendMessage(uint msg, IntPtr wParam, StringBuilder lParam)
        {
            return UnsafeNativeMethods.SendMessage(Handle, msg, wParam, lParam);
        }

        /// <summary>
        /// Send a Message to a Control in the window
        /// </summary>
        /// <param name="nIDDlgItem">The ControlId of the target control.</param>
        /// <param name="msg">Message to send.</param>
        /// <param name="wParam">wparam</param>
        /// <param name="lParam">lparam</param>
        public IntPtr SendDlgItemMessage(int nIDDlgItem, uint msg, UIntPtr wParam, IntPtr lParam)
        {
            return UnsafeNativeMethods.SendDlgItemMessage(Handle, nIDDlgItem, msg, wParam, lParam);
        }

        /// <summary>
        /// Send a Message to a Control in the window
        /// </summary>
        /// <param name="nIDDlgItem">The ControlId of the target control.</param>
        /// <param name="msg">Message to send.</param>
        /// <param name="wParam">wparam</param>
        /// <param name="lParam">lparam</param>
        public IntPtr SendDlgItemMessage(int nIDDlgItem, uint msg, UIntPtr wParam, StringBuilder lParam)
        {
            return UnsafeNativeMethods.SendDlgItemMessage(Handle, nIDDlgItem, msg, wParam, lParam);
        }

        /// <summary>
        /// Get Text of an dialog item
        /// </summary>
        /// <param name="dlgItemId">ControlId of Control</param>
        /// <param name="maxCount">Maximum number of characters</param>
        /// <returns>Text of control or null</returns>
        public string GetDlgItemText(int dlgItemId, int maxCount)
        {
            // Build StringBuilder
            StringBuilder dlgText = new StringBuilder(maxCount + 1);

            // Call GeDlgItemText and check rc
            if (UnsafeNativeMethods.GetDlgItemText(Handle, dlgItemId, dlgText, maxCount) != 0)
                return dlgText.ToString();
            else
                return null;
        }

        #endregion

        #region WinApi Functions

        static IntPtr GetWindowLong(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
                return UnsafeNativeMethods.GetWindowLongPtr64(hWnd, nIndex);
            else
                return (IntPtr)UnsafeNativeMethods.GetWindowLong32(hWnd, nIndex);
        }

        static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
                return UnsafeNativeMethods.GetClassLongPtr64(hWnd, nIndex);
            else
                return (IntPtr)UnsafeNativeMethods.GetClassLongPtr32(hWnd, nIndex);
        }

        static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size > 4)
                return UnsafeNativeMethods.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return (IntPtr)UnsafeNativeMethods.SetWindowLong32(hWnd, nIndex, (int)dwNewLong);
        }

        /// <summary>
        /// Delegate for the EnumChildWindows method
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
        /// <returns>True to continue enumerating, false to stop.</returns>
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr parameter);

        static Color ColorFromReference(int colorref)
        {
            int red = colorref & 0xff;
            int green = (colorref >> 8) & 0xff;
            int blue = (colorref >> 0x10) & 0xff;
            int a = (colorref >> 18) & 0xff;
            return Color.FromArgb(a, red, green, blue);
        }
        static int ColorToReference(Color color)
        {
            return ((color.A << 0x18) | color.R | (color.G << 8) | (color.B << 0x10));
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Enumerates all top-level windows on the screen by passing the handle to each window, in turn, 
        /// to an application-defined callback function. EnumWindows continues until the last top-level window is enumerated 
        /// or the callback function returns FALSE. For details see EnumWindows in MSDN.
        /// </summary>
        /// <param name="proc">Delegate to use.</param>
        /// <param name="lParam">Specifies an application-defined value to be passed to the callback function.</param>
        internal static bool EnumWindows(EnumWindowsProc proc, IntPtr lParam)
        {
            return UnsafeNativeMethods.EnumWindows_(proc, lParam);
        }

        /// <summary>
        /// Enumerates all nonchild windows associated with the specified thread by passing the handle to each window, 
        /// in turn, to an application-defined callback function. Continues until the last window is enumerated 
        /// or the callback function returns FALSE. For details see EnumThreadWindows in MSDN.
        /// </summary>
        /// <param name="threadId">ThreadId to list windows from.</param>
        /// <param name="proc">Delegate to use.</param>
        /// <param name="lParam">Specifies an application-defined value to be passed to the callback function.</param>
        internal static bool EnumThreadWindows(uint threadId, EnumWindowsProc proc, IntPtr lParam)
        {
            return UnsafeNativeMethods.EnumThreadWindows_(threadId, proc, lParam);
        }

        /// <summary>
        /// Callback to enumerate Child Windows
        /// </summary>
        /// <param name="handle">handle</param>
        /// <param name="pointer">pointer</param>
        internal static bool EnumChildrenProc(IntPtr handle, IntPtr pointer)
        {
            var gch = GCHandle.FromIntPtr(pointer);
            var list = gch.Target as List<Win32Window>;
            if (list == null)
                return false;
            list.Add(new Win32Window(handle));
            return true;
        }

        /// <summary>
        /// Enumerates the child windows that belong to this window by passing the handle to each child window, 
        /// in turn, to an application-defined callback function. Continues until the last child window is enumerated 
        /// or the callback function returns false. For details see EnumChildWindows in MSDN.
        /// </summary>
        /// <param name="proc">Delegate to use.</param>
        /// <param name="lParam">Specifies an application-defined value to be passed to the callback function.</param>
        internal bool EnumChildWindows(EnumWindowsProc proc, IntPtr lParam)
        {
            return UnsafeNativeMethods.EnumChildWindows(Handle, proc, lParam);
        }

        /// <summary>
        /// Enumerates all nonchild windows associated with the current thread by passing the handle to each window, 
        /// in turn, to an application-defined callback function. Continues until the last window is enumerated 
        /// or the callback function returns FALSE. For details see EnumThreadWindows in MSDN.
        /// </summary>
        /// <param name="proc">Delegate to use.</param>
        /// <param name="lParam">Specifies an application-defined value to be passed to the callback function.</param>
        internal bool EnumThreadWindows(EnumWindowsProc proc, IntPtr lParam)
        {
            return UnsafeNativeMethods.EnumThreadWindows_(ThreadId, proc, lParam);
        }

        /// <summary>
        /// EnumModalProc
        /// </summary>
        /// <param name="handle">Handle.</param>
        /// <param name="pointer">Pointer</param>
        /// <returns>Some bool valiue.</returns>
        private bool EnumModalProc(IntPtr handle, IntPtr pointer)
        {
            if (handle == Handle)
                return true;
            Win32Window cWin = new Win32Window(handle);
            var styles = cWin.Styles;
            if (!styles.Check(Win32WindowStyles.WS_VISIBLE))
                return true;
            var res = GCHandle.FromIntPtr(pointer).Target as Win32Window;
            if (!styles.Check(Win32WindowStyles.WS_DISABLED))
            {
                if (res != null) res.Handle = IntPtr.Zero;
                return false;
            }
            if (res != null) res.Handle = Handle;
            return true;
        }

        #endregion
    }
}