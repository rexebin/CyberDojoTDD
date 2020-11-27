using System.Collections.Generic;

namespace CyberDojo.CountingCoins
{
    public class CountingCoins
    {
        public enum Coins
        {
            TwentyFive = 25,
            Ten = 10,
            Five = 5
        }

        public int CalcCombinations(int amount, Coins largestCoin = Coins.TwentyFive)
        {
            if (largestCoin == Coins.Five)
                return amount / 5 + 1;

            var coins = new List<int> {25, 10, 5};
            var nextLargestCoinValue = (Coins) coins[coins.IndexOf((int) largestCoin) + 1];
            var combinations = 0;
            for (var i = 0; i <= amount / (int) largestCoin; i++)
            {
                combinations += CalcCombinations(amount - (int) largestCoin * i, nextLargestCoinValue);
            }

            return combinations;
        }
    }
}