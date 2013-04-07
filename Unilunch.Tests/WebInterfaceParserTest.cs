#region using directives

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnilunchService;

#endregion

namespace Unilunch.Tests
{
    [TestClass]
    public class WebInterfaceParserTest
    {
        [TestMethod]
        public void NullDateReturnsDateRangeForToday()
        {
            var range = WebInterfaceParser.ResolveDateRange(null);
            Assert.AreEqual(DateTime.Today, range.Start);
            Assert.AreEqual(DateTime.Today.AddDays(1), range.End);
        }

        [TestMethod]
        public void EmptyDateReturnsDateRangeForToday()
        {
            var range = WebInterfaceParser.ResolveDateRange("");
            Assert.AreEqual(DateTime.Today, range.Start);
            Assert.AreEqual(DateTime.Today.AddDays(1), range.End);
        }

        [TestMethod]
        public void SingleDateReturnsDateRangeForThatDay()
        {
            var range = WebInterfaceParser.ResolveDateRange("23042013");
            Assert.AreEqual(new DateTime(2013, 4, 23), range.Start);
            Assert.AreEqual(new DateTime(2013, 4, 24), range.End);
        }

        [TestMethod]
        public void MalformedDateReturnsDateRangeForToday()
        {
            var range = WebInterfaceParser.ResolveDateRange("xxx");
            Assert.AreEqual(DateTime.Today, range.Start);
            Assert.AreEqual(DateTime.Today.AddDays(1), range.End);
        }

        [TestMethod]
        public void DateRangeReturnsDateRange()
        {
            var range = WebInterfaceParser.ResolveDateRange("23042013-01052013");
            Assert.AreEqual(new DateTime(2013, 4, 23), range.Start);
            Assert.AreEqual(new DateTime(2013, 5, 2), range.End);
        }

        [TestMethod]
        public void MalformedFirstDateInRangeReturnsToday()
        {
            var range = WebInterfaceParser.ResolveDateRange("2304x013-01052013");
            Assert.AreEqual(DateTime.Today, range.Start);
            Assert.AreEqual(DateTime.Today.AddDays(1), range.End);
        }

        [TestMethod]
        public void MalformedSecondDateInRangeReturnsToday()
        {
            var range = WebInterfaceParser.ResolveDateRange("23042013-01052xz13");
            Assert.AreEqual(DateTime.Today, range.Start);
            Assert.AreEqual(DateTime.Today.AddDays(1), range.End);
        }

        [TestMethod]
        public void PlusOneReturnsDateRangeForOneDay()
        {
            var range = WebInterfaceParser.ResolveDateRange("23042013plus1");
            Assert.AreEqual(new DateTime(2013, 4, 23), range.Start);
            Assert.AreEqual(new DateTime(2013, 4, 25), range.End);
        }

        [TestMethod]
        public void PlusThreeReturnsDateRangeForThreeDays()
        {
            var range = WebInterfaceParser.ResolveDateRange("23042013plus3");
            Assert.AreEqual(new DateTime(2013, 4, 23), range.Start);
            Assert.AreEqual(new DateTime(2013, 4, 27), range.End);
        }

        [TestMethod]
        public void MalformedDateInPlusReturnsToday()
        {
            var range = WebInterfaceParser.ResolveDateRange("230420x3plus5");
            Assert.AreEqual(DateTime.Today, range.Start);
            Assert.AreEqual(DateTime.Today.AddDays(1), range.End);
        }

        [TestMethod]
        public void MalformedNumberOfDaysInPlusReturnsCorrectDayPlusOne()
        {
            var range = WebInterfaceParser.ResolveDateRange("23042013plusx");
            Assert.AreEqual(new DateTime(2013, 4, 23), range.Start);
            Assert.AreEqual(new DateTime(2013, 4, 24), range.End);
        }
    }
}