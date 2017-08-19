namespace Neitzel.Win32
{
    /// <summary>
    /// Win32 window ancestor flags.
    /// </summary>
    public enum Win32WindowAncestor
    {
        /// <summary>
        /// Retrieves the parent window. This does not include the owner, as it does with the GetParent function. 
        /// </summary>
        Parent = 1,

        /// <summary>
        /// Retrieves the root window by walking the chain of parent windows.
        /// </summary>
        Root = 2,

        /// <summary>
        /// Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent. 
        /// </summary>
        RootOwner = 3
    }
}