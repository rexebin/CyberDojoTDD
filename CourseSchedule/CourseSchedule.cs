using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CyberDojo.CourseSchedule;

public class CourseSchedule
{
    [Test]
    public void ShouldReturnAdjacentTree()
    {
        var input = new[] { new[] { 1, 0 } }; //[[1,0]]
        var adjacentTree = new Dictionary<int, int[]>
        {
            { 1, new[] { 0 } },
            {0, Array.Empty<int>()}
        };
        Assert.AreEqual(adjacentTree, CourseScheduler.ConvertToAdjacentTree(input, 2));
    }

    [Test]
    public void ShouldReturnAdjacentTreeComplicated()
    {
        var input = new[] { new[] { 1, 0 }, new[] { 2, 0 } }; //[[1,0]]
        var adjacentTree = new Dictionary<int, int[]>
        {
            { 1, new[] { 0 } },
            { 2, new[] { 0 } },
            { 0, Array.Empty<int>()}
        };
        Assert.AreEqual(adjacentTree, CourseScheduler.ConvertToAdjacentTree(input, 3));
    }

    [Test]
    public void ShouldReturnCorrectAnswer()
    {
        var input = new[] { new[] { 1, 0 } };
        var expected = new[] { 0, 1 };
        Assert.AreEqual(expected, CourseScheduler.FindOrder(input, 2));
    }
    
    [Test]
    public void ShouldReturnCorrectAnswer1()
    {
        var input = new[] { new[] { 1, 0 }, new []{2, 0}, new []{3, 1}, new []{3, 2}  }; //[[1,0],[2,0],[3,1],[3,2]]
        var expected = new[] { 0, 1, 2 , 3 };
        var result = CourseScheduler.FindOrder(input, 4);
        Assert.AreEqual(expected, result);
    }
}

public class CourseScheduler
{
    public static Dictionary<int, int[]> ConvertToAdjacentTree(int[][] input, int courses)
    {
        var result = input.GroupBy(x => x[0]).ToDictionary(x => x.Key, x => x.Select(y => y[1]).Distinct().ToArray());
        for (int i = 0; i < courses; i++)
        {
            if (!result.TryGetValue(i, out var _))
            {
                result.Add(i, new int[]{});
            };
        }
        return result;
    }

    public static int[] FindOrder(int[][] input, int numberOfCourse)
    {
        var adjTree = ConvertToAdjacentTree(input, numberOfCourse);
        var result = new List<int>();
        var visited = new HashSet<int>();
        var cycle = new HashSet<int>();
        bool SearchPrerequisites(int i)
        {
            if (cycle.Contains(i))
            {
                return false;
            }

            if (visited.Contains(i))
            {
                return true;
            }
            cycle.Add(i);
            foreach (var dep in adjTree[i])
            {
                if (SearchPrerequisites(dep) == false)
                {
                    return false;
                }
            }

            cycle.Remove(i);
            visited.Add(i);
            result.Add(i);
            return true;
        }

        for (var i = 0; i < numberOfCourse; i++)
        {
            if (!SearchPrerequisites(i))
            {
                return new int[] { };
            }
        }

        return result.ToArray();
        
        
    }

    
}