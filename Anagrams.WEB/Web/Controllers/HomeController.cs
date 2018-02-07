using Anagrams.Interfaces;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordRepository<string> _repository;
        public AnagramSolver _solver { get; private set; }

        public HomeController(IWordRepository<string> repository)
        {
            _repository = repository;
            ApplyLogic(_repository);
        }

        private void ApplyLogic(IWordRepository<string> repository)
        {
            _solver = new AnagramSolver(repository);
           
        }
  
        public ActionResult Index(string query)
        {
            ViewBag.Model = null;
            if (query != null)
            {
                 ViewBag.Model = _solver.GetAnagram(query);
            }
         
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

        [OutputCache(Duration = 5)]
        public ActionResult GetDictionary(int size, int pageNumber)
        {
            var list = new List<string>();
            var dictionary = _repository.GetData();

            int count = 0;
            int counterSize = 1;
            int index = size * (pageNumber - 1);
            bool CorrectPlace = false;

            foreach(var valueWord in dictionary)
            {
                if(count == index)
                {
                    CorrectPlace = true;
                    list.Add(valueWord);
                }
                count++;

                if(CorrectPlace == true && counterSize <= 100)
                {
                    list.Add(valueWord);
                    counterSize++;
                }
                if(counterSize == 100)
                {
                    break;
                }
            }
               
         return View(list);
        }


        
    }
}