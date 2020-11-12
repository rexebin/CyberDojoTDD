using System.Collections.Generic;
using System.Linq;

namespace CyberDojo.Anagrams
{
    public class Anagrams
    {
        public List<string> GetAnagrams(params string[] list)
        {
            if (list.Length == 1)
            {
                return list.ToList();
            }

            var result = new List<string>();
            for (var i = 0; i < list.Length; i++)
            {
                var restOfList = list.ToList();
                restOfList.RemoveAt(i);
                var anagrams = GetAnagrams(restOfList.ToArray()).Select(x => list[i] + x).ToList();
                result = result.Concat(anagrams).ToList();
            }

            return result;
        }

        public List<string> GetAnagrams(string text)
        {
            return GetAnagrams(text.Select(x => x.ToString()).ToArray());
        }
    }
}