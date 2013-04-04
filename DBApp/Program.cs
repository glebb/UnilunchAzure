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

            Database.SetInitializer(new DropCreateDatabaseAlways<UnilunchContext>());
            using (var context = new UnilunchContext())
            {
                foreach (var name in sonaatti.Restaurants)
                {
                    context.Restaurants.Add(name);
                }
                context.SaveChanges();

                var query = from r in context.Restaurants orderby r.name select r;
                Console.WriteLine("hep");
                foreach (var item in query)
                {
                    Console.WriteLine(item.name);
                }

            }



        }
    }
}
