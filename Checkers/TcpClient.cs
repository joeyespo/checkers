using System;
using System.Net.Sockets;
using System.Net;

namespace Checkers
{
    // Workaround class to be able to access sockets directly
    public class TcpClient : System.Net.Sockets.TcpClient
    {
        private object tag = null;

        public TcpClient()
        {
        }
        public TcpClient(string host, int port)
            : base(host, port)
        {
        }
        public TcpClient(IPEndPoint endPoint)
            : base(endPoint)
        {
        }
        public TcpClient(Socket socket)
        {
            Client = socket;
        }

        public Socket Socket
        {
            get
            {
                return Client;
            }
        }

        public object Tag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
            }
        }
    }
}
