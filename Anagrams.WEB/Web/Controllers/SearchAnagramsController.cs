using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces;
using Anagrams.Interfaces.Services;
using Anagrams.Interfaces.WebServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Helpers;

namespace Web.Controllers
{
    public class SearchAnagramsController : Controller
    {
        private readonly IAdditionalSearchService _services;
        private readonly IWordRepository<Word> _repository;
        private readonly ITimingService _timeService;


        public SearchAnagramsController(IAdditionalSearchService services, 
                                    IWordRepository<Word> repository,
                                    ITimingService timeService)
        {
            _services = services;
            _repository = repository;
            _timeService = timeService;
        }
        // GET: SearchAnagrams
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
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

                var wordWithoutSpaces = searchedWord
                            .Name.GetWithoutWhiteSpace();

                if (ModelState.IsValid && ViewBag.Model == null)
                {
                    ViewBag.list = _timeService
                        .FetchData(wordWithoutSpaces, _services.GetIpAddress(), searchedWord);

                    return View();
                }
            }
            else
            {
                return Content("Try later");
            }

            return View(searchedWord);
        }
    
    }
}