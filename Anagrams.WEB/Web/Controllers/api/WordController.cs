using Anagrams.Interfaces;
using System.Linq;
using System.Web.Http;

namespace Web.Controllers.api
{
    public class WordController : ApiController
    {
        private readonly IWordRepository<string> _repository;
        private IAnagramSolver _solver;

        public WordController(IWordRepository<string> repository, IAnagramSolver solver)
        {
            _repository = repository; 
            _solver = solver; // //factory design pattern
        }

        /*It is not entity framework!!!! mapping entity from parameters, actually does asp.net itself  */
        /*default Get, jei nera httpget, be jo privalo buti tik Get metodo pavadinimas */
        [HttpGet]
        public IHttpActionResult GetAnagram(string name)
        {
            var result = _solver.GetAnagram(name);

            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }
      
    }
}
