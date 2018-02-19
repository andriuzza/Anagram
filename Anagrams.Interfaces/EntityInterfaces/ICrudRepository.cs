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
        T GetByString(object searchField); //get by id
        //get all
        int Save();
    }
}
