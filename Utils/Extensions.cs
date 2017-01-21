using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace chumakeveryday.Utils
{
    public static class Extensions
    {
        public static IEnumerable<string> SplitByLength(this string str, int maxLength)
        {
            var words = str.Split(' ');
            var line = new StringBuilder();

            foreach (var word in words)
            {
                if (line.Length + word.Length >= maxLength)
                {
                    yield return line.ToString();
                    line.Clear();
                }

                line.AppendFormat("{0}{1}", line.Length > 0 ? " " : "", word);
            }

            yield return line.ToString();
        }

        public static void DrawOutlinedString(
            this Graphics context,
            string text,
            Font font,
            StringFormat format,
            Brush outlineBrush,
            Brush textBrush,
            PointF point
        )
        {
            var outliner = new Action<int, int>((x, y) =>
                context.DrawString(text,
                    font,
                    outlineBrush,
                    point.X + x,
                    point.Y + y,
                    format
                ));

            outliner(+2, +2);
            outliner(+0, +3);
            outliner(-2, +2);
            outliner(-3, +0);
            outliner(-2, -2);
            outliner(+0, -3);
            outliner(+2, -2);
            outliner(+3, +0);

            context.DrawString(text, font, textBrush, point, format);
        }
    }
}