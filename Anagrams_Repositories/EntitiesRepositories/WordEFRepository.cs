using Anagrams.EFCF.Core;
using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces.DtoModel;
using Anagrams.Interfaces.EntityInterfaces;
using Anagrams_Repositories.IPClickRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams_Repositories.EntitiesRepositories
{
    public class WordEFRepository : GenericRepository<WordDto>, IDictionaryRepository<WordDto>
    {

        public void  Add(WordDto model)
        {
            _context.Words.Add(new Word
            {
                Name = model.Name
            });
            _context.SaveChanges();
        }

        public void Delete(string searchField)
        {
           var result = base.GetByKey(searchField);

            base.Delete(result);

            base.Save();
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

        public override int Save()
        {
          return  _context.SaveChanges();
        }
    }
}
