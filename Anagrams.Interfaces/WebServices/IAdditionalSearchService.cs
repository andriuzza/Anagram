using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Interfaces.WebServices
{
    public interface IAdditionalSearchService
    {
       void AdditionalSearches(string ip);
       bool IfAllowedToSearch(string ip);
    }
}
