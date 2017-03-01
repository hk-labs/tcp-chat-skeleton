using System;

namespace ChatProtocol
{
    public class PrivateMessage : ChatPDU
    {
        public const byte Type = 4;

        public string Source { get; }

        public string Target { get; }

        public string Message { get; }

        public PrivateMessage(string source, string target, string message)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            Source = source;
            Target = target;
            Message = message;
        }

        public override byte[] Serialize()
        {
            var message = $"{Source}:{Target}:{Message}";

            var messageSize = Encoding.GetByteCount(message);
            var bufferSize = messageSize + HeaderSize;

            var buffer = new byte[bufferSize];
            WriteHeader(buffer, messageSize, Type);
            Encoding.GetBytes(message, 0, message.Length, buffer, HeaderSize);

            return buffer;
        }

        internal static PrivateMessage Parse(byte[] bytes, int offset, int size)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            var message = Encoding.GetString(bytes, offset, size);

            var parts = message.Split(':');
            if (parts.Length < 3)
                throw new BadMessageException();

            return new PrivateMessage(parts[0], parts[1], parts[2]);
        }
    }
}