using System;
using Anagrams_Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace FileReadingTests
{
    [TestClass]
    public class FileReadingRepositoryTests
    {

        [TestMethod]
        public void GetResultsOfAnagram_NewRepositoryNotEmpty_ReturnCorrectNumberOfAnagram()
        {
            // Create
            EFRepository _repo = new EFRepository();

            AnagramSolver _service = new AnagramSolver(_repo);

            // Act
            _repo.GetData("testing");

            // Assert
            Assert.AreEqual(_service.GetResultsOfAnagram().Count, 0);

        }
    }
}
