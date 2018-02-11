using Anagrams.Interfaces;
using Anagrams.Interfaces.FactoryInterface;
using PagedList;
using Services;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Web.FactoryDesignPatternForLogic;
using Web.Models;
using Services.Helpers;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordRepository<string> _repository;
        private IAnagramSolver<string> _solver;

        public HomeController(IWordRepository<string> repository, IAnagramFactoryManager factory)
        {
            _repository = repository;
            _solver = factory.GetInstance(repository);
        }

        public ActionResult Index(string query)
        {
            IEnumerable<string> list;
            if (string.IsNullOrEmpty(query))
            {
                return View();
            }
            try
            {
                list = _solver.GetAnagram(query.GetWithoutWhiteSpace());
            }
            catch (Exception)
            {
                return Content("Wrong dictionary's path");
            }

            UpdateCookieVisitingNumber(query);

            ViewBag.CookieName = query;

            return View(list);
        }

        private void UpdateCookieVisitingNumber(string query)
        {
            var howManyTimes = Request.Cookies[query];

            if (howManyTimes == null)
            {
                howManyTimes = new HttpCookie(query)
                {
                    Value = "1"
                };

                ControllerContext.HttpContext
                    .Response.Cookies.Add(howManyTimes);
            }
            else
            {
                var str = howManyTimes.Value;
                int parseToInterger = Int32.Parse(str);
                parseToInterger++;

                Response.Cookies[query].Value = parseToInterger.ToString();
            }
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.aspx"
            };
            filterContext.ExceptionHandled = true;
        }
    }
}
