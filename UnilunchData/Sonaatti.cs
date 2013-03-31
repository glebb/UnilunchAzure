using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UnilunchData
{
    public class Sonaatti : IRestaurantPlugin
    {
        IDataSource data;
        List<RestaurantDetail> _restaurants;

        public IList<RestaurantDetail> Restaurants { get { return _restaurants; } }

        public Sonaatti(IDataSource data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            this.data = data;
            
            _restaurants = new List<RestaurantDetail>();

            createRestaurant("Piato", "62.232037", "25.736421", "http://www.sonaatti.fi/piato");
            createRestaurant("Kvarkki", "62.230115", "25.741338", "http://www.sonaatti.fi/kvarkki");
        }

        private void createRestaurant(string name, string latitude, string longitude, string url)
        {
            var restaurant = new RestaurantDetail()
            {
                name = name,
                company = "Sonaatti"
            };
            restaurant.location.latitude = latitude;
            restaurant.location.longitude = longitude;
            restaurant.contact.website = url;

            var dom = CQ.Create(data.Load(new Uri(url)));
            restaurant.dates.AddRange(SonaattiParserHelpers.createMenu(dom));
            _restaurants.Add(restaurant);
        }

    }
}
