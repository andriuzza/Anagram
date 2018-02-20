using Anagrams.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Anagrams.Interfaces
{
    public interface IWordRepository<T> : ICacheManager where T : class
    {
       HashSet<T> GetData(string Name = null);
       HashSet<T> Contains(string Name);
    }

    public interface ICacheManager
    {
        bool InsertLogUser(long TIME, string ip, string query); 
        HashSet<string> GetCachedData(string Name); 
        List<TimeResultModel> ReturnIPSearches(string IP);
        bool InsertCache(HashSet<string> elements, string query);
    }

}
    