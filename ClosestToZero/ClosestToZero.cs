using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberDojoTDD.ClosestToZero
{
    public class ClosestToZero
    {
        public int GetNumberClosestToZero(int[] numbers)
        {
            return numbers.OrderBy(x => x, new CompareAbs()).First();
        }
    }

    internal class CompareAbs : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            var order = Math.Abs(x) - Math.Abs(y);
            return order == 0 ? x > y ? -1 : 1 : order;
        }
    }
}