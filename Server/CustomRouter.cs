using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using CoreSandbox.Factory;
using Microsoft.AspNetCore.Http;

namespace CoreSandbox.Server
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
                    break;
                default:
                    break;
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
