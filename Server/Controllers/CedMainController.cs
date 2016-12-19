using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using chumakeveryday.Factory;
using Microsoft.AspNetCore.Mvc;

namespace chumakeveryday.Server.Controllers
{
    public class CedMainController : Controller
    {
        private readonly IImageFactory _factory;

        public CedMainController(IImageFactory factory)
        {
            _factory = factory;
        }

        [HttpGet("/random")]
        public async Task<IActionResult> Random()
        {
            return ReturnImage(await _factory.GenerateRandom());
        }

        [HttpGet("/daily")]
        public async Task<IActionResult> Daily()
        {
            return ReturnImage(await _factory.GenerateDaily());
        }

        private FileStreamResult ReturnImage(Image image)
        {
            var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;

            return File(stream, "image/jpeg");
        }
    }
}