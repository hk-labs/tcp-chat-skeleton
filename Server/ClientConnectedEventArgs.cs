using System;
using System.Net.Sockets;

namespace Server
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public Socket Socket { get; }

        public ClientConnectedEventArgs(Socket socket)
        {
            Socket = socket;
        }
    }
}