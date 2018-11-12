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
            //IPEndPoint direccion = new IPEndPoint(IPAddress.Any, 5879);  "127.0.0.1"
            IPEndPoint direccion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1988);
            try
            {
                miPrimerSocket.Bind(direccion);
                miPrimerSocket.Listen(1);

                Console.WriteLine("Ecuchando servicio ...");
                Socket Escuchar = miPrimerSocket.Accept();

                if (Escuchar.Connected)
                {
                    Console.WriteLine("Cliente conectado");
                }

                byte[] byData = new byte[1024];
                var value = Escuchar.Receive(byData);
                var resultado = Encoding.UTF8.GetString(byData);

                Console.WriteLine("Conectado con exito: " + resultado);


                miPrimerSocket.Send(Encoding.UTF8.GetBytes("Aqui te mando otros datos"));

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
