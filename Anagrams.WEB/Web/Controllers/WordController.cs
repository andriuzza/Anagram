using Anagrams.Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models;
using Services.Helpers;
using System.Diagnostics;
using Anagrams.Interfaces.WebServices;
using Anagrams.EFCF.Core.Models;

namespace Web.Controllers
{
    public class WordController : Controller
    {
        private readonly IWordRepository<Word> _repository;
        private IAnagramSolver _solver;
        private readonly IAdditionalSearchService _services;

        public WordController(IWordRepository<Word> repository,
            IAnagramSolver solver,
            IAdditionalSearchService services)
        {
            _repository = repository;
            _solver = solver; 
            _services = services;
        }

        public ActionResult Index()
        {
            return View();
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

    }
}