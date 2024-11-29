using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace infraedgeTest
{
    public static class utilityFunctions
    {
        public static Dictionary<string, int> GetUniqueWords(string text)
        {
            text = Regex.Replace(text, @"[\{\<].*?[\}\>]", "");

            text = Regex.Replace(text, @"''(.*?)''", "$1");  // Remove single quotes around words

            text = Regex.Replace(text, @"<.*?>", "");  // Remove any HTML-like tags

            text = Regex.Replace(text, @"={3,}(.*?)={3,}", "$1");  // Remove leading/trailing equal signs around headings (keep the words)

            text = Regex.Replace(text, @"\[\[([^\]]+)\]\]", match =>
            {
                return match.Groups[1].Value.Replace(" ", " ");
            });

            text = Regex.Replace(text, @"(\w+)-(\w+)", "$1 $2"); // Split hyphenated words

            text = Regex.Replace(text, @"\[[^\]]*\]", "");

            IEnumerable<string> words = text
                .ToLower()
                .Split(new[] { ' ', '\n', '\r', '\t', ',', '.', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.Trim('\'', '\"', '.', ',', ';', ':', '!', '?', '(', ')', '[', ']'))
                .Where(word => word.All(Char.IsLetter));

            Dictionary<string, int> wordCount = words
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count());

            return wordCount;
        }

    }
}