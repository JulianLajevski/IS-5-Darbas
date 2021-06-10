using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _5_practical_task
{
    class Sender
    {
        public void Send(string text, byte[] signed, string publicKey)
        {
            // Receiverio adresas
            IPAddress receiverIP = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(receiverIP, 2024);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            byte[] textBytes = Encoding.ASCII.GetBytes(text);
            byte[] Key = Encoding.ASCII.GetBytes(publicKey);
            
            socket.Connect(endPoint);
            socket.Send(textBytes);
            socket.Close();

            Socket socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket1.Connect(endPoint);
            socket1.Send(signed);
            socket1.Close();

            Socket socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket2.Connect(endPoint);
            socket2.Send(Key);
            socket2.Close();



        }
      

    }
}
