#region using directives

using System;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;
using UnilunchData;

#endregion

namespace UnilunchService
{
    public class Unilunch : IUnilunchService
    {
        private static DateTime _timestamp;
        private static Sonaatti _sonaatti;

        public Stream UpdateDatabase()
        {
            if (_sonaatti == null)
            {
                _sonaatti = new Sonaatti(new DataSource());
                _timestamp = DateTime.Now;
            }
            else if ((DateTime.Now - _timestamp).TotalMinutes > 2)
            {
                _sonaatti = new Sonaatti(new DataSource());
                _timestamp = DateTime.Now;
            }

            Database.SetInitializer(new DropCreateDatabaseAlways<UnilunchContext>());

            using (var context = new UnilunchContext())
                DbHandler.SaveToDb(_sonaatti, context);
            var res = JsonConvert.SerializeObject("OK");
            return CreateJsonResponse(res);
        }

        public Stream FetchData(string date, string name, string id)
        {
            var dateRange = WebInterfaceParser.ResolveDateRange(date);
            var container = new RestaurantJsonContainer();
            string res;
            using (var context = new UnilunchContext())
            {
                var result = DbHandler.RestaurantsQuery(name, id, context, dateRange);
                container.restaurant.AddRange(result);
                res = JsonConvert.SerializeObject(container);
            }
            return CreateJsonResponse(res);
        }

        public Stream AllData()
        {
            var container = new RestaurantJsonContainer();
            string res;
            using (var context = new UnilunchContext())
            {
                container.restaurant.AddRange(context.Restaurants.Include(r => r.dates));
                res = JsonConvert.SerializeObject(container);
            }
            return CreateJsonResponse(res);
        }

        private static Stream CreateJsonResponse(string res)
        {
            Debug.Assert(WebOperationContext.Current != null, "WebOperationContext.Current != null");
            WebOperationContext.Current.OutgoingResponse.ContentType =
                "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(res));
        }
    }
}