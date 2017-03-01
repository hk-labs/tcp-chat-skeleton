using System;

namespace ChatProtocol
{
    public class PublicMessage : ChatPDU
    {
        public const byte Type = 5;

        public string Source { get; }

        public string Message { get; }

        public PublicMessage(string source, string message)
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

        internal static PublicMessage Parse(byte[] bytes, int offset, int size)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            var message = Encoding.GetString(bytes, offset, size);

            var parts = message.Split(':');
            if (parts.Length < 2)
                throw new BadMessageException();

            return new PublicMessage(parts[0], parts[1]);
        }
    }
}