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

        public Message JsonData()
        {
            var container = new RestaurantJsonContainer();
            var sonaatti = new Sonaatti(new DataSource());
            container.restaurant = sonaatti.Restaurants;
            var res = JsonConvert.SerializeObject(container);
            return WebOperationContext.Current.CreateTextResponse(res,
                "application/json; charset=utf-8", Encoding.UTF8);
        }
    }
}
