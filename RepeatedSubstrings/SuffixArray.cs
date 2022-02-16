using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberDojo.RepeatedSubstrings;

public static class SuffixArray
{
    public static int GetRepeatedSubstringAmount(this string input)
    {
        return input.GetRepeatSubstrings().Count();
    }

    public static IEnumerable<string> GetRepeatSubstrings(this string input)
    {
        var commonPrefixes = input.GetCommonPrefixes();
        return commonPrefixes
            .SelectMany(x => x.GetAllSubstringCombinations()).GroupBy(x => x)
            .Select(x => x.First());
    }

    public static IEnumerable<int> GetSuffixArray(this string input)
    {
        return input.Select((_, i) => (index: i, substring: input[i..]))
            .OrderBy(x => x.substring, StringComparer.Ordinal).Select((tuple) => tuple.index);
    }

    public static IEnumerable<string> GetCommonPrefixes(this string input)
    {
        var lcp = input.GetSuffixArray().ToArray();
        /* aab, aabaab, ab, abaab, b, baab
            * Suffix Array: 3, 0, 4, 1, 5, 2 
         */
        var result = Array.Empty<string>();
        for (var i = 1; i < lcp.Length; i++)
        {
            var prefix = GetCommonPrefix(input, lcp[i - 1], lcp[i]);
            if (prefix.Length > 0)
            {
                result = result.Append(prefix).ToArray();
            }
        }

        return result;
    }

    public static string GetCommonPrefix(this string input, int index1, int index2)
    {
        var substring1 = input[index1..];
        var substring2 = input[index2..];
        var result = new char[] { };
        for (var i = 0; i < Math.Min(substring1.Length, substring2.Length); i++)
        {
            if (substring1[i] != substring2[i]) return new string(result);
            result = result.Append(substring1[i]).ToArray();
        }

        return new string(result);
    }

    public static IEnumerable<string> GetAllSubstringCombinations(this string input)
    {
        // "aab"
        // new string[] { "b", "ab", "aab", "a", "aa", "a"};
        var result = Array.Empty<string>();
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = i; j < input.Length; j++)
            {
                result = result.Append(input[i..(j + 1)]).ToArray();
            }
        }

        return result;
    }
}