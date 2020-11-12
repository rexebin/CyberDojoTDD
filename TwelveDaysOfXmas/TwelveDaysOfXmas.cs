using System;
using System.Collections.Generic;

namespace CyberDojo.TwelveDaysOfXmas
{
    public class TwelveDaysOfXmas
    {
        private readonly List<string> _days = new List<string>
        {
            "first", "second", "third", "forth", "fifth", "sixth",
            "seventh", "eighth", "ninth", "tenth", "eleventh", "twelfth"
        };

        private readonly List<string> _sentences = new List<string>
        {
            "A partridge in a pear tree.",
            "Two turtle doves and",
            "Three french hens",
            "Four calling birds",
            "Five golden rings",
            "Six geese a-laying",
            "Seven swans a-swimming",
            "Eight maids a-milking",
            "Nine ladies dancing",
            "Ten lords a-leaping",
            "Eleven pipers piping",
            "Twelve drummers drumming"
        };

        public string PrintLyricByDay(int day)
        {
            return @$"On the {_days[day-1]} day of Christmas,
My true love gave to me:
{GetSentences(day)}";
        }

        private string GetSentences(int day)
        {
            var result = new List<string>();
            for (var j = day - 1; j >= 0; j--)
            {
                result.Add(_sentences[j]);
            }
            return string.Join(Environment.NewLine, result);
        }

        public string PrintLyric()
        {
            var result = new List<string>();
            for (var i = 0; i < 12; i++)
            {
                result.Add(PrintLyricByDay(i+1));
            }
            return string.Join($"{Environment.NewLine}{Environment.NewLine}", result);
        }
    }
}