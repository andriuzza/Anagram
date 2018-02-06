using System;
using System.Collections.Generic;

namespace Anagrams.Interfaces
{
    public interface IRepository<T>
    {
       HashSet<T> GetData(string Name);
    }
}
