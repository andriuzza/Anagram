using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Anagram
    {
        [Required]
        public string Name { get; set; }
    }
}