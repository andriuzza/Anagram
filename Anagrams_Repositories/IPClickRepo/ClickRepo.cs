using Anagrams.EFCF.Core;
using Anagrams.EFCF.Core.Migrations;
using Anagrams.Interfaces.EntityInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anagrams.EFCF.Core.Models;
using System.Data.Entity;

/*namespace Anagrams_Repositories.IPClickRepo
{
   public class ClickRepo 
    {
        private readonly ManagerContext _context;

        public ClickRepo()
        {
            _context = new ManagerContext();
        }

        public IPClick GetUser(string ip)
        {
            return _context.IPClicks
                .FirstOrDefault(s => s.IP.Equals(ip));
        }


        public void DeleteWord(string Word)
        {
            var word = _context.Words
                .Where(e => e.Name.Equals(Word)).First();

            _context.Words.Remove(word);

            _context.SaveChanges();
        }

        public void UpdateWord(string Word, string updatedWord)
        {
            var word = _context.Words
                .Where(e => e.Name.Equals(Word)).First();

            word.Name = updatedWord;

            _context.SaveChanges();

        }

        public void AddWord(string Word)
        {
            _context.Words.Add(new Anagrams.EFCF.Core.Models.Word
            {
                Name = Word
            });

            _context.SaveChanges();
        }

        public void AddIPAdress(string ip)
        {
            _context.IPClicks
                .Add(new IPClick(ip));

            _context.SaveChanges();
        }

        public void Delete(IPClick entity)
        {
            var word = _context.Words
                .Where(e => e.Name.Equals(Word)).First();

            _context.Words.Remove(word);

            _context.SaveChanges();
        }

        public void Edit(IPClick entity, string updateField)
        {
            throw new NotImplementedException();
        }

        public void Add(IPClick entity)
        {
            throw new NotImplementedException();
        }

        public IPClick GetByString(string searchField)
        {
            throw new NotImplementedException();
        }
    }
}
*/