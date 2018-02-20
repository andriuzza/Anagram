using Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Anagrams.Interfaces;
using Moq;
using System.Collections.Generic;
using Anagrams.EFCF.Core.Models;

namespace WebUnitTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IWordRepository<Word>> repository;
        private Mock<IAnagramSolver> anagram;
        private Mock<ICookiesManager> cookies;

        [TestInitialize]
        public void TestInit()
        {
             repository = new Mock<IWordRepository<Word>>();
             anagram = new Mock<IAnagramSolver>();
             cookies = new Mock<ICookiesManager>();

             anagram.Setup(x => x.GetAnagram("vaidenasi")).Returns(new HashSet<string>());
           
        }

        [TestMethod]
        public void Index_EmptyQuery_ReturnsEmptyView()
        {
            // Arranges
            var homeController = new HomeController(repository.Object, anagram.Object);

            // Act
            var result = homeController.Index("") as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Index_QueryOfOneWord_ReturnsSomething()
        {
            // Arrange
            var homeController = new HomeController(repository.Object
                                                            , anagram.Object);
            // Act
            var result = homeController.Index("vaidenasi") as ViewResult;

            // Assert
            Assert.AreEqual("vaidenasi", result.ViewName);
        }
    }
}
