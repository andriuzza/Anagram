using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LogUserIPController : Controller
    {
        // GET: LogUserIP

        private readonly IWordRepository<Word> _repository;

        public LogUserIPController(IWordRepository<Word> repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLogData()
        {
            var result = _repository.ReturnIPSearches(IpGet());

            return View(result);
        }
        public string IpGet()
        {
           return System.Net.Dns
             .GetHostEntry(System.Net.Dns.GetHostName())
             .AddressList[1].ToString();
        }
    }
}