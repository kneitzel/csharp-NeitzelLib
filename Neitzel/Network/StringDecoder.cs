using System;
using System.Text;

namespace Neitzel.Network
{
    /// <summary>
    /// Decodes bytes from BytesReceivedEventArgs and send out Text messages
    /// </summary>
    /// <remarks>
    /// Such a decode class which subscribes to an MessageReceived event is possible as shown here
    /// and it makes it possible to have a chain of decoder who checks the messages for different things,
    /// but this design is not required. 
    /// </remarks>
    public class StringDecoder
    {
        /// <summary>
        /// Event that a Message was Received
        /// </summary>
        /// <remarks>
        /// This is done on the IO Thread of the socket so keep it a short as possible. Push execution to
        /// other Threads if required.
        /// </remarks>
        public event EventHandler<TextMessageEventArgs> MessageDecoded;


        /// <summary>
        /// Encoder used to encode strings.
        /// </summary>
        public Encoding InternalEncoder { get; set; }

        /// <summary>
        /// Text we received so far.
        /// </summary>
        private string _receivedText = string.Empty;

        /// <summary>
        /// Create a new instance of StringDecoder.
        /// </summary>
        /// <param name="internalEncoder"></param>
        public StringDecoder(Encoding internalEncoder)
        {
            InternalEncoder = internalEncoder;
        }

        /// <summary>
        /// MessageReceiver should receive the BytesReceivedEventArgs and should be registered with such an event.
        /// </summary>
        /// <param name="sender">Sender of that message.</param>
        /// <param name="bytesReceivedEvent">The bytes received event</param>
        public void MessageReceiver(object sender, BytesReceivedEventArgs bytesReceivedEvent)
        {
            // validate arguments
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (bytesReceivedEvent == null) throw new ArgumentNullException(nameof(bytesReceivedEvent));

            _receivedText += InternalEncoder.GetString(bytesReceivedEvent.ReceivedBytes);
            // As long as there is a "\r\n" in the Buffer
            int locationCrlf;
            while ((locationCrlf = _receivedText.IndexOf("\r\n", StringComparison.OrdinalIgnoreCase)) > 0)
            {
                // The received Message is start of buffer till \r\n
                string message = _receivedText.Substring(0, locationCrlf);

                // The Buffer is now starting behind the \r\n combination
                _receivedText = _receivedText.Substring(locationCrlf + 2);

                // Trigger the event that we got a new IrcMessage
                OnMessageDecoded(sender, new TextMessageEventArgs(message));
            }
        }

        /// <summary>
        /// Message was decoded.
        /// </summary>
        /// <param name="sender">Sender of the message,</param>
        /// <param name="textMessageEvent">TextMessageEvent with message.</param>
        public void OnMessageDecoded(object sender, TextMessageEventArgs textMessageEvent)
        {
            var handler = MessageDecoded;

            handler?.Invoke(sender, textMessageEvent);
        }

        /// <summary>
        /// Encode a message with the internal encoder.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public byte[] EncodeMessage(string message)
        {
            return InternalEncoder.GetBytes(message);
        }
    }
}
