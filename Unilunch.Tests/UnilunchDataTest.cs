#region using directives

using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnilunchData;

#endregion

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