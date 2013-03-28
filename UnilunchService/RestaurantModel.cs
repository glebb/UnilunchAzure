using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnilunchService
{
    public class Address
    {
        public string street_address { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
    }

    public class Contact
    {
        public string website { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }

    public class OpenHours
    {
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }

    }

    public class LunchHours
    {
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
    }

    public class Price
    {
        public string student_price { get; set; }
        public string staff_price { get; set; }
    }

    public class Item
    {
        public Item()
        {
            price = new Price();
            diets = new List<string>();
        }
        public string title { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public Price price { get; set; }
        public List<string> diets;
    
    }

    public class MenuDate
    {
        public MenuDate()
        {
            open_hours = new OpenHours();
            lunch_hours = new LunchHours();
            items = new List<Item>();
        }
        public DateTime date { get; set; }
        public OpenHours open_hours { get; set; }
        public LunchHours lunch_hours { get; set; }
        public List<Item> items { get; set; }
    }

    public class Location
    {
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class RestaurantDetail
    {
        public RestaurantDetail()
        {
            location = new Location();
            address = new Address();
            contact = new Contact();
            dates = new List<MenuDate>();
        }
        
        public int id { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public Location location { get; set; }
        public Address address { get; set; }
        public Contact contact { get; set; }
        public List<MenuDate> dates { get; set; }
    }

    public class RestaurantModel
    {
        public RestaurantModel()
        {
            restaurant = new List<RestaurantDetail>();
        }
        public List<RestaurantDetail> restaurant { get; set; } 
    }
}