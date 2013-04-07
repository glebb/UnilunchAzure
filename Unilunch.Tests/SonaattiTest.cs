#region using directives

using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnilunchData;

#endregion

namespace Unilunch.Tests
{
    [TestClass]
    public class SonaattiTest
    {
        private Sonaatti _sonaatti;
        private readonly FakeDataSource _source;

        public SonaattiTest()
        {
            _source = new FakeDataSource();
            var streamReader = new StreamReader("Piato.html");
            _source.Data = streamReader.ReadToEnd();
            streamReader.Close();

            streamReader = new StreamReader("Kvarkki.html");
            _source.Data2 = streamReader.ReadToEnd();
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
        [TestInitialize]
        public void MyTestInitialize()
        {
            _sonaatti = new Sonaatti(_source);
        }

        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        public void ItFindsTheFirstLunchDate()
        {
            var expected = _sonaatti.Restaurants[0].dates[0];
            Assert.AreEqual("02.04.2013", expected.date);
        }

        [TestMethod]
        public void ItFindsTheFirstMenuItemDescriptionForFirstDate()
        {
            Assert.AreEqual("Naudanliha-smetanakastiketta", _sonaatti.Restaurants[0].dates[0].foods[0].description);
        }

        [TestMethod]
        public void ItFindsTheSecondMenuItemDescriptionForFirstDate()
        {
            Assert.AreEqual("Kalaa katkarapukastikkeessa", _sonaatti.Restaurants[0].dates[0].foods[1].description);
        }

        [TestMethod]
        public void ItFindsTheStudentPrice()
        {
            Assert.AreEqual("2,60", _sonaatti.Restaurants[0].dates[1].foods[0].student_prize);
        }

        [TestMethod]
        public void ItFindsTheStudentPriceForFirstDayItem()
        {
            Assert.AreEqual("2,60", _sonaatti.Restaurants[0].dates[0].foods[0].student_prize);
        }

        [TestMethod]
        public void ItFindsTheStaffPrice()
        {
            Assert.AreEqual("8,00", _sonaatti.Restaurants[0].dates[1].foods[0].staff_prize);
        }

        [TestMethod]
        public void ItReturnsEmptyListOfDatesWithUnexpectedData()
        {
            var s = new FakeDataSource {Data = "<html></html>", Data2 = "<html></html>"};
            var sona = new Sonaatti(s);
            Assert.IsTrue(sona.Restaurants[0].dates.Count == 0);
        }

        [TestMethod]
        public void ItFindsTheFirstLunchDateForSecondRestaurant()
        {
            var expected = _sonaatti.Restaurants[1].dates[0];
            Assert.AreEqual("01.04.2013", expected.date);
        }

        [TestMethod]
        public void ItFindsTheFirstMenuItemDescriptionForSecondDateForSecondRestaurant()
        {
            Assert.AreEqual("Nakkikastiketta", _sonaatti.Restaurants[1].dates[1].foods[0].description);
        }

        [TestMethod]
        public void ItFindsTheLastMenuItemDescriptionForLastDateForSecondRestaurant()
        {
            Assert.AreEqual("Broileria kookoskermakastikkeessa",
                            _sonaatti.Restaurants[1].dates.Last().foods.Last().description);
        }

        [TestMethod]
        public void ItFindsTheMenuItemWithoutDiets()
        {
            Assert.AreEqual("Uunik", _sonaatti.Restaurants[1].dates.Last().foods[1].description);
        }

        [TestMethod]
        public void ItExcludesEmptyDescriptions()
        {
            foreach (var food in _sonaatti.Restaurants[0].dates[0].foods)
            {
                Assert.IsTrue(!String.IsNullOrWhiteSpace(food.description));
            }
        }

        [TestMethod]
        public void ItSetsPricesForAllFoodsWhenAvailable()
        {
            foreach (var food in _sonaatti.Restaurants[0].dates[0].foods)
            {
                Assert.IsTrue(!String.IsNullOrWhiteSpace(food.student_prize));
            }
        }

        [TestMethod]
        public void ItShouldFindDietsForFood()
        {
            Assert.IsTrue(_sonaatti.Restaurants[0].dates[0].foods[0].diets.Contains("VH"));
            Assert.IsTrue(_sonaatti.Restaurants[0].dates[0].foods[0].diets.Contains("L"));
        }

        [TestMethod]
        public void ItShouldStripExtraParenthesisFromDescription()
        {
            Assert.AreEqual("salaatti", SonaattiParser.CleanDescriptionFromPrice("salaatti ()"));
        }

        [TestMethod]
        public void ItShouldKeepSpecialCharInTheEbd()
        {
            Assert.AreEqual("Kalkkuna-rakuunakastiketta ja tummaa riisiä",
                            SonaattiParser.CleanDescriptionFromPrice(
                                "Kalkkuna-rakuunakastiketta ja tummaa riisi&#228; #L #G"));
        }

        [TestMethod]
        public void ItShouldWorkOnThisSpecialCase()
        {
            Assert.AreEqual("L", SonaattiParser.Diets("Kalkkuna-rakuunakastiketta ja tummaa riisiä #L #G")[0]);
        }

    }
}