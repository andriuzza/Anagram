using Anagrams.EFCF.Core;
using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces.DtoModel;
using Anagrams.Interfaces.EntityInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams_Repositories.EntitiesRepositories
{
    public class WordEFRepository : IDictionaryRepository<WordDto>
    {
        private readonly ManagerContext _context;

        public WordEFRepository()
        {
            _context = new ManagerContext();
        }


        public void Add(WordDto model)
        {
            _context.Words.Add(new Word
            {
                Name = model.Name
            });
            _context.SaveChanges();
        }

        public void Delete(string searchField)
        {
            var result = GetModel(searchField);
            _context.Words.Remove(result);

            _context.SaveChanges();

        }

        public WordDto GetEntity(string searchField)
        {
            var result = GetModel(searchField);

            return new WordDto
            {
                Name = result.Name
            };
        }

        public bool IsExist(string Name)
        {
            return _context.Words.Any(s => s.Name.Equals(Name));
        }

        public void Update(WordDto model, string searchField)
        {
            var result = GetModel(searchField);
            result.Name = model.Name;
            _context.SaveChanges();

        }

        private Word GetModel(string searchField)
        {
           return  _context.Words
                   .SingleOrDefault(s => s.Name.Equals(searchField));
        }

        public int Save()
        {
          return  _context.SaveChanges();
        }
    }
}
