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
    [TestClass]
    public class SonaattiTest
    {

        FakeDataSource source;
        public SonaattiTest()
        {
            source = new FakeDataSource();
            StreamReader streamReader = new StreamReader("Piato.html");
            source.Data = streamReader.ReadToEnd();
            streamReader.Close();

            streamReader = new StreamReader("Kvarkki.html");
            source.Data2 = streamReader.ReadToEnd();
            streamReader.Close();

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
        public void ItFindsTheFirstLunchDate()
        {
            var sonaatti = new Sonaatti(source);
            MenuDate expected = sonaatti.Restaurants[0].dates[0];
            Assert.AreEqual("03.04.2013", expected.date);
        }

        [TestMethod]
        public void ItFindsTheFirstMenuItemDescriptionForFirstDate()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("Jauhelihakebakoita", sonaatti.Restaurants[0].dates[0].foods[0].description);
        }

        [TestMethod]
        public void ItFindsTheSecondtMenuItemDescriptionForFirstDate()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("Lohi-kasvispataa", sonaatti.Restaurants[0].dates[0].foods[1].description);
        }

        [TestMethod]
        public void ItFindsTheStudentPrice()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("2,60", sonaatti.Restaurants[0].dates[0].foods[0].student_prize);
        }

        [TestMethod]
        public void ItFindsTheStaffPrice()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("8,00", sonaatti.Restaurants[0].dates[0].foods[0].staff_prize);
        }

        [TestMethod]
        public void createMenuReturnsEmptyListWithUnexpectedData()
        {
            Assert.IsTrue(SonaattiParserHelpers.createMenu(CQ.Create("<html></html>")).Count == 0);
        }


    }
}
