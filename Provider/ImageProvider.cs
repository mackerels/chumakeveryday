using System.Drawing;
using System.IO;
using chumakeveryday.Config;

namespace chumakeveryday.Provider
{
    public static class ImageProvider
    {
        private static readonly byte[] ImageBytes;

        static ImageProvider()
        {
            ImageBytes = File.ReadAllBytes(Configurator.Config.Image);
        }

        public static Image GetImage()
        {
            using (var ms = new MemoryStream(ImageBytes))
            {
                return Image.FromStream(ms);
            }
        }
    }
}