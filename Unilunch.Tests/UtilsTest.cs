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
        public void TestConstructDateWithEmptyThrowsException()
        {
            Utils.ConstructDateFromSonaattiDate("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructDateWithNullThrowsException()
        {
            Utils.ConstructDateFromSonaattiDate(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructDateWithWrongDataThrowsException()
        {
            Utils.ConstructDateFromSonaattiDate("wrong data");
        }

        [TestMethod]
        public void TestConstructDateWithCorrectStringReturnsDate()
        {
            Utils.ConstructDateFromSonaattiDate("2.6.2014");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructDateWithNonnumericDataThrowsException()
        {
            Utils.ConstructDateFromSonaattiDate("abc.dfe.xcv");
        }

        [TestMethod]
        public void TestConstructDateWithCorrectStringReturnsCorrectDate()
        {
            var expected = new DateTime(2014, 6, 3);
            Assert.AreEqual(expected.ToShortDateString(), Utils.ConstructDateFromSonaattiDate("3.6.2014").ToShortDateString());
        }
    }
}
