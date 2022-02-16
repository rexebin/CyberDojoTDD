using System.Collections.Generic;
using System.Linq;

namespace CyberDojo.TaskScheduler;
public static class TaskSchedule
{
    public static int LeastInterval(char[] tasks, int n)
    {
        var heap = new PriorityQueue<int, int>(tasks.GroupBy(x => x).Select(x => (x.Count(), -x.Count())));
        var queue = new Queue<CountTime>();
        var time = 0;
        while (heap.Count > 0 || queue.Count > 0)
        {
            time++;
            if (heap.Count > 0)
            {
                var count = heap.Dequeue();
                if (count-1 > 0)
                {
                    var item = new CountTime(count - 1, time + n);
                    queue.Enqueue(item);
                }
            }

            if (queue.Count <= 0) continue;
            var top = queue.Peek();
            if (top.Time == time)
            {
                heap.Enqueue(top.Count, -top.Count);
                queue.Dequeue();
            }
        }

        return time;
    }
}

public class CountTime
{
    public CountTime(int count, int time)
    {
        Count = count;
        Time = time;
    }

    public int Count { get; set; }
    public int Time { get; set; }
}