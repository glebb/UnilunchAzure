using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CsQuery;
using UnilunchData;

namespace Unilunch.Tests
{
    /// <summary>
    /// Summary description for SonaattiTest
    /// </summary>
    [TestClass]
    public class SonaattiTest
    {

        string mainPage;
        public SonaattiTest()
        {
            StreamReader streamReader = new StreamReader("Piato.html");
            mainPage = streamReader.ReadToEnd();
            streamReader.Close();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which providesa
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
        public void itFindsTodayString()
        {
            var dom = CQ.Create("");
            var sonaatti = new Sonaatti(mainPage);
            Assert.AreEqual("02.04.2013", sonaatti.TodayDate().ToString("dd.MM.yyyy"));
        }
    }
}
