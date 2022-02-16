using NUnit.Framework;

namespace CyberDojo.TaskScheduler;

public class TaskTest
{
    [Test]
    [TestCase(new[] { 'A', 'A', 'A', 'B', 'B', 'B' }, 2, 8)]
    [TestCase(new[] { 'A', 'A', 'A', 'B', 'B', 'B' }, 0, 6)]
    [TestCase(new[] { 'A', 'A', 'A', 'A', 'A', 'A', 'B', 'C', 'D', 'E', 'F', 'G' }, 2, 16)]
    public void ShouldReturnAnswer(char[] input, int n, int expected)
    {
        var result = TaskSchedule.LeastInterval(input, n);
        Assert.AreEqual(result, expected);
    }
}