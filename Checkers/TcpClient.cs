using System;
using System.Net.Sockets;
using System.Net;

namespace AIC
{
  // Workaround class to be able to access sockets directly
  public class TcpClient : System.Net.Sockets.TcpClient
  {
    public TcpClient ()
    {}
    public TcpClient (Socket client)
    { Client = client; }
    
    public Socket Socket
    { get { return Client; } }
  }
}
