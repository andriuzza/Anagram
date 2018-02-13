using Anagrams.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Anagrams.Interfaces
{
    public interface IWordRepository<T> : ICacheManager
    {
       HashSet<T> GetData(string Name = null);
       bool InsertNewWord(string Name);
       string ReturnFilePath(); // delete this method
       HashSet<string> Contains(string Name); // change name
    }

    public interface ICacheManager
    {
        void InsertLogUser(long TIME, string ip, string query); 
        HashSet<string> GetCachedData(string Name); 
        List<TimeResultModel> ReturnIPSearches(string IP);
    }

}
    