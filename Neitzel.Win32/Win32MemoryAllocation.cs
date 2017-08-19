using System;

namespace Neitzel.Win32
{
    /// <summary>
    /// AllocationType
    /// </summary>
    [Flags]
    public enum Win32MemoryAllocation
    {
        /// <summary>
        /// MEM_COMMIT
        /// </summary>
        /// <remarks>
        /// Allocates physical storage in memory or in the paging file on disk for the specified reserved memory pages. The function initializes the memory to zero. 
        /// To reserve and commit pages in one step, call VirtualAllocEx with MEM_COMMIT | MEM_RESERVE.
        /// The function fails if you attempt to commit a page that has not been reserved. The resulting error code is ERROR_INVALID_ADDRESS.
        /// An attempt to commit a page that is already committed does not cause the function to fail. This means that you can commit pages without first determining the current commitment state of each page.
        /// </remarks>
        Commit = 0x1000,
        
        /// <summary>
        /// MEM_RESERVE
        /// </summary>
        /// <remarks>
        /// Reserves a range of the process's virtual address space without allocating any actual physical storage in memory or in the paging file on disk. 
        /// You commit reserved pages by calling VirtualAllocEx again with MEM_COMMIT. To reserve and commit pages in one step, call VirtualAllocEx with MEM_COMMIT |MEM_RESERVE.
        /// Other memory allocation functions, such as "malloc()" and LocalAlloc, cannot use reserved memory until it has been released.
        /// </remarks>
        Reserve = 0x2000,

        /// <summary>
        /// De-commits the specified region of committed pages. After the operation, the pages are in the reserved state. 
        /// The function does not fail if you attempt to de-commit an uncommitted page. This means that you can de-commit a range of pages without first determining their current commitment state.
        /// Do not use this value with MEM_RELEASE.
        /// </summary>
        Decommit = 0x4000,

        /// <summary>
        /// Releases the specified region of pages. After the operation, the pages are in the free state. 
        /// If you specify this value, dwSize must be 0 (zero), and lpAddress must point to the base address returned by the VirtualAllocEx function when the region is reserved. The function fails if either of these conditions is not met.
        /// If any pages in the region are committed currently, the function first de-commits, and then releases them.
        /// The function does not fail if you attempt to release pages that are in different states, some reserved and some committed. This means that you can release a range of pages without first determining the current commitment state.
        /// Do not use this value with MEM_DECOMMIT.
        /// </summary>
        Release = 0x8000,

        /// <summary>
        /// MEM_RESET
        /// </summary>
        /// <remarks>
        /// Indicates that data in the memory range specified by lpAddress and dwSize is no longer of interest. The pages should not be read from or written to the paging file. However, the memory block will be used again later, so it should not be de-committed. This value cannot be used with any other value. 
        /// Using this value does not guarantee that the range operated on with MEM_RESET will contain zeroes. If you want the range to contain zeroes, de-commit the memory and then recommit it.
        /// When you use MEM_RESET, the VirtualAllocEx function ignores the value of fProtect. However, you must still set fProtect to a valid protection value, such as PAGE_NOACCESS.
        /// VirtualAllocEx returns an error if you use MEM_RESET and the range of memory is mapped to a file. A shared view is only acceptable if it is mapped to a paging file.
        /// </remarks>
        Reset = 0x80000,

        /// <summary>
        /// MEM_PHYSICAL
        /// </summary>
        /// <remarks>
        /// Allocates physical memory with read-write access. This value is solely for use with Address Windowing Extensions (AWE) memory. 
        /// This value must be used with MEM_RESERVE and no other values.
        /// </remarks>
        Physical = 0x400000,

        /// <summary>
        /// MEM_TOP_DOWN
        /// </summary>
        /// <remarks>
        /// Allocates memory at the highest possible address.
        /// </remarks>
        TopDown = 0x100000,

        /// <summary>
        /// Write watch
        /// </summary>
        WriteWatch = 0x200000,

        /// <summary>
        /// MEM_LARGE_PAGES
        /// </summary>
        /// <remarks>
        /// Allocates memory using large page support.
        /// The size and alignment must be a multiple of the large-page minimum. To obtain this value, use the GetLargePageMinimum function.
        /// Windows XP/2000:   This flag is not supported.
        /// </remarks>
        LargePages = 0x20000000
    }
}