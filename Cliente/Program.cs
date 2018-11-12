using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            Conectar();
        }

        public static void Conectar()
        {
            Socket miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint miDireccion = new IPEndPoint(IPAddress.Parse("172.11.23.78"), 5879);

            try
            {
                miPrimerSocket.Connect(miDireccion); // Conectamos               
                Console.WriteLine("Conectado con exito");
                //Envío de datos
                byte[] byData = System.Text.Encoding.UTF8.GetBytes("Esto es un envío de datos al server");
                miPrimerSocket.Send(byData);

                //Espera a recibir datos
                miPrimerSocket.Accept();
                byte[] otroDAta = new byte[1024];
                var value = miPrimerSocket.Receive(otroDAta);
                var resultado = Encoding.UTF8.GetString(otroDAta);

                //Escribe resultados recibidos
                Console.WriteLine("Resultado: " + resultado);

                miPrimerSocket.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}",error.ToString());
            }
            Console.WriteLine("Presione cualquier tecla para terminar");
            Console.ReadLine();
        }
    }
}
