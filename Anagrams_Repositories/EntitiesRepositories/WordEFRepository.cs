using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces.DtoModel;
using Anagrams.Interfaces.EntityInterfaces;
using Anagrams_Repositories.GenericRepository;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
namespace Anagrams_Repositories.EntitiesRepositories
{
    public class WordEFRepository : GenericRepository<Word>, IDictionaryRepository<Word>
    {
       
        public async Task Delete(string searchField)
        {
            var result = GetByString(searchField);

            var resDelete = await result;

            _context.Words.Remove(resDelete);

          await _context.SaveChangesAsync();
        }

        private async Task<Word> GetByString(string searchField)
        {
            return await _context.Words
                .SingleOrDefaultAsync(s => s.Name.Equals(searchField));
        }

        public  async Task<Word> GetEntityDto(string searchField)
        {
            var result = await GetModel(searchField);

            return result;
        }

        public async Task<bool> IsExist(string Name)
        {
            var result = await _context.Words.AnyAsync(s => s.Name.Equals(Name));

            return result;
        }

        public async Task Update(Word model, string searchField)
        {
            var result = await GetModel(searchField);
            result.Name = model.Name;
           await _context.SaveChangesAsync();

        }

        private async Task<Word> GetModel(string searchField)
        {
           return await _context.Words
                   .SingleOrDefaultAsync(s => s.Name.Equals(searchField));

        }
    }
}
