using System;

namespace Neitzel.Network
{
    /// <summary>
    /// Event Argument that hands over receibed bytes.
    /// </summary>
    public class TextMessageEventArgs : EventArgs
    {
        /// <summary>
        /// The message we received
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Create a new instance of TextMessageEventArgs.
        /// </summary>
        public TextMessageEventArgs() { }

        /// <summary>
        /// Create a new instance of TextMessageEventArgs.
        /// </summary>
        /// <param name="message">Received message.</param>
        public TextMessageEventArgs(string message)
        {
            Message = message;
        }
         
    }
}
