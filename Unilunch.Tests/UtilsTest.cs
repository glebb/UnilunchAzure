using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnilunchData;

namespace Unilunch.Tests
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testConstructDateWithEmptyThrowsException()
        {
            Utils.ConstructDateFromSonaattiDate("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testConstructDateWithNullThrowsException()
        {
            Utils.ConstructDateFromSonaattiDate(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testConstructDateWithWrongDataThrowsException()
        {
            Utils.ConstructDateFromSonaattiDate("wrong data");
        }

        [TestMethod]
        public void testConstructDateWithCorrectStringReturnsDate()
        {
            Utils.ConstructDateFromSonaattiDate("2.6.2014");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testConstructDateWithNonNumericDataThrowsException()
        {
            Utils.ConstructDateFromSonaattiDate("abc.dfe.xcv");
        }

        [TestMethod]
        public void testConstructDateWithCorrectStringReturnsCorrectDate()
        {
            var expected = new DateTime(2014, 6, 3);
            Assert.AreEqual(expected.ToShortDateString(), Utils.ConstructDateFromSonaattiDate("3.6.2014").ToShortDateString());
        }
    }
}
