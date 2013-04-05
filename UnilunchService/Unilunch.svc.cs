using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
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

        public Message JsonData()
        {
            var container = new RestaurantJsonContainer();
            if (_sonaatti == null)
            {
                _sonaatti = new Sonaatti(new DataSource());
                _timestamp = DateTime.Now;
            }
            else if ((DateTime.Now - _timestamp).TotalMinutes > 360 )
            {
                _sonaatti = new Sonaatti(new DataSource());
                _timestamp = DateTime.Now;
            }

            //Database.SetInitializer(new DropCreateDatabaseAlways<UnilunchContext>());
            string res;

            using (var context = new UnilunchContext())
            {
                foreach (var r in _sonaatti.Restaurants)
                {
                    if (!context.Restaurants.Any(e => e.name == r.name))
                    {
                        context.Restaurants.Add(r);
                    }
                    else
                    {
                        var id = from result in context.Restaurants
                                 where result.name == r.name
                                 select result.RestaurantDetailId;
                        foreach (var date in r.dates)
                        {
                            if (!context.MenuDates.Any(e => e.RealDate == date.RealDate))
                            {
                                context.MenuDates.Add(date);
                            }
                        }
                    }
                }
                context.SaveChanges();

                var query = from r in context.Restaurants select r;
                container.restaurant.AddRange(query);
                res = JsonConvert.SerializeObject(container);
            }


            //container.restaurant.AddRange(_sonaatti.Restaurants);

            Debug.Assert(WebOperationContext.Current != null, "WebOperationContext.Current != null");
            return WebOperationContext.Current.CreateTextResponse(res,
                "application/json; charset=utf-8", Encoding.UTF8);
        }
    }
}
