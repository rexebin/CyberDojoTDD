using System;
using System.Linq;

namespace CyberDojo.CalcStats
{
    public class CalcStats
    {
        public int GetMinValue(int[] numbers)
        {
            return numbers.Min();
        }

        public int GetMaxValue(int[] numbers)
        {
            return numbers.Max();
        }

        public int GetLength(int[] numbers)
        {
            return numbers.Length;
        }

        public double GetAverageValue(int[] numbers)
        {
            return Math.Round(numbers.Average(), 6);
        }
    }
}