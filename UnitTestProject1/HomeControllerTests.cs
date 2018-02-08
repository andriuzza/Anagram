using System;
using Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace WebUnitTests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_EmptyQuery_ReturnsEmptyView()
        {
            // Arranges
            var homeController = new HomeController(null, null);

            // Act
            var result = homeController.Index("") as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Index_QueryOfOneWord_ReturnsSomething()
        {
            // Arrange
            var homeController = new HomeController(null, null);

            // Act
            var result = homeController.Index("vaidenasi") as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName);
        }
    }
}
