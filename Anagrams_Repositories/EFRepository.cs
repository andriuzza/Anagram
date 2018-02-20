using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Anagrams.Interfaces.Models;
using Anagrams.EFCF.Core;
using Services.Helpers;
using Anagrams.EFCF.Core.Models;

namespace Anagrams_Repositories
{
    public class EFRepository : IWordRepository<Word>
    {
        private readonly ManagerContext _context;
        private HashSet<string> InitializeDictionary;

        public EFRepository()
        {
            _context = new ManagerContext();
            InitializeDictionary = new HashSet<string>(_context.Words.Select(a=>a.Name).ToList());
        }

        public HashSet<string> Contains(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var result = InitializeDictionary.
                Where(b => b.Contains(Name));

                return new HashSet<string>(result);
            }
            return GetData(Name);
        }


        public HashSet<string> GetCachedData(string Name)
        {
            var listOfAnagrams = new HashSet<string>();

            var str = Name.ToLower().GetWithoutWhiteSpace();
            var sortedName = String.Concat(str.OrderBy(c => c));

            var result = from ids in _context.CacheMaps
                         join names in _context.CacheAnagrams
                             on ids.Id equals names.Id
                         join wordName in _context.Words
                         on names.WordId equals wordName.Id
                         where ids.SortedWord.Equals(sortedName)
                         select new { id = ids.Id, name = wordName.Name };

            int count = 0;
            foreach (var item in result.ToList())
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
            if (!string.IsNullOrEmpty(Name))
            {
                return InitializeDictionary;
            }
            return InitializeDictionary;

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
                foreach(var name in UnconcatWords(item.Word))
                {
                    var wordId = _context.Words
                               .Where(a => a.Name.Equals(name)).First();
                    CacheAnagram anagram = new CacheAnagram
                    {
                        Id = item.Number,
                        WordId = wordId.Id
                    };

                    if (_context.CacheMaps.Any(g => g.SortedWord.Equals(sortedName)))
                    {
                        _context.CacheAnagrams.Add(anagram);
                    }
                }
            }
            
            if(_context.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }


        public List<TimeResultModel> ReturnIPSearches(string IP)
        {
            var list = new List<TimeResultModel>();
            var strings = from item in _context.IPLogUsers
                          where item.IP.Equals(IP)
                          select new { item.SortedWord, item.Time };

            foreach (var item in strings.ToList())
            {
                var data = GetCachedData(item.SortedWord);
                var anagrams = data ?? new HashSet<string>();
                list.Add(new TimeResultModel
                {
                    Time = item.Time,
                    Anagrams = new HashSet<string>(anagrams)
                });
            }

            return list.Count > 0 ? list : null;
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

        private List<string> UnconcatWords(string Name)
        {
            var list = new List<string>();
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
    }
}