﻿using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces.DtoModel;
using Anagrams.Interfaces.EntityInterfaces;
using Anagrams_Repositories.GenericRepository;
using System.Linq;

namespace Anagrams_Repositories.EntitiesRepositories
{
    public class WordEFRepository : GenericRepository<WordDto>, IDictionaryRepository<WordDto>
    {

        public new void Add(WordDto model)
        {
            _context.Words.Add(new Word
            {
                Name = model.Name
            });

            _context.SaveChanges();
        }

        public void Delete(string searchField)
        {
            var result = GetByString(searchField);

            _context.Words.Remove(result);

            _context.SaveChanges();
        }

        private Word GetByString(string searchField)
        {
            return _context.Words
                .SingleOrDefault(s => s.Name.Equals(searchField));
        }

        public WordDto GetEntityDto(string searchField)
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
    }
}