using Anagrams.EFCF.Core;
using Anagrams.Interfaces.EntityInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams_Repositories.GenericRepository
{
    public abstract class GenericRepository<T> : ICrudRepository<T> where T : class
    {
        protected readonly ManagerContext _context;

        protected GenericRepository()
        {
           _context = new ManagerContext();
        }
        public void Add(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(object searchField)
        {
            T exsitng = _context.Set<T>().Find(searchField);
            _context.Set<T>().Remove(exsitng);
        }

        public void Edit(T entity, string updateField)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public T GetByKey(object searchField)
        {
            return _context.Set<T>().Find(searchField);
        }

    }
}
