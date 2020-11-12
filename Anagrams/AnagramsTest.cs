using System.Collections.Generic;
using NUnit.Framework;

namespace CyberDojo.Anagrams
{
    public class AnagramsTest
    {
        private Anagrams sut = null!;
        [SetUp]
        public void ShouldCreate()
        {
            sut = new Anagrams();
        }

        [Test]
        public void Given_AB_ShouldList_AB_BA()
        {
            var expected = new List<string>()
            {
                "ab", "ba"
            };
            var result = sut.GetAnagrams("a", "b");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Given_ABC_ShouldListAllSixCombinations()
        {
            var expected = new List<string>
            {
                "abc", "acb", "bac", "bca", "cab", "cba"
            };
            var result = sut.GetAnagrams("a", "b", "c");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Given_BIRO_ShouldReturnGivenResult()
        {
            var expected = new List<string>()
            {

                "biro", "bior", "brio", "broi", "boir", "bori",
                "ibro", "ibor", "irbo", "irob", "iobr", "iorb",
                "rbio", "rboi", "ribo", "riob", "robi", "roib",
                "obir", "obri", "oibr", "oirb", "orbi", "orib",
            };
            var result = sut.GetAnagrams("biro");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Given_BIROA_ShouldReturn120Combinations()
        {
            var result = sut.GetAnagrams("biroa");
            Assert.AreEqual(120, result.Count);
        }
        
    }
}