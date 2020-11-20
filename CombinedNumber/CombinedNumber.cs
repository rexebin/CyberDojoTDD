using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberDojoTDD.CombinedNumber
{
    public class CombinedNumber
    {
        public int GetLargestCombinedNumber(int[] numbers)
        {
            return int.Parse(string.Join("", numbers.Select(x => x.ToString()).OrderByDescending(x => x, new NComparer())));
        }
    }

    internal class NComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            var lengthDiff = Math.Abs(x.Length - y.Length);
            if (lengthDiff == 0)
            {
                return string.Compare(x, y);
            }
            var isXLonger = x.Length > y.Length;
            for (int i = 0; i < lengthDiff; i++)
            {
                if (isXLonger)
                {
                    y += y[0];
                }
                else
                {
                    x += x[0];
                }
            }
            return string.Compare(x, y);
        }
    }
}