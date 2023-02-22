using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using static System.Net.WebRequestMethods;

namespace Neitzel.Forms
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// ClipboardMonitor
    /// <see href="https://stackoverflow.com/a/1394225/11484412" />
    /// </summary>
    /// <remarks>
    /// Must inherit Control, not Component, in order to have Handle
    /// </remarks>
    [DefaultEvent("ClipboardChanged")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed. Suppression is OK here.")]
    public partial class ClipboardMonitor : Control
    {
        /// <summary>
        /// WM_DRAWCLIPBOARD value of winuser.h
        /// </summary>
        public const int WM_DRAWCLIPBOARD = 0x308;

        /// <summary>
        /// WM_CHANGECBCHAIN  value of winuser.h
        /// </summary>
        public const int WM_CHANGECBCHAIN = 0x030D;

        /// <summary>
        /// Next clipboard viewer
        /// </summary>
        private IntPtr nextClipboardViewer;

        /// <summary>
        /// Creates a new instance of <see cref="ClipboardMonitor"/>.
        /// </summary>
        public ClipboardMonitor()
        {
            this.BackColor = Color.Red;
            this.Visible = false;

            this.nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
        }

        /// <summary>
        /// Clipboard contents changed.
        /// </summary>
        public event EventHandler<ClipboardChangedEventArgs> ClipboardChanged;

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        /// <param name="disposing">Set to true when calling from close, set to false when calling from finalize Method.</param>
        protected override void Dispose(bool disposing)
        {
            ChangeClipboardChain(this.Handle, nextClipboardViewer);
            base.Dispose(disposing);
        }

        /// <summary>
        /// SetClipbloardViewer function of User32.
        /// </summary>
        /// <param name="hWndNewViewer"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWndRemove"></param>
        /// <param name="hWndNewNext"></param>
        /// <returns></returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    this.OnClipboardChanged();
                    SendMessage(this.nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == this.nextClipboardViewer)
                        this.nextClipboardViewer = m.LParam;
                    else
                        SendMessage(this.nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void OnClipboardChanged()
        {
            try
            {
                IDataObject iData = Clipboard.GetDataObject();
                if (this.ClipboardChanged != null)
                {
                    this.ClipboardChanged(this, new ClipboardChangedEventArgs(iData));
                }

            }
            catch (Exception e)
            {
                // Swallow or pop-up, not sure
                // Trace.Write(e.ToString());
                MessageBox.Show(e.ToString());
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ClipboardChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly IDataObject DataObject;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataObject"></param>
        public ClipboardChangedEventArgs(IDataObject dataObject)
        {
            this.DataObject = dataObject;
        }
    }
}
