using System;
using System.Runtime.InteropServices;

namespace Neitzel.Win32
{
    /// <summary>
    /// Defines safe (public) native Win32 helper methods.
    /// </summary>
    /// <remarks>
    /// The internal native methods should never be exposed or referenced by dependent assemblies.
    /// Internal unsafe methods are defined in the <see cref="UnsafeNativeMethods"/> class and not externally visible.
    /// Safe externally visible methods are defined here which call the internal methods and deal with
    /// translation of inputs, outputs and errors.
    /// </remarks>
    public static class SafeNativeMethods
    {
        #region Kernel32.dll

        /// <summary>
        /// Gets the message text for a Win32 return code.
        /// </summary>
        /// <param name="returnCode">Return code which has just been received via the <see cref="Marshal.GetLastWin32Error()"/> method.</param>
        /// <returns>Message text or null when not found.</returns>
        public static string FormatMessage(int returnCode)
        {
            string messageText;
            UnsafeNativeMethods.FormatMessage((uint)(
                Win32FormatMessageOptions.AllocateBuffer |
                Win32FormatMessageOptions.MessageFromSystem |
                Win32FormatMessageOptions.MessageIgnoreInserts),
                IntPtr.Zero, (uint)returnCode, 0, out messageText, 0, IntPtr.Zero);
            return messageText;
        }

        #endregion
    }
}
