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
            Assert.AreEqual("02.04.2013", expected.date);
        }

        [TestMethod]
        public void ItFindsTheFirstMenuItemDescriptionForFirstDate()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("Naudanliha-smetanakastiketta", sonaatti.Restaurants[0].dates[0].foods[0].description);
        }

        [TestMethod]
        public void ItFindsTheSecondMenuItemDescriptionForFirstDate()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("Kalaa katkarapukastikkeessa", sonaatti.Restaurants[0].dates[0].foods[1].description);
        }

        [TestMethod]
        public void ItFindsTheStudentPrice()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("2,60", sonaatti.Restaurants[0].dates[1].foods[0].student_prize);
        }

        [TestMethod]
        public void ItFindsTheStudentPriceForFirstDayItem()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("2,60", sonaatti.Restaurants[0].dates[0].foods[0].student_prize);
        }


        [TestMethod]
        public void ItFindsTheStaffPrice()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("8,00", sonaatti.Restaurants[0].dates[1].foods[0].staff_prize);
        }

        [TestMethod]
        public void ItReturnsEmptyListOfDatesWithUnexpectedData()
        {
            var s = new FakeDataSource();
            s.Data = "<html></html>";
            s.Data2 = "<html></html>";
            var sonaatti = new Sonaatti(s);

            Assert.IsTrue(sonaatti.Restaurants[0].dates.Count == 0);
        }

        [TestMethod]
        public void ItFindsTheFirstLunchDateForSecondRestaurant()
        {
            var sonaatti = new Sonaatti(source);
            MenuDate expected = sonaatti.Restaurants[1].dates[0];
            Assert.AreEqual("01.04.2013", expected.date);
        }

        [TestMethod]
        public void ItFindsTheFirstMenuItemDescriptionForSecondDateForSecondRestaurant()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("Nakkikastiketta", sonaatti.Restaurants[1].dates[1].foods[0].description);
        }

        [TestMethod]
        public void ItFindsTheLastMenuItemDescriptionForLastDateForSecondRestaurant()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("Broileria kookoskermakastikkeessa", sonaatti.Restaurants[1].dates.Last().foods.Last().description);
        }

        [TestMethod]
        public void ItFindsTheMenuItemWithoutDiets()
        {
            var sonaatti = new Sonaatti(source);
            Assert.AreEqual("Uunik", sonaatti.Restaurants[1].dates.Last().foods[1].description);
        }
        
        [TestMethod]
        public void ItExcludesEmptyDescriptions()
        {
            var sonaatti = new Sonaatti(source);
            foreach (var food in sonaatti.Restaurants[0].dates[0].foods)
            {
                Assert.IsTrue(!String.IsNullOrWhiteSpace(food.description));
            }
        }

        [TestMethod]
        public void ItSetsPricesForAllFoodsWhenAvailable()
        {
            var sonaatti = new Sonaatti(source);
            foreach (var food in sonaatti.Restaurants[0].dates[0].foods)
            {
                Assert.IsTrue(!String.IsNullOrWhiteSpace(food.student_prize));
            }
        }

    }
}
