using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Interfaces.EntityInterfaces
{
    public interface IDictionaryRepository<T>
    {
        bool IsExist(string Name);
        void Add(T model);
        void Delete(string searchField);
        void Update(T model, string searchField);
        T GetEntityDto(string searchField);
    }
}
