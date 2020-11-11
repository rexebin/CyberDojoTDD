using System.Collections.Generic;

namespace CyberDojo.OneHundredDoors
{
    public class OneHundredDoors
    {
        public List<bool> Doors { get; }

        public OneHundredDoors()
        {
            Doors = new List<bool>();
            for (var i = 0; i < 100; i++)
            {
                Doors.Add(false);
            }
        }

        public void Visit(int turnCount)
        {
            for (var i = turnCount; i <= 100; i += turnCount)
            {
                Doors[i - 1] = !Doors[i - 1];
            }
        }
    }
}