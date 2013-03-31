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
            var downconts = dom.Select("#lista > .pari, .odd").Select(".downcont");
            foreach (var singleDayTexts in downconts)
            {
                var date = createSingleDayMenu(singleDayTexts);
                menus.Add(date);
            }

            return menus;
        }

        public static MenuDate createSingleDayMenu(IDomObject singleDayTexts)
        {
            var date = new MenuDate();
            date.SetRealDate(Utils.ConstructDateFromSonaattiDate(singleDayTexts.Cq().Find("span.paiva").Text()));

            var rawMenuTextAllItems = singleDayTexts.Cq().Find("p").Text().ToString().Split(new string[] { ")," }, StringSplitOptions.None);
            foreach (var rawMenuItem in rawMenuTextAllItems)
            {
                var menuItem = new RestaurantMenuItem();
                var description = rawMenuItem.Split('#').First().Trim();
                menuItem.description = cleanDescriptionFromPrice(description);
                var pattern = "[0-9]+,[0-9]{1,2}";
                menuItem.student_prize = Regex.Match(rawMenuItem, pattern).ToString();
                menuItem.staff_prize = Regex.Match(rawMenuItem, pattern, RegexOptions.RightToLeft).ToString();
                date.foods.Add(menuItem);
            }
            return date;
        }

        private static string cleanDescriptionFromPrice(string description)
        {
            var pattern = @"\([0-9]+,[0-9]{1,2}.*$";
            return Regex.Replace(description, pattern, "").Trim();
        }
    }
}
