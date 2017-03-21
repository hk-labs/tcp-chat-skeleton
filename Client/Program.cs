using ChatProtocol;
using System;
using System.Net;
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
            var bytes = chatPdu.Serialize();

            try
            {
                var position = 0;
                do
                {
                    position += socket.Send(bytes, position, bytes.Length - position, SocketFlags.None);
                } while (position < bytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
