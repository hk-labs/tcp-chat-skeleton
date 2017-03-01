using System;

namespace ChatProtocol
{
    public class PDUParser
    {
        private readonly byte[] _buffer;
        private int _bufferSize;

        public PDUParser(int capacity = 1024)
        {
            _buffer = new byte[capacity];
        }

        public void AddChunk(byte[] receivedBytes, int receivedCount)
        {
            if (_bufferSize + receivedCount > _buffer.Length)
                throw new InvalidOperationException("Buffer overflow");

            Array.Copy(receivedBytes, 0, _buffer, _bufferSize, receivedCount);
            _bufferSize += receivedCount;
        }

        public bool TryParse(out ChatPDU pdu)
        {
            if (_bufferSize > 0)
            {
                var messageLength = _buffer[0];

                if (_bufferSize >= messageLength)
                {
                    pdu = Parse(_buffer, 0, messageLength);
                    Array.Copy(_buffer, messageLength, _buffer, 0, messageLength);
                    _bufferSize -= messageLength;
                    return true;
                }
            }

            pdu = null;
            return false;
        }

        private static ChatPDU Parse(byte[] bytes, int offset, int size)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            var payloadSize = size - ChatPDU.HeaderSize;
            var payloadOffset = offset + ChatPDU.HeaderSize;

            switch (bytes[offset + ChatPDU.TypePosition])
            {
                case JoinChat.Type:
                    return JoinChat.Parse(bytes, payloadOffset, payloadSize);

                case ClientJoinedChat.Type:
                    return ClientJoinedChat.Parse(bytes, payloadOffset, payloadSize);

                case ClientLeftChat.Type:
                    return ClientLeftChat.Parse(bytes, payloadOffset, payloadSize);

                case PrivateMessage.Type:
                    return PrivateMessage.Parse(bytes, payloadOffset, payloadSize);

                case PublicMessage.Type:
                    return PublicMessage.Parse(bytes, payloadOffset, payloadSize);

                case IncomingMessage.Type:
                    return IncomingMessage.Parse(bytes, payloadOffset, payloadSize);
            }

            throw new BadMessageException();
        }
    }
}
