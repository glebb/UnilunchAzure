using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using UnilunchData;
namespace UnilunchService
{

    public class Unilunch : IUnilunchService
    {

        public Message JsonData()
        {
            var container = new RestaurantJsonContainer();
            var detail = new RestaurantDetail
            {
                id = "1",
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
            detail.dates.Add(new MenuDate());
            detail.dates[0].SetRealDate(new DateTime(2013, 03, 28));
            detail.dates[0].lunch_hours.start_time = "11:00";
            detail.dates[0].lunch_hours.end_time = "12:30";
            detail.dates[0].open_hours.end_time = "16:00";
            detail.dates[0].open_hours.start_time = "09:00";
            detail.dates[0].foods.Add(new RestaurantMenuItem
            {
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
            detail.dates[0].foods.Add(new RestaurantMenuItem
            {
                description = "Broilerin rintaleike",
                student_prize = "",
                staff_prize = "8,60"
            });
            detail.dates[0].foods[3].diets.Add("L");
            detail.dates[0].foods[3].diets.Add("G");
            detail.dates[0].foods[3].diets.Add("VH");

            container.restaurant.Add(detail);
            var res = new JavaScriptSerializer().Serialize(container);
            return WebOperationContext.Current.CreateTextResponse(res,
                "application/json; charset=utf-8",
                Encoding.UTF8);
        }
    }
}
