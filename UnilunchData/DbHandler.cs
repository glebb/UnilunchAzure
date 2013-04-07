using System.Linq;

namespace UnilunchData
{
    public static class DbHandler
    {
        public static void SaveToDb(Sonaatti sonaatti, UnilunchContext context)
        {
            foreach (var r in sonaatti.Restaurants)
            {
                if (!RestaurantExists(context, r))
                {
                    context.Restaurants.Add(r);
                }
                else
                {
                    var r1 = r;
                    var res = context.Restaurants.Single(temp => temp.name == r1.name);
                    foreach (var date in r.dates.Where(date => !context.MenuDates.Any(e =>
                                                                                      e.RealDate != date.RealDate &&
                                                                                      e.RestaurantDetailId ==
                                                                                      res.RestaurantDetailId)))
                    {
                        context.MenuDates.Add(date);
                    }
                }
            }
            context.SaveChanges();
        }

        private static bool RestaurantExists(UnilunchContext context, RestaurantDetail r)
        {
            return context.Restaurants.Any(e => e.name == r.name);
        }
    }
}