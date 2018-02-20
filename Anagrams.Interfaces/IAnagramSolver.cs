using System.Collections.Generic;

namespace Anagrams.Interfaces
{
    public interface IAnagramSolver
    {
        HashSet<string> GetAnagram(string Name);
        HashSet<string> GetResultsOfAnagram();
    }
}
