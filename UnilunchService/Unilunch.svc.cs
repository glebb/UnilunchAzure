using System.Data.Entity;
using System.Data.Objects;
using System.Diagnostics;
using System.Linq;
using DBApp;
using Newtonsoft.Json;
using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using UnilunchData;
namespace UnilunchService
{

    public class Unilunch : IUnilunchService
    {
        static DateTime _timestamp;
        static Sonaatti _sonaatti;

        public Message UpdateDatabase()
        {
            if (_sonaatti == null)
            {
                _sonaatti = new Sonaatti(new DataSource());
                _timestamp = DateTime.Now;
            }
            else if ((DateTime.Now - _timestamp).TotalMinutes > 2 )
            {
                _sonaatti = new Sonaatti(new DataSource());
                _timestamp = DateTime.Now;
            }

            Database.SetInitializer(new DropCreateDatabaseAlways<UnilunchContext>());

            using (var context = new UnilunchContext())
                DbHandler.SaveToDb(_sonaatti, context);
            const string responseText = "OK";
            var res = JsonConvert.SerializeObject(responseText);
            Debug.Assert(WebOperationContext.Current != null, "WebOperationContext.Current != null");
            return WebOperationContext.Current.CreateTextResponse(res, "application/json; charset=utf-8", Encoding.UTF8);
        }

        public Message JsonData(string date)
        {
            var userDate = !String.IsNullOrEmpty(date) ? DateTime.ParseExact(date, "ddMMyyyy", null) : DateTime.Today;
            var container = new RestaurantJsonContainer();
            string res;
            using (var context = new UnilunchContext())
            {
                var result = context.MenuDates
                    .Where(d => EntityFunctions.TruncateTime(d.RealDate) == userDate.Date)
                    .Select(d=>d.RestaurantDetail)
                    .Select(r => new
                    {
                        RestauraurantDetail = r,
                        dates = r.dates.Where(d => EntityFunctions.TruncateTime(d.RealDate) == userDate.Date)
                    })
                    .ToList();
                container.restaurant.AddRange(result.Select(a => a.RestauraurantDetail).ToList());
                res = JsonConvert.SerializeObject(container);
            }

            Debug.Assert(WebOperationContext.Current != null, "WebOperationContext.Current != null");
            return WebOperationContext.Current.CreateTextResponse(res,
                "application/json; charset=utf-8", Encoding.UTF8);
        }

        public Message AllData()
        {
            var container = new RestaurantJsonContainer();
            string res;
            using (var context = new UnilunchContext())
            {
                container.restaurant.AddRange(context.Restaurants.Include(r=>r.dates));
                res = JsonConvert.SerializeObject(container);
            }
            Debug.Assert(WebOperationContext.Current != null, "WebOperationContext.Current != null");
            return WebOperationContext.Current.CreateTextResponse(res,
                "application/json; charset=utf-8", Encoding.UTF8);
        }

    }
}
