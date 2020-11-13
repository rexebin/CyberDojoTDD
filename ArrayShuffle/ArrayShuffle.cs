using System;

namespace CyberDojo.ArrayShuffle
{
    public class ArrayShuffle
    {
        public int Roll(int min, int max)
        {
            return new Random().Next(min, max);
        }

        public char[] Shuffle(char[] input)
        {
            var inputLength = input.Length;
            var result = new char[inputLength];
            for (var i = 0; i < inputLength; i++)
            {
                var foundNewIndex = false;
                while (!foundNewIndex)
                {
                    var newIndex = Roll(0, inputLength);
                    if (result[newIndex] != '\0') continue;
                    result[newIndex] = input[i];
                    foundNewIndex = true;
                }
            }

            return result;
        }
    }
}