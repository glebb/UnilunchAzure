﻿#region using directives

using System;
using System.Data.Entity;
using Newtonsoft.Json;
using UnilunchData;

#endregion

namespace DBApp
{
    internal static class Program
    {
        private static void Main()
        {
            var source = new DataSource();
            var sonaatti = new Sonaatti(source);

            Database.SetInitializer(new DropCreateDatabaseAlways<UnilunchContext>());
            using (var context = new UnilunchContext())
            {
                DbHandler.SaveToDb(sonaatti, context);
                var query = context.Restaurants;
                var container = new RestaurantJsonContainer();
                container.restaurant.AddRange(query);
                var res = JsonConvert.SerializeObject(container);
                Console.WriteLine(res);
            }
        }
    }
}