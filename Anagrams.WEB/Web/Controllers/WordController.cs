using Anagrams.Interfaces;
using Anagrams.Interfaces.FactoryInterface;
using PagedList;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.FactoryDesignPatternForLogic;
using Web.Models;
using Web.Controllers.api;
using Services.Helpers;
using Services.CachedServices;

namespace Web.Controllers
{
    public class WordController : Controller
    {
        private readonly IWordRepository<string> _repository;
        private IAnagramSolver<string> _solver;
        private readonly CachedAnagram caching;

        public WordController(IWordRepository<string> repository,
            IAnagramFactoryManager factory,
             CachedAnagram caching)
        {
            _repository = repository;
            _solver = factory.GetInstance(repository); // //factory design pattern
            this.caching = caching;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Anagram anagram)
        {
            ViewBag.Model = null;

            var wordWithoutSpaces = anagram.Name.GetWithoutWhiteSpace();

            var listOfResult = caching.GetCachedData(anagram.Name);
            ViewBag.Model = listOfResult.ListOfAanagrams;

            if (ModelState.IsValid && ViewBag.Model == null)
            {
                ViewBag.Model = _solver.GetAnagram(wordWithoutSpaces);
                caching.InsertCache(listOfResult.AnagramsId);

                return View();
            }

            return View(anagram);
        }

        public ActionResult PostToDictionary(string Name)
        {
            return Content(Name + "Not implemented");
        }

        [OutputCache(Duration = 5)]
        public ActionResult GetDictionary(int? page, string currentFilter, string searchString)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            HashSet<string> dictionary = new HashSet<string>();

            if (!string.IsNullOrEmpty(searchString))
            {
                dictionary = _repository.Contains(searchString);
            }
            else
            {
                dictionary = _repository.GetData();
            }
           
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(dictionary.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DownloadDictinary()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(_repository.ReturnFilePath());
            string fileName = "dictionary.txt";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public static implicit operator WordController(api.WordController v)
        {
            throw new NotImplementedException();
        }
    }
}