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
    public class ClickEFRepository : IClickRepository
    {
        private readonly ManagerContext _context;

        public ClickEFRepository()
        {
            _context = new ManagerContext();
        }

        public void Add(string ip)
        {
            _context.IPClicks.Add(new IPClick(ip));

            _context.SaveChanges();
        }

        public IPClickDto GetEntity(string IP)
        {
            var entity = _context.IPClicks
                .FirstOrDefault(s => s.IP.Equals(IP));

            if(entity == null)
            {
                return null;
            }

            return new IPClickDto
            {
                Count = entity.Count,
                Ip = entity.IP,
                Expiration = entity.Expiration
            };
        }

        public void Update(IPClickDto model)
        {
            var result = _context.IPClicks.SingleOrDefault(u => u.IP.Equals(model.Ip));

            result.Count = model.Count;
            result.Expiration = model.Expiration;

            _context.SaveChanges();
        }
    }
}
