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
          /*  Services.AnagramSolver _service = new Services.AnagramSolver(null);

            var isEmpty = _service.ShowResultsOfAnagram();

            Assert.AreEqual(isEmpty, false);*/
        }

        [TestMethod]
        public void CheckIfResultsAreEqual()
        {
            // Arrange
            WordRepository _repo = new WordRepository(@"C:\Users\PC\Documents\Anagram\Anagrams.Repositories\zodynastest.txt");

            // Act
            AnagramSolver _service = new AnagramSolver(_repo);
            _repo.GetData("labadiena");

            // Assert
            Assert.AreEqual(_service.GetResultsOfAnagram().Count, 0);

        }


    }
}
