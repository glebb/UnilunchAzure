using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnilunchData
{
    public class Sonaatti : IRestaurantPlugin
    {
        IDataSource data;

        List<RestaurantDetail> _restaurants;
        public Sonaatti(IDataSource data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            this.data = data;
            
            _restaurants = new List<RestaurantDetail>();
            parseRestaurants();
        }

        private void parseRestaurants()
        {
            createRestaurant("Piato", "62.232037", "25.736421", "http://www.sonaatti.fi/piato"); 
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
            var menus = new List<MenuDate>();

            var objects = dom.Select("#lista > .pari, .odd");
            var downconts = objects.Select(".downcont");
            foreach (var x in downconts)
            {
                var date = new MenuDate();
                
                date.SetRealDate(Utils.ConstructDateFromSonaattiDate(x.Cq().Find("span.paiva").Text()));

                var menuItem = new RestaurantMenuItem();
                var rawMenuText = x.Cq().Find("p").Text().ToString();
                menuItem.description = rawMenuText.Split(',').First().Split('#').First().Trim();
                date.foods.Add(menuItem);

                menus.Add(date);
            }
            restaurant.dates.AddRange(menus);
            _restaurants.Add(restaurant);
        }

        public IList<RestaurantDetail> Restaurants
        {
            get
            {
                return _restaurants;
            }
            
        }
    }
}
