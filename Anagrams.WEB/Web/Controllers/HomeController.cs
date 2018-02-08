﻿using Anagrams.Interfaces;
using Anagrams.Interfaces.FactoryInterface;
using PagedList;
using Services;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Web.FactoryDesignPatternForLogic;
using Web.Models;

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
