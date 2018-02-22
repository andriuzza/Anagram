using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams.Interfaces.EntityInterfaces
{
    public interface IDictionaryRepository<T>
    {
        Task<bool> IsExist(string Name);
        void Add(T model);
        Task Delete(string searchField);
        Task Update(T model, string searchField);
        Task<T> GetEntityDto(string searchField);
    }
}
