using System;
using System.Linq;
using NUnit.Framework;

namespace CyberDojo.SingleNumber;

public class SingleNumber
{
    [Test]
    public void ShouldReturnSingleNumber()
    {
        var input = new[] { 2, 2, 1 };
        Assert.AreEqual(FindSingleNumber(input), 1);
    }
    
    [Test]
    public void ShouldReturnSingleNumber1()
    {
        var input = new[] { 4,1,2,1,2};
        Assert.AreEqual(FindSingleNumber(input), 4);
    }

    private int FindSingleNumber(int[] input)
    {
        return input.Skip(1).Aggregate(input[0], (curr, next) => curr ^ next);
    }
}