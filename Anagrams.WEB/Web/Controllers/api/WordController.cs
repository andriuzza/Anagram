using Anagrams.Interfaces;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers.api
{
    public class WordController : ApiController
    {
        private readonly IWordRepository<string> _repository;
        private AnagramSolver _solver;

        public WordController(IWordRepository<string> repository)
        {
            _repository = repository;
            _solver = new AnagramSolver(_repository);
        }

        /*It is not entity framework!!!! mapping entity from parameters, actually does asp.net itself  */
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
