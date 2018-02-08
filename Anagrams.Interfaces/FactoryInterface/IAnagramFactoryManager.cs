using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Interfaces.FactoryInterface
{
    public interface IAnagramFactoryManager
    {
        IAnagramSolver<string> GetInstance(IWordRepository<string> repository);
    }
}
