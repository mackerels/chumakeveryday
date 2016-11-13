using System.Collections.Generic;

namespace CoreSandbox.Utils
{
    public static class Extensions
    {
        public static IEnumerable<string> SplitByLength(this string str, int maxLength)
        {
            var index = 0;
            while (index + maxLength < str.Length)
            {
                yield return str.Substring(index, maxLength);
                index += maxLength;
            }

            yield return str.Substring(index);
        }
    }
}