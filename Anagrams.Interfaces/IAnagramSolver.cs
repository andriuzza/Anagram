using System.Collections.Generic;

namespace Anagrams.Interfaces
{
    public interface IAnagramSolver<T>
    {
        void GetAnagram(string Name);
        HashSet<T> GetResultsOfAnagram();
    }
}
