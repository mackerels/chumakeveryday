using System.Collections.Generic;
using System.Text;

namespace CoreSandbox.Utils
{
    public static class Extensions
    {
        public static IEnumerable<string> SplitByLength(this string str, int maxLength)
        {
            var words = str.Split(' ');
            var line = string.Empty;

            foreach (var word in words)
            {
                if ((line.Length + word.Length) >= maxLength)
                {
                    yield return line;
                    line = string.Empty;
                }

                line += string.Format("{0}{1}", line.Length > 0 ? " " : "", word);
            }

            yield return line;
        }
    }
}