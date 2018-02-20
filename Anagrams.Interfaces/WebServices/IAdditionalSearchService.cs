using Anagrams.Interfaces.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Interfaces.WebServices
{
    public interface IAdditionalSearchService
    {
       void AdditionalSearches(string ip);

       bool IfAllowedToSearch();

       void DeleteWord(string nameField);

       void UpdateWord(WordDto updatedWord, string Word);

       void AddWord(WordDto word);

       string GetIpAddress();
    }
}
