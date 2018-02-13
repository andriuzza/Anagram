using Anagrams.Interfaces;
using Anagrams.Interfaces.FactoryInterface;
using Services.CachedServices;
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
        private readonly CachedAnagram caching;

        public LogUserIPController(IWordRepository<string> repository,
            IAnagramFactoryManager factory)
        {
            _repository = repository;
            _solver = factory.GetInstance(repository); // //factory design pattern
            caching = new CachedAnagram(@"Data Source=(localdb)\MSSQLLocalDB;
                        Initial Catalog=ConnectionDb2018;Integrated Security=True;
                            Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;
                                 ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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

            var result = caching
                .ReturnIPSearches(ip);

            return View(result);
        }
    }
}