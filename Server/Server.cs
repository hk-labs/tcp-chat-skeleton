using System;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Server
    {
        private readonly Socket _listeningSocket;
        private readonly Thread _thread;
        private bool _stop;

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        public Server(Socket listeningSocket)
        {
            _listeningSocket = listeningSocket;
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
            }
        }
    }
}