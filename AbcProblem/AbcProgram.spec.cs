using NUnit.Framework;

namespace CyberDojo.AbcProblem
{
    public class AbcProblemTest
    {
        [Test]
        [TestCase("A", true)]
        [TestCase("BARK", true)]
        [TestCase("BOOK", false)]
        [TestCase("TREAT", true)]
        [TestCase("COMMON", false)]
        [TestCase("SQUAD", true)]
        [TestCase("CONFUSE", true)]
        [TestCase("Confuse", true)]
        public void CanMakeWord(string word, bool canMake)
        {
            var sut = new AbcProblem();
            var result = sut.MakeWord(word);
            Assert.AreEqual(canMake, result);
        }
    }
}