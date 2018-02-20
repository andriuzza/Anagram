using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces.DtoModel;
using Anagrams.Interfaces.EntityInterfaces;
using Anagrams_Repositories.GenericRepository;
using System.Linq;

namespace Anagrams_Repositories.EntitiesRepositories
{
    public class ClickEFRepository : GenericRepository<IPClick>, IClickRepository
    {

        public void Add(string ip)
        {
            base.Add(new IPClick(ip));

            _context.SaveChanges();
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
