using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnilunchData
{
    public class Sonaatti
    {
        private string mainPage;

        public Sonaatti(string mainPage)
        {
            this.mainPage = mainPage;
        }

        public DateTime TodayDate()
        {
            var dom = CQ.Create(mainPage);
            var temp = dom.Select("#lista .paivanlounas span.paiva").Text().Split(' ').Last();
            return Utils.ConstructDateFromSonaattiDate(temp);
        }
    }
}
