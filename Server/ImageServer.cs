using CoreSandbox.Config;
using Microsoft.AspNetCore.Hosting;

namespace CoreSandbox.Server
{
    public class ImageServer
    {
        private readonly IWebHost _host;

        public ImageServer()
        {
            _host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls($"http://localhost:{Configurator.Config.Port}")
                .Build();
        }

        public void Run() => _host.Run();
    }
}