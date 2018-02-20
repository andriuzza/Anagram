using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Interfaces.EntityInterfaces
{
    public interface ICrudRepository<T>
    {
        void Delete(object seachField);
        void Edit(T entity, string updateField);
        void Add(T entity);
        T GetByKey(object searchField); //get by id
    }
}
