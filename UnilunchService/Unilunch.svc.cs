using System.Data.Entity;
using System.Data.Objects;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.ServiceModel.Web;
using System.Text;
using UnilunchData;
namespace UnilunchService
{

    public class Unilunch : IUnilunchService
    {
        static DateTime _timestamp;
        static Sonaatti _sonaatti;

        public Stream UpdateDatabase()
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
            var res = JsonConvert.SerializeObject("OK");
            return CreateJsonResponse(res);
        }

        public Stream FetchData(string date)
        {
            var dateRange = WebInterfaceParser.ResolveDateRange(date);
            var container = new RestaurantJsonContainer();
            string res;
            using (var context = new UnilunchContext())
            {
                var result = context.MenuDates
                    .Where(d => d.RealDate >= dateRange.Start && d.RealDate < dateRange.End)
                    .Select(d=>d.RestaurantDetail)
                    .Select(r => new
                    {
                        RestauraurantDetail = r,
                        dates = r.dates.Where(d => EntityFunctions.TruncateTime(d.RealDate) == dateRange.Start.Date)
                    })
                    .ToList();
                container.restaurant.AddRange(result.Select(a => a.RestauraurantDetail).ToList());
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

        public Stream AllData()
        {
            var container = new RestaurantJsonContainer();
            string res;
            using (var context = new UnilunchContext())
            {
                container.restaurant.AddRange(context.Restaurants.Include(r=>r.dates));
                res = JsonConvert.SerializeObject(container);
            }
            return CreateJsonResponse(res);
        }

    }
}
