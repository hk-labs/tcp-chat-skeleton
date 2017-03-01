using System;

namespace ChatProtocol
{
    public class JoinChat : ChatPDU
    {
        public const byte Type = 1;

        public string Name { get; }

        public JoinChat(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public override byte[] Serialize()
        {
            var messageSize = Encoding.GetByteCount(Name);
            var bufferSize = messageSize + HeaderSize;

            var buffer = new byte[bufferSize];
            WriteHeader(buffer, messageSize, Type);
            Encoding.GetBytes(Name, 0, Name.Length, buffer, HeaderSize);

            return buffer;
        }

        internal static JoinChat Parse(byte[] bytes, int offset, int size)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            var message = Encoding.GetString(bytes, offset, size);

            return new JoinChat(message);
        }
    }
}