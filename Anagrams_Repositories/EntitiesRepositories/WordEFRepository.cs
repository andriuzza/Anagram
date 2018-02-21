using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces.DtoModel;
using Anagrams.Interfaces.EntityInterfaces;
using Anagrams_Repositories.GenericRepository;
using System.Linq;

namespace Anagrams_Repositories.EntitiesRepositories
{
    public class WordEFRepository : GenericRepository<Word>, IDictionaryRepository<Word>
    {
       
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

        public Word GetEntityDto(string searchField)
        {
            var result = GetModel(searchField);

            return result;
        }

        public bool IsExist(string Name)
        {
            return _context.Words.Any(s => s.Name.Equals(Name));
        }

        public void Update(Word model, string searchField)
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
