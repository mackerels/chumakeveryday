using System;
using System.Drawing;
using System.Drawing.Imaging;
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
    public class ImageFactory : IImageFactory
    {
        private readonly Font _font;
        private readonly StringFormat _lineFormat;
        private int _imageWidth;
        private int _imageHeight;

        private const string DailyPath = "Images/daily.jpg";
        private const string MetaPath = "metadata.chumak";

        private Image _daily;

        public ImageFactory()
        {
            var privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile(Configurator.Config.Font);

            _font = new Font(privateFontCollection.Families[0], 32);

            _lineFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
        }

        public async Task<Image> GenerateRandom()
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

        public async Task<Image> GenerateDaily()
        {
            if (!File.Exists(MetaPath))
            {
                File.WriteAllText(MetaPath, DateTime.Now.ToBinary().ToString());
            }

            if (!File.Exists(DailyPath))
            {
                _daily = await GenerateRandom();
                _daily.Save(DailyPath);
            }

            if (_daily == null && File.Exists(DailyPath))
            {
                _daily = Image.FromFile(DailyPath);
            }

            var metadate = DateTime.FromBinary(long.Parse(File.ReadAllText(MetaPath)));

            if ((DateTime.Now - metadate).Days >= 1)
            {
                File.WriteAllText(MetaPath, DateTime.Now.ToBinary().ToString());

                _daily = await GenerateRandom();
                _daily.Save(DailyPath);
            }

            return _daily;
        }

        private async Task WriteText(Graphics context)
        {
            Quote quote;
            var config = Configurator.Config;

            do
            {
                quote = await QuotesProvider.GetRandomQuote();
            } while (quote.Text.Length > config.LinesNumber * config.LineWidth);

            var chunks = quote.Text.SplitByLength(config.LineWidth).Reverse().ToArray();

            for (var i = 0; i < chunks.Length; i++)
            {
                context.DrawOutlinedString(chunks[i], _font, _lineFormat, Brushes.Black, Brushes.White,
                    new PointF(_imageWidth / 2,
                        _imageHeight - config.VerticalMargin - i * (_font.Size + config.LineSpacing)),
                    config.Outline);
            }

            context.Save();
        }
    }
}