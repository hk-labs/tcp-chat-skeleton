using System;
using System.Net.Sockets;
using System.Threading;
using ChatProtocol;

namespace Client
{
    public class ClientMessageListener
    {
        private readonly Socket _socket;
        private readonly Thread _thread;
        private bool _stop;

        private readonly PDUParser _parser = new PDUParser();

        public ClientMessageListener(Socket socket)
        {
            if (socket == null)
                throw new ArgumentNullException(nameof(socket));

            _socket = socket;
            _thread = new Thread(Run);
        }

        public void Start()
        {
            _thread.Start();
        }

        public void Stop()
        {
            _stop = true;
            _thread.Join();
        }

        private void Run()
        {
            var buffer = new byte[1024];

            while (!_stop)
            {

                if (_socket.Available > 0)
                {
                    var readBytes = _socket.Receive(buffer);

                    if (readBytes == 0)
                        return;

                    _parser.AddChunk(buffer, readBytes);

                    ChatPDU chatPdu;
                    while (_parser.TryParse(out chatPdu))
                    {
                        var clientJoined = chatPdu as ClientJoinedChat;
                        if (clientJoined != null)
                        {
                            Console.WriteLine($"Client {clientJoined.Name} has joined chat");
                        }

                        var clientLeft = chatPdu as ClientLeftChat;
                        if (clientLeft != null)
                        {
                            Console.WriteLine($"Client {clientLeft.Name} has left chat");
                        }

                        var incomingMessage = chatPdu as IncomingMessage;
                        if (incomingMessage != null)
                        {
                            Console.WriteLine($"{incomingMessage.Source}: {incomingMessage.Message}");
                        }
                    }
                }

                Thread.Sleep(1);
            }
        }
    }
}
