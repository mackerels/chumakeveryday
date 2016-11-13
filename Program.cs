using System;
using System.Text;
using CoreSandbox.Server;

namespace CoreSandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var server = new ImageServer();
            server.Run();
        }
    }
}