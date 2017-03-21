using ChatProtocol;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class ClientChannel
    {
        public Socket Socket { get; }
        public string Name { get; private set; }

        private readonly Thread _thread;
        private bool _stop;
        private bool _authorized;
        private readonly PDUParser _parser = new PDUParser();

        public event EventHandler<PublicMessage> ClientSentPublicMessage;
        public event EventHandler<PrivateMessage> ClientSentPrivateMessage;
        public event EventHandler<JoinChat> ClientJoined;
        public event EventHandler ClientDisconnected;

        public ClientChannel(Socket socket)
        {
            if (socket == null)
                throw new ArgumentNullException(nameof(socket));

            Socket = socket;
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

                Thread.Sleep(1);
            }

            if (_authorized)
                ClientDisconnected?.Invoke(this, EventArgs.Empty);

            Socket.Dispose();
        }

        public void Send(ChatPDU pdu)
        {
        }
    }
}