using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Neitzel.Win32
{
    /// <summary>
    /// Defines unsafe internal Win32 native methods.
    /// </summary>
    /// <remarks>
    /// Do not make methods here public, instead leave internal then add 
    /// a safe wrapper method to the <see cref="SafeNativeMethods"/> class.
    /// </remarks>
    internal static class UnsafeNativeMethods
    {
        
        #region AdvApi32.dll

        /// <summary>
        /// Initiates a shutdown and optional restart of the specified computer, and optionally records the reason for the shutdown.
        /// </summary>
        /// <param name="machineName">The network name of the computer to be shut down. If lpMachineName is NULL or an empty string, the function shuts down the local computer.</param>
        /// <param name="message">The message to be displayed in the shutdown dialog box. This parameter can be NULL if no message is required. </param>
        /// <param name="timeout">
        /// The length of time that the shutdown dialog box should be displayed, in seconds. While this dialog box is displayed, shutdown can be stopped by the AbortSystemShutdown function. 
        /// If dwTimeout is not zero, InitiateSystemShutdownEx displays a dialog box on the specified computer. The dialog box displays the name of the user who called the function, displays the message specified by the lpMessage parameter, and prompts the user to log off. The dialog box beeps when it is created and remains on top of other windows in the system. The dialog box can be moved but not closed. A timer counts down the remaining time before shutdown.
        /// If dwTimeout is zero, the computer shuts down without displaying the dialog box, and the shutdown cannot be stopped by AbortSystemShutdown.
        /// </param>
        /// <param name="forceAppsClosed">If this parameter is TRUE, applications with unsaved changes are to be forcibly closed. If this parameter is FALSE, the system displays a dialog box instructing the user to close the applications.</param>
        /// <param name="rebootAfterShutdown">If this parameter is TRUE, the computer is to restart immediately after shutting down. If this parameter is FALSE, the system flushes all caches to disk and safely powers down the system.</param>
        /// <param name="reason">The reason for initiating the shutdown. This parameter must be one of the system shutdown reason codes.
        /// If this parameter is zero, the default is an undefined shutdown that is logged as "No title for this reason could be found". By default, it is also an unplanned shutdown. Depending on how the system is configured, an unplanned shutdown triggers the creation of a file that contains the system state information, which can delay shutdown. Therefore, do not use zero for this parameter.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool InitiateSystemShutdownEx(string machineName, string message, uint timeout,
            [MarshalAs(UnmanagedType.Bool)] bool forceAppsClosed,
            [MarshalAs(UnmanagedType.Bool)] bool rebootAfterShutdown,
            uint reason);

        #endregion

        #region Gdi32.dll

        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll")]
        internal static extern int GetRgnBox(IntPtr hrgn, out Win32Rectangle lprc);

        #endregion

        #region Kernel32.dll

        /// <summary>
        /// Retrieves a pseudo handle for the current process.
        /// </summary>
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetCurrentProcess();

        /// <summary>
        /// Retrieves the thread identifier of the calling thread.
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int GetCurrentThreadId();

        /// <summary>
        /// Duplicates an object handle.
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DuplicateHandle(SafeHandle hSourceProcessHandle, SafeHandle hSourceHandle, SafeHandle hTargetProcessHandle, ref IntPtr lpTargetHandle, uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);

        /// <summary>
        /// Opens an existing local process object. (Kernel32.dll)
        /// </summary>
        /// <param name="dwDesiredAccess">
        ///   The access to the process object. This access right is checked against the security descriptor for the process. This parameter can be one or more of the process access rights.
        ///   If the caller has enabled the SeDebugPrivilege privilege, the requested access is granted regardless of the contents of the security descriptor.
        /// </param>
        /// <param name="bInheritHandle">If this value is TRUE, processes created by this process will inherit the handle. Otherwise, the processes do not inherit this handle.</param>
        /// <param name="dwProcessId">The identifier of the local process to be opened.</param>
        /// <returns>
        /// If the function succeeds, the return value is an open handle to the specified process.
        /// If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// To open a handle to another local process and obtain full access rights, you must enable the SeDebugPrivilege privilege. For more information, see Changing Privileges in a Token.
        /// The handle returned by the OpenProcess function can be used in any function that requires a handle to a process, such as the wait functions, provided the appropriate access rights were requested.
        /// When you are finished with the handle, be sure to close it using the CloseHandle function.
        /// </remarks>
        [DllImport("kernel32.dll")]
        internal static extern IntPtr OpenProcess(Win32ProcessAccessType dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

        /// <summary>
        /// Reserves or commits a region of memory within the virtual address space of a specified process. The function initializes the memory it allocates to zero, unless MEM_RESET is used.
        /// </summary>
        /// <param name="hProcess">
        /// The handle to a process. The function allocates memory within the virtual address space of this process. 
        /// The handle must have the PROCESS_VM_OPERATION access right. For more information, see Process Security and Access Rights.
        /// </param>
        /// <param name="lpAddress">
        /// The pointer that specifies a desired starting address for the region of pages that you want to allocate. 
        /// If you are reserving memory, the function rounds this address down to the nearest multiple of the allocation granularity.
        /// If you are committing memory that is already reserved, the function rounds this address down to the nearest page boundary. To determine the size of a page and the allocation granularity on the host computer, use the GetSystemInfo function.
        /// If lpAddress is NULL, the function determines where to allocate the region.
        /// </param>
        /// <param name="dwSize">
        /// The size of the region of memory to allocate, in bytes. 
        /// If lpAddress is NULL, the function rounds dwSize up to the next page boundary.
        /// If lpAddress is not NULL, the function allocates all pages that contain one or more bytes in the range from lpAddress to lpAddress+dwSize. This means, for example, that a 2-byte range that straddles a page boundary causes the function to allocate both pages.
        /// </param>
        /// <param name="flWin32AllocationType">The type of memory allocation. </param>
        /// <param name="flProtect">
        /// The memory protection for the region of pages to be allocated. If the pages are being committed, you can specify any one of the memory protection constants.
        /// Protection attributes specified when protecting a page cannot conflict with those specified when allocating a page.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the base address of the allocated region of pages.
        /// If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// Each page has an associated page state. The VirtualAllocEx function can perform the following operations:
        /// - Commit a region of reserved pages 
        /// - Reserve a region of free pages 
        /// - Simultaneously reserve and commit a region of free pages 
        /// VirtualAllocEx cannot reserve a reserved page. It can commit a page that is already committed. This means you can commit a range of pages, regardless of whether they have already been committed, and the function will not fail.
        /// You can use VirtualAllocEx to reserve a block of pages and then make additional calls to VirtualAllocEx to commit individual pages from the reserved block. This enables a process to reserve a range of its virtual address space without consuming physical storage until it is needed.
        /// If the lpAddress parameter is not NULL, the function uses the lpAddress and dwSize parameters to compute the region of pages to be allocated. The current state of the entire range of pages must be compatible with the type of allocation specified by the flAllocationType parameter. Otherwise, the function fails and none of the pages is allocated. This compatibility requirement does not preclude committing an already committed page; see the preceding list.
        /// To execute dynamically generated code, use VirtualAllocEx to allocate memory and the VirtualProtectEx function to grant PAGE_EXECUTE access.
        /// The VirtualAllocEx function can be used to reserve an Address Windowing Extensions (AWE) region of memory within the virtual address space of a specified process. This region of memory can then be used to map physical pages into and out of virtual memory as required by the application. The MEM_PHYSICAL and MEM_RESERVE values must be set in the AllocationType parameter. The MEM_COMMIT value must not be set. The page protection must be set to PAGE_READWRITE.
        /// The VirtualFreeEx function can decommit a committed page, releasing the page's storage, or it can simultaneously decommit and release a committed page. It can also release a reserved page, making it a free page.
        /// </remarks>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, [MarshalAs(UnmanagedType.SysInt)] int dwSize, Win32MemoryAllocation flWin32AllocationType, Win32MemoryProtection flProtect);

        /// <summary>
        /// Reads data from an area of memory in a specified process. The entire area to be read must be accessible or the operation fails.
        /// </summary>
        /// <param name="hProcess">A handle to the process with memory that is being read. The handle must have PROCESS_VM_READ access to the process.</param>
        /// <param name="lpBaseAddress">A pointer to the base address in the specified process from which to read. Before any data transfer occurs, the system verifies that all data in the base address and memory of the specified size is accessible for read access, and if it is not accessible the function fails.</param>
        /// <param name="lpBuffer">A pointer to a buffer that receives the contents from the address space of the specified process.</param>
        /// <param name="dwSize">The number of bytes to be read from the specified process.</param>
        /// <param name="lpNumberOfBytesRead">A pointer to a variable that receives the number of bytes transferred into the specified buffer. If lpNumberOfBytesRead is NULL, the parameter is ignored.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is 0 (zero). To get extended error information, call GetLastError.
        /// The function fails if the requested read operation crosses into an area of the process that is inaccessible.
        /// </returns>
        /// <remarks>
        /// ReadProcessMemory copies the data in the specified address range from the address space of the specified process into the specified buffer of the current process. Any process that has a handle with PROCESS_VM_READ access can call the function. Typically but not always, the process with address space that is being written to is being debugged.
        /// The entire area to be read must be accessible, and if it is not accessible, the function fails.
        /// </remarks>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, IntPtr dwSize, out IntPtr lpNumberOfBytesRead);

        /// <summary>
        /// Writes data to an area of memory in a specified process. The entire area to be written to must be accessible or the operation fails.
        /// </summary>
        /// <param name="hProcess">A handle to the process memory to be modified. The handle must have PROCESS_VM_WRITE and PROCESS_VM_OPERATION access to the process.</param>
        /// <param name="lpBaseAddress">A pointer to the base address in the specified process to which data is written. Before data transfer occurs, the system verifies that all data in the base address and memory of the specified size is accessible for write access, and if it is not accessible, the function fails.</param>
        /// <param name="lpBuffer">A pointer to the buffer that contains data to be written in the address space of the specified process.</param>
        /// <param name="nSize">The number of bytes to be written to the specified process.</param>
        /// <param name="lpNumberOfBytesWritten">A pointer to a variable that receives the number of bytes transferred into the specified process. This parameter is optional. If lpNumberOfBytesWritten is NULL, the parameter is ignored.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is 0 (zero). To get extended error information, call GetLastError. The function fails if the requested write operation crosses into an area of the process that is inaccessible.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref Win32ListViewItemData lpBuffer, [MarshalAs(UnmanagedType.SysInt)] int nSize, out IntPtr lpNumberOfBytesWritten);

        /// <summary>
        /// Releases, decommits, or releases and decommits a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="hProcess">
        /// A handle to a process. The function frees memory within the virtual address space of the process. 
        /// The handle must have the PROCESS_VM_OPERATION access right. For more information, see Process Security and Access Rights.
        /// </param>
        /// <param name="lpAddress">
        /// A pointer to the starting address of the region of memory to be freed. 
        /// If the dwFreeType parameter is MEM_RELEASE, lpAddress must be the base address returned by the VirtualAllocEx function when the region is reserved.
        /// </param>
        /// <param name="dwSize">
        /// The size of the region of memory to free, in bytes.
        /// If the dwFreeType parameter is MEM_RELEASE, dwSize must be 0 (zero). The function frees the entire region that is reserved in the initial allocation call to VirtualAllocEx.
        /// If dwFreeType is MEM_DECOMMIT, the function decommits all memory pages that contain one or more bytes in the range from the lpAddress parameter to (lpAddress+dwSize). This means, for example, that a 2-byte region of memory that straddles a page boundary causes both pages to be decommitted. If lpAddress is the base address returned by VirtualAllocEx and dwSize is 0 (zero), the function decommits the entire region that is allocated by VirtualAllocEx. After that, the entire region is in the reserved state.
        /// </param>
        /// <param name="dwFreeType">
        /// The type of free operation.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a nonzero value.
        /// If the function fails, the return value is 0 (zero). To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, [MarshalAs(UnmanagedType.SysInt)] int dwSize, Win32MemoryAllocation dwFreeType);

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="hObject">A valid handle to an open object.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// If the application is running under a debugger, the function will throw an exception if it receives either a handle value that is not valid or a pseudo-handle value. This can happen if you close a handle twice, or if you call CloseHandle on a handle returned by the FindFirstFile function instead of calling the FindClose function.
        /// </returns>
        /// <remarks>
        /// The CloseHandle function closes handles to the following objects:
        /// - Access token 
        /// - Communications device 
        /// - Console input 
        /// - Console screen buffer 
        /// - Event 
        /// - File 
        /// - File mapping 
        /// - I/O completion port 
        /// - Job 
        /// - Mailslot 
        /// - Memory resource notification 
        /// - Mutex 
        /// - Named pipe 
        /// - Pipe 
        /// - Process 
        /// - Semaphore 
        /// - Socket 
        /// - Thread 
        /// - Transaction 
        /// - Waitable timer 
        /// The documentation for the functions that create these objects indicates that CloseHandle should be used when you are finished with the object, and what happens to pending operations on the object after the handle is closed. In general, CloseHandle invalidates the specified object handle, decrements the object's handle count, and performs object retention checks. After the last handle to an object is closed, the object is removed from the system. For a summary of the creator functions for these objects, see Kernel Objects.
        /// Generally, an application should call CloseHandle once for each handle it opens. It is usually not necessary to call CloseHandle if a function that uses a handle fails with ERROR_INVALID_HANDLE, because this error usually indicates that the handle is already invalidated. However, some functions use ERROR_INVALID_HANDLE to indicate that the object itself is no longer valid. For example, a function that attempts to use a handle to a file on a network might fail with ERROR_INVALID_HANDLE if the network connection is severed, because the file object is no longer available. In this case, the application should close the handle.
        /// If a handle is transacted, all handles bound to a transaction should be closed before the transaction is committed. If a transacted handle was opened by calling CreateFileTransacted with the FILE_FLAG_DELETE_ON_CLOSE flag, the file is not deleted until the application closes the handle and calls CommitTransaction. For more information about transacted objects, see Working With Transactions.
        /// Closing a thread handle does not terminate the associated thread or remove the thread object. Closing a process handle does not terminate the associated process or remove the proces object. To remove a thread object, you must terminate the thread, then close all handles to the thread. For more information, see Terminating a Thread. To remove a process object, you must terminate the process, then close all handles to the process. For more information, see Terminating a Process.
        /// Closing a handle to a file mapping can succeed even when there are file views that are still open. For more information, see Closing a File Mapping Object.
        /// After closing a socket using the CloseSocket function, ensure that you release the socket by calling CloseHandle. For more information, see Socket Closure.
        /// </remarks>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// Formats a message string. The function requires a message definition as input. The message definition can come from a buffer passed into the function. It can come from a message table resource in an already-loaded module. Or the caller can ask the function to search the system's message table resource(s) for the message definition. The function finds the message definition in a message table resource based on a message identifier and a language identifier. The function copies the formatted message text to an output buffer, processing any embedded insert sequences if requested.
        /// </summary>
        /// <param name="dwFlags">The formatting options, and how to interpret the lpSource parameter. The low-order byte of dwFlags specifies how the function handles line breaks in the output buffer. The low-order byte can also specify the maximum width of a formatted output line.</param>
        /// <param name="lpSource">The location of the message definition. The type of this parameter depends upon the settings in the dwFlags parameter. </param>
        /// <param name="dwMessageId">The message identifier for the requested message. This parameter is ignored if dwFlags includes FORMAT_MESSAGE_FROM_STRING.</param>
        /// <param name="dwLanguageId">The language identifier for the requested message. This parameter is ignored if dwFlags includes FORMAT_MESSAGE_FROM_STRING.</param>
        /// <param name="lpBuffer">
        /// A pointer to a buffer that receives the null-terminated string that specifies the formatted message. If dwFlags includes FORMAT_MESSAGE_ALLOCATE_BUFFER, the function allocates a buffer using the LocalAlloc function, and places the pointer to the buffer at the address specified in lpBuffer.
        /// This buffer cannot be larger than 64K bytes.
        /// </param>
        /// <param name="nSize">If the FORMAT_MESSAGE_ALLOCATE_BUFFER flag is not set, this parameter specifies the size of the output buffer, in TCHARs. If FORMAT_MESSAGE_ALLOCATE_BUFFER is set, this parameter specifies the minimum number of TCHARs to allocate for an output buffer.</param>
        /// <param name="argumentsLong">
        /// An array of values that are used as insert values in the formatted message. A %1 in the format string indicates the first value in the Arguments array; a %2 indicates the second argument; and so on. 
        /// The interpretation of each value depends on the formatting information associated with the insert in the message definition. The default is to treat each value as a pointer to a null-terminated string.
        /// By default, the Arguments parameter is of type va_list*, which is a language- and implementation-specific data type for describing a variable number of arguments. The state of the va_list argument is undefined upon return from the function. To use the va_list again, destroy the variable argument list pointer using va_end and reinitialize it with va_start.
        /// If you do not have a pointer of type va_list*, then specify the FORMAT_MESSAGE_ARGUMENT_ARRAY flag and pass a pointer to an array of DWORD_PTR values; those values are input to the message formatted as the insert values. Each insert must have a corresponding element in the array.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the number of TCHARs stored in the output buffer, excluding the terminating null character.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern UInt32 FormatMessage(uint dwFlags, IntPtr lpSource, uint dwMessageId,
            uint dwLanguageId, out string lpBuffer, uint nSize, IntPtr argumentsLong);

        /// <summary>
        /// LocalFree method of kernel32.dll.
        /// </summary>
        /// <param name="hMem">Pointer to memory to free.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        internal static extern IntPtr LocalFree(IntPtr hMem);

        #endregion
        
        /*
        #region NetApi32.dll

        /// <summary>
        /// The NetWkstaGetInfo function returns information about the configuration of a workstation.
        /// </summary>
        /// <param name="servername">Pointer to a string that specifies the DNS or NetBIOS name of the remote server on which the function is to execute. If this parameter is NULL, the local computer is used.</param>
        /// <param name="level">Specifies the information level of the data.</param>
        /// <param name="bufptr">Pointer to the buffer that receives the data. The format of this data depends on the value of the level parameter. This buffer is allocated by the system and must be freed using the NetApiBufferFree function. For more information, see Network Management Function Buffers and Network Management Function Buffer Lengths.</param>
        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode)]
        static internal extern unsafe int NetWkstaGetInfo([MarshalAs(UnmanagedType.LPWStr)] string servername, int level, byte** bufptr);

        /// <summary>
        /// The NetApiBufferFree function frees the memory that the NetApiBufferAllocate function allocates. Call NetApiBufferFree to free the memory that other network management functions return.
        /// </summary>
        /// <returns>Pointer to a buffer returned previously by another network management function.</returns>
        [DllImport("Netapi32.dll")]
        static internal extern unsafe int NetApiBufferFree(byte* bufptr);

        #endregion
        */

        #region User32.dll

        [DllImport("user32.dll")]
        internal static extern IntPtr BeginDeferWindowPos(int nNumWindows);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseDesktop(IntPtr hDesktop);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseWindowStation(IntPtr hWinsta);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetProcessWindowStation();

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetThreadDesktop(int dwThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EndDeferWindowPos(IntPtr hWinPosInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumChildWindows(IntPtr hwndParent, Win32Window.EnumWindowsProc lpEnumFunc,
            IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "EnumWindows")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumWindows_(Win32Window.EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "EnumThreadWindows")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumThreadWindows_(uint dwThreadId, Win32Window.EnumWindowsProc lpfn, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint nullValue);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetFocus();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr SetCapture(IntPtr hwnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowUnicode(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UpdateWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RedrawWindow(IntPtr hwnd, IntPtr rcUpdate, IntPtr hrgnUpdate,
            Win32RedrawOptions flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool InvalidateRect(IntPtr hWnd, IntPtr rect, [MarshalAs(UnmanagedType.Bool)] bool erase);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, Win32ShowWindow nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, ref Win32MinMaxInfo lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr DeferWindowPos(IntPtr hWinPosInfo, IntPtr hWnd,
            Win32WindowPositionAfter hWndInsertAfter, int x, int y, int cx, int cy, Win32WindowPositionOptions uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetMenu(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsMenu(IntPtr hMenu);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetMenu(IntPtr hWnd, IntPtr hMenu);

        [DllImport("user32.dll", SetLastError = false)]
        internal static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern IntPtr DeferWindowPos(IntPtr hWinPosInfo, IntPtr hWnd, IntPtr hWndInsertAfter, int x,
            int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll", EntryPoint = "ChildWindowFromPointEx", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr ChildWindowFromPointEx(IntPtr hwndParent, Win32Point pt, int uFlags);

        [DllImport("user32.dll")]
        internal static extern IntPtr WindowFromPoint(Win32Point point);

        [DllImport("user32.dll")]
        internal static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bRedraw);

        [DllImport("user32.dll")]
        internal static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        /// <summary>
        /// The GetDC function retrieves a handle to a device context (DC) for the client area of a specified window or
        /// for the entire screen. You can use the returned handle in subsequent GDI functions to draw in the DC.
        /// The device context is an opaque data structure, whose values are used internally by GDI. 
        /// The GetDCEx function is an extension to GetDC, which gives an application more control over how and
        /// whether clipping occurs in the client area. 
        /// </summary>
        /// <param name="hwnd">A handle to the window whose DC is to be retrieved. If this value is NULL, GetDC retrieves the DC for the entire screen.</param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the DC for the specified window's client area.
        /// If the function fails, the return value is NULL.
        /// </returns>
        /// <remarks>
        /// The GetDC function retrieves a common, class, or private DC depending on the class style of the specified window.
        /// For class and private DCs, GetDC leaves the previously assigned attributes unchanged. 
        /// However, for common DCs, GetDC assigns default attributes to the DC each time it is retrieved.
        /// For example, the default font is System, which is a bitmap font. Because of this,
        /// the handle to a common DC returned by GetDC does not tell you what font, color, 
        /// or brush was used when the window was drawn. To determine the font, call GetTextFace. 
        /// Note that the handle to the DC can only be used by a single thread at any one time.
        /// After painting with a common DC, the ReleaseDC function must be called to release the DC.
        /// Class and private DCs do not have to be released. ReleaseDC must be called from the s
        /// ame thread that called GetDC. The number of DCs is limited only by available memory. 
        /// </remarks>
        [DllImport("user32.dll", EntryPoint = "GetDC")]
        internal static extern IntPtr GetDC(IntPtr hwnd);

        /// <summary>
        /// The ReleaseDC function releases a device context (DC), freeing it for use by other applications.
        /// The effect of the ReleaseDC function depends on the type of DC. It frees only common and window DCs.
        /// It has no effect on class or private DCs.
        /// </summary>
        /// <param name="hwnd">A handle to the window whose DC is to be released.</param>
        /// <param name="hdc">A handle to the DC to be released.</param>
        /// <returns>
        /// The return value indicates whether the DC was released. If the DC was released, the return value is 1.    
        /// If the DC was not released, the return value is zero.
        /// </returns>
        /// <remarks>
        /// The application must call the ReleaseDC function for each call to the GetWindowDC function and
        /// for each call to the GetDC function that retrieves a common DC. 
        /// An application cannot use the ReleaseDC function to release a DC that was created by calling the CreateDC function;
        /// instead, it must use the DeleteDC function. ReleaseDC must be called from the same thread that called GetDC. 
        /// </remarks>
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        internal static extern UInt32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsHungAppWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        internal static extern int GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        internal static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        internal static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        internal static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        internal static extern uint RealGetWindowClass(IntPtr hwnd, [Out] StringBuilder pszType, uint cchType);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetLayeredWindowAttributes(IntPtr hwnd, out int crKey, out byte bAlpha,
            out int dwFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);

        [DllImport("user32.dll")]
        internal static extern uint GetDlgItemText(IntPtr hDlg, int nIdDlgItem, [Out] StringBuilder lpString,
            int nMaxCount);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendDlgItemMessage(IntPtr hDlg, int nIdDlgItem, uint msg, UIntPtr wParam,
            IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendDlgItemMessage(IntPtr hDlg, int nIdDlgItem, uint msg, UIntPtr wParam,
            StringBuilder lParam);

        [DllImport("user32.dll")]
        internal static extern int GetDlgCtrlID(IntPtr hwndCtl);

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        internal static extern int GetClassLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        internal static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SystemParametersInfo(int uiAction, int uiParam, ref Win32AnimationInfo pvParam,
            int fWinIni);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "GetTopWindow", CharSet = CharSet.Ansi, SetLastError = true,
            ExactSpelling = true)]
        internal static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hWnd, out Win32Rectangle lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetClientRect(IntPtr hWnd, out Win32Rectangle lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hWnd, Win32WindowPositionAfter hWndInsertAfter, int x, int y,
            int cx, int cy, Win32WindowPositionOptions flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ClientToScreen(IntPtr hWnd, ref Win32Point pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ScreenToClient(IntPtr hWnd, ref Win32Point pt);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int MapWindowPoints(IntPtr hwndFrom, IntPtr hwndTo, ref Win32Rectangle lpPoints,
            int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern int GetWindowPlacement(IntPtr hWnd, ref Win32WindowPlacement placement);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref Win32WindowPlacement placement);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AdjustWindowRect(ref Win32Rectangle lpRect, int dwStyle,
            [MarshalAs(UnmanagedType.Bool)] bool bMenu);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnableWindow(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool enable);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetAncestor(IntPtr hwnd, Win32WindowAncestor gaFlags);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        internal static extern IntPtr FindWindow_(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
            string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowEnabled(IntPtr hWnd);

        #endregion

        #region gdi32

        /// <summary>
        /// The SelectObject function selects an object into the specified device context (DC).
        /// The new object replaces the previous object of the same type.
        /// </summary>
        /// <param name="hdc">A handle to the DC.</param>
        /// <param name="hgdiobj">A handle to the object to be selected.</param>
        /// <returns>
        /// If the selected object is not a region and the function succeeds, the return value is a handle to the object being replaced.
        /// If an error occurs and the selected object is not a region, the return value is NULL. Otherwise, it is HGDI_ERROR.
        /// </returns>
        /// <remarks>
        /// This function returns the previously selected object of the specified type.
        /// An application should always replace a new object with the original, 
        /// default object after it has finished drawing with the new object.
        /// An application cannot select a single bitmap into more than one DC at a time.
        /// ICM: If the object being selected is a brush or a pen, color management is performed.
        /// </remarks>
        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        /// <summary>
        /// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette,
        /// freeing all system resources associated with the object. After the object is deleted,
        /// the specified handle is no longer valid.
        /// </summary>
        /// <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the specified handle is not valid or is currently selected into a DC, the return value is zero.
        /// </returns>
        /// <remarks>
        /// Do not delete a drawing object (pen or brush) while it is still selected into a DC.
        /// When a pattern brush is deleted, the bitmap associated with the brush is not deleted. The bitmap must be deleted independently.
        /// </remarks>
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);

        #endregion

    }
}
