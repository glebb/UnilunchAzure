using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnilunchData;
using System.IO;
using System.Runtime.Serialization.Json;

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
            DataContractJsonSerializer ser =
              new DataContractJsonSerializer(typeof(RestaurantJsonContainer));
            ser.WriteObject(stream1, container);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            Assert.AreEqual("{\"restaurant\":[]}", sr.ReadToEnd());
        }

        [TestMethod]
        public void itShouldCreateCorrectObjectForSingleRestaurantAndSingleDay()
        {
            var container = new RestaurantJsonContainer();
            var detail = new RestaurantDetail {
                 id = 1,
                 name = "Aallokko",
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
                student_price = "",
                staff_price = "5,60"
            });
            detail.dates[0].foods[0].diets.Add("L");
            detail.dates[0].foods[0].diets.Add("M");
            detail.dates[0].foods[0].diets.Add("G");
            detail.dates[0].foods[0].diets.Add("VH");
            detail.dates[0].foods.Add(new RestaurantMenuItem
            {
                description = "Kirjolohilasagnettea",
                student_price = "",
                staff_price = "5,60"
            });
            detail.dates[0].foods[1].diets.Add("VL");
            detail.dates[0].foods.Add(new RestaurantMenuItem
            {
                description = "Tomaattivuohenjuustokastiketta",
                student_price = "",
                staff_price = "5,60"
            });
            detail.dates[0].foods[2].diets.Add("VH");
            detail.dates[0].foods[2].diets.Add("G");
            detail.dates[0].foods.Add(new RestaurantMenuItem
            {
                description = "Broilerin rintaleike",
                student_price = "",
                staff_price = "5,60"
            });
            detail.dates[0].foods[3].diets.Add("L");
            detail.dates[0].foods[3].diets.Add("G");
            detail.dates[0].foods[3].diets.Add("VH");

            container.restaurant.Add(detail);

            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser =
              new DataContractJsonSerializer(typeof(RestaurantJsonContainer));
            ser.WriteObject(stream1, container);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            Assert.AreEqual("{\"restaurant\":[{\"address\":{\"city\":\"JyvÃ¤skylÃ¤\",\"postal_code\":\"40600\",\"street_address\":\"Keskussairaalantie 4 (P-rak.)\"},\"company\":\"Sonaatti\",\"contact\":{\"email\":\"lozzi@sonaatti.fi\",\"phone_number\":\"040 8054012\",\"website\":\"http:\\/\\/www.sonaatti.fi\\/alvari\"},\"dates\":[{\"date\":\"28.03.2013\",\"foods\":[{\"description\":\"Meksikolaista jauhelihakasvisrisottoa\",\"diets\":[\"L\",\"M\",\"G\",\"VH\"],\"staff_price\":\"5,60\",\"student_price\":\"\"},{\"description\":\"Kirjolohilasagnettea\",\"diets\":[\"VL\"],\"staff_price\":\"5,60\",\"student_price\":\"\"},{\"description\":\"Tomaattivuohenjuustokastiketta\",\"diets\":[\"VH\",\"G\"],\"staff_price\":\"5,60\",\"student_price\":\"\"},{\"description\":\"Broilerin rintaleike\",\"diets\":[\"L\",\"G\",\"VH\"],\"staff_price\":\"5,60\",\"student_price\":\"\"}],\"lunch_hours\":{\"end_time\":\"12:30\",\"start_time\":\"11:00\"},\"open_hours\":{\"end_time\":\"16:00\",\"start_time\":\"09:00\"}}],\"id\":1,\"location\":{\"latitude\":\"62.235411\",\"longitude\":\"25.730384\"},\"name\":\"Aallokko\"}]}", sr.ReadToEnd());

        }

        [TestMethod]
        public void itShouldCreateCorrectObjectForManyRestaurantAndSingleDay()
        {
            var container = new RestaurantJsonContainer();
            var detail = new RestaurantDetail
            {
                id = 1,
                name = "Aallokko",
                company = "Sonaatti",
            };
            container.restaurant.Add(detail);

            var detail2 = new RestaurantDetail
            {
                id = 2,
                name = "Piato",
                company = "Sonaatti",
            };
            container.restaurant.Add(detail2);

            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser =
              new DataContractJsonSerializer(typeof(RestaurantJsonContainer));
            ser.WriteObject(stream1, container);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            Assert.AreEqual("{\"restaurant\":[{\"address\":{\"city\":null,\"postal_code\":null,\"street_address\":null},\"company\":\"Sonaatti\",\"contact\":{\"email\":null,\"phone_number\":null,\"website\":null},\"dates\":[],\"id\":1,\"location\":{\"latitude\":null,\"longitude\":null},\"name\":\"Aallokko\"},{\"address\":{\"city\":null,\"postal_code\":null,\"street_address\":null},\"company\":\"Sonaatti\",\"contact\":{\"email\":null,\"phone_number\":null,\"website\":null},\"dates\":[],\"id\":2,\"location\":{\"latitude\":null,\"longitude\":null},\"name\":\"Piato\"}]}", sr.ReadToEnd());
        }
    }
}
