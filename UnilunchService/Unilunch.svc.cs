using Newtonsoft.Json;
using System;
using System.IO;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using UnilunchData;
namespace UnilunchService
{

    public class Unilunch : IUnilunchService
    {
        static DateTime _timestamp;
        static Sonaatti _sonaatti;

        public Unilunch()
        {
        }
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
            container.restaurant.AddRange(_sonaatti.Restaurants);
            var res = JsonConvert.SerializeObject(container);
            return WebOperationContext.Current.CreateTextResponse(res,
                "application/json; charset=utf-8", Encoding.UTF8);
        }
    }
}
