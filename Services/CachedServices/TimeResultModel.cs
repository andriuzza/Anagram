using System.Collections.Generic;

namespace Services.CachedServices
{
    public class TimeResultModel
    {
        public int Time { get; set; }
        public List<string> Anagrams {get;set;}

        public TimeResultModel()
        {
            Anagrams = new List<string>();
        }
    }
}