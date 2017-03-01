using System;

namespace ChatProtocol
{
    public class IncomingMessage : ChatPDU
    {
        public const byte Type = 6;

        public string Source { get; }

        public string Message { get; }

        public IncomingMessage(string source, string message)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            Source = source;
            Message = message;
        }

        public override byte[] Serialize()
        {
            var message = $"{Source}:{Message}";

            var messageSize = Encoding.GetByteCount(message);
            var bufferSize = messageSize + HeaderSize;

            var buffer = new byte[bufferSize];
            WriteHeader(buffer, messageSize, Type);
            Encoding.GetBytes(message, 0, message.Length, buffer, HeaderSize);

            return buffer;
        }

        internal static IncomingMessage Parse(byte[] bytes, int offset, int size)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            var message = Encoding.GetString(bytes, offset, size);

            var parts = message.Split(':');
            if (parts.Length < 2)
                throw new BadMessageException();

            return new IncomingMessage(parts[0], parts[1]);
        }
    }
}