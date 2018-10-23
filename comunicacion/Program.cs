using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace comunicacion
{
    class Program
    {
        public static void Conectar()
        {
            Socket miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direccion = new IPEndPoint(IPAddress.Any, 5879);

            try
            {
                miPrimerSocket.Bind(direccion);
                miPrimerSocket.Listen(1);

                Console.WriteLine("Ecuchando servicio ...");
                Socket Escuchar = miPrimerSocket.Accept();

                byte[] byData = new byte[1024];
                var value = Escuchar.Receive(byData);
                var resultado = System.Text.Encoding.ASCII.GetString(byData);

                Console.WriteLine("Conectado con exito: " + resultado);
                miPrimerSocket.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString());
            }

            Console.WriteLine("Presione cualquier tecla para terminar");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            Conectar();
        }
    }
}
