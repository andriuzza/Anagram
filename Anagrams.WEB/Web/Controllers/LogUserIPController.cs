using Anagrams.Interfaces;
using Anagrams.Interfaces.FactoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LogUserIPController : Controller
    {
        // GET: LogUserIP

        private readonly IWordRepository<string> _repository;
        private IAnagramSolver<string> _solver;

        public LogUserIPController(IWordRepository<string> repository,
            IAnagramFactoryManager factory)
        {
            _repository = repository;
            _solver = factory.GetInstance(repository); // //factory design pattern
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