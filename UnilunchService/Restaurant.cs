using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnilunchService
{
    public class Test
    {
        public string address { get; set; }
    }

    public class Restaurant
    {
        public List<Test> restaurant
        {
            get 
            { 
                var b = new Test {address = "Mattilanniemi xzcxzczxc..."};
                var l = new List<Test> {b};
                return l;
            }
        }
    }
}