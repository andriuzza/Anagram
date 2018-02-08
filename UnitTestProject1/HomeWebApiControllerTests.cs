using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Anagrams.Interfaces;
using Anagrams.Interfaces.FactoryInterface;
using Web.Controllers;
using System.Web.Http.Results;

namespace UnitTestProject1
{
    [TestClass]
    public class HomeWebApiControllerTests
    {
        private Web.Controllers.api.WordController SystemUnderTest;

        [TestInitialize]
        public void TestInit() {

            // Arrange
            var repository = new Mock<IWordRepository<string>>();
            var solver = new Mock<IAnagramSolver<string>>();
            solver.Setup(x => x.GetAnagram("TESTING")).Returns(new HashSet<string>());
            var factory = new Mock<IAnagramFactoryManager>();
            factory.Setup(x => x.GetInstance(repository.Object)).Returns(solver.Object);
            SystemUnderTest = new Web.Controllers.api.WordController(repository.Object, factory.Object);
        }

        [TestMethod]
        public void GetAnagram_GivenNotEmptyDictionary_ReturnNotFoundClass()
        {
            // Act
            var result = SystemUnderTest.GetAnagram("TESTING");

            // Assert
            Assert.IsNotNull(result);
            
        }
    }
}
