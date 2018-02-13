using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.ADO.NET;

namespace Web.EFRepo
{
    public class EFRepository : IWordRepository<string>
    {
  
        private readonly ConnectionDb2018Entities3 _context;

        public EFRepository(string path)
        {
            _context = new ConnectionDb2018Entities3();
        }
        public HashSet<string> Contains(string Name)
        {
            HashSet<string> list;
            if (!string.IsNullOrEmpty(Name))
            {
                var result = _context.Words.
                    Where(b=>b.Name.Contains(Name))
                    .Select(a => a.Name)
                    .ToList();

                list = new HashSet<string>(result);
            }
            return GetData(Name);
        }

        public HashSet<string> GetData(string Name = null)
        {
            var result = _context.Words.Select(a => a.Name);
            return new HashSet<string>(result);
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