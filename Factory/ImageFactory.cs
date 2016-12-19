using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using chumakeveryday.Config;
using chumakeveryday.Provider;
using chumakeveryday.Provider.Quote;
using chumakeveryday.Utils;

namespace chumakeveryday.Factory
{
    public static class ImageFactory
    {
        private static readonly Font Font;
        private static readonly StringFormat LineFormat;
        private static readonly PrivateFontCollection _privateFontCollection;
        private static int _imageWidth;
        private static int _imageHeight;

        static ImageFactory()
        {
            _privateFontCollection = new PrivateFontCollection();
            _privateFontCollection.AddFontFile(Configurator.Config.Font);

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
            var config = Configurator.Config;

            do
            {
                quote = await QuotesProvider.GetRandomQuote();
            } while (quote.Text.Length > config.LinesNumber*config.LineWidth);

            var chunks = quote.Text.SplitByLength(config.LineWidth).Reverse().ToArray();

            for (var i = 0; i < chunks.Length; i++)
            {
                context.DrawOutlinedString(chunks[i], Font, LineFormat, Brushes.Black, Brushes.White,
                    new PointF(_imageWidth/2, _imageHeight - config.VerticalMargin - i*(Font.Size + config.LineSpacing)),
                    config.Outline);
            }

            context.Save();
        }
    }
}