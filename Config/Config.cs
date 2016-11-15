using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSandbox.Config
{
    public struct Config
    {
        public int Port;

        public int VerticalMargin;
        public int LinesNumber;
        public int LineWidth;
        public int LineSpacing;
        public int Outline;

        public string Font;
        public string Image;
        public string QuoteProvider;
    }
}
