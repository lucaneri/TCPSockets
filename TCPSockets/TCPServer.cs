using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

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
            
                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(10);
            
            Console.WriteLine("Server socket listening");
            byte[] bytes = new byte[1024];
            while (true)
            {

                string data = "";
                var bindedS = serverSocket.Accept();//bloccante, il server attende che una socket client si connetta
                
                var x = bindedS.Receive(bytes);//legge i byte dalla socket / dallo stream di input della socket
                #region code to deploy in the final version
                //string fileName = "/dev/ttyACM0";
                //using (var file = File.OpenRead(fileName))
                //    using(var fileReader = new StreamReader(file, Encoding.UTF8, true, 1024))
                //{

                //}
                #endregion
                data += Encoding.ASCII.GetString(bytes, 0, x); //decodifica i dati, creando una stringa
                Console.WriteLine("Received:" + data);
                byte[] msg = Encoding.ASCII.GetBytes("data received");
                bindedS.Send(msg);//server invia i messaggi al client, usando la socket che quest'ultimo ha aperto
            }
        }
    }
}
