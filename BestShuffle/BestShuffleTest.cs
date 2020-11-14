using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CyberDojo.BestShuffle
{
    [TestFixture]
    public class BestShuffleTest
    {
        private BestShuffle _sut = null!;

        [SetUp]
        public void Create()
        {
            _sut = new BestShuffle();
        }

        [Test]
        public void Given_ABBCCCDDDD_ShouldReturn_Array_SortingLetterByCount_AndThen_Index()
        {
            var expected = new List<LetterCount>
            {
                new('d', 4),
                new('c', 3),
                new('b', 2),
                new('e', 1, 10),
                new('a', 1, 0)
            };
            var result = BestShuffle.GetLetterCounts("abbcccdddde");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        [TestCase('a', new char[] { }, 'd')]
        [TestCase('b', new[] {'d'}, 'd')]
        [TestCase('b', new[] {'d', 'd'}, 'd')]
        [TestCase('c', new[] {'d', 'd', 'd'}, 'd')]
        [TestCase('c', new[] {'d', 'd', 'd', 'd'}, 'b')]
        [TestCase('c', new[] {'d', 'd', 'd', 'd', 'b'}, 'b')]
        [TestCase('d', new[] {'d', 'd', 'd', 'd', 'b', 'b'}, 'c')]
        [TestCase('d', new[] {'d', 'd', 'd', 'd', 'b', 'b', 'c'}, 'c')]
        [TestCase('d', new[] {'d', 'd', 'd', 'd', 'b', 'b', 'c', 'c'}, 'c')]
        [TestCase('d', new[] {'d', 'd', 'd', 'd', 'b', 'b', 'c', 'c', 'c'}, 'a')]
        public void Should_GetDifferentLetter_WithMostCount(char letterInQuestion, char[] usedLetters, char expected)
        {
            var selectionPool = new List<LetterCount>
            {
                new('d', 4),
                new('c', 3),
                new('b', 2),
                new('a', 1)
            };

            var result = _sut.GetNextBest(
                new FindBestModel(letterInQuestion, usedLetters, selectionPool));
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase('e', new char[] { }, 'k')]
        [TestCase('l', new[] {'k'}, 'e')]
        [TestCase('k', new[] {'k', 'e'}, 'l')]
        public void Should_GetDifferentLetter_WithBiggestLastIndex(char letterInQuestion, char[] usedLetters, char expected)
        {
            var selectionPool = new List<LetterCount>
            {
                new('k', 1, 2),
                new('l', 1, 1),
                new('e', 1, 0)
            };

            var result = _sut.GetNextBest(
                new FindBestModel(letterInQuestion, usedLetters, selectionPool));
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("", "")]
        [TestCase("a", "a")]
        [TestCase("up", "pu")]
        [TestCase("grrrrrr", "rgrrrrr")]
        [TestCase("elk", "kel")]
        [TestCase("seesaw", "essewa")]
        [TestCase("abracadabra", "baabararadc")]
        [TestCase("tree", "eert")]
        public void GivenA_ShouldReturnA(string input, string expected)
        {
            var result = _sut.Shuffle(input);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("a", "a, a, 1")]
        [TestCase("up", "up, pu, 0")]
        [TestCase("grrrrrr", "grrrrrr, rgrrrrr, 5")]
        [TestCase("elk", "elk, kel, 0")]
        [TestCase("seesaw", "seesaw, essewa, 0")]
        [TestCase("abracadabra", "abracadabra, baabararadc, 0")]
        [TestCase("tree", "tree, eert, 0")]
        public void GivenString_ShouldShuffleAndReturnFormattedResult(string original, string expected)
        {
            var result = _sut.Print(original);
            Assert.AreEqual(expected, result);
        }
    }
}