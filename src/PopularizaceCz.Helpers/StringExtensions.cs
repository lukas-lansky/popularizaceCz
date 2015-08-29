using System.Collections.Generic;
using System.Linq;

namespace PopularizaceCz.Helpers
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhitespace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
        
        public static string JoinToString<T>(this IEnumerable<T> objects, string delimiter = ",")
        {
            return string.Join(delimiter, objects.Select(s => s.ToString()));
        }

        public static string FormatWith(this string s, params object[] ps)
        {
            return string.Format(s, ps);
        }
    }
}