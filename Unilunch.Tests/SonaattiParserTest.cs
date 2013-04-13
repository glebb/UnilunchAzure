#region using directives

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnilunchData;

#endregion

namespace Unilunch.Tests
{
    [TestClass]
    public class SonaattiParserTest
    {
        #region ConstructDateFromSonaattiDate

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void ConstructDateWithEmptyThrowsException()
        {
            SonaattiParser.ConstructDateFromSonaattiDate("");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void ConstructDateWithNullThrowsException()
        {
            SonaattiParser.ConstructDateFromSonaattiDate(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void ConstructDateWithWrongDataThrowsException()
        {
            SonaattiParser.ConstructDateFromSonaattiDate("wrong data");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void ConstructDateWithNonnumericDataThrowsException()
        {
            SonaattiParser.ConstructDateFromSonaattiDate("abc.dfe.xcv");
        }

        [TestMethod]
        public void ConstructDateWithCorrectStringReturnsCorrectDate()
        {
            var expected = new DateTime(2014, 6, 3);
            Assert.AreEqual(expected.ToShortDateString(),
                            SonaattiParser.ConstructDateFromSonaattiDate("3.6.2014").ToShortDateString());
        }

        #endregion

        #region CleanDescriptionFromPrice

        [TestMethod]
        public void ItExcludesPrice()
        {
            var description = SonaattiParser.CleanDescriptionFromPrice("Test (6,00€)");
            Assert.AreEqual("Test", description);
        }

        [TestMethod]
        public void ItExcludesTwoPrices()
        {
            var description = SonaattiParser.CleanDescriptionFromPrice("Test (6,00€ / 12,00€)");
            Assert.AreEqual("Test", description);
        }

        [TestMethod]
        public void ItWorksIfThereIsNoPrice()
        {
            var description = SonaattiParser.CleanDescriptionFromPrice("Test ");
            Assert.AreEqual("Test", description);
        }

        [TestMethod]
        public void ItExcludesDiets()
        {
            var description = SonaattiParser.CleanDescriptionFromPrice("Test #L#G");
            Assert.AreEqual("Test", description);
        }

        [TestMethod]
        public void ItExcludesDietsAndPrice()
        {
            var description = SonaattiParser.CleanDescriptionFromPrice("Test #L#G (6,00€)");
            Assert.AreEqual("Test", description);
        }

        [TestMethod]
        public void ItExcludesDietsWithoutSpace()
        {
            var description = SonaattiParser.CleanDescriptionFromPrice("Test#L#G");
            Assert.AreEqual("Test", description);
        }


        [TestMethod]
        public void ItExcluesPricesWhenThereAreNoDiets()
        {
            var description = SonaattiParser.CleanDescriptionFromPrice("Test (6,00 € / 12,00 €)");
            Assert.AreEqual("Test", description);
        }

        [TestMethod]
        public void ItExcludesJustBrackets()
        {
            var description = SonaattiParser.CleanDescriptionFromPrice("Test ()");
            Assert.AreEqual("Test", description);
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

        #endregion

        #region Diets

        [TestMethod]
        public void ItFindsDiets()
        {
            var diets = SonaattiParser.Diets("Test #L#G");
            Assert.AreEqual("L", diets[0]);
            Assert.AreEqual("G", diets[1]);
            Assert.AreEqual(2, diets.Count);
        }

        [TestMethod]
        public void ItReturnsEmptyListIfNoDiets()
        {
            var diets = SonaattiParser.Diets("Test (6,00€)");
            Assert.AreEqual(0, diets.Count);
        }

        [TestMethod]
        public void ItFindsDietsWithPrice()
        {
            var diets = SonaattiParser.Diets("Test #GL #V (6,00€)");
            Assert.AreEqual(2, diets.Count);
            Assert.AreEqual("GL", diets[0]);
            Assert.AreEqual("V", diets[1]);
        }

        [TestMethod]
        public void ItShouldWorkOnThisSpecialCase()
        {
            Assert.AreEqual("L", SonaattiParser.Diets("Kalkkuna-rakuunakastiketta ja tummaa riisiä #L #G")[0]);
        }

        #endregion

        #region SetPrices

        [TestMethod]
        public void ItSetsBothPricesFromOnePrice()
        {
            var menuItem = new RestaurantMenuItem();
            SonaattiParser.SetPrices("Test #GL #V (6,00€)", menuItem);
            Assert.AreEqual("6,00", menuItem.student_prize);
            Assert.AreEqual("6,00", menuItem.staff_prize);
        }

        [TestMethod]
        public void ItFindsBothPrices()
        {
            var menuItem = new RestaurantMenuItem();
            SonaattiParser.SetPrices("Test #GL #V (6,00€ / 12,00€)", menuItem);
            Assert.AreEqual("6,00", menuItem.student_prize);
            Assert.AreEqual("12,00", menuItem.staff_prize);
        }

        [TestMethod]
        public void ItFindsBothPricesWithoutSpaceInBetween()
        {
            var menuItem = new RestaurantMenuItem();
            SonaattiParser.SetPrices("Test #GL #V (6,00€/12,00€)", menuItem);
            Assert.AreEqual("6,00", menuItem.student_prize);
            Assert.AreEqual("12,00", menuItem.staff_prize);
        }

        [TestMethod]
        public void ItFindsBothPricesWithoutBrackets()
        {
            var menuItem = new RestaurantMenuItem();
            SonaattiParser.SetPrices("Test #GL #V 6,00€ / 12,00€", menuItem);
            Assert.AreEqual("6,00", menuItem.student_prize);
            Assert.AreEqual("12,00", menuItem.staff_prize);
        }

        [TestMethod]
        public void ItFindsBothPricesWithSpaceBeforeEuroSign()
        {
            var menuItem = new RestaurantMenuItem();
            SonaattiParser.SetPrices("Test #GL #V 6,00 € / 12,00 €", menuItem);
            Assert.AreEqual("6,00", menuItem.student_prize);
            Assert.AreEqual("12,00", menuItem.staff_prize);
        }

        #endregion
    }
}