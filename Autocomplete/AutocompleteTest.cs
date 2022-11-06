using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace CyberDojo.Autocomplete;

public class AutocompleteTest
{
    [Test]
    public void ShouldConcatenateToStrings()
    {
        var input = new[]
        {
            "Hello",
            "allow",
            "world",
            "word"
        };
        var result = input.ConcatenateToString();
        const string expected = "Hello$allow$world$word$";
        result.Should().Be(expected);
    }

    [Test]
    public void ShouldGetAllSuffixes()
    {
        const string input = "Hello$allow$";
        var expected = new SuffixItem[]
        {
            new("Hello$allow$", 0, 0),
            new("ello$allow$", 0, 1),
            new("llo$allow$", 0, 2),
            new("lo$allow$", 0, 3),
            new("o$allow$", 0, 4),
            new("$allow$", 1, 5),
            new("allow$", 1, 6),
            new("llow$", 1, 7),
            new("low$", 1, 8),
            new("ow$", 1, 9),
            new("w$", 1, 10),
            new("$", 2, 11),
        };
        var result = input.GetAllSuffixes();
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ShouldRemoveSuffixesStartingWithDollar()
    {
        var input = new SuffixItem[]
        {
            new("Hello$allow$", 0, 0),
            new("ello$allow$", 0, 1),
            new("llo$allow$", 0, 2),
            new("lo$allow$", 0, 3),
            new("o$allow$", 0, 4),
            new("$allow$", 1, 5),
            new("allow$", 1, 6),
            new("llow$", 1, 7),
            new("low$", 1, 8),
            new("ow$", 1, 9),
            new("w$", 1, 10),
            new("$", 2, 11),
        };
        var expected = new SuffixItem[]
        {
            new("Hello$allow$", 0, 0),
            new("ello$allow$", 0, 1),
            new("llo$allow$", 0, 2),
            new("lo$allow$", 0, 3),
            new("o$allow$", 0, 4),
            new("allow$", 1, 6),
            new("llow$", 1, 7),
            new("low$", 1, 8),
            new("ow$", 1, 9),
            new("w$", 1, 10),
        };
        var result = input.RemoveSuffixesStartingWithDollar();
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ShouldSortSuffixItems()
    {
        var input = new SuffixItem[]
        {
            new("Hello$allow$", 0, 0),
            new("ello$allow$", 0, 1),
            new("llo$allow$", 0, 2),
            new("lo$allow$", 0, 3),
            new("o$allow$", 0, 4),
            new("allow$", 1, 6),
            new("llow$", 1, 7),
            new("low$", 1, 8),
            new("ow$", 1, 9),
            new("w$", 1, 10),
        };
        var expected = new SuffixItem[]
        {
            new("Hello$allow$", 0, 0),
            new("allow$", 1, 6),
            new("ello$allow$", 0, 1),
            new("llo$allow$", 0, 2),
            new("llow$", 1, 7),
            new("lo$allow$", 0, 3),
            new("low$", 1, 8),
            new("o$allow$", 0, 4),
            new("ow$", 1, 9),
            new("w$", 1, 10),
        };
        var result = input.Sort().ToArray();
        result[0].Should().BeEquivalentTo(expected[0]);
        result[1].Should().BeEquivalentTo(expected[1]);
        result[2].Should().BeEquivalentTo(expected[2]);
        result[3].Should().BeEquivalentTo(expected[3]);
        result[4].Should().BeEquivalentTo(expected[4]);
        result[5].Should().BeEquivalentTo(expected[5]);
        result[6].Should().BeEquivalentTo(expected[6]);
        result[7].Should().BeEquivalentTo(expected[7]);
        result[8].Should().BeEquivalentTo(expected[8]);
        result[9].Should().BeEquivalentTo(expected[9]);
    }

    [Test]
    public void ShouldGetSuffixArray()
    {
        var input = new SuffixItem[]
        {
            new("allow$", 1, 6),
            new("ello$allow$", 0, 1),
            new("Hello$allow$", 0, 0),
            new("llo$allow$", 0, 2),
            new("llow$", 1, 7),
            new("lo$allow$", 0, 3),
            new("low$", 1, 8),
            new("o$allow$", 0, 4),
            new("ow$", 1, 9),
            new("w$", 1, 10),
        };
        var expected = new SuffixArrayItem[]
        {
            new(1, 6), new(0, 1), new(0, 0),
            new(0, 2), new(1, 7), new(0, 3), new(1, 8), new(0, 4),
            new(1, 9), new(1, 10)
        };
        var result = input.GetSuffixArray().ToArray();
        result[0].Should().BeEquivalentTo(expected[0]);
        result[1].Should().BeEquivalentTo(expected[1]);
        result[2].Should().BeEquivalentTo(expected[2]);
        result[3].Should().BeEquivalentTo(expected[3]);
        result[4].Should().BeEquivalentTo(expected[4]);
        result[5].Should().BeEquivalentTo(expected[5]);
        result[6].Should().BeEquivalentTo(expected[6]);
        result[7].Should().BeEquivalentTo(expected[7]);
        result[8].Should().BeEquivalentTo(expected[8]);
        result[9].Should().BeEquivalentTo(expected[9]);
    }

    [Test]
    public void ShouldGetSuffixArrayFromSource()
    {
        var input = new[]
        {
            "hello",
            "allow"
        };
        var expected = new SuffixArrayItem[]
        {
            new(1, 6),
            new(0, 1),
            new(0, 0),
            new(0, 2),
            new(1, 7),
            new(0, 3),
            new(1, 8),
            new(0, 4),
            new(1, 9),
            new(1, 10)
        };
        var suffixArrayResult = input.GetSuffixArray();
        suffixArrayResult.ConcatenatedString.Should().Be("hello$allow$");
        var result = suffixArrayResult.SuffixArray.ToArray();
        result[0].Should().BeEquivalentTo(expected[0]);
        result[1].Should().BeEquivalentTo(expected[1]);
        result[2].Should().BeEquivalentTo(expected[2]);
        result[3].Should().BeEquivalentTo(expected[3]);
        result[4].Should().BeEquivalentTo(expected[4]);
        result[5].Should().BeEquivalentTo(expected[5]);
        result[6].Should().BeEquivalentTo(expected[6]);
        result[7].Should().BeEquivalentTo(expected[7]);
        result[8].Should().BeEquivalentTo(expected[8]);
        result[9].Should().BeEquivalentTo(expected[9]);
    }

    [Test]
    public void ShouldFindSuffix()
    {
        var suffixArray = new SuffixArrayItem[]
        {
            new(1, 6),
            new(0, 1),
            new(0, 0),
            new(0, 2),
            new(1, 7),
            new(0, 3),
            new(1, 8),
            new(0, 4),
            new(1, 9),
            new(1, 1)
        };
        const string input = "Hello$allow$";
        const string search = "el";
        var result = suffixArray.FindAnyMatchingSuffixIndex(input, search);
        result.Should().Be(1);
    }

    [Test]
    public void ShouldFindMatchingSuffixes()
    {
        var suffixArray = new SuffixArrayItem[]
        {
            new(1, 6),
            new(0, 1),
            new(0, 0),
            new(0, 2),
            new(1, 7),
            new(0, 3),
            new(1, 8),
            new(0, 4),
            new(1, 9),
            new(1, 10)
        };
        const string input = "Hello$allow$";
        const string search = "lo";
        var suffixArrayResult = new SuffixArrayResult(suffixArray, input);
        var result = suffixArrayResult.FindMatchingSuffixIndexes(search);
        result.Should().BeEquivalentTo(new[] { 5, 6 });
    }

    [Test]
    public void ShouldGetAutocompleteList()
    {
        var input = new[]
        {
            "hello",
            "allow",
            "world",
            "word"
        };
        var suffixArrayResult = input.GetSuffixArray();
        input.FilterByKeyword("or", suffixArrayResult).Should()
            .BeEquivalentTo("world", "word");
        input.FilterByKeyword("lo", suffixArrayResult).Should()
            .BeEquivalentTo("hello", "allow");
        input.FilterByKeyword("hello", suffixArrayResult).Should()
            .BeEquivalentTo("hello");
        input.FilterByKeyword("Hello", suffixArrayResult).Should()
            .BeEquivalentTo("hello");
        input.FilterByKeyword("wo", suffixArrayResult).Should()
            .BeEquivalentTo("word", "world");
        input.FilterByKeyword("o", suffixArrayResult).Should()
            .BeEquivalentTo("hello", "allow", "world", "word");
        input.FilterByKeyword("w", suffixArrayResult).Should()
            .BeEquivalentTo("allow", "world", "word");
    }

    [Test]
    public void ShouldReplaceSpaceWithDollar()
    {
        var input = @"hello world 
What is your name?";
        var words = input.FilterByKeyword("a");
        words.Should().BeEquivalentTo(@"
What", "name?");
    }
}