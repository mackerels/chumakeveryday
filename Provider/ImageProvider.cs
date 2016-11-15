using System.Drawing;
using CoreSandbox.Config;

namespace CoreSandbox.Provider
{
    public class ImageProvider
    {
        public static Image GetImage()
        {
            var original = Image.FromFile(Configurator.Config.Image);
            var generated = new Bitmap(original.Width, original.Height);

            using (var graph = Graphics.FromImage(generated))
            {
                graph.DrawImage(original, 0, 0);
                graph.Save();
            }

            return generated;
        }
    }
}