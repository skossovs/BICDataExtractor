using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BIC.Utils
{
    public static class RegExSimple
    {
        public static string ApplyRegex(this string original, string pattern)
        {
            Regex r = new Regex(pattern);

            if (r.IsMatch(original))
                return r.Match(original).Value;
            else
                return string.Empty;
        }

        // TODO: Group theory application is needed.
        public static string RegexGetSqueeze(this string original, string beginPattern, string endPattern)
        {
            Regex rBegin      = new Regex(beginPattern);
            Regex rEnd        = new Regex(endPattern);
            Regex rEverything = new Regex(beginPattern + ".*" + endPattern);
            string everything = rEverything.Match(original).Value;
            string begin      = rBegin.Match(everything).Value;
            string end        = rEnd.Match(everything).Value;

            everything = everything.RemoveFirst(begin);
            everything = everything.RemoveLast(end);

            return everything;
        }

        private static string RemoveFirst(this string text, string search)
        {
            int i = 0;
            do
            {
                if (text[i] != search[i])
                    break;
                i++;
            }
            while (i < search.Length);

            return text.Substring(i);
        }
        private static string RemoveLast(this string text, string search)
        {
            return text.Substring(0, text.Length - search.Length);
        }
    }
}
