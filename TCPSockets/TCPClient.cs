using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TCPSockets
{
    class TCPClient
    {
        public TCPClient()
        {

            // var ipHostInfo = Dns.GetHostEntry("172.16.6.244"); //ottiene il nome dns della raspi che ha quell'ip

            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());//ottiene il nome per il localhost
            var ipAddress = ipHostInfo.AddressList[0];//ottiene l'indirizzo IP associato a quel nome 
            
            
            var serverSocket = new IPEndPoint(ipAddress, 11000); //crea un riferimento al server a cui si vuole connettere

            //crea una socket "client", associata ad una classe di ip, usando il protocollo tcp
            var clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            byte[] bytes = new byte[1024];
            try
            {
                clientSocket.Connect(serverSocket); //socket del client prova a connettersi con la socket del server.

                Console.WriteLine("Socket connected to {0}",
                    clientSocket.RemoteEndPoint.ToString());

                // codifica la stringa in un array di byte  
                byte[] msg = Encoding.ASCII.GetBytes("Get GPS datas");

                // invia msg al server 
                int bytesSent = clientSocket.Send(msg);

                // riceve la risposta  
                int bytesRec = clientSocket.Receive(bytes);
                Console.WriteLine("Echoed test = {0}",
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // chiude la socket  
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            new TCPClient();
        }
    }
}
