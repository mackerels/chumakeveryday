using System;
using System.Text;
using chumakeveryday.Server;

namespace chumakeveryday
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