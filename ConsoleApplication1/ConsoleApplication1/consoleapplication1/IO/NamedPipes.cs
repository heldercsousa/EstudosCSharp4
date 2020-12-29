using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;

namespace EstudosCSharp.IO
{
    public static class NamedPipes
    {
        private static readonly string PIPE = "PipeSample";
        
        public static void Sample(string clientServer=null)
        {
            if (!string.IsNullOrEmpty(clientServer))
                Client(clientServer);
            else
                Server();
        }

        private static void Client(string server)
        {
            using (var client = new NamedPipeClientStream(server, PIPE, PipeDirection.In))
            {
                Console.Write("Connecting to { 0}... ", server);
                client.Connect();
                Console.WriteLine("Connected");
                using (var reader = new StreamReader(client))
                {
                    var message = reader.ReadLine();
                    Console.WriteLine("Received message: " +message);
                }
            }
        }

        private static void Server()
        {
            using (var server = new NamedPipeServerStream(PIPE, PipeDirection.Out))
            {
                Console.Write("Waiting for connection... ");
                server.WaitForConnection();
                Console.WriteLine("Connected");
                using (var writer = new StreamWriter(server))
                {
                    Console.Write("Enter message: ");
                    var message = Console.ReadLine();
                    writer.WriteLine(message);
                }
            }
        }

        //  parei na A Primer to (Named) Pipes- 1423
    }
}
