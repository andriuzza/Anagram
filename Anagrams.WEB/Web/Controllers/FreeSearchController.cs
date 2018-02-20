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

        public FreeSearchController(IAdditionalSearchService services)
        {
            _services = services;
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
                return Content("Wrong something with db");
                //log error
                //show error page
            }
          

            return Content("Successfuly removed!");
        }


        public ActionResult UpdateWord(string Word)
        {
            /*var result = _wordsRepo.GetEntity(Word);*/
            return View(/*result*/);
        }

        [HttpPost]
        public ActionResult UpdateWord(WordDto updatedWord, string Word)
        {
            if (ModelState.IsValid)
            {
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