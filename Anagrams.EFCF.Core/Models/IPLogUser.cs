using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams.EFCF.Core.Models
{
    public partial class IPLogUser
    {
        [Key, Column(Order = 0)]
        public string IP { get; set; }
        public int Time { get; set; }
        [Key, Column(Order = 1)]
        public string SortedWord { get; set; }
    }
}
