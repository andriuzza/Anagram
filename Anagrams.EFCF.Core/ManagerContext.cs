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
        public DbSet<IPLogUser> IPLogUser { get; set; }
        public DbSet<CacheAnagram> CacheAnagram { get; set; }
        public DbSet<CacheMap> CacheMap { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            base.OnModelCreating(mb);
        }
    }
}
