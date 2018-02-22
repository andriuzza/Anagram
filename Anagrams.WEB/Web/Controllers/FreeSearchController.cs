using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces;
using Anagrams.Interfaces.DtoModel;
using Anagrams.Interfaces.EntityInterfaces;
using Anagrams.Interfaces.WebServices;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class FreeSearchController : Controller
    {  
        private readonly IAdditionalSearchService _services;
        private readonly IDictionaryRepository<Word> _wordsRepo;
        

        public FreeSearchController(IAdditionalSearchService services
                                        , IDictionaryRepository<Word> wordsRepo)
        {
            _services = services;
            _wordsRepo = wordsRepo;
        }

        public async Task<ActionResult> RemoveWord(string searchName)
        {
            try
            {
                //call methods
                await _services.DeleteWord(searchName);
            }
            catch (Exception)
            {
              
                return Content("wrong anagram's name");
                //log error
                //show error page
            }

            return Content("Successfuly removed!");
        }


        public async Task<ActionResult> UpdateWord(string Word)
        {
            Word result = null;

            try
            {
                result = await _wordsRepo.GetEntityDto(Word);

            }
            catch (Exception)
            {
                RedirectToAction("GetDictionary", "Word");
            }

            var dto = new WordDto
            {
                Name = result.Name
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateWord(WordDto updatedWord, string Word)
        {
            if (ModelState.IsValid)
            {
                await _services.UpdateWord(updatedWord, Word);

                return Content("Success!");
            }

            return Content("Somewthing wrong");
        }

        public ActionResult AddWord()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddWord(WordDto word)
        {
            if (ModelState.IsValid)
            {
                await _services.AddWord(word);

                return Content("Successfuly addded to db!");
            }

            return View();
        }
    }
}