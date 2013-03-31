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
        static int counter = 1;
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
            createRestaurant("Cafe Libri", "62.237685", "25.736614", "http://www.sonaatti.fi/cafe-libri");
            createRestaurant("Lozzi", "62.235411", "25.730384", "http://www.sonaatti.fi/lozzi");
            createRestaurant("Musica", "62.238292", "25.734508", "http://www.sonaatti.fi/musica");
            createRestaurant("Syke", "62.234724", "25.729208", "http://www.sonaatti.fi/syke");
            createRestaurant("Wilhelmiina", "62.23104", "25.733803", "http://www.sonaatti.fi/wilhelmiina");
            createRestaurant("Hestia", "62.226731", "25.744484", "http://www.sonaatti.fi/hestia");
            createRestaurant("Ylistö", "62.230115", "25.741338", "http://www.sonaatti.fi/ylisto");
            createRestaurant("Novelli", "62.238562", "25.742791", "http://www.sonaatti.fi/novelli");
            createRestaurant("Normaalikoulu", "62.239837", "25.736257", "http://www.sonaatti.fi/normaalikoulu");
        }

        private void createRestaurant(string name, string latitude, string longitude, string url)
        {
            var restaurant = new RestaurantDetail()
            {
                id = counter.ToString(),
                name = name,
                company = "Sonaatti"
            };
            counter++;
            restaurant.location.latitude = latitude;
            restaurant.location.longitude = longitude;
            restaurant.contact.website = url;

            var dom = CQ.Create(data.Load(new Uri(url)));
            restaurant.dates.AddRange(SonaattiParserHelpers.createMenu(dom));
            _restaurants.Add(restaurant);
        }

    }
}
