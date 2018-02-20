using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Services.ManageService
{
    public class TimingService
    {
        private readonly IWordRepository<Word> _repository;
        private readonly IAnagramSolver _solver;

        public TimingService(IWordRepository<Word> repository, IAnagramSolver solver)
        {
            _repository = repository;
            _solver = solver;
        }

        public HashSet<string> FetchData(string wordWithoutSpaces, string ip, Word anagram)
        {
            var wt = Stopwatch.StartNew();
            var list = _solver.GetAnagram(wordWithoutSpaces);
            wt.Stop();

            ViewBag.Model = list; /*Not sure yet if I can create ViewBag in void function */

            var time = wt.ElapsedMilliseconds;

            _repository.InsertCache(list, anagram.Name);
            _repository.InsertLogUser(time, ip, anagram.Name);

            return list;
        }
    }
}
