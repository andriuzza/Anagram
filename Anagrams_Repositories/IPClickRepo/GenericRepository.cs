using Anagrams.EFCF.Core;
using Anagrams.Interfaces.EntityInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams_Repositories.IPClickRepo
{
    public class GenericRepository<T> : ICrudRepository<T> where T : class
    {
        private readonly ManagerContext _context = null;
        private DbSet<T> entity = null;

        public GenericRepository(ManagerContext context)
        {
           _context = context;
           entity =  _context.Set<T>();
        }
        public void Add(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(object searchField)
        {
            T exsitng = entity.Find(searchField);
            entity.Remove(exsitng);
        }

        public void Edit(T entity, string updateField)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public T GetByString(object searchField)
        {
            return entity.Find(searchField);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
