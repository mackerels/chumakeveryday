using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using chumakeveryday.Factory;
using Microsoft.AspNetCore.Http;

namespace chumakeveryday.Server
{
    public class CustomRouter
    {
        public async Task Handle(HttpContext context)
        {
            switch (context.Request.Path)
            {
                case "/":
                    await Random(context.Response);
                    break;
                case "/daily":
                    await Daily(context.Response);
                    break;
                case "/portal":
                    await Portal(context.Response);
                    break;
                default:
                    await Nothing(context.Response);
                    break;
            }
        }

        public async Task Nothing(HttpResponse response)
        {
            response.ContentType = "text/html";

            using (var writer = new StreamWriter(response.Body))
            {
               await writer.WriteLineAsync("Something goes wrong.");
            }
        }

        public async Task Portal(HttpResponse response)
        {
            response.ContentType = "text/html";

            using (var stream = new FileStream("Views/chumak.html", FileMode.Open))
            {
                await stream.CopyToAsync(response.Body);
            }
        }

        public async Task Random(HttpResponse response)
        {
            response.ContentType = "image/jpeg";

            using (var image = await ImageFactory.GenerateChumak())
            {
                image.Save(response.Body, ImageFormat.Jpeg);
            }
        }

        public async Task Daily(HttpResponse response)
        {
            response.ContentType = "image/jpeg";

            using (var image = await ImageFactory.GenerateDailyChumak())
            {
                image.Save(response.Body, ImageFormat.Jpeg);
            }
        }
    }
}
