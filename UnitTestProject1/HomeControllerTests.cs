using System;
using Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Anagrams.Interfaces;
using Moq;
using Anagrams.Interfaces.FactoryInterface;
using System.Collections.Generic;

namespace WebUnitTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IWordRepository<string>> repository;
        private Mock<IAnagramFactoryManager> angramSolverFactory;
        private Mock<IAnagramSolver<string>> anagram;

        [TestInitialize]
        public void TestInit()
        {
             repository = new Mock<IWordRepository<string>>();
             angramSolverFactory = new Mock<IAnagramFactoryManager>();
             anagram = new Mock<IAnagramSolver<string>>();

             anagram.Setup(x => x.GetAnagram("vaidenasi")).Returns(new HashSet<string>());
             angramSolverFactory
                .Setup(x => x.GetInstance(repository.Object))
                .Returns(anagram.Object);
        }

        [TestMethod]
        public void Index_EmptyQuery_ReturnsEmptyView()
        {
            // Arranges
            var homeController = new HomeController(repository.Object, angramSolverFactory.Object);

            // Act
            var result = homeController.Index("") as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Index_QueryOfOneWord_ReturnsSomething()
        {
            // Arrange
            var homeController = new HomeController(repository.Object, angramSolverFactory.Object);
            
            // Act
            var result = homeController.Index("vaidenasi") as ViewResult;

            // Assert
            Assert.AreEqual("vaidenasi", result.ViewName);
        }
    }
}
