using ChatProtocol;
using System;
using System.Net.Sockets;

namespace Client
{
    public static class Program
    {
        public static void Main()
        {
            Console.WindowHeight = 10;

            const string address = "127.0.0.1";
            const int port = 9000;
        }

        private static void Send(Socket socket, ChatPDU chatPdu)
        {
        }
    }
}
