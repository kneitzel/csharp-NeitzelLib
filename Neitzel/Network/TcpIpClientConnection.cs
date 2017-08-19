using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Neitzel.Network
{

    /// <summary>
    /// A tcp/ip client connection which reads the data with its own thread.
    /// </summary>
    public class TcpIpClientConnection : DisposableObject
    {
        #region Fields

        /// <summary>
        /// Tracesource for all log messages.
        /// </summary>
        private static readonly TraceSource Logger = new TraceSource(NetworkConstants.TraceSourceName);

        /// <summary>
        /// A Flag to indicate, that the Thread should end.
        /// </summary>
        private bool _endThread;

        /// <summary>
        /// TcpClient used.
        /// </summary>
        private TcpClient _client;

        /// <summary>
        /// Listener thread.
        /// </summary>
        private Thread _listenerThread;

        /// <summary>
        /// Stream used for sending and receiving of data once we are connected.
        /// </summary>
        private NetworkStream _ioStream;

        #endregion

        #region Lifetime

        /// <summary>
        /// Creates a new instance of TcpIpClientConnection
        /// </summary>
        public TcpIpClientConnection() { }

        /// <summary>
        /// Creates a new instance of TcpIpClientConnection
        /// </summary>
        /// <param name="hostName">HostName of server to connect to.</param>
        /// <param name="port">Port to connect to.</param>
        public TcpIpClientConnection(string hostName, int port)
            : this()
        {
            HostName = hostName;
            Port = port;
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~TcpIpClientConnection()
        {
            Logger.TraceEvent(TraceEventType.Error, 0, "Instance of {0} wasn't disposed!", GetType().Name);
            Dispose(false);
        }

        /// <summary>
        /// Dispose this instance.
        /// </summary>
        /// <param name="disposing">Active disposing or called by finalizer?</param>
        protected override void Dispose(bool disposing)
        {
            // Check if are already disposed.
            if (Disposed)
                return;

            // Write log message
            Logger.TraceEvent(TraceEventType.Information, 0, "Disposing the TcpIpClientConnection.");

            // Call Dispose in super class
            base.Dispose(true);

            // Dispose the instance.
            if (disposing)
            {
                // Make sure, that we do not send any more events
                RemoveAllHandlers();

                // Indicate that the thread should end.
                _endThread = true;

                // Handle the IOStream
                _ioStream?.Close();

                // Handle the _client
                _client?.Close();

                // Abort the Thread
                _listenerThread?.Abort();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event that a Message was Received
        /// </summary>
        public event EventHandler<BytesReceivedEventArgs> MessageReceived;

        #endregion

        #region Properties

        /// <summary>
        /// Hostname to connect to.
        /// </summary>
        /// <remarks>
        /// Changeing this  property does not have any effect on the connection.
        /// </remarks>
        public string HostName { get; set; }

        /// <summary>
        /// Port number to connect to.
        /// </summary>
        /// <remarks>
        /// Changeing this property does not have any effect on a connection.</remarks>
        public int Port { get; set; }

        /// <summary>
        /// Gets a value indicating whether we an connection is established or not.
        /// </summary>
        public bool Connected => _client?.Connected ?? false;

        /// <summary>
        /// Size of buffer used when receiving data.
        /// </summary>
        public int BufferSize { get; set; } = 40960;

        /// <summary>
        /// Timeout of the connection in seconds. 0 means no timeout.
        /// </summary>
        public int Timeout { get; set; } = NetworkConstants.DefaultTimeout;

        #endregion

        #region Public Methods

        /// <summary>
        /// Connect to the server.
        /// </summary>
        public void Connect()
        {
            // Validate state
            if (string.IsNullOrEmpty(HostName))
                throw new InvalidOperationException("HostName not set.");
            if (_ioStream != null)
                throw new InvalidOperationException("Already connected.");

            // connect
            try
            {
                // Get the connection up
                var serverEndpoint = new IPEndPoint(Dns.GetHostAddresses(HostName).First(), Port);

                _client = new TcpClient();
                _client.Connect(serverEndpoint);

                // Get the Stream 
                _ioStream = _client.GetStream();

                _listenerThread = new Thread(ListenForMessages);
                _listenerThread.Start();
            }
            catch (Exception ex)
            {
                // Error when connecting to the server ....
                Logger.TraceEvent(TraceEventType.Error, 0, "Unable to connect to Server. {0} / {1}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Send a Message to the server
        /// </summary>
        /// <param name="message">An message to send</param>
        public void SendMessage(byte[] message)
        {
            // validate
            if (message == null) throw new ArgumentNullException(nameof(message));

            try
            {
                _ioStream.Write(message, 0, message.Length);
            }
            catch (Exception ex)
            {
                // Exception while sending Message!
                Logger.TraceEvent(TraceEventType.Error, 0, "Exception when sending message. {0} / {1}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Close the connection / Dispose this instance.
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Send the MessageReceived Event
        /// </summary>
        /// <param name="e">BytesReceivedEventArgs to send.</param>
        protected virtual void OnMessageReceived(BytesReceivedEventArgs e)
        {
            // Local copy to prevent race conditions
            var handler = MessageReceived;

            // raise the event
            handler?.Invoke(this, e);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Remove all event handlers from all events. This makes sure, that we do not try to send any events while we Close / Dispose.
        /// </summary>
        private void RemoveAllHandlers()
        {
            MessageReceived = null;
        }

        /// <summary>
        /// Thread routine, that will permanently waits for new messages and once
        /// a message is received completly, it will fire an event!
        /// private, so that it cannot be called from outside.
        /// </summary>
        private void ListenForMessages()
        {
            var rawMessage = new byte[BufferSize];
            var lastActivity = DateTime.Now;

            try
            {
                // As long as we are connexted
                while (!_endThread)
                {
                    // End thread if we are no longer connected
                    if (!_client.Connected)
                        _endThread = true;

                    if (_ioStream.DataAvailable)
                    {
                        // We got activity.
                        lastActivity = DateTime.Now;

                        // Read from the IOStream
                        int bytesReceived = _ioStream.Read(rawMessage, 0, rawMessage.Length);
                        if (bytesReceived > 0)
                        {
                            var received = rawMessage.Take(bytesReceived).ToArray();
                            OnMessageReceived(new BytesReceivedEventArgs(received));
                        }
                    }
                    else
                    {
                        // Check for timeout
                        if (Timeout > 0 && DateTime.Now.Subtract(lastActivity).TotalSeconds > Timeout)
                        {
                            // Timeout, log message and return - Close call is inside finally clause.
                            Logger.TraceEvent(TraceEventType.Information, 0,
                                "A timeout occured, closing the connection.");
                            return;
                        }

                        // Sleep a moment.
                        Thread.Sleep(10);
                    }
                }
            }
            catch (IOException ex)
            {
                // Exception while reading from socket!
                Logger.TraceEvent(TraceEventType.Error, 0, "Exception in ListenForMessages {0} / {1}", ex.Message,
                    ex.StackTrace);
            }
            finally
            {
                // Close the connection / Dispose the instance.
                Close();
            }
        }

        #endregion
    }
}
