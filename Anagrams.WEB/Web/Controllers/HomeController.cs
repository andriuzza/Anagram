using Anagrams.Interfaces;
using PagedList;
using Services;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordRepository<string> _repository;
        private AnagramSolver _solver;

        public HomeController(IWordRepository<string> repository)
        {
            _repository = repository;
            _solver = new AnagramSolver(_repository);
        }

        public ActionResult Index(string query)
        {
            //if model state bad return error

            if (string.IsNullOrEmpty(query))
            {
                return View();
            }

            IEnumerable<string> list = null;
            ViewBag.Model = null;
            if (query != null)//""
            {
                list = _solver.GetAnagram(query);
                ViewBag.Model = _solver.GetAnagram(query);
            }

            return View(list);
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
            if (ModelState.IsValid)
            {

            }

            return View();
        }

        [OutputCache(Duration = 5)]
        public ActionResult GetDictionary(/*int size=100, int pageNumber=1*/int? page, string currentFilter)
        {
            var dictionary = _repository.GetData();
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(dictionary.ToPagedList(pageNumber, pageSize));
        }
    }
}
/* long count = 0;
            long counterSize = 1;
            long index = size * (pageNumber - 1);
            bool CorrectPlace = false;

            foreach(var valueWord in dictionary)
            {
                if(count == index)
                {
                    CorrectPlace = true;
                    list.Add(valueWord);
                }
                count++;

                if(CorrectPlace == true && counterSize <= size)
                {
                    list.Add(valueWord);
                    counterSize++;
                }
                if(counterSize == 100)
                {
                    break;
                }
            }*/
