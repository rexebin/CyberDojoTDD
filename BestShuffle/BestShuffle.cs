using System.Collections.Generic;
using System.Linq;

namespace CyberDojo.BestShuffle
{
    public sealed record LetterCount(char Letter, int Count, int LastIndex = -1);

    public sealed record FindBestModel(
        char LetterInQuestion,
        IEnumerable<char> UsedLetters,
        IEnumerable<LetterCount> SelectionPool);

    public class BestShuffle
    {
        public string Shuffle(string s)
        {
            var pool = GetLetterCounts(s);
            var shuffled = new char[s.Length];
            for (var index = 0; index < s.Length; index++)
            {
                var bestReplacement = GetNextBest(new FindBestModel(s[index], shuffled, pool));
                shuffled[index] = bestReplacement ?? s[index];
            }

            return string.Join("", shuffled);
        }

        public static List<LetterCount> GetLetterCounts(string input)
        {
            var chars = input.ToCharArray().ToList();
            return chars.GroupBy(x => x)
                .Select(x => new LetterCount(x.Key, x.Count(), x.Count() == 1 ? chars.LastIndexOf(x.Key) : -1))
                .OrderByDescending(x => x.Count).ThenByDescending(x => x.LastIndex).ToList();
        }

        public char? GetNextBest(FindBestModel model)
        {
            var poolWithoutLetterInQuestion =
                model.SelectionPool.Where(x => x.Letter != model.LetterInQuestion);
            var poolWithRemaining = RemoveUsedUpLetter(poolWithoutLetterInQuestion, model.UsedLetters);
            return poolWithRemaining.FirstOrDefault()?.Letter;
        }

        private IEnumerable<LetterCount> RemoveUsedUpLetter(
            IEnumerable<LetterCount> poolWithoutLetterInQuestion,
            IEnumerable<char> modelUsedLetters)
        {
            return poolWithoutLetterInQuestion.Select(x =>
                new LetterCount(x.Letter, x.Count - modelUsedLetters.Count(c => c == x.Letter)
                )).Where(x => x.Count > 0).ToList();
        }

        public string Print(string original)
        {
            var shuffled = Shuffle(original);
            var score = original.Where((t, i) => t == shuffled[i]).Count();
            return $@"{original}, {shuffled}, {score}";
        }
    }
}