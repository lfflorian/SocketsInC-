using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppInver
{
    class Program
    {
        private static int portNum = 2020;
        delegate void SetTextCallback(string text);
        static TcpClient client;
        static NetworkStream ns;
        static Thread t = null;
        private const string hostName = "localhost";

        static void Main(string[] args)
        {
            // Crea un hilo para que permita seguir utilizando el programa 
            Thread piThread = new Thread(new ThreadStart(Init));
            piThread.Start();

            var escritura = Console.ReadLine();
            Recursivo(escritura);
        }

        private static void Recursivo(string escritura)
        {
            if (escritura.ToUpper() != "N")
            {
                Console.WriteLine("Escriba cualquier caracter para obtener datos");
                SolicitarDatos();
                var r = Console.ReadLine();
                Recursivo(r);
            }
            else
                Console.ReadKey();
        }
        
        public static void Init()
        {
            // setea los datos del socket a conectar
            client = new TcpClient(hostName, portNum);
            ns = client.GetStream();
            String s = "Connected";
            byte[] byteTime = Encoding.ASCII.GetBytes(s);
            ns.Write(byteTime, 0, byteTime.Length);
            t = new Thread(DoWork);
            t.Start();
        }
        
        private static void SolicitarDatos()
        {
            //envia una solicitud al servidor para que devuelca los datos
            byte[] byteTime = Encoding.ASCII.GetBytes("Solicitud de datos");
            ns.Write(byteTime, 0, byteTime.Length);
        }
        
        public static void DoWork()
        {
            // este se ejecuta al recibir los datos del servidor
            byte[] bytes = new byte[1024];
            while (true)
            {
                int bytesRead = ns.Read(bytes, 0, bytes.Length);
                SetText(Encoding.ASCII.GetString(bytes, 0, bytesRead));
            }
        }

        private static void SetText(string text)
        {
            //escribe los datos recibidos
            Console.WriteLine(text);
        }
    }
}
