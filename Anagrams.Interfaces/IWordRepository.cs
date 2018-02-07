using System;
using System.Collections.Generic;

namespace Anagrams.Interfaces
{
    public interface IWordRepository<T>
    {
       HashSet<T> GetData(string Name = null);
    }
}
