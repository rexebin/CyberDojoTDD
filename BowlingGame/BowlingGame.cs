using System.Collections.Generic;
using System.Linq;

namespace CyberDojo.BowlingGame
{
    public class BowlingGame
    {
        public int[] GetFrameScore(string frameResult)
        {
            return (from t in frameResult
                select GetScore(t, frameResult[0])
                into score
                where score != null
                select score.Value).ToArray();
        }

        private int? GetScore(char ball, char previousScore)
        {
            switch (ball)
            {
                case '-':
                    return 0;
                case 'X':
                    return 10;
                case '/':
                    return 10 - int.Parse(previousScore.ToString());
                default:
                    var isNumber = int.TryParse(ball.ToString(), out int score);
                    return isNumber ? (int?) score : null;
            }
        }

        public string[] GetFrameResults(string tenFrameResult)
        {
            var allResults = tenFrameResult.Split("||").Where(x => x != "").ToList();
            return allResults.Count == 1
                ? allResults[0].Split("|")
                : allResults[0].Split("|").Append(allResults[1]).ToArray();
        }

        public int GetTotalCore(string results)
        {
            var scores = GetFrameResults(results).Select(GetFrameScore).ToList();
            var finalScore = 0;
            for (var i = 0; i < 10; i++)
            {
                if (scores[i].Length == 1 && scores[i][0] == 10)
                {
                    finalScore += 10 + GetRestOfScores(scores, i + 1).Take(2).Sum();
                    continue;
                }

                if (scores[i].Sum() == 10)
                {
                    finalScore += 10 + GetRestOfScores(scores, i + 1).First();
                    continue;
                }

                finalScore += scores[i].Sum();
            }

            return finalScore;
        }

        private static IEnumerable<int> GetRestOfScores(List<int[]> scores, int skip)
        {
            return scores.Skip(skip).SelectMany(x => x);
        }
    }
}