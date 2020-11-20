using NUnit.Framework;

namespace CyberDojoTDD.CombinedNumber
{
    public class CombinedNumberTest
    {
        private CombinedNumber sut = null!;

        [SetUp]
        public void Create()
        {
            sut = new CombinedNumber();
        }


        [Test]
        [TestCase(new[] { 1 }, 1)]
        [TestCase(new[] { 1, 2 }, 21)]
        [TestCase(new[] { 1, 2, 3 }, 321)]
        [TestCase(new[] { 1, 2, 3, 9 }, 9321)]
        [TestCase(new[] { 50, 2, 1, 9 }, 95021)]
        [TestCase(new[] { 420, 42, 423 }, 42423420)]
        [TestCase(new[] { 5, 50, 56 }, 56550)]
        public void GivenPositiveNumbers_ShouldReturnLargestNumber(int[] numbers, int expected)
        {

            Assert.AreEqual(expected, sut.GetLargestCombinedNumber(numbers));
        }

    }
}