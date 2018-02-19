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
    public class ClickEFRepository : GenericRepository<IPClick>, IClickRepository
    {

        public void Add(string ip)
        {
            base.Add(new IPClick(ip));
            base.Save();
        }

        public IPClickDto GetEntity(string IP)
        {
            var entity = base.GetByKey(IP);

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
