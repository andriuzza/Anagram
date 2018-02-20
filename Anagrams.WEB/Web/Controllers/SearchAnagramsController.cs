using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces;
using Anagrams.Interfaces.WebServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SearchAnagramsController : Controller
    {
        private readonly IAdditionalSearchService _services;
        private readonly IWordRepository<Word> _repository;


        public SearchAnagramsController(IAdditionalSearchService services, 
                                    IWordRepository<Word> repository)
        {
            _services = services;
            _repository = repository;
        }
        // GET: SearchAnagrams
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(Word searchedWord)
        {

            var result = _services.IfAllowedToSearch();
            if (result)
            {
                ViewBag.Model = _repository.GetCachedData(searchedWord.Name); // list of concated strings

                var wordWithoutSpaces = searchedWord.Name.GetWithoutWhiteSpace();

                if (ModelState.IsValid && ViewBag.Model == null)
                {
                    FetchData(wordWithoutSpaces, _services.GetIpAddress(), anagram);

                    return View();
                }
            }
            else
            {
                return Content("Try later");
            }

            return View(anagram);
        }
        private void FetchData(string wordWithoutSpaces, string ip, Word anagram)
        {
            var wt = Stopwatch.StartNew();
            var list = _solver.GetAnagram(wordWithoutSpaces);
            wt.Stop();
            ViewBag.Model = list; /*Not sure yet if I can create ViewBag in void function */

            var time = wt.ElapsedMilliseconds;

            _repository.InsertCache(list, anagram.Name);
            _repository.InsertLogUser(time, ip, anagram.Name);
        }
    }
}