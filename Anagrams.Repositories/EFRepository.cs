using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Repositories
{
    public class EFRepository : IWordRepository<string>
    {
        public HashSet<string> Contains(string Name)
        {
            throw new NotImplementedException();
        }

        public HashSet<string> GetData(string Name = null)
        {
            throw new NotImplementedException();
        }

        public bool InsertNewWord(string Name)
        {
            throw new NotImplementedException();
        }

        public string ReturnFilePath()
        {
            throw new NotImplementedException();
        }
    }
}
