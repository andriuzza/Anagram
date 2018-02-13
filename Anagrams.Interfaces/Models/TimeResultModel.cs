using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Interfaces.Models
{
    public class TimeResultModel
    {
        public int Time { get; set; }
        public List<string> Anagrams { get; set; }

        public TimeResultModel()
        {
            Anagrams = new List<string>();
        }
    }
}
