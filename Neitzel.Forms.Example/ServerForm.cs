using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Forms;
using Neitzel.Forms.Example.Properties;
using Neitzel.Network;

namespace Neitzel.Forms.Example
{
    public partial class ServerForm : Form
    {
        public const string NotConnectedStatus = "Not connected!";
        public const string NumberClientsStatus = "Clients: {0}";

        /// <summary>
        /// Port in use.
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// The server connection.
        /// </summary>
        private TcpIpServerConnection _connection;

        /// <summary>
        /// Collection of connected clients.
        /// </summary>
        private Collection<TcpIpServerConnection.ClientConnection> _clients  = new Collection<TcpIpServerConnection.ClientConnection>();

        /// <summary>
        /// Creates a new instance of ServerForm.
        /// </summary>
        public ServerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Status of the connection.
        /// </summary>
        public string Status => _connection == null
            ? NotConnectedStatus
            : string.Format(CultureInfo.CurrentCulture, NumberClientsStatus, _clients.Count);

        /// <summary>
        /// Handles the click on Stop Button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void OnStopButtonClick(object sender, EventArgs e)
        {
            StopButton.Enabled = false;
            _connection.Dispose();
            _clients.Clear();
            _connection = null;
            StartButton.Enabled = true;
            PortTextBox.ReadOnly = false;
            StatusLabel.Text = Status;
        }

        /// <summary>
        /// Handle a new connected client.
        /// </summary>
        private void OnClientConnected(object sender, ClientEventArgs e)
        {
            _clients.Add(e.Client);
            StatusLabel.Text = Status;
        }

        /// <summary>
        /// Handle a client disconnect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClientDisconnected(object sender, ClientEventArgs e)
        {
            _clients.Remove(e.Client);
            StatusLabel.Text = Status;
        }

        private void OnMessageReceived(object sender, BytesReceivedEventArgs e)
        {
            // We simply send a message to all clients!
            foreach (var client in _clients)
            {
                client.Send(e.ReceivedBytes);
            } 
        }

        /// <summary>
        /// Handles the click on Start Button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void OnStartButtonClick(object s, EventArgs e)
        {
            StartButton.Enabled = false;
            int port;
            if (!int.TryParse(PortTextBox.Text, out port))
            {
                StatusLabel.Text = Resources.StatusPortCannotBeParsed;
                StartButton.Enabled = true;
                return;
            }

            Port = port;
            _connection = new TcpIpServerConnection(port);
            _connection.ClientConnected += (sender, args) => Invoke(new EventHandler<ClientEventArgs>(OnClientConnected), sender, args);
            _connection.ClientDisconnected += (sender, args) => Invoke(new EventHandler<ClientEventArgs>(OnClientDisconnected), sender, args);
            _connection.MessageReceived += OnMessageReceived;
            _connection.Open();
            StatusLabel.Text = Status;
            PortTextBox.ReadOnly = true;
            StopButton.Enabled = true;
        }

        /// <summary>
        /// Form is closed.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            // Reset ServerWindow so that a new ServerForm can be opened
            ExampleForm.ServerWindow = null;
        }

        /// <summary>
        /// Prepares the form for closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // Dispose the connection.
            _connection?.Dispose();
        }
    }
}
