using System.Collections.Generic;
namespace UnilunchService
{
    public class UnilunchServiceImpl : IUnilunchServiceImpl
    {
        public RestaurantModel JsonData()
        {
            var r = new RestaurantModel();
            var detail = new RestaurantDetail();
            detail.address.city = "Jyväskylä";
            detail.company = "Sonaatti";
            detail.contact.email = "blah@blah.com";
            detail.contact.phone = "+359 1233245345";
            detail.contact.website = "http://www.jyu.fi";
            detail.id = 1;
            detail.name = "Piato";
            r.restaurant.Add(detail);
            return r;
        }
    }
}
