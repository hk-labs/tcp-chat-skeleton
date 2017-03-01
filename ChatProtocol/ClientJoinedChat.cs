using System;

namespace ChatProtocol
{
    public class ClientJoinedChat : ChatPDU
    {
        public const byte Type = 2;

        public string Name { get; }

        public ClientJoinedChat(string name)
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

        internal static ClientJoinedChat Parse(byte[] bytes, int offset, int size)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            var message = Encoding.GetString(bytes, offset, size);

            return new ClientJoinedChat(message);
        }

    }
}