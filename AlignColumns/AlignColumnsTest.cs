using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CyberDojo.AlignColumns
{
    public class AlignColumnsTest
    {
        private AlignColumns _sut = null!;
        [SetUp]
        public void GetInstance()
        {
            _sut = new AlignColumns();
        }
        
        [Test]
        public void GivenSingleLineString_ShouldGetFieldWidths()
        {
            var input = "Given$a$text$file$of$many$lines";
            var expected = new List<List<int>>
            {
                new[] {6, 2, 5, 5, 3, 5, 6}.ToList()
            };
            var result = _sut.GetAllColumnWidths(input);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GivenMultipleLinesText_ShouldGetAllFieldWidths()
        {
            var input = @"Given$a$text$file$of$many$lines
                        $where$fields$within$a$line$";
            var expected = new List<List<int>>
            {
                new[] {6, 2, 5, 5, 3, 5, 6}.ToList(),
                new[] {6, 7, 7, 2, 5}.ToList()
            };
            var result = _sut.GetAllColumnWidths(input);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GivenTwoListOfColumnWidths_ShouldGetListOfMaxColumnWidth()
        {
            var input = new List<List<int>>
            {
                new[] {6, 2, 5, 5, 3, 5, 6}.ToList(),
                new[] {7, 1, 10, 4, 6}.ToList()
            };
            var expected = new[] {7, 2, 10, 5, 6, 5, 6};
            var result = _sut.CalculateMaxColumnWidth(input);
            
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenText_ShouldGetMaxColumnWidths()
        {
            var text = @"Given$a$text$file$of$many$lines,$where$fields$within$a$line$
            are$delineated$by$a$single$'dollar'$character,$write$a$program
                that$aligns$each$column$of$fields$by$ensuring$that$words$in$each$
            column$are$separated$by$at$least$one$space.";
            var expected = new List<int>
            {
                7, 11, 10, 7, 7, 9,11,9,7,8,3,5 
            };
            var result = _sut.GetMaxColumnWidth(text);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void GivenText_ShouldPrintAlignedText()
        {
            var text = @"Given$a$text$file$of$many$lines,$where$fields$within$a$line$
            are$delineated$by$a$single$'dollar'$character,$write$a$program
                that$aligns$each$column$of$fields$by$ensuring$that$words$in$each$
            column$are$separated$by$at$least$one$space.";
            var result = _sut.Print(text);
            var expected = 
@"Given  a          text      file   of     many     lines,     where    fields within  a  line
are    delineated by        a      single 'dollar' character, write    a      program
that   aligns     each      column of     fields   by         ensuring that   words   in each
column are        separated by     at     least    one        space.";
            Assert.AreEqual(expected, result);
        }
        
        
    }
}