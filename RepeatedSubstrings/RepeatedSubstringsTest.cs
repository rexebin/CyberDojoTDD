using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace CyberDojo.RepeatedSubstrings;

public class RepeatedSubstringsTest
{
    /**
         * aabaab, abaab, baab, aab, ab, b
         * 0,       1,      2,  3,   4,  5
         * aab, aabaab, ab, abaab, b, baab
         * Suffix Array: 3, 0, 4, 1, 5, 2
         * Longest common prefix: 0, 3, 1, 2, 0, 1 // unique substrings: (6*(6+1)/2 - (2+ 1+ 2+ 1)) = 21 - 6 = 15
         * Repeated substrings: aab: a, aa, aab, a, ab, b; a: a; ab: a, ab, b; b: b; = 3*(3+1)/2 = 6 minus one repeated substring = 5
         * each common prefix: unique substring array
         * merge all common prefix's unique substring array
         * distict substring is the repeated substrings.
         */
    /**
     * 1. generate suffix array
     * 2. get all common prefix
     * 3. generate unique substring array for each prefix
     * 4. concat all unique substring array
     * 5. get distinct substring
     */
    [Test]
    public void ShouldGenerateSuffixArray()
    {
        var input = "abc";
        //abc, bc, c: 0, 1, 2
        var expected = new int[] { 0, 1, 2 };
        var actual = input.GetSuffixArray();
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ShouldGenerateSuffixArrayCamel()
    {
        var input = "camel";
        /**
         * camel, amel, mel, el, l
         * amel, camel, el, l, mel
         * 1,0,3,4,2
         */
        var actual = input.GetSuffixArray().ToArray();
        actual[0].Should().Be(1);
        actual[1].Should().Be(0);
        actual[2].Should().Be(3);
        actual[3].Should().Be(4);
        actual[4].Should().Be(2);
    }


    [Test]
    public void ShouldGetCommonPrefix()
    {
        var input = "aabaab";
        // var expected = new string[] { "aab", "ab", "a", "b" };
        var result = input.GetCommonPrefix(3, 0);
        result.Should().BeEquivalentTo("aab");
    }

    [Test]
    public void ShouldGetCommonPrefixes()
    {
        var input = "aabaab";
        var expected = new string[] { "aab", "a", "ab", "b" };
        var result = input.GetCommonPrefixes();
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ShouldGetAllSubstringCombinations()
    {
        var input = "aab";
        var expected = new string[] { "a", "aa", "aab", "a", "ab", "b" };
        var result = input.GetAllSubstringCombinations();
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ShouldGetRepeatedSubStringsAABAAB()
    {
        var input = "aabaab";
        var expected = new string[] { "a", "aa", "aab", "ab", "b" };
        var result = input.GetRepeatSubstrings();
        result.Should().BeEquivalentTo(expected);
        input.GetRepeatedSubstringAmount().Should().Be(5);
    }

    [Test]
    public void ShouldGetRepeatedSubStringsAAAAA()
    {
        var input = "aaaaa";
        var expected = new string[] { "a", "aa", "aaa", "aaaa" };
        var result = input.GetRepeatSubstrings();
        result.Should().BeEquivalentTo(expected);
        input.GetRepeatedSubstringAmount().Should().Be(4);
    }

    [Test]
    public void ShouldGetRepeatedSubStringsAaAaA()
    {
        var input = "AaAaA";
        /**
         * AaAaA, aAaA, AaA, aA, A
         * A, AaA, AaAaA, aA, aAaA
         * 4, 2, 0, 3, 1
         * A, AaA, aA
         * a, A, Aa, AaA, aA
         */
        var lcp = input.GetSuffixArray().ToArray();
        lcp[0].Should().Be(4);
        lcp[1].Should().Be(2);
        lcp[2].Should().Be(0);
        lcp[3].Should().Be(3);
        lcp[4].Should().Be(1);

        var expected = new string[] { "A", "a", "Aa", "aA", "AaA" };
        var result = input.GetRepeatSubstrings();
        result.Should().BeEquivalentTo(expected);
        input.GetRepeatedSubstringAmount().Should().Be(5);
    }
}

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
        for (int i = 0; i < Math.Min(substring1.Length, substring2.Length); i++)
        {
            if (substring1[i] == substring2[i])
            {
                result = result.Append(substring1[i]).ToArray();
                continue;
            }

            return new string(result);
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