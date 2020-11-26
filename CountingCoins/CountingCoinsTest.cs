using NUnit.Framework;

namespace CyberDojo.CountingCoins
{
    public class CountingCoinsTest
    {
        private CountingCoins _sut = null!;

        [SetUp]
        public void Setup()
        {
            _sut = new CountingCoins();
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(5, 2)]
        [TestCase(10, 3)]
        [TestCase(16, 4)]
        [TestCase(26, 6)]
        public void GivenAmount_CalcCombinationsForCoin5and1(int amount, int expected)
        {
            Assert.AreEqual(expected, _sut.CalcCombinationsFor5And1(amount));
        }
        
        [Test]
        [TestCase(10, 4)]
        [TestCase(15, 6)]
        // 20,0,0; 10,10,0; 10,5,5; 10,0,10; 0,20,0; 0,15,5; 0,10,10, 0,5,15, 0,0,20
        [TestCase(20, 9)]
        public void GivenAmount_CalcCombinationsForCoin10and5and1(int amount, int expected)
        {
            Assert.AreEqual(expected, _sut.CalcCombinationsFor10And5And1(amount));
        }
        
        [Test]
        [TestCase(1, 1)]
        [TestCase(5, 2)]
        [TestCase(10, 4)]
        [TestCase(15, 6)]
        [TestCase(20, 9)]
        //25/0/0/0 0/20/5/0 0/20/0/5 0/10/15/0 0/10/10/5 0/10/5/10 0/10/0/15
        //0/0/25/0 0/0/20/5 0/0/15/10 0/0/10/15 0/0/5/20 0/0/5/25
        [TestCase(25, 13)]
        [TestCase(100, 242)]
        public void GivenAmount_CalcCombinationsForAllCoins(int amount, int expected)
        {
            Assert.AreEqual(expected, _sut.CalcCombinations(amount));
        }
    }
}