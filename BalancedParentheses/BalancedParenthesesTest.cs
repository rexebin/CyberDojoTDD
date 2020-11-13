using NUnit.Framework;

namespace CyberDojo.BalancedParentheses
{
    public class BalancedParenthesesTest
    {
        private BalancedParentheses sut = null!;

        [SetUp]
        public void Create()
        {
            sut = new BalancedParentheses();
        }

        [Test]
        [TestCase("[{}]", "[]")]
        [TestCase("[]", "")]
        [TestCase("{{)(}}", "{{)(}}")]
        [TestCase("({)}", "(")]
        [TestCase("(", "(")]
        [TestCase("{}([])", "{}()")]
        [TestCase("{}()", "{}")]
        [TestCase("{}", "")]
        [TestCase("", "")]
        [TestCase("word{word}Word", "wordWord")]
        [TestCase("word{(word}Word)", "word{")]
        public void GivenText_ShouldRemoveLastMatchedParentheses(string input, string expected)
        {
            var result = sut.RemoveLastMatchedParentheses(input);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("{")]
        [TestCase("[")]
        [TestCase("(")]
        [TestCase("{]")]
        [TestCase("{[}]")]
        [TestCase("{{)(}}")]
        [TestCase("{word{word)(word}word}")]
        [TestCase("({)}")]
        public void GivenUnMatchedParentheses_ShouldReturnFalse(string input)
        {
            Assert.IsFalse(sut.IsBalanced(input));
        }
        
        [Test]
        [TestCase("{}")]
        [TestCase("[]")]
        [TestCase("()")]
        [TestCase("[({})]")]
        [TestCase("{}([])")]
        [TestCase("{()}[[{}]]")]
        [TestCase("word {(word)}word [123[{ sdf}] sdf ]")]
        public void GivenMatchedParentheses_ShouldReturnTrue(string input)
        {
            Assert.IsTrue(sut.IsBalanced(input));
        }
    }
}