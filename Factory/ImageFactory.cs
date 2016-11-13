using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CoreSandbox.Provider;
using CoreSandbox.Utils;

namespace CoreSandbox.Factory
{
    public class ImageFactory
    {
        private static readonly Font Font;
        private static readonly int Width = 36;

        static ImageFactory()
        {
            Font = new Font(FontFamily.GenericMonospace, 32);
        }

        public static async Task<Image> GenerateChumak()
        {
            var img = ImageProvider.GetImage();

            using (var context = Graphics.FromImage(img))
            {
                await WriteText(context);
                return img;
            }
        }

        private static async Task WriteText(Graphics context)
        {
            var quote = await QuotesProvider.GetRandomQuote();

            var chunks = quote.Text.SplitByLength(Width).Reverse().ToArray();

            for (var i = 0; i < chunks.Length; i++)
            {
                context.DrawString(chunks[i], Font, Brushes.AntiqueWhite, 0, 860 - i*32);
            }

            context.Save();
        }
    }
}