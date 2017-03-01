using System;
using System.Text;

namespace ChatProtocol
{
    public abstract class ChatPDU
    {
        internal const int SizePosition = 0;
        internal const int TypePosition = 1;

        public const int MaxSize = 255;

        internal static byte HeaderSize => 2;

        public static Encoding Encoding { get; } = Encoding.UTF8;

        protected static void WriteHeader(byte[] buffer, int messageSize, byte type)
        {
            var totalSize = messageSize + HeaderSize;
            if (totalSize > MaxSize)
                throw new InvalidOperationException("Message is too long");

            buffer[SizePosition] = (byte)totalSize;
            buffer[TypePosition] = type;
        }

        public abstract byte[] Serialize();
    }
}
