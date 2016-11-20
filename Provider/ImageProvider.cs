using System.Drawing;
using CoreSandbox.Config;

namespace CoreSandbox.Provider
{
    public class ImageProvider
    {
        public static Image GetImage() => Image.FromFile(Configurator.Config.Image);
    }
}