﻿using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LogUserIPController : Controller
    {
        // GET: LogUserIP

        private readonly IWordRepository<Word> _repository;
        private readonly IAnagramSolver _solver;

        public LogUserIPController(IWordRepository<Word> repository,
            IAnagramSolver solver)
        {
            _repository = repository;
            _solver = solver; 
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLogData()
        {
            string ip = System.Net.Dns
                .GetHostEntry(System.Net.Dns.GetHostName())
                .AddressList[1].ToString();

            var result = _repository.ReturnIPSearches(ip);

            return View(result);
        }
    }
}