using System;
using System.Text;
using System.Windows.Forms;
using Neitzel.Network;

namespace Neitzel.Forms.Example
{
    public partial class ClientForm : Form
    {
        private TcpIpClientConnection _connection;

        /// <summary>
        /// Creates a new instance of ClientForm.
        /// </summary>
        public ClientForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the click on the send button.
        /// </summary>
        private void OnSendButtonClick(object sender, EventArgs e)
        {
            // Check if we must connect first.
            if (_connection == null || !_connection.Connected)
            {
                // Dispose if instance is there.
                _connection?.Dispose();

                _connection = new TcpIpClientConnection("127.0.0.1", ExampleForm.ServerWindow.Port);
                _connection.MessageReceived += (s,args) => Invoke(new EventHandler<BytesReceivedEventArgs>(OnMessageReceived), s, args);
                _connection.Connect();
                return;
            }

            // Check if we have text to send.
            if (string.IsNullOrEmpty(InputBox.Text))
                return;

            _connection.SendMessage(Encoding.UTF8.GetBytes(InputBox.Text + "\r\n"));
            InputBox.Text = "";
        }

        /// <summary>
        /// Handle a received message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMessageReceived(object sender, BytesReceivedEventArgs e)
        {
            string text = Encoding.UTF8.GetString(e.ReceivedBytes);
            OutputBox.Text += text;
        }

        /// <summary>
        /// Prepares the form to be closed.
        /// </summary>
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            _connection?.Dispose();
        }
    }
}
