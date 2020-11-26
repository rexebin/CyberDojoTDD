using NUnit.Framework;

namespace CyberDojo.ClosestToZero
{
    public class ClosestToZeroTest
    {
        private CyberDojoTDD.ClosestToZero.ClosestToZero sut = null!;

        [SetUp]
        public void Create()
        {
            sut = new CyberDojoTDD.ClosestToZero.ClosestToZero();
        }


        [Test]
        [TestCase(new[] { 1, 2 }, 1)]
        [TestCase(new[] { 3, 4 }, 3)]
        [TestCase(new[] { -2, 3, 4 }, -2)]
        [TestCase(new[] { -3, -2, 3, 4 }, -2)]
        [TestCase(new[] { -3, -2, 2, 4 }, 2)]
        [TestCase(new[] { -3, -2, 0, 2, 4 }, 0)]
        [TestCase(new[] { -3, -2, -1, 1, 2, 4 }, 1)]
        [TestCase(new[] { -3, -4 }, -3)]
        [TestCase(new[] { -3, -1, -1, 2, 3, -4 }, -1)]
        public void GivenNumbers_ReturnClosest(int[] numbers, int expected)
        {
            Assert.AreEqual(expected, sut.GetNumberClosestToZero(numbers));
        }
    }
}