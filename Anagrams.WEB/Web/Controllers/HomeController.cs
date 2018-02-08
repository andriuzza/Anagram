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
            IEnumerable<string> list = null;

            if (query != null)
            {
                list = _solver.GetAnagram(query);
            }
            ViewBag.CookieName = query;
            string howManyTimes = HttpContext.Response.Cookies[query].Value;

            if (string.IsNullOrEmpty(howManyTimes))
            {
                howManyTimes = "0";
            }

            int parseToInterger = Int32.Parse(howManyTimes);
            parseToInterger++;
            howManyTimes = parseToInterger.ToString();
         

           // HttpContext.Response.Cookies.Add(cookieOfThisRequest);
           
          //  Response.AppendCookie(cookieOfThisRequest);

            return View(list);
        }
    }
}
