using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CyberDojo.MergeIntervals;

public class MergeIntervalsTest
{
    [Test]
    public void ShouldReturnMergedIntervals()
    {
        var input = new [] { new[] { 1, 4 }, new[] { 4, 5 } };
        var expected = new [] { new[] { 1, 5 } };
        Assert.AreEqual(MergeInterval.GetMergedIntervals(input), expected);
    }

    [Test]
    public void ShouldReturnMergedIntervals1()
    {
        var input = new []
        {
            new[] { 1, 3 }, new[] { 2, 6 }, new[] { 8, 10 }, new[] { 15, 18 }
        };
        var expected = new [] { new[] { 1, 6 }, new[] { 8, 10 }, new[] { 15, 18 } };
        Assert.AreEqual(MergeInterval.GetMergedIntervals(input), expected);
    }
    
    [Test]
    public void ShouldReturnMergedIntervals3()
    {
        var input = new []
        {
            new[] { 1, 4 }, new[] { 0, 1 }
        };
        var expected = new [] { new[] { 0, 4 }};
        Assert.AreEqual(MergeInterval.GetMergedIntervals(input), expected);
    }
    
    [Test]
    public void ShouldReturnMergedIntervals4()
    {
        var input = new []
        {
            new[] { 1, 4 }, new[] { 0, 0 }
        };
        var expected = new [] { new[] { 0, 0 }, new[]{1,4}};
        Assert.AreEqual(MergeInterval.GetMergedIntervals(input), expected);
    }
}

public class MergeInterval
{
    public static int[][] GetMergedIntervals(int[][] input)
    {
        var sortedInput = input.OrderBy(x => x[0]).ToList();
        var merged = new List<int[]> 
        {
          sortedInput[0]  
        }; 
        for (int i = 1; i < sortedInput.Count; i++)
        {
            var last = merged.Last();
            var current = sortedInput[i];
            if (last[1] < current[0])
            {
                merged.Add(sortedInput[i]);
                continue;
            }

            last[0] = Math.Min(last[0], current[0]);
            last[1] = Math.Max(last[1], current[1]);
        }

        return merged.ToArray();
    }
}