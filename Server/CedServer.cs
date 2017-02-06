using System.IO;
using chumakeveryday.Config;
using Microsoft.AspNetCore.Hosting;

namespace chumakeveryday.Server
{
    public class CedServer
    {
        private readonly IWebHost _host;

        public CedServer()
        {
            _host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseContentRoot(Path.Combine(Directory.GetCurrentDirectory(), "frontend"))
                .UseUrls($"http://localhost:{Configurator.Config.Port}")
                .Build();
        }

        public void Run() => _host.Run();
    }
}