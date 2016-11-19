using System;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreSandbox.Provider;
using CoreSandbox.Provider.Quote;
using CoreSandbox.Utils;

namespace CoreSandbox.Factory
{
    public static class ImageFactory
    {
        private static readonly Font Font;
        private static readonly StringFormat LineFormat;
        private static readonly int VerticalMargin = 100;
        private static readonly int LinesNumber = 4;
        private static readonly int LineWidth = 30;
        private static readonly int LineSpacing = 15;
        private static readonly int Outline = 2;
        private static readonly PrivateFontCollection _privateFontCollection;
        private static int _imageWidth;
        private static int _imageHeight;

        static ImageFactory()
        {
            _privateFontCollection = new PrivateFontCollection();
            _privateFontCollection.AddFontFile(@"Fonts/UbuntuMono-R.ttf");

            Font = new Font(_privateFontCollection.Families[0], 32);

            LineFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
        }

        public static async Task<Image> GenerateChumak()
        {
            var img = ImageProvider.GetImage();
            _imageWidth = img.Width;
            _imageHeight = img.Height;

            using (var context = Graphics.FromImage(img))
            {
                await WriteText(context);
                return img;
            }
        }

        public static async Task<Image> GenerateDailyChumak()
        {
            if (!File.Exists("metadata.chumak"))
            {
                File.WriteAllText("metadata.chumak", DateTime.Now.ToBinary().ToString());
            }

            if (!File.Exists("Images/daily.jpg"))
            {
                (await GenerateChumak()).Save("Images/daily.jpg");
            }

            var metadate = DateTime.FromBinary(long.Parse(File.ReadAllText("metadata.chumak")));

            if ((DateTime.Now - metadate).Days >= 1)
            {
                File.WriteAllText("metadata.chumak", DateTime.Now.ToBinary().ToString());
                (await GenerateChumak()).Save("Images/daily.jpg");
            }

            return Image.FromFile("Images/daily.jpg");
        }

        private static async Task WriteText(Graphics context)
        {
            Quote quote;

            do
            {
                quote = await QuotesProvider.GetRandomQuote();
            } while (quote.Text.Length > LinesNumber*LineWidth);

            var chunks = quote.Text.SplitByLength(LineWidth).Reverse().ToArray();

            for (var i = 0; i < chunks.Length; i++)
            {
                context.DrawOutlinedString(chunks[i], Font, LineFormat, Brushes.Black, Brushes.White,
                    new PointF(_imageWidth/2, _imageHeight - VerticalMargin - i*(Font.Size + LineSpacing)), Outline);
            }

            context.Save();
        }
    }
}