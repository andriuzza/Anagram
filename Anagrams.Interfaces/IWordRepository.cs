using Anagrams.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anagrams.Interfaces
{
    public interface IWordRepository<T> : IWordRepositoryAsync<T>, ICacheManager where T : class
    {
        HashSet<string> GetData(string Name = null);
        HashSet<string> Contains(string Name);
        void RefrehDictionary();
    }

    public interface ICacheManager
    {
        bool InsertLogUser(long TIME, string ip, string query);
        HashSet<string> GetCachedData(string Name);
        List<TimeResultModel> ReturnIPSearches(string IP);
        bool InsertCache(HashSet<string> elements, string query);
    }

    public interface IWordRepositoryAsync<T>
    {
        Task RefrehDictionaryAsync();
    }

}
    