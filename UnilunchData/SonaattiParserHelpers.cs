using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UnilunchData
{
    public static class SonaattiParserHelpers
    {
        public static List<MenuDate> createMenu(CQ dom)
        {
            var menus = new List<MenuDate>();
            handleFirstDay(menus, dom);
            var downconts = dom.Select("#lista > .pari, .odd").Select(".downcont");
            foreach (var singleDayTexts in downconts)
            {
                var date = createSingleDayMenu(singleDayTexts);
                menus.Add(date);
            }

            return menus;
        }

        private static void handleFirstDay(List<MenuDate> menus, CQ dom)
        {
            try
            {
                var date = dom.Select("#lista > .paivanlounas > span.paiva").Text().Split(' ').Last();
                var menuDate = new MenuDate();
                menuDate.SetRealDate(Utils.ConstructDateFromSonaattiDate(date));
                var foods = dom.Select("#lista > .listapaikka > .ruuat p");
                foreach (var food in foods)
                {
                    if (!String.IsNullOrWhiteSpace(food.InnerText))
                    {
                        var menuItem = new RestaurantMenuItem();
                        menuItem.description = cleanDescriptionFromPrice(food.InnerText);
                        menuDate.foods.Add(menuItem);
                    }

                }
                var prices = dom.Select("#lista > .listapaikka > .hinnat").Html().Split(new string[] { "<br>" }, StringSplitOptions.None).Where(s => !String.IsNullOrWhiteSpace(s)).ToList();

                if (prices.Count() == menuDate.foods.Count())
                {
                    for (int i = 0; i < menuDate.foods.Count(); i++)
                    {
                        setPrices(prices[i], menuDate.foods[i]);
                    }
                }
                menus.Add(menuDate);
            }
            catch (ArgumentException)
            { 
                //TODO: inform about exception
            }
        }

        public static MenuDate createSingleDayMenu(IDomObject singleDayTexts)
        {
            var date = new MenuDate();
            date.SetRealDate(Utils.ConstructDateFromSonaattiDate(singleDayTexts.Cq().Find("span.paiva").Text()));

            var rawMenuTextAllItems = singleDayTexts.Cq().Find("p").Text().ToString().Split(new string[] { ")," }, StringSplitOptions.None);
            foreach (var rawMenuItem in rawMenuTextAllItems)
            {
                var menuItem = new RestaurantMenuItem();
                menuItem.description = cleanDescriptionFromPrice(rawMenuItem);
                setPrices(rawMenuItem, menuItem);
                date.foods.Add(menuItem);
            }
            return date;
        }

        private static void setPrices(string rawMenuItem, RestaurantMenuItem menuItem)
        {
            var pattern = "[0-9]+,[0-9]{1,2}";
            menuItem.student_prize = Regex.Match(rawMenuItem, pattern).ToString();
            menuItem.staff_prize = Regex.Match(rawMenuItem, pattern, RegexOptions.RightToLeft).ToString();
        }

        private static string cleanDescriptionFromPrice(string description)
        {
            var temp = description.Split('#').First().Trim();
            var pattern = @"\([0-9]+,[0-9]{1,2}.*$";
            return Regex.Replace(temp, pattern, "").Trim();
        }
    }
}
