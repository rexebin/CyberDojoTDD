using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberDojo.Autocomplete;

public static class AutoComplete
{
    /**
     * one time suffix array construction per set of terms.
     */
    public static SuffixArrayResult GetSuffixArray(this IEnumerable<string> input)
    {
        var concatenateToString = input.ConcatenateToString();
        var suffixArray = concatenateToString.ToLower()
            .GetAllSuffixes()
            .RemoveSuffixesStartingWithDollar()
            .Sort()
            .GetSuffixArray().ToArray();
        return new SuffixArrayResult(suffixArray, concatenateToString);
    }

    /**
     * extension method to given terms, take keyword and saved suffix array for the terms, and return filtered terms. 
     */
    public static IEnumerable<string> FilterByKeyword(this IEnumerable<string> source,
        string keyword,
        SuffixArrayResult suffixArrayResult)
    {
        var sourceTerms = source.ToArray();
        var result = suffixArrayResult
            .FindMatchingSuffixIndexes(keyword.ToLower())
            .ToArray();
        var termIndexes = result.Select(i => suffixArrayResult.SuffixArray[i].TermIndex);
        return termIndexes.Select(termIndex => sourceTerms[termIndex]);
    }


    internal static IEnumerable<int> FindMatchingSuffixIndexes(
        this SuffixArrayResult suffixArrayResult, string search)
    {
        var matchingIndex =
            suffixArrayResult.SuffixArray.FindAnyMatchingSuffixIndex(suffixArrayResult.ConcatenatedString,
                search);
        return matchingIndex == -1
            ? Enumerable.Empty<int>()
            : GetAdjacentMatchingSuffixIndexes(suffixArrayResult, matchingIndex, search);
    }

    private static IEnumerable<int> GetAdjacentMatchingSuffixIndexes(
        SuffixArrayResult suffixArrayResult, int matchingIndex,
        string search)
    {
        var result = new[] { matchingIndex };
        var arrayItems = suffixArrayResult.SuffixArray;
        var leftIndex = matchingIndex;
        while (true)
        {
            leftIndex -= 1;
            if (leftIndex < 0) break;
            var substring =
                suffixArrayResult.ConcatenatedString[arrayItems[leftIndex].WordIndex..];
            if (!substring.StartsWith(search)) break;

            result = result.Append(leftIndex).ToArray();
        }

        var rightIndex = matchingIndex;

        while (true)
        {
            rightIndex += 1;
            if (rightIndex >= arrayItems.Length) break;
            var substring =
                suffixArrayResult.ConcatenatedString[arrayItems[rightIndex].WordIndex..];
            if (!substring.StartsWith(search)) break;

            result = result.Append(rightIndex).ToArray();
        }

        return result;
    }

    internal static int FindAnyMatchingSuffixIndex(
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
        return input.Select(x => new SuffixArrayItem(x.TermIndex, x.WordIndex));
    }

    internal static string ConcatenateToString(this IEnumerable<string> input)
    {
        return input.Aggregate("",
            (current, word) => String.IsNullOrEmpty(current) ? $"{word}$" : $"{current}{word}$");
    }

    internal static IEnumerable<SuffixItem> GetAllSuffixes(this string input)
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