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
