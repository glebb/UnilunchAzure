using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnilunchData;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

namespace Unilunch.Tests
{
    [TestClass]
    public class UnilunchDataTest
    {
        [TestMethod]
        public void itShouldCreateOnlyMainObjectWithoutData()
        {
            var container = new RestaurantJsonContainer();
            MemoryStream stream1 = new MemoryStream();
            var res = new JavaScriptSerializer().Serialize(container);
            Assert.AreEqual("{\"restaurant\":[]}", res);
        }

        [TestMethod]
        public void itShouldCreateCorrectObjectForSingleRestaurantAndSingleDay()
        {
            var container = new RestaurantJsonContainer();
            var detail = new RestaurantDetail {
                 id = "1",
                 name = "Alvari",
                 company = "Sonaatti",
            };
            detail.location.latitude = "62.235411";
            detail.location.longitude = "25.730384";
            detail.address.street_address = "Keskussairaalantie 4 (P-rak.)";
            detail.address.postal_code = "40600";
            detail.address.city = "JyvÃ¤skylÃ¤";
            detail.contact.phone_number = "040 8054012";
            detail.contact.email = "lozzi@sonaatti.fi";
            detail.contact.website = "http://www.sonaatti.fi/alvari";
            detail.dates.Add(new MenuDate
            {
                date = "28.03.2013"
            });
            detail.dates[0].lunch_hours.start_time = "11:00";
            detail.dates[0].lunch_hours.end_time = "12:30";
            detail.dates[0].open_hours.end_time = "16:00";
            detail.dates[0].open_hours.start_time = "09:00";
            detail.dates[0].foods.Add(new RestaurantMenuItem { 
                description = "Meksikolaista jauhelihakasvisrisottoa",
                student_prize = "",
                staff_prize = "5,60"
            });
            detail.dates[0].foods[0].diets.Add("L");
            detail.dates[0].foods[0].diets.Add("M");
            detail.dates[0].foods[0].diets.Add("G");
            detail.dates[0].foods[0].diets.Add("VH");
            
            detail.dates[0].foods.Add(new RestaurantMenuItem
            {
                description = "Kirjolohilasagnettea",
                student_prize = "",
                staff_prize = "5,60"
            });
            detail.dates[0].foods[1].diets.Add("VL");

            detail.dates[0].foods.Add(new RestaurantMenuItem
            {
                description = "Tomaattivuohenjuustokastiketta",
                student_prize = "",
                staff_prize = "5,60"
            });
            detail.dates[0].foods[2].diets.Add("VH");
            detail.dates[0].foods[2].diets.Add("G");
            
            detail.dates[0].foods.Add(new RestaurantMenuItem());
            detail.dates[0].foods[3].description = "Broilerin rintaleike";
            detail.dates[0].foods[3].student_prize = "";
            detail.dates[0].foods[3].staff_prize = "8,60";
            detail.dates[0].foods[3].diets.Add("L");
            detail.dates[0].foods[3].diets.Add("G");
            detail.dates[0].foods[3].diets.Add("VH");

            container.restaurant.Add(detail);

            var res = new JavaScriptSerializer().Serialize(container);
            Assert.AreEqual("{\"restaurant\":[{\"id\":\"1\",\"name\":\"Alvari\",\"company\":\"Sonaatti\",\"location\":{\"longitude\":\"25.730384\",\"latitude\":\"62.235411\"},\"address\":{\"street_address\":\"Keskussairaalantie 4 (P-rak.)\",\"postal_code\":\"40600\",\"city\":\"JyvÃ¤skylÃ¤\"},\"contact\":{\"phone_number\":\"040 8054012\",\"email\":\"lozzi@sonaatti.fi\",\"website\":\"http://www.sonaatti.fi/alvari\"},\"dates\":[{\"date\":\"28.03.2013\",\"open_hours\":{\"start_time\":\"09:00\",\"end_time\":\"16:00\"},\"lunch_hours\":{\"start_time\":\"11:00\",\"end_time\":\"12:30\"},\"foods\":[{\"description\":\"Meksikolaista jauhelihakasvisrisottoa\",\"student_prize\":\"\",\"staff_prize\":\"5,60\",\"diets\":[\"L\",\"M\",\"G\",\"VH\"]},{\"description\":\"Kirjolohilasagnettea\",\"student_prize\":\"\",\"staff_prize\":\"5,60\",\"diets\":[\"VL\"]},{\"description\":\"Tomaattivuohenjuustokastiketta\",\"student_prize\":\"\",\"staff_prize\":\"5,60\",\"diets\":[\"VH\",\"G\"]},{\"description\":\"Broilerin rintaleike\",\"student_prize\":\"\",\"staff_prize\":\"8,60\",\"diets\":[\"L\",\"G\",\"VH\"]}]}]}]}", res);

        }

        [TestMethod]
        public void itShouldCreateCorrectObjectForManyRestaurantAndSingleDay()
        {
            var container = new RestaurantJsonContainer();
            var detail = new RestaurantDetail
            {
                id = "1",
                name = "Aallokko",
                company = "Sonaatti",
            };
            container.restaurant.Add(detail);

            var detail2 = new RestaurantDetail
            {
                id = "2",
                name = "Piato",
                company = "Sonaatti",
            };
            container.restaurant.Add(detail2);
            var res = new JavaScriptSerializer().Serialize(container);
            Assert.AreEqual("{\"restaurant\":[{\"id\":\"1\",\"name\":\"Aallokko\",\"company\":\"Sonaatti\",\"location\":{\"longitude\":null,\"latitude\":null},\"address\":{\"street_address\":null,\"postal_code\":null,\"city\":null},\"contact\":{\"phone_number\":null,\"email\":null,\"website\":null},\"dates\":[]},{\"id\":\"2\",\"name\":\"Piato\",\"company\":\"Sonaatti\",\"location\":{\"longitude\":null,\"latitude\":null},\"address\":{\"street_address\":null,\"postal_code\":null,\"city\":null},\"contact\":{\"phone_number\":null,\"email\":null,\"website\":null},\"dates\":[]}]}", res);
        }
    }
}
