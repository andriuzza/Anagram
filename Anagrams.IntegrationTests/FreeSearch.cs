using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Anagrams.EFCF.Core;
using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces;
using Anagrams.Interfaces.DtoModel;
using Anagrams.Interfaces.EntityInterfaces;
using Anagrams_Repositories;
using Anagrams_Repositories.EntitiesRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Services;
using Web.Controllers;

namespace Anagrams.IntegrationTests
{
    [TestFixture]
    public class FreeSearchControllerTest
    {
        private Mock<IDictionaryRepository<Word>> mockRepository;
        private Mock<IClickRepository> mockClickRepository;
        private Mock<IWordRepository<Word>> mockWordRepository;
        private ManagerContext context;
        private FreeSearchController _controller;
        [SetUp]
        public void SetUp()
        {
            mockRepository = new Mock<IDictionaryRepository<Word>>();
            mockClickRepository = new Mock<IClickRepository>();
            mockWordRepository = new Mock<IWordRepository<Word>>();

            mockClickRepository.Setup(x => x.GetEntity(It.IsAny<string>())).Returns(new IPClickDto { Count = 0, Ip= ""});
            context = new ManagerContext();

            _controller = new FreeSearchController(new AdditionalSearchService(mockClickRepository.Object,
               new WordEFRepository(), mockWordRepository.Object)
                       , mockRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        [Isolated]
        public void FreeSearch_Success()
        {
            //Arange
            WordDto word = new WordDto
            {
                Name = "pica"
            };

            //act
            var result = _controller.AddWord(word).GetAwaiter().GetResult() as ContentResult;
            var contentResult = result.Content;
            //assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(contentResult, "Successfuly addded to db!");
        }
    }
}
