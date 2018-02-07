using System.Collections.Generic;

namespace Anagrams.Interfaces
{
    public interface IAnagramSolver<T>
    {
        HashSet<string> GetAnagram(string Name);
        HashSet<T> GetResultsOfAnagram();
    }
}
