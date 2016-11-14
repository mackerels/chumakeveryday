using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using CoreSandbox.Provider;
using CoreSandbox.Provider.Quote;
using CoreSandbox.Utils;
using System.Drawing.Drawing2D;

namespace CoreSandbox.Factory
{
    public static class ImageFactory
    {
        private static readonly Font Font;
        private static readonly Brush TextColor;
        private static readonly StringFormat LineFormat;
        private static readonly Pen TextOutline;
        private static readonly int VerticalMargin = 90;
        private static readonly int LinesNumber = 4;
        private static readonly int LineWidth = 32;
        private static readonly int LineSpacing = 6;
        private static PrivateFontCollection _privateFontCollection;
        private static int _imageWidth;
        private static int _imageHeight;

        static ImageFactory()
        {
            _privateFontCollection = new PrivateFontCollection();
            _privateFontCollection.AddFontFile(@"Fonts/UbuntuMono-R.ttf");

            Font = new Font(_privateFontCollection.Families[0], 48);

            TextColor = Brushes.AntiqueWhite;

            LineFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
           
            TextOutline = new Pen(Brushes.Black, 2)
            {
                LineJoin = LineJoin.Round
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

        private static async Task WriteText(Graphics context)
        {
            Quote quote;

            do
            {
                quote = await QuotesProvider.GetRandomQuote();
            } while (quote.Text.Length > LinesNumber*LineWidth);

            var chunks = quote.Text.SplitByLength(LineWidth).Reverse().ToArray();

            GraphicsPath graphicsPath = new GraphicsPath();
            
            context.SmoothingMode = SmoothingMode.AntiAlias;
            context.PixelOffsetMode = PixelOffsetMode.HighQuality;

            for (var i = 0; i < chunks.Length; i++)
            {
                graphicsPath.AddString(chunks[i], Font.FontFamily, (int)Font.Style, Font.Size,
                    new Point(_imageWidth / 2, (int)(_imageHeight - VerticalMargin - i * Font.Size + LineSpacing)), 
                    LineFormat);
            }

            context.DrawPath(TextOutline, graphicsPath);
            context.FillPath(TextColor, graphicsPath);

            context.Save();
        }
    }
}