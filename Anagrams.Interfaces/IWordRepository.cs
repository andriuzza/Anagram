using Anagrams.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Anagrams.Interfaces
{
    public interface IWordRepository<T> : ICacheManager<T>
    {
       HashSet<T> GetData(string Name = null);
       HashSet<T> Contains(string Name);
    }

    public interface ICacheManager<T>
    {
        bool InsertLogUser(long TIME, string ip, string query); 
        HashSet<T> GetCachedData(string Name); 
        List<TimeResultModel> ReturnIPSearches(string IP);
        bool InsertCache(HashSet<T> elements, string query);
    }

}
    