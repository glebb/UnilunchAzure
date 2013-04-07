#region using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using CsQuery;

#endregion

namespace UnilunchData
{
    public static class SonaattiParser
    {
        public static IEnumerable<MenuDate> CreateMenu(CQ dom)
        {
            if (dom == null)
            {
                throw new ArgumentNullException("dom");
            }

            var menus = new List<MenuDate>();
            HandleFirstDay(menus, dom);

            var allNormalDays = dom.Select("#lista > .pari, .odd").Select(".downcont");
            menus.AddRange(allNormalDays.Select(CreateSingleDayMenu));

            return menus;
        }

        private static void HandleFirstDay(List<MenuDate> menus, CQ dom)
        {
            var menuDate = new MenuDate();
            try
            {
                var date = dom.Select("#lista > .paivanlounas > span.paiva").Text().Split(' ').Last();
                menuDate.SetRealDate(ConstructDateFromSonaattiDate(WebUtility.HtmlDecode(date)));
            }
            catch (ArgumentException)
            {
                //TODO: inform about exception
                return;
            }

            FoodsForFirstDate(dom, menuDate);
            PricesForFirstDate(dom, menuDate);

            menus.Add(menuDate);
        }

        private static void PricesForFirstDate(CQ dom, MenuDate menuDate)
        {
            var prices =
                dom.Select("#lista > .listapaikka > .hinnat")
                   .Html()
                   .Split(new[] {"<br>"}, StringSplitOptions.None)
                   .Where(s => !String.IsNullOrWhiteSpace(s))
                   .ToList();
            if (prices.Count() == menuDate.foods.Count())
            {
                for (var i = 0; i < menuDate.foods.Count(); i++)
                {
                    SetPrices(prices[i], menuDate.foods[i]);
                }
            }
        }

        private static void FoodsForFirstDate(CQ dom, MenuDate menuDate)
        {
            var foods = dom.Select("#lista > .listapaikka > .ruuat p");
            foreach (var food in foods)
            {
                if (!String.IsNullOrWhiteSpace(food.InnerText))
                {
                    var menuItem = new RestaurantMenuItem {description = CleanDescriptionFromPrice(food.InnerText)};
                    menuItem.diets.AddRange(Diets(food.InnerText));

                    menuDate.foods.Add(menuItem);
                }
            }
        }

        private static MenuDate CreateSingleDayMenu(IDomObject singleDayTexts)
        {
            var date = new MenuDate();
            date.SetRealDate(ConstructDateFromSonaattiDate(singleDayTexts.Cq().Find("span.paiva").Text()));

            var rawMenuTextAllItems = singleDayTexts.Cq().Find("p").Text().Split(new[] {"),"}, StringSplitOptions.None);
            foreach (var rawMenuItem in rawMenuTextAllItems)
            {
                var menuItem = new RestaurantMenuItem {description = CleanDescriptionFromPrice(rawMenuItem)};
                menuItem.diets.AddRange(Diets(rawMenuItem));
                SetPrices(rawMenuItem, menuItem);
                date.foods.Add(menuItem);
            }
            return date;
        }

        public static IList<string> Diets(string rawMenuItem)
        {
            var temp = WebUtility.HtmlDecode(rawMenuItem);
            const string pattern = @"#[^\s^\d^#]+[\b]?";
            var matches = Regex.Matches(temp, pattern);

            return (from object match in matches select match.ToString().Replace("#", "").Trim()).ToList();
        }

        public static void SetPrices(string rawMenuItem, RestaurantMenuItem menuItem)
        {
            var temp = WebUtility.HtmlDecode(rawMenuItem);
            const string pattern = "[0-9]+,[0-9]{1,2}";
            menuItem.student_prize = Regex.Match(temp, pattern).ToString();
            menuItem.staff_prize = Regex.Match(temp, pattern, RegexOptions.RightToLeft).ToString();
        }

        public static string CleanDescriptionFromPrice(string description)
        {
            var temp = WebUtility.HtmlDecode(description);
            temp = temp.Split(new[] {"#"}, StringSplitOptions.None).First().Trim();
            const string pattern = @"\([0-9]+,[0-9]{1,2}.*$";
            return Regex.Replace(temp, pattern, "").Replace("()", "").Trim();
        }


        public static DateTime ConstructDateFromSonaattiDate(string value)
        {
            if (value == null)
            {
                throw new ArgumentException("Null value received");
            }
            var temp = WebUtility.HtmlDecode(value);

            var dates = temp.Split('.');
            if (dates.Length != 3)
            {
                throw new ArgumentException("Format of parameters is incorrect", temp);
            }

            int day, month, year;
            if (!Int32.TryParse(dates[0], out day) || !Int32.TryParse(dates[1], out month) ||
                !Int32.TryParse(dates[2], out year))
            {
                throw new ArgumentException("Parameter does not contain integer values", temp);
            }

            return new DateTime(year, month, day);
        }
    }
}