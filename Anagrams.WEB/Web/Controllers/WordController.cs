using Anagrams.Interfaces;
using Anagrams.Interfaces.FactoryInterface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models;
using Services.Helpers;
using System.Diagnostics;
using Anagrams.Interfaces.WebServices;

namespace Web.Controllers
{
    public class WordController : Controller
    {
        private readonly IWordRepository<string> _repository;
        private IAnagramSolver<string> _solver;
        private readonly IAdditionalSearchService _services;

        public WordController(IWordRepository<string> repository,
            IAnagramFactoryManager factory,
            IAdditionalSearchService services)
        {
            _repository = repository;
            _solver = factory.GetInstance(repository); // //factory design pattern
            _services = services;
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
            string ip = GetIp();

            var result = _services.IfAllowedToSearch(ip);
            if (result)
            {
                ViewBag.Model = _repository.GetCachedData(anagram.Name); // list of concated strings

                var wordWithoutSpaces = anagram.Name.GetWithoutWhiteSpace();

                if (ModelState.IsValid && ViewBag.Model == null)
                {
                    FetchData(wordWithoutSpaces, ip, anagram);

                    return View();
                }
            }
            else
            {
                return Content("Try later");
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
            /*  byte[] fileBytes = System.IO.File.ReadAllBytes(_repository.ReturnFilePath());
              string fileName = "dictionary.txt";
              return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);*/
            return View();
        }

        public static implicit operator WordController(api.WordController v)
        {
            throw new NotImplementedException();
        }
        private string GetIp()
        {
            return System.Net.Dns
                .GetHostEntry(System.Net.Dns.GetHostName())
                .AddressList[1].ToString();
        }

        private void FetchData(string wordWithoutSpaces, string ip, Anagram anagram)
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