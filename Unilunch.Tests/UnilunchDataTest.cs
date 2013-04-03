using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnilunchData;
using System.Web.Script.Serialization;

namespace Unilunch.Tests
{
    [TestClass]
    public class UnilunchDataTest
    {
        [TestMethod]
        public void ItShouldCreateEmptyMainObjectWithoutData()
        {
            var container = new RestaurantJsonContainer();
            var res = new JavaScriptSerializer().Serialize(container);
            Assert.AreEqual("{\"restaurant\":[]}", res);
        }
    }
}
