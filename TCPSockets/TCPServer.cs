using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPSockets
{
    class TCPServer
    {
        public TCPServer()
        {
            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); //ottiene il nome dell'host
            var ipAddress = ipHostInfo.AddressList[0];//ottiene l'indirizzo IP associato a quel nome 
            var localEndPoint = new IPEndPoint(ipAddress, 11000); //crea un end point Ip per quell'ip e con quella porta
            var serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            string data = "";
                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(10);
            Console.WriteLine("Process started, waiting datas");
            byte[] bytes = new byte[1024];
            while (true)
            {
                var bindedS = serverSocket.Accept();
                var inputStream = new NetworkStream(bindedS);
                var x = bindedS.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, x);
                Console.WriteLine("Received:" + data);
            }
        }
    }
}
