using Anagrams.Interfaces;
using Anagrams.Interfaces.FactoryInterface;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.FactoryDesignPatternForLogic
{
    public class AnagramFactoryManager : IAnagramFactoryManager
    {
        public IAnagramSolver<string> GetInstance(IWordRepository<string> repository)
        {
            return new AnagramSolver(repository);
        }
    }
}