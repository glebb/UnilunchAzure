using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnilunchData
{
    public class Utils
    {

        public static DateTime ConstructDateFromSonaattiDate(string dateString)
        {
            if (dateString == null) 
            {
                throw new ArgumentException();
            }

            var dates = dateString.Split('.');
            if (dates.Length != 3)
            {
                throw new ArgumentException();
            }

            int day, month, year;
            if (!Int32.TryParse(dates[0], out day) || !Int32.TryParse(dates[1], out month) || !Int32.TryParse(dates[2], out year))
            {
                throw new ArgumentException();
            }

            return new DateTime(year, month, day);
            
        }
    }
}
