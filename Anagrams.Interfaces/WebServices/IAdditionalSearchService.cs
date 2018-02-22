using Anagrams.Interfaces.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams.Interfaces.WebServices
{
    public interface IAdditionalSearchService
    {
       void AdditionalSearches(string ip);

       bool IfAllowedToSearch();

       Task DeleteWord(string nameField);

       Task UpdateWord(WordDto updatedWord, string Word);

       Task AddWord(WordDto word);

       string GetIpAddress();
    }
}
