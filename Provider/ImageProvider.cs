using System.Drawing;

namespace CoreSandbox.Provider
{
    public class ImageProvider
    {
        public static Image GetImage() => Image.FromFile("Images/chumak.jpg");
    }
}