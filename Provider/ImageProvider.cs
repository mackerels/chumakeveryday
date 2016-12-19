using System.Drawing;
using chumakeveryday.Config;

namespace chumakeveryday.Provider
{
    public class ImageProvider
    {
        public static Image GetImage() => Image.FromFile(Configurator.Config.Image);
    }
}