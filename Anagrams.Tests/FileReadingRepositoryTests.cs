using Anagrams.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using System;

namespace Anagrams.Tests
{
    [TestClass]
    public class FileReadingRepositoryTests
    {
        [TestMethod]
        public void CheckIfResultsAreEmpty()
        {
            ToolService _service = new ToolService(null, "TESTING");

            var isEmpty = _service.ShowResultsOfAnagram();

            Assert.AreEqual(isEmpty, false);
            
        }

        [TestMethod]
        public void CheckIfResultsAreEqual()
        {

            //   FileReadingRepository repo = new FileReadingRepository();
            // var result =  repo.GetData("alus", @"C:\Users\andrius.butkevicius\source\repos\Anagrams\Anagrams.Repositories\zodynas.txt");
          FileReadingRepository _repo =  new FileReadingRepository(@"C:\Users\andrius.butkevicius\source\repos\Anagrams\Anagrams.Repositories\zodynas.txt");
         

          ToolService _service = new ToolService(_repo, "alus");
            _repo.GetData("alus");



            Assert.AreEqual(_service.GetResults().Count, 0);

        }


    }
}
