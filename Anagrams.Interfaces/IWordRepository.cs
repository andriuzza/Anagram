using System;
using System.Collections.Generic;

namespace Anagrams.Interfaces
{
    public interface IWordRepository<T> : IRepo<string>
    {
       HashSet<T> GetData(string Name = null);
       bool InsertNewWord(string Name);
       string ReturnFilePath(); // delete this method
       HashSet<string> Contains(string Name); // change name
    }

    public interface IRepo<T>
    {
        //crud
    }
}
    