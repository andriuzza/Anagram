using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces.EntityInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anagrams.Interfaces.WebServices;
namespace Web.Services
{
    public class AdditionalSearchService : IAdditionalSearchService
    {
        private const int MaxCount = 3;
        private const int MaxTimeInMinutes = 3;

        private readonly IClickRepository _repo;
      
        public AdditionalSearchService(IClickRepository repo)
        {
            _repo = repo;
        }

        public void AddNewIpAddress(string ip)
        {

        }

        public void AdditionalSearches(string ip)
        {
            var getModel = _repo.GetEntity(ip);
            getModel.Count--;

            _repo.Update(getModel);
        }


        public bool IfAllowedToSearch(string ip)
        {
            var getModel = _repo.GetEntity(ip);
            if(getModel == null)
            {
                _repo.Add(ip);

                return true;
            }

            if (getModel.Count == MaxCount
               && getModel.Expiration <= DateTime.Now)
            {
                getModel.Count = 0;
                getModel.Expiration = DateTime.Now;

                _repo.Update(getModel);
                return true;
            }

            if (getModel.Count == MaxCount)
            {
                return false;
            }
            
            getModel.Count++;

            if(getModel.Count == MaxCount)
            {
                getModel.Expiration = DateTime.Now.AddMinutes(MaxTimeInMinutes);
            }

            _repo.Update(getModel);

            return true;
        }

    }   
}
