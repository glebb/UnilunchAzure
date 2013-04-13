#region using directives

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Web.Script.Serialization;
using Effort.DataLoaders;
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

        [TestMethod]
        public void LunchHoursIsPresentedCorrectly()
        {
            var lunch = new LunchHours
                {
                    RealStart = new DateTime(2013, 04, 13, 11, 00, 00),
                    RealEnd = new DateTime(2013, 04, 13, 15, 00, 00)
                };
            Assert.AreEqual("11:00", lunch.start_time);
            Assert.AreEqual("15:00", lunch.end_time);
        }

        [TestMethod]
        public void LunchHoursReturnsEmptyWithNull()
        {
            var lunch = new LunchHours {RealStart = null, RealEnd = null};
            Assert.AreEqual("", lunch.start_time);
            Assert.AreEqual("", lunch.end_time);
        }

        [TestMethod]
        public void OpenHoursReturnsEmptyWithNull()
        {
            var openHours = new OpenHours {RealStart = null, RealEnd = null};
            Assert.AreEqual("", openHours.start_time);
            Assert.AreEqual("", openHours.end_time);
        }

        [TestMethod]
        public void OpenHoursIsPresentedCorrectly()
        {
            var openHours = new OpenHours
                {
                    RealStart = new DateTime(2013, 04, 13, 8, 00, 00),
                    RealEnd = new DateTime(2013, 04, 13, 16, 00, 00)
                };
            Assert.AreEqual("08:00", openHours.start_time);
            Assert.AreEqual("16:00", openHours.end_time);
        }

        [TestMethod]
        public void DietSerialization()
        {
            var menuItem = new RestaurantMenuItem {DietsSer = "L;G"};
            Assert.AreEqual("G", menuItem.diets[1]);
        }

        [TestMethod]
        public void DietDeSerialization()
        {
            var menuItem = new RestaurantMenuItem();
            menuItem.diets.AddRange(new List<string>
                {
                    "L", "G"
                });
            Assert.AreEqual("L;G", menuItem.DietsSer);
        }

        [TestMethod]
        public void SaveToDb()
        {
            IDataLoader loader = new CsvDataLoader("./");
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient(loader);
            using (var ctx = new UnilunchContext(connection))
            {
                var restaurants = ctx.Restaurants.ToList();
                restaurants.Add(new RestaurantDetail
                    {
                        name = "Testi Ravintola"
                    });
                DbHandler.SaveToDb(restaurants, ctx);
                Debug.Assert(ctx.Restaurants != null, "ctx.Restaurants != null");
                Assert.AreEqual("Testi Ravintola", actual: ctx.Restaurants.FirstOrDefault(r=>r.name == "Testi Ravintola").name);
            }
        }

        [TestMethod]
        public void GetRestaurants()
        {
            IDataLoader loader = new CsvDataLoader("./");
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient(loader);
            using (var ctx = new UnilunchContext(connection))
            {
                var restaurants = DbHandler.RestaurantsQuery("Piato", null, ctx, new DateRange
                    {
                        Start = new DateTime(2013,4,15),
                        End = new DateTime(2013,4,16)
                    });
                Assert.AreEqual(1, restaurants.Count);
                Assert.AreEqual("Piato", restaurants[0].name);
                Assert.AreEqual(1, restaurants[0].dates.Count);
            }
        }

    }
}