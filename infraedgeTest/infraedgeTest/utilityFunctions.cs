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
            // Remove curly braces and angle brackets with their content
            text = Regex.Replace(text, @"[\{\<].*?[\}\>]", "");

            // Remove single square brackets but keep content within double square brackets
            text = Regex.Replace(text, @"(?<!\[)\[(?!\[).*?(?<!\])\](?!\])", "");

            IEnumerable<string> words = text
                .ToLower()
                .Split(new[] { ' ', '\n', '\r', '\t', ',', '.', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.Trim('\'', '\"')) // Remove surrounding single or double quotes
                .Where(word => word.All(Char.IsLetter));

            Dictionary<string, int> wordCount = words.GroupBy(word => word)
                                 .ToDictionary(group => group.Key, group => group.Count());

            return wordCount;


        }
    }
}