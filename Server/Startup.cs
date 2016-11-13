using System.Drawing.Imaging;
using System.Threading.Tasks;
using CoreSandbox.Factory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CoreSandbox.Server
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app) => app.Run(Handler);

        private Task Handler(HttpContext context) => Task.Run(async () =>
        {
            context.Response.ContentType = "image/jpeg";

            using (var image = await ImageFactory.GenerateChumak())
            {
                image.Save(context.Response.Body, ImageFormat.Jpeg);
            }
        });
    }
}