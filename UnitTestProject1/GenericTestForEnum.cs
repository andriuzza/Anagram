using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Anagrams.EFCF.GenericTask;
using Anagrams.EFCF.GenericTask.Enums;

namespace UnitTestProject1
{
    /// <summary>
    /// Summary description for GenericTestForEnum
    /// </summary>
    [TestClass]
    public class GenericTestForEnum
    {
        public GenericTestForEnum()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CheckIfReturnSameEnumWeekday_StringInput_ReturnTrue()
        {
      
            // Create
            /*Empty not needed */

            // Act
            var nonGeneric = GenericAndEnum.MapStringToWeekday("1");

            var generic = GenericAndEnum.MapValueToEnum<Weekday>("1");

            //Assert

            Assert.AreEqual(generic, nonGeneric);
        }
        [TestMethod]
        public void CheckIfReturnSameEnumGender_IntInput_ReturnTrue()
        {

            // Create
            /*Empty not needed */

            // Act
            var nonGeneric = GenericAndEnum.MapIntToGender(1);

            var generic = GenericAndEnum.MapValueToEnum<Gender>(1);

            //Assert

            Assert.AreEqual(generic, nonGeneric);
        }
        [TestMethod]
        public void CheckIfReturnSameEnumGender_StringInput_ReturnTrue()
        {

            // Create
            /*Empty not needed */

            // Act
            var nonGeneric = GenericAndEnum.MapStringToGender("1");

            var generic = GenericAndEnum.MapValueToEnum<Gender>("1");

            //Assert

            Assert.AreEqual(generic, nonGeneric);
        }
    }
}
