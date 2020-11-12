using System.Collections.Generic;
using System.Linq;

namespace CyberDojo.AlignColumns
{
    /**
     * Public api
     */
    public interface IAlignColumns
    {
        string Print(string text);
    }
    public class AlignColumns : IAlignColumns
    {
        public List<List<int>> GetAllColumnWidths(string input)
        {
            var result = GetLines(input)
                .Select(line =>
                    GetColumns(line)
                        .Select(w => w.Length + 1).ToList()).ToList();
            return result;
        }

        private static IEnumerable<string> GetLines(string input)
        {
            return input
                .Split("\n")
                .Where(x => x.Length > 0);
        }

        private static IEnumerable<string> GetColumns(string line)
        {
            return line.Trim().Split("$")
                .Where(w => w.Length > 0);
        }

        public List<int> CalculateMaxColumnWidth(List<List<int>> input)
        {
            var maxColumnCount = GetMaxColumnCount(input);
            var result = new List<int>();
            for (var i = 0; i < maxColumnCount; i++)
            {
                var col = i;
                result.Add(GetColumnMaxWidth(input, col));
            }
            return result;
        }

        private static int GetMaxColumnCount(List<List<int>> input)
        {
            return input.Aggregate(0,
                (acc, curr) => acc < curr.Count ? curr.Count : acc);
        }

        private static int GetColumnMaxWidth(List<List<int>> input, int col)
        {
            return input.Select(x => x.Count <= col ? 0 : x[col])
                .Aggregate(0, (acc, curr) => acc < curr ? curr : acc);
        }

        public List<int> GetMaxColumnWidth(string text)
        {
            var allColumnWidths = GetAllColumnWidths(text);
            var result = CalculateMaxColumnWidth(allColumnWidths);
            return result;
        }

        public string Print(string text)
        {
            var colWidth = GetMaxColumnWidth(text);
            var result = GetLines(text)
                .Select(x => GetColumns(x).Select((x, index) => x.PadRight(colWidth[index], ' ')));
            return string.Join("\n",  result.Select(x => string.Join("", x).Trim())).Trim();
        }
    }

 
}