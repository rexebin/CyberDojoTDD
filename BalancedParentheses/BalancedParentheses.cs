using System.Linq;

namespace CyberDojo.BalancedParentheses
{
    public class BalancedParentheses
    {
        private readonly char[] _leftParentheses = {'{', '[', '('};
        private readonly char[] _rightParentheses = {'}', ']', ')'};

        public bool IsBalanced(string text)
        {
            var isBalanced = false;
            var input = text;
            var allMatchedParenthesesRemoved = false;
            while (!allMatchedParenthesesRemoved)
            {
                var remaining = RemoveLastMatchedParentheses(input);
                if (remaining == input)
                {
                    isBalanced = !HasAnyRemainingParentheses(remaining);
                    allMatchedParenthesesRemoved = true;
                }

                input = remaining;
            }

            return isBalanced;
        }

        private bool HasAnyRemainingParentheses(string text)
        {
            return text.Any(x => _leftParentheses.Any(c => c == x) || _rightParentheses.Any(c => c == x));
        }


        public string RemoveLastMatchedParentheses(string text)
        {
            var lastLeftParenthesis = text.ToCharArray().LastOrDefault(x => _leftParentheses.Any(c => c == x));
            if (lastLeftParenthesis == '\0') return text;

            var lastIndexLeftParenthesis = text.LastIndexOf(lastLeftParenthesis);
            var firstMatchedIndex =
                text.Substring(lastIndexLeftParenthesis)
                    .IndexOf(_rightParentheses[_leftParentheses.ToList().IndexOf(lastLeftParenthesis)]) +
                lastIndexLeftParenthesis;
            return firstMatchedIndex < 0
                ? text
                : Slice(text, lastIndexLeftParenthesis, firstMatchedIndex);
        }

        private static string Slice(string s, int startIndex, int endIndex)
        {
            return s.Substring(0, startIndex) +
                   s.Substring(endIndex + 1);
        }
    }
}