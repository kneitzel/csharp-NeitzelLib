using System;

namespace Neitzel.Network
{
    /// <summary>
    /// Event Argument that hands over receibed bytes.
    /// </summary>
    public class BytesReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// The bytes we received
        /// </summary>
        public byte[] ReceivedBytes { get; set; }
        
        /// <summary>
        /// Create a new instance of ByteReceivedEventArgs.
        /// </summary>
        public BytesReceivedEventArgs() { }

        /// <summary>
        /// Create a new instance of ByteReceivedEventArgs.
        /// </summary>
        /// <param name="receivedBytes">Received bytes.</param>
        public BytesReceivedEventArgs(byte[] receivedBytes)
        {
            ReceivedBytes = receivedBytes;
        }
         
    }
}
