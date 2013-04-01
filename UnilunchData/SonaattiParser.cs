﻿using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace UnilunchData
{
    public static class SonaattiParser
    {
        public static IList<MenuDate> CreateMenu(CQ dom)
        {
            if (dom == null)
            {
                throw new ArgumentNullException("dom");
            }
            
            var menus = new List<MenuDate>();
            handleFirstDay(menus, dom);
            
            var allNormalDays = dom.Select("#lista > .pari, .odd").Select(".downcont");
            foreach (var singleDayTexts in allNormalDays)
            {
                var date = createSingleDayMenu(singleDayTexts);
                menus.Add(date);
            }

            return menus;
        }

        private static void handleFirstDay(List<MenuDate> menus, CQ dom)
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

            foodsForFirstDate(dom, menuDate);
            pricesForFirstDate(dom, menuDate);

            menus.Add(menuDate);
        }

        private static void pricesForFirstDate(CQ dom, MenuDate menuDate)
        {
            var prices = dom.Select("#lista > .listapaikka > .hinnat").Html().Split(new string[] { "<br>" }, StringSplitOptions.None).Where(s => !String.IsNullOrWhiteSpace(s)).ToList();
            if (prices.Count() == menuDate.foods.Count())
            {
                for (int i = 0; i < menuDate.foods.Count(); i++)
                {
                    setPrices(prices[i], menuDate.foods[i]);
                }
            }
        }

        private static void foodsForFirstDate(CQ dom, MenuDate menuDate)
        {
            var foods = dom.Select("#lista > .listapaikka > .ruuat p");
            foreach (var food in foods)
            {
                if (!String.IsNullOrWhiteSpace(food.InnerText))
                {
                    var menuItem = new RestaurantMenuItem();
                    menuItem.description = cleanDescriptionFromPrice(food.InnerText);
                    menuItem.diets.AddRange(diets(food.InnerText));

                    menuDate.foods.Add(menuItem);
                }
            }
        }

        private static MenuDate createSingleDayMenu(IDomObject singleDayTexts)
        {
            var date = new MenuDate();
            date.SetRealDate(ConstructDateFromSonaattiDate(singleDayTexts.Cq().Find("span.paiva").Text()));

            var rawMenuTextAllItems = singleDayTexts.Cq().Find("p").Text().ToString().Split(new string[] { ")," }, StringSplitOptions.None);
            foreach (var rawMenuItem in rawMenuTextAllItems)
            {
                var menuItem = new RestaurantMenuItem();
                menuItem.description = cleanDescriptionFromPrice(rawMenuItem);
                menuItem.diets.AddRange(diets(rawMenuItem));
                setPrices(rawMenuItem, menuItem);
                date.foods.Add(menuItem);
            }
            return date;
        }

        public static IList<string> diets(string rawMenuItem)
        {
            var temp = WebUtility.HtmlDecode(rawMenuItem);
            var res = new List<string>();
            var pattern = @"#[^\s^\d^#]+[\b]?";
            var matches = Regex.Matches(temp, pattern);
            foreach (var match in matches)
            {
                res.Add(match.ToString().Replace("#", "").Trim());
            }

            return res;
        }

        private static void setPrices(string rawMenuItem, RestaurantMenuItem menuItem)
        {
            var temp = WebUtility.HtmlDecode(rawMenuItem);
            var pattern = "[0-9]+,[0-9]{1,2}";
            menuItem.student_prize = Regex.Match(temp, pattern).ToString();
            menuItem.staff_prize = Regex.Match(temp, pattern, RegexOptions.RightToLeft).ToString();
        }

        public static string cleanDescriptionFromPrice(string description)
        {
            var temp = WebUtility.HtmlDecode(description);
            temp = temp.Split(new string[] { "#" }, StringSplitOptions.None).First().Trim();
            var pattern = @"\([0-9]+,[0-9]{1,2}.*$";
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
            if (!Int32.TryParse(dates[0], out day) || !Int32.TryParse(dates[1], out month) || !Int32.TryParse(dates[2], out year))
            {
                throw new ArgumentException("Parameter does not contain integer values", temp);
            }

            return new DateTime(year, month, day);

        }

    }
}
