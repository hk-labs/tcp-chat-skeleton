using System;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    public class ClientMessageListener
    {
        private readonly Socket _socket;
        private readonly Thread _thread;
        private bool _stop;

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
            while (!_stop)
            {

                Thread.Sleep(1);
            }
        }
    }
}
