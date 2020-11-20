using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CyberDojo.ArrayShuffle
{
    public class ArrayShuffleTest
    {
        private ArrayShuffle _sut = null!;
        private const int ShuffleTimes = 200;

        [SetUp]
        public void Create()
        {
            _sut = new ArrayShuffle();
        }

        [Test]
        public void Given_1_7_ShouldGenerateARandomNumberBetween1_6Inclusive()
        {
            for (int i = 0; i < ShuffleTimes; i++)
            {
                var result = _sut.Roll(1, 7);
                Assert.IsTrue(result >= 1 && result < 7);
            }
        }

        [Test]
        public void ShouldNotMutateGivenArray()
        {
            var input = "abc".ToCharArray();
            _sut.Shuffle(input);
            Assert.IsTrue(input.SequenceEqual("abc".ToCharArray()));
        }

        [Test]
        public void Given_AB_ShouldReturn_AB_Or_BA()
        {
            var expectedPossibility = new List<char[]>
            {
                new[] {'a', 'b'},
                new[] {'b', 'a'}
            };
            var abCount = 0;
            var baCount = 0;
            for (int i = 0; i < ShuffleTimes; i++)
            {
                char[] result = _sut.Shuffle("ab".ToCharArray());
                if (result.SequenceEqual(expectedPossibility.First())) abCount++;
                if (result.SequenceEqual(expectedPossibility.Last())) baCount++;
                Assert.IsTrue(expectedPossibility.Find(x => x.SequenceEqual(result)) != null);
            }

            Assert.IsTrue(abCount > 0 && abCount < ShuffleTimes / 1.5);
            Assert.IsTrue(baCount > 0 && baCount < ShuffleTimes / 1.5);
        }

        [Test]
        public void Given_ABC_ShouldReturn_ResultInGivenRange()
        {
            var expectedPossibility = new List<char[]>
            {
                new[] {'a', 'b', 'c'},
                new[] {'a', 'c', 'b'},
                new[] {'b', 'a', 'c'},
                new[] {'b', 'c', 'a'},
                new[] {'c', 'a', 'b'},
                new[] {'c', 'b', 'a'}
            };
            var count = new[] { 0, 0, 0, 0, 0, 0 };

            for (var i = 0; i < ShuffleTimes; i++)
            {
                char[] result = _sut.Shuffle("abc".ToCharArray());
                var index = expectedPossibility.FindIndex(x => x.SequenceEqual(result));
                Assert.IsTrue(index >= 0 && index < 6);
                count[index]++;
            }

            foreach (var c in count)
            {
                Assert.IsTrue(c > 0 && c < ShuffleTimes / 2);
            }
        }

    }
}