#region using directives

using System;
using System.Globalization;

#endregion

namespace UnilunchService
{
    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public static class WebInterfaceParser
    {
        public static DateRange ResolveDateRange(string value)
        {
            DateTime userDate;
            DateTime userDate2;

            if (String.IsNullOrEmpty(value))
            {
                SetDefaultDateValues(out userDate, out userDate2);
            }

            else if (value.Contains("-"))
            {
                HandleDateRange(value, out userDate, out userDate2);
            }
            else if (value.Contains("plus"))
            {
                HandlePlusDate(value, out userDate, out userDate2);
            }
            else
            {
                HandleSingleDate(value, out userDate, out userDate2);
            }
            return new DateRange
                {
                    Start = userDate,
                    End = userDate2
                };
        }

        private static void HandleSingleDate(string value, out DateTime userDate, out DateTime userDate2)
        {
            if (!TryParseExactDate(value, out userDate))
            {
                SetDefaultDateValues(out userDate, out userDate2);
            }
            else
            {
                userDate2 = userDate.AddDays(1);
            }
        }

        private static void HandlePlusDate(string value, out DateTime userDate, out DateTime userDate2)
        {
            if (!TryParseExactDate(value.Split(new[] {"plus"}, StringSplitOptions.None)[0], out userDate))
            {
                SetDefaultDateValues(out userDate, out userDate2);
            }
            else
            {
                try
                {
                    var days = Int32.Parse(value.Split(new[] {"plus"}, StringSplitOptions.None)[1]);
                    userDate2 = userDate.AddDays(1 + days);
                }
                catch (FormatException)
                {
                    userDate2 = userDate.AddDays(1);
                }
            }
        }

        private static void HandleDateRange(string value, out DateTime userDate, out DateTime userDate2)
        {
            if (
                !(TryParseExactDate(value.Split('-')[0], out userDate) &&
                  TryParseExactDate(value.Split('-')[1], out userDate2)))
            {
                SetDefaultDateValues(out userDate, out userDate2);
            }
        }

        private static void SetDefaultDateValues(out DateTime userDate, out DateTime userDate2)
        {
            userDate = DateTime.Today;
            userDate2 = DateTime.Today.AddDays(1);
        }

        private static bool TryParseExactDate(string value, out DateTime userDate)
        {
            return DateTime.TryParseExact(value, "ddMMyyyy", null, DateTimeStyles.None, out userDate);
        }
    }
}