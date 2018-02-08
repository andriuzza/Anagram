using Anagrams.Interfaces;
using Anagrams.Interfaces.FactoryInterface;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.FactoryDesignPatternForLogic;

namespace Web.Controllers.api
{
    public class WordController : ApiController
    {
        private readonly IWordRepository<string> _repository;
        private IAnagramSolver<string> _solver;

        public WordController(IWordRepository<string> repository, IAnagramFactoryManager factory)
        {
            _repository = repository; 
            _solver = factory.GetInstance(_repository); // //factory design pattern
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
