using Anagrams.Interfaces.EntityInterfaces;
using System;
using Anagrams.Interfaces.WebServices;
using Anagrams.Interfaces.DtoModel;
using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces.Helpers;

namespace Services
{
    public class AdditionalSearchService : IAdditionalSearchService
    {
        private const int MaxCount = 3;
        private const int MaxTimeInMinutes = 3;

        private readonly IClickRepository _clickRepo;
        private readonly IDictionaryRepository<Word> _wordsRepo;

        private string ip = System.Net.Dns
               .GetHostEntry(System.Net.Dns.GetHostName())
               .AddressList[1].ToString();

        public AdditionalSearchService(IClickRepository clickRepo, IDictionaryRepository<Word> wordsRepo)
        {
            _clickRepo = clickRepo;
            _wordsRepo = wordsRepo;
        }

        public void DeleteWord(string nameField)
        {
            _wordsRepo.Delete(nameField);
            AdditionalSearches(ip);
        }

        public void UpdateWord(WordDto updatedWord, string Word)
        {
            if (_wordsRepo.IsExist(Word))
            {
                _wordsRepo.Update(updatedWord.ToEntity(), Word);
                AdditionalSearches(ip);
            }
            else
            {
                throw new Exception("The word is not defined yet");
            }
  
        }

        public void AddWord(WordDto word)
        {
            _wordsRepo.Add(word.ToEntity());
            AdditionalSearches(ip);
        }


        public void AdditionalSearches(string ip)
        {
            var getModel = _clickRepo.GetEntity(ip);
            getModel.Count--;

            _clickRepo.Update(getModel);
        }


        public bool IfAllowedToSearch()
        {
            var getModel = _clickRepo.GetEntity(ip);
            if(getModel == null)
            {
                _clickRepo.Add(ip);

                return true;
            }

            if (getModel.Count == MaxCount
               && getModel.Expiration <= DateTime.Now)
            {
                getModel.Count = 0;
                getModel.Expiration = DateTime.Now;

                _clickRepo.Update(getModel);
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

            _clickRepo.Update(getModel);

            return true;
        }

        public string GetIpAddress()
        {
            return ip;
        }
    }   
}
