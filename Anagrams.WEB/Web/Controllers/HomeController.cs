using Anagrams.Interfaces;
using PagedList;
using Services;
using System;
using System.Collections.Generic;
using System.Web;
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
           // RepositoryDirectoryStatic = _solver.GetAnagram(null);
        }

        public ActionResult Index(string query)
        {
            //if model state bad return error
            if (string.IsNullOrEmpty(query))
            {
                return View();
            }

            IEnumerable<string> list = _solver.GetAnagram(query);

            var howManyTimes = Request.Cookies[query];

            if(howManyTimes  == null)
            {
                howManyTimes = new HttpCookie(query)
                {
                    Value = "1"
                };
            }
            else
            {
                var str = howManyTimes.Value;
                int parseToInterger = Int32.Parse(str);
                parseToInterger++;

                Response.Cookies[query].Value = parseToInterger.ToString();
            }

            ViewBag.CookieName = query;

            return View(list);
        }
    }
}
