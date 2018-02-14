/*using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

using Anagrams.Interfaces.Models;
using Services.Helpers;
using EntityframeworkDB.ADO.NET;

namespace Anagrams.Repositories.EFRepo
{
    public class EFRepository : IWordRepository<string>
    {
  
        private readonly ConnectionDb2018Entities1 _context;

        public EFRepository(string path)
        {
            _context = new ConnectionDb2018Entities1();
        }
        public HashSet<string> Contains(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var result = _context.Words.
                    Where(b=>b.Name.Contains(Name))
                    .Select(a => a.Name)
                    .ToList();

                return new HashSet<string>(result);
            }
            return GetData(Name);
        }

        public HashSet<string> GetCachedData(string Name)
        {
            var listOfAnagrams = new HashSet<string>();

            var str = Name.ToLower().GetWithoutWhiteSpace();
            var sortedName = String.Concat(str.OrderBy(c => c));

            var result = from ids in _context.CacheMaps.ToList()
                         join names in _context.CacheAnagrams.ToList()
                             on ids.Id equals names.Id
                         join wordName in _context.Words.ToList()
                         on names.WordId equals wordName.Id
                         where ids.SortedWord.Equals(sortedName)
                         select new { id = ids.Id, name = wordName.Name };

            int count = 0;
            foreach (var item in result)
            {
                string name = "";
                
                foreach(var itemNext in result)
                {
                    if(item.id == itemNext.id && item.name != itemNext.name)
                    {
                        name += item.name + " " + itemNext.name;
                    }
                }
                if (!string.IsNullOrEmpty(name))
                {
                    listOfAnagrams.Add(name);
                    name = "";
                    count++;
                }
               
            }

            return count > 0 ? listOfAnagrams : null;

        }

   

        public HashSet<string> GetData(string Name = null)
        {
            var result = _context.Words.Select(a => a.Name);
            return new HashSet<string>(result);
        }

        public bool InsertCache(HashSet<string> elements, string query)
        {
            var str = query.ToLower().GetWithoutWhiteSpace();
            var sortedName = String.Concat(str.OrderBy(c => c));

            for (var i = 0; i < elements.Count; i++)
            {
                CacheMap map = new CacheMap
                {
                    SortedWord = sortedName
                };
                _context.CacheMaps.Add(map);
            }

            
            _context.SaveChanges();
            

            var ids = from id in _context.CacheMaps
                      where id.SortedWord.Equals(sortedName)
                      select id.Id;



            var idAndWords = ids.ToList().Zip(elements, (n, w) => new { Number = n, Word = w });

            foreach(var item in idAndWords)
            {
                foreach(var name in GetString(item.Word))
                {
                    var wordId = _context.Words
                               .Where(a => a.Name.Equals(name)).First();
                    CacheAnagram anagram = new CacheAnagram
                    {
                        Id = item.Number,
                        WordId = wordId.Id
                    };
                    _context.CacheAnagrams.Add(anagram);
                }
            }

            if(_context.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public bool InsertLogUser(long TIME, string ip, string query)
        {
            var sortedName = query.ToLower().GetWithoutWhiteSpace();
            var str = String.Concat(sortedName.OrderBy(c => c));
            IPLogUser log = new IPLogUser
            {
                IP = ip,
                Time = (int)TIME,
                SortedWord = str
            };

          

            if(_context.IPLogUsers.Any(p=>p.IP.Equals(ip) && p.SortedWord.Equals(str)))
            {
                return true;
            }

            _context.IPLogUsers.Add(log);
            if (_context.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        private List<string> GetString(string Name)
        {
            List<string> list = new List<string>();
            string newStr = "";
            for (var i = 0; i < Name.Length; i++)
            {
                if (Name[i] == ' ')
                {
                    if (newStr.Length > 1 && i != Name.Length - 1)
                    {
                        list.Add(newStr);
                        newStr = "";
                        continue;
                    }
                    break;
                }
                newStr += Name[i];
            }
            list.Add(newStr);
            return list;
        }
        public bool InsertNewWord(string Name)
        {
            throw new NotImplementedException();
        }

        public string ReturnFilePath()
        {
            throw new NotImplementedException();
        }

        public List<TimeResultModel> ReturnIPSearches(string IP)
        {
            var list = new List<TimeResultModel>();
            var strings = from item in _context.IPLogUsers.ToList()
                          where item.IP.Equals(IP)
                          select new { item.SortedWord, item.Time };

            foreach(var item in strings)
            {
                list.Add(new TimeResultModel
                {
                    Time = item.Time,
                    Anagrams = new HashSet<string>(GetCachedData(item.SortedWord))
                });
            }

            return list.Count > 0 ?list : null;
        }
    }
}*/