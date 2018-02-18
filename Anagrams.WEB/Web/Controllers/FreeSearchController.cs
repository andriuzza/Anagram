using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces.DtoModel;
using Anagrams.Interfaces.EntityInterfaces;
using Anagrams.Interfaces.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class FreeSearchController : Controller
    {
        private readonly IDictionaryRepository<WordDto> _wordsRepo;
        private readonly IAdditionalSearchService _services;

        private string ip = System.Net.Dns
                .GetHostEntry(System.Net.Dns.GetHostName())
                .AddressList[1].ToString();

        public FreeSearchController(IDictionaryRepository<WordDto> wordsRepo,
                                        IAdditionalSearchService services)
        {
            _wordsRepo = wordsRepo;
            _services = services;
        }

        public ActionResult RemoveWord()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RemoveWord(string searchName)
        {
            if (ModelState.IsValid)
            {
                _wordsRepo.Delete(searchName);

                _services.AdditionalSearches(ip);

                return Content("Successfuly updated!");
            }

            return View();
        }

        public ActionResult UpdateWord()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateWord(WordDto updatedWord, string Word)
        {
            if (ModelState.IsValid)
            {
                if (!_wordsRepo.IsExist(Word))
                {
                    _wordsRepo.Update(updatedWord, Word);
                    _services.AdditionalSearches(ip);
                    return Content("Success!");
                }

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
                _wordsRepo.Add(word);

                _services.AdditionalSearches(ip);

                return Content("Successfuly addded to db!");
            }

            return View();
        }
    }
}