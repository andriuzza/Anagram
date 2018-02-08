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

namespace Web.Controllers
{
    public class WordController : Controller
    {
        private readonly IWordRepository<string> _repository;
        private IAnagramSolver<string> _solver;

        public WordController(IWordRepository<string> repository, IAnagramFactoryManager factory)
        {
            _repository = repository;
            _solver = factory.GetInstance(repository); // //factory design pattern
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
            if (ModelState.IsValid)
            {
                ViewBag.Model = _solver.GetAnagram(anagram.Name);
                return View();
            }
            return View(anagram);
        }

        public ActionResult PostToDictionary(string Name)
        {
            return Content(Name + "Not implemented");
        }

        [OutputCache(Duration = 5)]
        public ActionResult GetDictionary(int? page, string currentFilter)
        {
            var dictionary = _repository.GetData();
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