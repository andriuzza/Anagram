using Anagrams.EFCF.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Interfaces.Services
{
    public interface ITimingService
    {
        HashSet<string> FetchData(string wordWithoutSpaces, string ip, Word anagram);
    }
}
