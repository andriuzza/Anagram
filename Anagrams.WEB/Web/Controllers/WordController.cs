using Anagrams.Interfaces;
using PagedList;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class WordController : Controller
    {
        // GET: Word
        private readonly IWordRepository<string> _repository;
        private AnagramSolver _solver;

        public WordController(IWordRepository<string> repository)
        {
            _repository = repository;
            _solver = new AnagramSolver(_repository);
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
    }
}