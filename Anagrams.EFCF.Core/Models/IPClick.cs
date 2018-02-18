using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams.EFCF.Core.Models
{
    public class IPClick
    {
        [Key]
        [Required]
        [MaxLength(30)]
        public string IP { get;  set; }

        [Required]
        public int Count { get; set; } = 0;

        [Required]
        public DateTime Expiration { get;  set; }

        private IPClick(){ }

        public IPClick(string IPAdress)
        {
            IP = IPAdress;
            Count = 1;
            Expiration = DateTime.Now;
        }
            
    }
}
