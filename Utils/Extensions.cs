using System.Collections.Generic;
using System.Text;

namespace CoreSandbox.Utils
{
    public static class Extensions
    {
        public static IEnumerable<string> SplitByLength(this string str, int maxLength)
        {
            var words = str.Split(' ');
            var line = new StringBuilder();

            foreach (var word in words)
            {
                if ((line.Length + word.Length) >= maxLength)
                {
                    yield return line.ToString();
                    line = new StringBuilder();
                }
    
                line.AppendFormat("{0}{1}", (line.Length > 0) ? " " : "", word);
            }

            yield return line.ToString();
        }
    }
}