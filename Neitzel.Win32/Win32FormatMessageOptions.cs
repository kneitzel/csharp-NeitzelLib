using System;

namespace Neitzel.Win32
{
    /// <summary>
    /// Options for the Win32 FormatMessage() method.
    /// </summary>
    [Flags, CLSCompliant(false)]
    public enum Win32FormatMessageOptions : uint
    {
        /// <summary>
        /// The lpBuffer parameter is a pointer to a PVOID pointer, and that the nSize parameter specifies the minimum number of TCHARs to allocate for an output message buffer. The function allocates a buffer large enough to hold the formatted message, and places a pointer to the allocated buffer at the address specified by lpBuffer. The caller should use the LocalFree function to free the buffer when it is no longer needed.
        /// </summary>
        AllocateBuffer = 0x100,

        /// <summary>
        /// The Arguments parameter is not a va_list structure, but is a pointer to an array of values that represent the arguments.
        /// This flag cannot be used with 64-bit integer values. If you are using a 64-bit integer, you must use the va_list structure.
        /// </summary>
        ArgumentArray = 0x2000,

        /// <summary>
        /// The lpSource parameter is a module handle containing the message-table resource(s) to search. If this lpSource handle is NULL, the current process's application image file will be searched. This flag cannot be used with FORMAT_MESSAGE_FROM_STRING.
        /// If the module has no message table resource, the function fails with ERROR_RESOURCE_TYPE_NOT_FOUND.
        /// </summary>
        MessageFromModuleHandle = 0x800,

        /// <summary>
        /// The lpSource parameter is a pointer to a null-terminated string that contains a message definition. The message definition may contain insert sequences, just as the message text in a message table resource may. This flag cannot be used with FORMAT_MESSAGE_FROM_HMODULE or FORMAT_MESSAGE_FROM_SYSTEM.
        /// </summary>
        MessageFromString = 0x400,

        /// <summary>
        /// The function should search the system message-table resource(s) for the requested message. If this flag is specified with FORMAT_MESSAGE_FROM_HMODULE, the function searches the system message table if the message is not found in the module specified by lpSource. This flag cannot be used with FORMAT_MESSAGE_FROM_STRING. 
        /// If this flag is specified, an application can pass the result of the GetLastError function to retrieve the message text for a system-defined error.
        /// </summary>
        MessageFromSystem = 0x1000,

        /// <summary>
        /// Insert sequences in the message definition are to be ignored and passed through to the output buffer unchanged. This flag is useful for fetching a message for later formatting. If this flag is set, the Arguments parameter is ignored.
        /// </summary>
        MessageIgnoreInserts = 0x200,

        /// <summary>
        /// The function ignores regular line breaks in the message definition text. The function stores hard-coded line breaks in the message definition text into the output buffer. The function generates no new line breaks.
        /// </summary>
        MessageMaxWidthMask = 0xFF
    }
}
