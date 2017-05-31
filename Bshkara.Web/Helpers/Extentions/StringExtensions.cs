using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bshkara.Web.Helpers.Extentions
{
    public static class StringExtensions
    {
        public static string TrimLongWords(this string original, int maxCount)
        {
            return Regex.Replace(original, string.Format(@"[\w]{{{0},}}", maxCount),
                m => { return m.Value.Substring(0, maxCount - 1) + "..."; });
        }

        public static List<string> CommaSeparatedToList(this string value)
        {
            if (value == null)
                return new List<string>();

            return value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static bool IsValidEmail(this string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool HasImageExtesion(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return str.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase)
                   || str.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase)
                   || str.EndsWith(".bmp", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool isLink(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return str.StartsWith("<a") && str.EndsWith("</a>");
        }

        public static string Right(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }

        public static string Left(this string source, int length)
        {
            if (source.Length <= length)
            {
                return source;
            }
            return source.Substring(0, length) + " ...";
        }
    }
}