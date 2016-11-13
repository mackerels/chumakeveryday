using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CoreSandbox.Provider;
using CoreSandbox.Utils;
using CoreSandbox.Provider.Quote;

namespace CoreSandbox.Factory
{
    public static class ImageFactory
    {
        private static readonly Font Font;
        private static readonly Brush TextColor;
        private static readonly StringFormat LineFormat;
        private static readonly int VerticalMargin = 100;
        private static readonly int LinesNumber = 5;
        private static readonly int LineWidth = 30;
        private static int ImageWidth;
        private static int ImageHeight;    

        static ImageFactory()
        { 
            Font = new Font(FontFamily.GenericMonospace, 32);
            TextColor = Brushes.AntiqueWhite;
            LineFormat = new StringFormat();
            LineFormat.LineAlignment = StringAlignment.Center;
            LineFormat.Alignment = StringAlignment.Center;
        }

        public static async Task<Image> GenerateChumak()
        {
            var img = ImageProvider.GetImage();
            ImageWidth = img.Width;
            ImageHeight = img.Height;

            using (var context = Graphics.FromImage(img))
            {
                await WriteText(context);
                return img;
            }
        }

        private static async Task WriteText(Graphics context)
        {
            Quote quote;

            do
            {
                quote = await QuotesProvider.GetRandomQuote();
            }
            while (quote.Text.Length > LinesNumber * LineWidth);

            var chunks = quote.Text.SplitByLength(LineWidth).Reverse().ToArray();

            for (var i = 0; i < chunks.Length; i++)
            {
                context.DrawString(chunks[i], Font, TextColor, ImageWidth/2,
                    ImageHeight - VerticalMargin - i*Font.Size, LineFormat);
            }

            context.Save();
        }
    }
}