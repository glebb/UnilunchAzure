#region using directives

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

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

        public static IEnumerable<RestaurantDetail> RestaurantsQuery(string name, string id, UnilunchContext context,
                                                                     DateRange dateRange)
        {
            var temp = context.MenuDates.Where(d => d.RealDate >= dateRange.Start && d.RealDate < dateRange.End)
                              .Select(d => d.RestaurantDetail).Distinct().Select(r => new
                                  {
                                      RestauraurantDetail = r,
                                      dates =
                                                                                          r.dates.Where(d =>
                                                                                                        d.RealDate >=
                                                                                                        dateRange.Start
                                                                                                        &&
                                                                                                        d.RealDate <
                                                                                                        dateRange.End)
                                  })
                              .ToList();
            if (!String.IsNullOrEmpty(name))
            {
                temp = temp.Where(r => r.RestauraurantDetail.name == name).ToList();
            }

            int tempId;
            if (Int32.TryParse(id, out tempId))
            {
                temp = temp.Where(r => r.RestauraurantDetail.RestaurantDetailId == tempId).ToList();
            }

            return temp.Select(a => a.RestauraurantDetail).ToList();
        }
    }
}