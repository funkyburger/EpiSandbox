using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace EpiSandbox.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAllOrSomeWords(this string str, string words)
        {
            foreach(var word in words.Split(' '))
            {
                if (str.ToLower().Contains(word.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        public static string HtmlBoldAllOrSomeWords(this string str, string words)
        {
            var text = str.Replace("<strong>", "").Replace("</strong>", "");

            var builder = new StringBuilder();
            var toBold = new List<Tuple<int, int>>();
            var cursor = 0;

            foreach (var word in words.Split(' '))
            {
                foreach(var index in text.IndexesOfAll(word))
                {
                    toBold.Add(new Tuple<int, int>(index, index + word.Length));
                }
            }

            foreach(var spaceToBold in toBold.OrderBy(t => t.Item1))
            {
                builder.Append(text.Substring(cursor, spaceToBold.Item1 - cursor));
                builder.Append($"<strong>{text.Substring(spaceToBold.Item1, spaceToBold.Item2 - spaceToBold.Item1)}</strong>");
                cursor = spaceToBold.Item2;
            }

            if(cursor < text.Length)
            {
                builder.Append(text.Substring(cursor, text.Length - cursor));
            }

            return builder.ToString();
        }

        public static string HtmlBoldWords(this string str, IEnumerable<string> words)
        {
            var text = str.Replace("<strong>", "").Replace("</strong>", "");
            var nonWordRegex = new Regex("\\W");

            var builder = new StringBuilder();
            var toBold = new List<Tuple<int, int>>();
            var cursor = 0;

            foreach (var fragment in words.Select(w => "(?i)" + nonWordRegex.Replace(w, "\\W+")))
            {
                var tempRegex = new Regex(fragment);

                foreach(Match match in tempRegex.Matches(text))
                {
                    toBold.Add(new Tuple<int, int>(match.Index, match.Index + match.Length));
                }
            }

            foreach (var spaceToBold in toBold.OrderBy(t => t.Item1))
            {
                builder.Append(text.Substring(cursor, spaceToBold.Item1 - cursor));
                builder.Append($"<strong>{text.Substring(spaceToBold.Item1, spaceToBold.Item2 - spaceToBold.Item1)}</strong>");
                cursor = spaceToBold.Item2;
            }

            if (cursor < text.Length)
            {
                builder.Append(text.Substring(cursor, text.Length - cursor));
            }

            return builder.ToString();
        }

        public static string CapLength(this string str, int length)
        {
            if(str.Length < length)
            {
                return str;
            }

            var text = str.Substring(0, length);
            return text.Substring(0, text.LastIndexOf(' '));
        }

        public static IEnumerable<int> IndexesOfAll(this string str, string value)
        {
            var cursor = 0;
            while(cursor >= 0 && cursor < str.Length)
            {
                cursor = str.ToLower().IndexOf(value.ToLower(), cursor);
                if(cursor >= 0)
                {
                    yield return cursor;
                    cursor++;
                }
            }
        }
    }
}