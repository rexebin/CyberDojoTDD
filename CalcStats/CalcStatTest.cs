using NUnit.Framework;

namespace CyberDojo.CalcStats
{
    public class CalcStatTest
    {
        private CalcStats sut = null!;

        [SetUp]
        public void Create()
        {
            sut = new CalcStats();
        }

        [Test]
        [TestCase(new[] {6, 9, 15, -2, 92, 11}, -2)]
        [TestCase(new[] {6, 9, 15, -5, 92, 11}, -5)]
        public void GivenNumbers_ShouldGetMinValue(int[] numbers, int expected)
        {
            var minValue = sut.GetMinValue(numbers);
            Assert.AreEqual(expected, minValue);
        }
        
        [Test]
        [TestCase(new[] {6, 9, 15, -2, 92, 11}, 92)]
        [TestCase(new[] {6, 9, 15, -5, 100, 11}, 100)]
        public void GivenNumbers_ShouldGetMaxValue(int[] numbers, int expected)
        {
            var maxValue = sut.GetMaxValue(numbers);
            Assert.AreEqual(expected, maxValue);
        }
        
         
        [Test]
        [TestCase(new[] {6, 9, 15, -2, 92, 11}, 6)]
        [TestCase(new[] {6, 9, 15, -5, 100}, 5)]
        public void GivenNumbers_ShouldGetNumberOfElement(int[] numbers, int expected)
        {
            var length = sut.GetLength(numbers);
            Assert.AreEqual(expected, length);
        }
        
        [Test]
        [TestCase(new[] {6, 9, 15, -2, 92, 11}, 21.833333)]
        [TestCase(new[] {6, 9, 15, -5, 100}, 25)]
        public void GivenNumbers_ShouldGetAverageValue(int[] numbers, double expected)
        {
            var length = sut.GetAverageValue(numbers);
            Assert.AreEqual(expected, length);
        }
    }
}