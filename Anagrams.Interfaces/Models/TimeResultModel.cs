using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Interfaces.Models
{
    public class TimeResultModel
    {
        public int Time { get; set; }
        public HashSet<string> Anagrams { get; set; } = null;

        public TimeResultModel()
        {
            Anagrams = new HashSet<string>();
        }
    }
}
