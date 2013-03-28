namespace UnilunchService
{
    public class UnilunchServiceImpl : IUnilunchServiceImpl
    {
        public RestaurantModel JsonData()
        {
            var r = new RestaurantModel();
            var detail = new RestaurantDetail
                {
                    address = {city = "Jyväskylä"},
                    company = "Sonaatti",
                    contact = {email = "blah@blah.com", phone = "+359 1233245345", website = "http://www.jyu.fi"},
                    id = 1,
                    name = "Piato"
                };
            r.restaurant.Add(detail);
            return r;
        }
    }
}
