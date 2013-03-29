using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnilunchData
{
    public static class Utils
    {

        public static DateTime ConstructDateFromSonaattiDate(string value)
        {
            if (value == null)
            {
                throw new ArgumentException("Null value received");
            }

            var dates = value.Split('.');
            if (dates.Length != 3)
            {
                throw new ArgumentException("Format of parameters is incorrect", value);
            }

            int day, month, year;
            if (!Int32.TryParse(dates[0], out day) || !Int32.TryParse(dates[1], out month) || !Int32.TryParse(dates[2], out year))
            {
                throw new ArgumentException("Parameter does not contain integer values", value);
            }

            return new DateTime(year, month, day);

        }
    }
}
