using System;
using NUnit.Framework;

namespace CyberDojo.TwelveDaysOfXmas
{
    public class TwelveDaysOfXmasTest
    {
        private TwelveDaysOfXmas Sut { get; set; } = null!;

        [SetUp]
        public void SetSubjectUnderTest()
        {
            Sut = new TwelveDaysOfXmas();
        }
        [Test]
        public void ShouldPrintDay1()
        {
            var result = Sut.PrintLyricByDay(1);
            Assert.AreEqual(@"On the first day of Christmas,
My true love gave to me:
A partridge in a pear tree.", result);
            
        }
        
        [Test]
        public void ShouldPrintDay2()
        {
            var result = Sut.PrintLyricByDay(2);
            Assert.AreEqual(@"On the second day of Christmas,
My true love gave to me:
Two turtle doves and
A partridge in a pear tree.", result);
        }

        [Test]
        public void ShouldPrintDay12()
        {
            var result = Sut.PrintLyricByDay(12);
            Assert.AreEqual(@"On the twelfth day of Christmas,
My true love gave to me:
Twelve drummers drumming
Eleven pipers piping
Ten lords a-leaping
Nine ladies dancing
Eight maids a-milking
Seven swans a-swimming
Six geese a-laying
Five golden rings
Four calling birds
Three french hens
Two turtle doves and
A partridge in a pear tree.", result);
        }

        [Test]
        public void ShouldPrintWholeLyric()
        {
            var sut = new TwelveDaysOfXmas();
            var result = sut.PrintLyric();
            Assert.AreEqual(@"On the first day of Christmas,
My true love gave to me:
A partridge in a pear tree.

On the second day of Christmas,
My true love gave to me:
Two turtle doves and
A partridge in a pear tree.

On the third day of Christmas,
My true love gave to me:
Three french hens
Two turtle doves and
A partridge in a pear tree.

On the forth day of Christmas,
My true love gave to me:
Four calling birds
Three french hens
Two turtle doves and
A partridge in a pear tree.

On the fifth day of Christmas,
My true love gave to me:
Five golden rings
Four calling birds
Three french hens
Two turtle doves and
A partridge in a pear tree.

On the sixth day of Christmas,
My true love gave to me:
Six geese a-laying
Five golden rings
Four calling birds
Three french hens
Two turtle doves and
A partridge in a pear tree.

On the seventh day of Christmas,
My true love gave to me:
Seven swans a-swimming
Six geese a-laying
Five golden rings
Four calling birds
Three french hens
Two turtle doves and
A partridge in a pear tree.

On the eighth day of Christmas,
My true love gave to me:
Eight maids a-milking
Seven swans a-swimming
Six geese a-laying
Five golden rings
Four calling birds
Three french hens
Two turtle doves and
A partridge in a pear tree.

On the ninth day of Christmas,
My true love gave to me:
Nine ladies dancing
Eight maids a-milking
Seven swans a-swimming
Six geese a-laying
Five golden rings
Four calling birds
Three french hens
Two turtle doves and
A partridge in a pear tree.

On the tenth day of Christmas,
My true love gave to me:
Ten lords a-leaping
Nine ladies dancing
Eight maids a-milking
Seven swans a-swimming
Six geese a-laying
Five golden rings
Four calling birds
Three french hens
Two turtle doves and
A partridge in a pear tree.

On the eleventh day of Christmas,
My true love gave to me:
Eleven pipers piping
Ten lords a-leaping
Nine ladies dancing
Eight maids a-milking
Seven swans a-swimming
Six geese a-laying
Five golden rings
Four calling birds
Three french hens
Two turtle doves and
A partridge in a pear tree.

On the twelfth day of Christmas,
My true love gave to me:
Twelve drummers drumming
Eleven pipers piping
Ten lords a-leaping
Nine ladies dancing
Eight maids a-milking
Seven swans a-swimming
Six geese a-laying
Five golden rings
Four calling birds
Three french hens
Two turtle doves and
A partridge in a pear tree.", result);
        }
    }
}