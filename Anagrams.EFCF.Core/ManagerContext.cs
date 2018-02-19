using Anagrams.EFCF.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams.EFCF.Core
{
    public class ManagerContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<IPClick> IPClicks { get; set; }
        public DbSet<IPLogUser> IPLogUsers { get; set; }
        public DbSet<CacheAnagram> CacheAnagrams { get; set; }
        public DbSet<CacheMap> CacheMaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
         //   mb.Entity<Word>().
            base.OnModelCreating(mb);
        }
    }
}
