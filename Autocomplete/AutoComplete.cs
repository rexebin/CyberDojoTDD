using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberDojo.Autocomplete;

public static class AutoComplete
{
    public static IEnumerable<string> FilterByKeyword(this IEnumerable<string> source,
        string keyword,
        SuffixArrayResult suffixArrayResult)
    {
        var sourceTerms = source.ToArray();
        var result = suffixArrayResult
            .FindMatchingSuffixes(keyword.ToLower())
            .ToArray();
        var indies = result.Select(i => suffixArrayResult.SuffixArray[i].TermIndex);
        return indies.Select(i => sourceTerms[i]);
    }

    public static SuffixArrayResult GetSuffixArray(this IEnumerable<string> input)
    {
        var concatenateToString = input.ConcatenateToString();
        var suffixArray = concatenateToString.ToLower()
            .GetSuffixes()
            .RemoveSuffixesStartingWithDollar()
            .Sort()
            .GetSuffixArray().ToArray();
        return new SuffixArrayResult(suffixArray, concatenateToString);
    }

    internal static IEnumerable<int> FindMatchingSuffixes(
        this SuffixArrayResult suffixArrayResult, string search)
    {
        var matchingIndex =
            suffixArrayResult.SuffixArray.FindMatchingIndex(suffixArrayResult.ConcatenatedString,
                search);
        return matchingIndex == -1
            ? Enumerable.Empty<int>()
            : GetAdjacentMatchingSuffixes(suffixArrayResult, matchingIndex, search);
    }

    private static IEnumerable<int> GetAdjacentMatchingSuffixes(
        SuffixArrayResult suffixArrayResult, int matchingIndex,
        string search)
    {
        var result = new[] { matchingIndex };
        var arrayItems = suffixArrayResult.SuffixArray;
        var previousIndex = matchingIndex;
        while (true)
        {
            previousIndex -= 1;
            if (previousIndex < 0) break;
            var substring =
                suffixArrayResult.ConcatenatedString[arrayItems[previousIndex].WordIndex..];
            if (!substring.StartsWith(search)) break;

            result = result.Append(previousIndex).ToArray();
        }

        var nextIndex = matchingIndex;

        while (true)
        {
            nextIndex += 1;
            if (nextIndex >= arrayItems.Length) break;
            var substring = suffixArrayResult.ConcatenatedString[arrayItems[nextIndex].WordIndex..];
            if (!substring.StartsWith(search)) break;

            result = result.Append(nextIndex).ToArray();
        }

        return result;
    }

    internal static int FindMatchingIndex(
        this IEnumerable<SuffixArrayItem> suffixArray, string input, string search)
    {
        var suffixArrayItems = suffixArray.ToArray();
        var low = 0;
        var high = suffixArrayItems.Length - 1;
        SuffixArrayItem? result = null;
        while (result == null && low <= high)
        {
            var middle = low + (high - low) / 2;
            var middleSubstring = input[suffixArrayItems[middle].WordIndex..];
            if (middleSubstring.StartsWith(search))
            {
                return middle;
            }

            if (String.CompareOrdinal(middleSubstring, search) < 0) // less
            {
                low = middle + 1;
            }
            else
            {
                high = middle - 1;
            }
        }

        return -1;
    }

    internal static IEnumerable<SuffixArrayItem> GetSuffixArray(this IEnumerable<SuffixItem> input)
    {
        return input.Select(x => new SuffixArrayItem(x.Index, x.TermIndex));
    }

    internal static string ConcatenateToString(this IEnumerable<string> input)
    {
        return input.Aggregate("",
            (current, word) => String.IsNullOrEmpty(current) ? $"{word}$" : $"{current}{word}$");
    }

    internal static IEnumerable<SuffixItem> GetSuffixes(this string input)
    {
        var i = 0;
        return input.Select((x, index) =>
        {
            if (x == '$')
            {
                i++;
            }

            var suffix = input[index..];
            return new SuffixItem(suffix, i, index);
        });
    }

    internal static IEnumerable<SuffixItem> RemoveSuffixesStartingWithDollar(
        this IEnumerable<SuffixItem> suffixItems)
    {
        return suffixItems.Where(x => !x.Suffix.StartsWith('$'));
    }

    internal static IEnumerable<SuffixItem> Sort(this IEnumerable<SuffixItem> suffixItems)
    {
        return suffixItems.OrderBy(x => x.Suffix, StringComparer.Ordinal);
    }
}