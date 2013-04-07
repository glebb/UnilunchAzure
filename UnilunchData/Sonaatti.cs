﻿using CsQuery;
using System;
using System.Collections.Generic;

namespace UnilunchData
{
    public class Sonaatti
    {
        readonly IDataSource _data;
        static int _counter = 1;
        readonly List<RestaurantDetail> _restaurants;

        public IList<RestaurantDetail> Restaurants { get { return _restaurants; } }

        public Sonaatti(IDataSource data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            _data = data;
            _restaurants = new List<RestaurantDetail>();

            CreateRestaurant("Piato", "62.232037", "25.736421", "http://www.sonaatti.fi/piato", "Mattilanniemi 2 (Agora);40100;Jyväskylä", "040 805 4016", "piato@sonaatti.fi");
            CreateRestaurant("Kvarkki", "62.230115", "25.741338", "http://www.sonaatti.fi/kvarkki", "Survontie 9 (YK-rak.);40500;Jyväskylä", "040 805 4005", "ylisto@sonaatti.fi");
            CreateRestaurant("Cafe Libri", "62.237685", "25.736614", "http://www.sonaatti.fi/cafe-libri", "Seminaarinkatu 15 (B-rak);40100;Jyväskylä", "040 805 4008", "cafelibri@sonaatti.fi");
            CreateRestaurant("Lozzi", "62.235411", "25.730384", "http://www.sonaatti.fi/lozzi", "Keskussairaalantie 2;40600;Jyväskylä", "040 8054012", "lozzi@sonaatti.fi");
            CreateRestaurant("Musica", "62.238292", "25.734508", "http://www.sonaatti.fi/musica", "Seminaarinkatu 15 (M-rak.);40100;Jyväskylä", "040 805 4009", "musica@sonaatti.fi");
            CreateRestaurant("Syke", "62.234724", "25.729208", "http://www.sonaatti.fi/syke", "Keskussairaalantie 4 L-rak.;40600;Jyväskylä", "040 805 4018", "syke@sonaatti.fi");
            CreateRestaurant("Wilhelmiina", "62.23104", "25.733803", "http://www.sonaatti.fi/wilhelmiina", "Ahlmaninkatu 2 (MaA-rak.);40100;Jyväskylä", "040 805 4014", "wilhelmiina@sonaatti.fi");
            CreateRestaurant("Hestia", "62.226731", "25.744484", "http://www.sonaatti.fi/hestia", "Ylistönmäentie 33;40500;Jyväskylä", "040 805 4019", "hestia@sonaatti.fi");
            CreateRestaurant("Ylistö", "62.230115", "25.741338", "http://www.sonaatti.fi/ylisto", "Survontie 9 (YFL-rak.);40500;Jyväskylä", " 040 805 4004", "ylisto@sonaatti.fi");
            CreateRestaurant("Novelli", "62.238562", "25.742791", "http://www.sonaatti.fi/novelli", "Vapaudenkatu 39-41 (pääkirjasto);40100;Jyväskylä", "014 2660215", "novelli@sonaatti.fi");
            CreateRestaurant("Normaalikoulu", "62.239837", "25.736257", "http://www.sonaatti.fi/normaalikoulu", "Yliopistonkatu 1;40100;Jyväskylä", "0400 248145", "muonatupa@sonaatti.fi");
        }

        private void CreateRestaurant(string name, string latitude, string longitude, string url, string address, string phone, string email)
        {
            var restaurant = new RestaurantDetail
                {
                RestaurantDetailId = _counter,
                name = name,
                company = "Sonaatti",
                category = "Jyväskylä"
            };
            _counter++;
            restaurant.location.latitude = latitude;
            restaurant.location.longitude = longitude;
            restaurant.contact.website = url;
            restaurant.contact.email = email;
            restaurant.contact.phone_number = phone;
            restaurant.address.street_address = address.Split(';')[0];
            restaurant.address.postal_code = address.Split(';')[1];
            restaurant.address.city = address.Split(';')[2];

            var dom = CQ.Create(_data.Load(new Uri(url)));
            restaurant.dates.AddRange(SonaattiParser.CreateMenu(dom));
            _restaurants.Add(restaurant);
        }

    }
}
