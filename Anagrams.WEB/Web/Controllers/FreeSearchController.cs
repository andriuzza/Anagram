using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces.DtoModel;
using Anagrams.Interfaces.EntityInterfaces;
using Anagrams.Interfaces.WebServices;
using System;
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

        public ActionResult RemoveWord(string searchName)
        {
            try
            {
                //call methods
                _services.DeleteWord(searchName);
            }
            catch (Exception ex)
            {
                return Content("Wrong something with db" + ex);
                //log error
                //show error page
            }
          

            return Content("Successfuly removed!");
        }


        public ActionResult UpdateWord(string Word)
        {
            var result = _wordsRepo.GetEntityDto(Word);

            var dto = new WordDto
            {
                Name = result.Name
            };

            return View(dto);
        }

        [HttpPost]
        public ActionResult UpdateWord(WordDto updatedWord, string Word)
        {
            if (ModelState.IsValid)
            {
                //var instance = new Word { Name = updatedWord.Name };
                _services.UpdateWord(updatedWord, Word);
                return Content("Success!");
            }

            return Content("Somewthing wrong");
        }

        public ActionResult AddWord()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWord(WordDto word)
        {
            if (ModelState.IsValid)
            {
                _services.AddWord(word);

                return Content("Successfuly addded to db!");
            }

            return View();
        }
    }
}