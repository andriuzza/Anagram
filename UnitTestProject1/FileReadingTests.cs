using System;
using Anagrams.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace FileReadingTests
{
    [TestClass]
    public class FileReadingRepositoryTests
    {
        [TestMethod]
        public void ShowResultsOfAanagram_EmptyRepository_ReturnEmpty()
        {
            // Create
            Services.AnagramSolver _service = new Services.AnagramSolver(null);

            // Act
           // var isEmpty = _service.ShowResultsOfAnagram();

            // Assert
           // Assert.AreEqual(isEmpty, false);
        }

        [TestMethod]
        public void GetResultsOfAnagram_NewRepositoryNotEmpty_ReturnCorrectNumberOfAnagram()
        {
            // Create
            WordRepository _repo = new WordRepository(@"C:\Users\andrius.butkevicius\source\repos\Anagrams\Anagrams.Repositories\zodynas.txt");

            AnagramSolver _service = new AnagramSolver(_repo);


            // Act
            _repo.GetData("testing");

            // Assert
            Assert.AreEqual(_service.GetResultsOfAnagram().Count, 0);

        }
    }
}
