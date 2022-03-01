using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CyberDojo.Aladin;

public class Aladin
{
    [Test]
    public void ShouldPass()
    {
        var magic = new List<int> { 3, 2, 5, 4 };
        var dist = new List<int> { 2, 3, 4, 2 };
        var result = optimalPoint(magic, dist);
        Assert.AreEqual(0, result);
    }
    
    [Test]
    public void ShouldPass1()
    {
        var magic = new List<int> {2, 4, 5, 2
        };
        var dist = new List<int> { 4, 3, 1, 3};
        var result = optimalPoint(magic, dist);
        Assert.AreEqual(1, result);
    }
    
    [Test]
    public void ShouldPass2()
    {
        var magic = new List<int> {8, 4, 1, 9
        };
        var dist = new List<int> { 10, 9, 3, 5};
        var result = optimalPoint(magic, dist);
        Assert.AreEqual(-1, result);
    }


    public static int optimalPoint(List<int> magic, List<int> dist)
    {
        var power = 0;
        var result = -1;
        for (var i = 0; i < magic.Count(); i++)
        {
            var powers = Resuffle(magic, i);
            var completed = true;
            
            var distance = Resuffle(dist, i);
            for (int j = 0; j < dist.Count(); j++)
            {
                    power += powers[j];
                    var d = distance[j];
                    if (d > power)
                    {
                        completed = false;
                        break;
                    }

                    power -= d;   
                
            }

            if (completed)
            {
                result = i;
                break;    
            };
            power = 0;

        }

        return result;
    }

    private static List<int> Resuffle(List<int> magic, int i)
    {
        var magicPower = magic.Skip(i).ToList();
        var firstPowers = magic.Take(i);
        var powers = magicPower.Concat(firstPowers).ToList();
        return powers;
    }
}