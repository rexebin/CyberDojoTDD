namespace CyberDojo.CountingCoins
{
    public class CountingCoins
    {
        public int CalcCombinations(int amount)
        {
            var combinations = 0;
            for (var i = 0; i <= amount / 25; i++)
            {
                combinations += CalcCombinationsFor10And5And1(amount - 25 * i);
            }

            return combinations;
        }

        public int CalcCombinationsFor10And5And1(int amount)
        {
            var combinations = 0;
            for (var i = 0; i <= amount / 10; i++)
            {
                combinations += CalcCombinationsFor5And1(amount - 10 * i);
            }

            return combinations;
        }

        public int CalcCombinationsFor5And1(int amount)
        {
            return amount / 5 + 1;
        }
    }
}