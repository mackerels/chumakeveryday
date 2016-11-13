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
                .UseUrls("http://localhost:11000")
                .Build();
        }

        public void Run() => _host.Run();
    }
}