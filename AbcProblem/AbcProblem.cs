using System.Collections.Generic;
using System.Linq;

namespace CyberDojo.AbcProblem
{
    public class AbcProblem
    {
        private readonly List<char[]> _blocks = new List<char[]>
        {
            new[] {'B', 'O'},
            new[] {'X', 'K'},
            new[] {'D', 'Q'},
            new[] {'C', 'P'},
            new[] {'N', 'A'},
            new[] {'G', 'T'},
            new[] {'R', 'E'},
            new[] {'T', 'G'},
            new[] {'Q', 'D'},
            new[] {'F', 'S'},
            new[] {'J', 'W'},
            new[] {'H', 'U'},
            new[] {'V', 'I'},
            new[] {'A', 'N'},
            new[] {'O', 'B'},
            new[] {'E', 'R'},
            new[] {'F', 'S'},
            new[] {'L', 'Y'},
            new[] {'P', 'C'},
            new[] {'Z', 'M'}
        };

        public bool MakeWord(string word)
        {
            var characters = word.ToCharArray();

            var canMake = false;
            foreach (var character in characters)
            {
                var result = _blocks.Find(x =>
                    x.Any(z => z == char.ToUpper(character)));
                if (result == null)
                {
                    canMake = false;
                    break;
                }

                _blocks.Remove(result);
                canMake = true;
            }

            return canMake;
        }
    }
}