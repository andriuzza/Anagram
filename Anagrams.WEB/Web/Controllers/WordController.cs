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
            IAnagramFactoryManager factory)
        {
            _repository = repository;
            _solver = factory.GetInstance(repository); // //factory design pattern
            this.caching = new CachedAnagram(@"Data Source=(localdb)\MSSQLLocalDB;
                        Initial Catalog=ConnectionDb2018;Integrated Security=True;
                            Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;
                                 ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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
            ViewBag.Model = caching.GetCachedData(anagram.Name); // list of concated strings

            var wordWithoutSpaces = anagram.Name.GetWithoutWhiteSpace();

            if (ModelState.IsValid && ViewBag.Model == null)
            {
                var list = _solver.GetAnagram(wordWithoutSpaces);
                ViewBag.Model = list;

                //INSERT DATA OF CACHE
                caching.InsertCache(list, anagram.Name);

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