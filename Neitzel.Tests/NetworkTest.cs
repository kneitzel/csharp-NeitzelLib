using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neitzel.Network;

namespace Neitzel.Tests
{
    /// <summary>
    /// Tests the Network classes.
    /// </summary>
    [TestClass]
    public class NetworkTest
    {
        /// <summary>
        /// Port to use in our tests.
        /// </summary>
        public const int TestPort = 12345;

        /// <summary>
        /// Dictionary with sender and a stack of received messages.
        /// </summary>
        public Dictionary<object, Stack<BytesReceivedEventArgs>> ReceivedMessages = new Dictionary<object, Stack<BytesReceivedEventArgs>>();

        /// <summary>
        /// Last message received through the StringDecoder.
        /// </summary>
        public string LastMessage;

        /// <summary>
        /// Tests the TcpIpServerConnection and TcpIpClientConnection.
        /// </summary>
        [TestMethod]
        public void ServerAndClientTests()
        {
            // Create and start a server
            var server = new TcpIpServerConnection(TestPort);
            server.Open();
            server.MessageReceived += MessageReceived;

            // Create client and connect to server
            var client = new TcpIpClientConnection("127.0.0.1", TestPort);
            client.MessageReceived += MessageReceived;
            client.Connect();

            // send a message from client to server.
            var byteMessage = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            client.SendMessage(byteMessage);

            // Sleep so message can arrive
            Thread.Sleep(10000);

            // Test that the message arrived
            Assert.AreEqual(1, ReceivedMessages.Count);
            var clientConnectionInServer = (TcpIpServerConnection.ClientConnection) ReceivedMessages.Keys.First();
            Assert.IsTrue(byteMessage.SequenceEqual(ReceivedMessages[clientConnectionInServer].Pop().ReceivedBytes));
            ReceivedMessages.Remove(clientConnectionInServer);

            // Send message from server to client
            clientConnectionInServer.Send(byteMessage);

            // Sleep so message can arrive
            Thread.Sleep(1000);

            // Test that the message arrived
            Assert.AreEqual(1, ReceivedMessages.Count);
            Assert.AreEqual(client, ReceivedMessages.Keys.First());
            Assert.IsTrue(byteMessage.SequenceEqual(ReceivedMessages[client].Pop().ReceivedBytes));

            // Include a StringDecoder and make sure that the string is correctly sent.
            string stringMessage = "Just a message";
            var decoder = new StringDecoder(Encoding.UTF8);
            clientConnectionInServer.MessageReceived += decoder.MessageReceiver;
            decoder.MessageDecoded += TextMessageReceived;
            client.SendMessage(decoder.EncodeMessage(stringMessage+ "\r\n"));

            // Sleep so message can arrive
            Thread.Sleep(1000);

            // Test that the message arrived
            Assert.AreEqual(stringMessage, LastMessage);

            server.Close();
            client.Close();
        }

        /// <summary>
        /// Handler that receives Messages.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="bytesReceivedEvent">Event with received message.</param>
        public void MessageReceived(object sender, BytesReceivedEventArgs bytesReceivedEvent)
        {
            if (!ReceivedMessages.ContainsKey(sender))
                ReceivedMessages.Add(sender, new Stack<BytesReceivedEventArgs>());

            var stackofMessages = ReceivedMessages[sender];
            stackofMessages.Push(bytesReceivedEvent);
        }

        /// <summary>
        /// Eventhandler for the TextMessageEventArgs that simply stored the last received message.
        /// </summary>
        /// <param name="sender">sender of the message.</param>
        /// <param name="textMessageEvent">Text message event argument.</param>
        public void TextMessageReceived(object sender, TextMessageEventArgs textMessageEvent)
        {
            LastMessage = textMessageEvent.Message;
        }
    }
}
