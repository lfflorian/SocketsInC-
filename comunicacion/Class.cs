using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace comunicacion
{
    class Class
    {
        Socket miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Class()
        {
            miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direccion = new IPEndPoint(IPAddress.Parse("172.11.23.78"), 5879);
            //IPAddress.Any  IPAddress.Parse("172.11.23.78")
            try
            {
                miPrimerSocket.Bind(direccion);
                miPrimerSocket.Listen(1);

                Console.WriteLine("Ecuchando servicio ...");
                Socket Escuchar = miPrimerSocket.Accept();
                Console.WriteLine("Conectado con exito");
                miPrimerSocket.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString());
            }

            Console.WriteLine("Presione cualquier tecla para terminar");
            Console.ReadLine();
        }
    }
}
