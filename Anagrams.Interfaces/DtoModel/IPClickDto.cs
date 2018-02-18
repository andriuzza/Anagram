using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams.Interfaces.DtoModel
{
    public class IPClickDto
    {
        public string Ip { get; set; }
        public int Count { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime LastClick { get; set; }
    }
}
