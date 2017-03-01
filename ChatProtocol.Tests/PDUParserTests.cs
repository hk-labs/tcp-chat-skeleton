using Xunit;

namespace ChatProtocol
{
    public class PDUParserTests
    {
        private readonly PDUParser _sut = new PDUParser();

        [Fact]
        public void ItShouldParseFullMessage()
        {
            var message = new JoinChat("test");
            var bytes = message.Serialize();

            var buffer = new byte[10];
            bytes.CopyTo(buffer, 0);

            _sut.AddChunk(buffer, bytes.Length);

            ChatPDU result;
            Assert.True(_sut.TryParse(out result));

            var receivedMessage = (JoinChat)result;
            Assert.Equal(message.Name, receivedMessage.Name);
        }

        [Fact]
        public void ItShouldParseChunkedMessage()
        {
            ChatPDU result;

            var message = new JoinChat("test");
            var bytes = message.Serialize();

            _sut.AddChunk(bytes, bytes.Length - 1);

            Assert.False(_sut.TryParse(out result));

            _sut.AddChunk(new[] { bytes[bytes.Length - 1] }, 1);

            Assert.True(_sut.TryParse(out result));

            Assert.Equal(message.Name, ((JoinChat)result).Name);
        }

        [Fact]
        public void ItShouldParseManyMessagesAtOnce()
        {
            ChatPDU result;

            var message1 = new JoinChat("test1");
            var bytes = message1.Serialize();
            _sut.AddChunk(bytes, bytes.Length);

            var message2 = new JoinChat("test2");
            bytes = message2.Serialize();
            _sut.AddChunk(bytes, bytes.Length);

            Assert.True(_sut.TryParse(out result));
            Assert.Equal(message1.Name, ((JoinChat)result).Name);

            Assert.True(_sut.TryParse(out result));
            Assert.Equal(message2.Name, ((JoinChat)result).Name);
        }
    }
}