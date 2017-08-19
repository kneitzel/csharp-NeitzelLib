using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neitzel.Network
{
    /// <summary>
    /// A message regarding a Client.
    /// </summary>
    /// <remarks>
    /// This Argument is used with ClientConnected and ClientDisconnected events.
    /// </remarks>
    public class ClientEventArgs : EventArgs
    {
        /// <summary>
        /// Client of this message.
        /// </summary>
        public TcpIpServerConnection.ClientConnection Client { get; set; }

        /// <summary>
        /// Creates a new instance of ClientEventArgs
        /// </summary>
        /// <param name="client">Client of this message.</param>
        public ClientEventArgs(TcpIpServerConnection.ClientConnection client)
        {
            Client = client;
        }
    }
}
