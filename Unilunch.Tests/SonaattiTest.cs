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
        Sonaatti sonaatti;
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
        [TestInitialize()]
        public void MyTestInitialize() 
        {
            sonaatti = new Sonaatti(source);
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
            MenuDate expected = sonaatti.Restaurants[0].dates[0];
            Assert.AreEqual("02.04.2013", expected.date);
        }

        [TestMethod]
        public void ItFindsTheFirstMenuItemDescriptionForFirstDate()
        {
            Assert.AreEqual("Naudanliha-smetanakastiketta", sonaatti.Restaurants[0].dates[0].foods[0].description);
        }

        [TestMethod]
        public void ItFindsTheSecondMenuItemDescriptionForFirstDate()
        {
            Assert.AreEqual("Kalaa katkarapukastikkeessa", sonaatti.Restaurants[0].dates[0].foods[1].description);
        }

        [TestMethod]
        public void ItFindsTheStudentPrice()
        {
            Assert.AreEqual("2,60", sonaatti.Restaurants[0].dates[1].foods[0].student_prize);
        }

        [TestMethod]
        public void ItFindsTheStudentPriceForFirstDayItem()
        {
            Assert.AreEqual("2,60", sonaatti.Restaurants[0].dates[0].foods[0].student_prize);
        }

        [TestMethod]
        public void ItFindsTheStaffPrice()
        {
            Assert.AreEqual("8,00", sonaatti.Restaurants[0].dates[1].foods[0].staff_prize);
        }

        [TestMethod]
        public void ItReturnsEmptyListOfDatesWithUnexpectedData()
        {
            var s = new FakeDataSource();
            s.Data = "<html></html>";
            s.Data2 = "<html></html>";
            var sona = new Sonaatti(s);
            Assert.IsTrue(sona.Restaurants[0].dates.Count == 0);
        }

        [TestMethod]
        public void ItFindsTheFirstLunchDateForSecondRestaurant()
        {
            MenuDate expected = sonaatti.Restaurants[1].dates[0];
            Assert.AreEqual("01.04.2013", expected.date);
        }

        [TestMethod]
        public void ItFindsTheFirstMenuItemDescriptionForSecondDateForSecondRestaurant()
        {
            Assert.AreEqual("Nakkikastiketta", sonaatti.Restaurants[1].dates[1].foods[0].description);
        }

        [TestMethod]
        public void ItFindsTheLastMenuItemDescriptionForLastDateForSecondRestaurant()
        {
            Assert.AreEqual("Broileria kookoskermakastikkeessa", sonaatti.Restaurants[1].dates.Last().foods.Last().description);
        }

        [TestMethod]
        public void ItFindsTheMenuItemWithoutDiets()
        {
            Assert.AreEqual("Uunik", sonaatti.Restaurants[1].dates.Last().foods[1].description);
        }
        
        [TestMethod]
        public void ItExcludesEmptyDescriptions()
        {
            foreach (var food in sonaatti.Restaurants[0].dates[0].foods)
            {
                Assert.IsTrue(!String.IsNullOrWhiteSpace(food.description));
            }
        }

        [TestMethod]
        public void ItSetsPricesForAllFoodsWhenAvailable()
        {
            foreach (var food in sonaatti.Restaurants[0].dates[0].foods)
            {
                Assert.IsTrue(!String.IsNullOrWhiteSpace(food.student_prize));
            }
        }

        [TestMethod]
        public void ItShouldFindDietsForFood()
        {
            Assert.IsTrue(sonaatti.Restaurants[0].dates[0].foods[0].diets.Contains("VH"));
            Assert.IsTrue(sonaatti.Restaurants[0].dates[0].foods[0].diets.Contains("L"));

        }

        [TestMethod]
        public void ItShouldStripExtraParenthesisFromDescription()
        {
            Assert.AreEqual("salaatti", SonaattiParser.cleanDescriptionFromPrice("salaatti ()"));
        }

        [TestMethod]
        public void ItShouldKeepSpecialCharInTheEbd()
        {
            Assert.AreEqual("Kalkkuna-rakuunakastiketta ja tummaa riisiä", SonaattiParser.cleanDescriptionFromPrice("Kalkkuna-rakuunakastiketta ja tummaa riisi&#228; #L #G"));
        }

        [TestMethod]
        public void ItShouldWorkOnThisSpecialCase()
        {
            Assert.AreEqual("L", SonaattiParser.diets("Kalkkuna-rakuunakastiketta ja tummaa riisiä #L #G")[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructDateWithEmptyThrowsException()
        {
            SonaattiParser.ConstructDateFromSonaattiDate("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructDateWithNullThrowsException()
        {
            SonaattiParser.ConstructDateFromSonaattiDate(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructDateWithWrongDataThrowsException()
        {
            SonaattiParser.ConstructDateFromSonaattiDate("wrong data");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructDateWithNonnumericDataThrowsException()
        {
            SonaattiParser.ConstructDateFromSonaattiDate("abc.dfe.xcv");
        }

        [TestMethod]
        public void ConstructDateWithCorrectStringReturnsCorrectDate()
        {
            var expected = new DateTime(2014, 6, 3);
            Assert.AreEqual(expected.ToShortDateString(), SonaattiParser.ConstructDateFromSonaattiDate("3.6.2014").ToShortDateString());
        }
    }
}
