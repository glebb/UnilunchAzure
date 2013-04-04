using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnilunchData;

namespace DBApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new DataSource();
            var sonaatti = new Sonaatti(source);

            //Database.SetInitializer(new DropCreateDatabaseAlways<UnilunchContext>());
            using (var context = new UnilunchContext())
            {
                foreach (var r in sonaatti.Restaurants)
                {
                    if (!context.Restaurants.Any(e => e.name == r.name))
                    {
                        context.Restaurants.Add(r);
                    }
                    else
                    {
                        var id = from res in context.Restaurants where res.name == r.name select res.RestaurantDetailId;
                        foreach (var date in r.dates)
                        {
                            if (!context.MenuDates.Any(e => e.RealDate == date.RealDate))
                            {
                                context.MenuDates.Add(date);
                            }
                        }
                    }
                }

                
                //foreach (var name in sonaatti.Restaurants)
                //{
                //    context.Restaurants.Add(name);
                //}
                context.SaveChanges();

                var query = from r in context.Restaurants orderby r.name select r;
                Console.WriteLine("hep");
                foreach (var item in query)
                {
                    Console.WriteLine(item.name);
                    Console.WriteLine(item.dates.Count.ToString());

                }

            }



        }
    }
}
