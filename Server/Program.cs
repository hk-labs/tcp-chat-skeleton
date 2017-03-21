using ChatProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public static class Program
    {
        private static readonly IList<ClientChannel> Clients = new List<ClientChannel>();

        public static void Main()
        {
            Console.WindowHeight = 10;

            const string listeningAddress = "127.0.0.1";
            const int port = 9000;
        }

        private static void OnClientConnected(object sender, ClientConnectedEventArgs args)
        {
            Console.WriteLine($"Client '{args.Socket.RemoteEndPoint}' connected");
        }

        private static void OnClientSentPublicMessage(object sender, PublicMessage message)
        {
        }

        private static void OnClientSentPrivateMessage(object sender, PrivateMessage message)
        {
        }

        private static void OnClientJoined(object sender, JoinChat message)
        {
        }

        private static void OnClientDisconnected(object sender, EventArgs args)
        {
        }
    }
}
