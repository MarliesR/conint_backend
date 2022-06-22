using System;
using System.Threading.Tasks;

namespace conint.server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server starting...");
            Server server = new Server();
            server.Start();
            Console.WriteLine("Listening to connections");
            Task listen = Server.HandleConnections();
            listen.GetAwaiter().GetResult();
            server.Close();
        }
    }
}
