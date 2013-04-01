using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnilunchData
{

    /// <summary>
    /// Container object for Unilunch restaurant data.
    /// Using the object to JSON serialization 
    /// (with JavascriptSeralizer or JSON.net serializer) produces an object conforming
    /// (but not limited to) spec at:
    /// https://trac.cc.jyu.fi/projects/dotnet/wiki/moba/s2012/specs
    /// </summary>
    
    public class RestaurantJsonContainer
    {
        public RestaurantJsonContainer()
        {
            restaurant = new List<RestaurantDetail>();
        }
        public List<RestaurantDetail> restaurant { get; private set; }
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
        public string id { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public Location location { get; set; }
        public Address address { get; set; }
        public Contact contact { get; set; }
        public List<MenuDate> dates { get; private set; }
        public string category { get; set; }
    }

    public class Address
    {
        public Address()
        {
            street_address = "";
            city = "";
            postal_code = "";
        }
        public string street_address { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
    }

    public class Contact
    {
        public Contact()
        {
            phone_number = "";
            email = "";
        }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string website { get; set; }
    }

    public class OpenHours
    {
        public OpenHours()
        {
            start_time = "";
            end_time = "";
        }
        public string start_time { get; set; }
        public string end_time { get; set; }
    }

    public class LunchHours
    {
        public LunchHours()
        {
            start_time = "";
            end_time = "";
        }
        public string start_time { get; set; }
        public string end_time { get; set; }
    }

    public class RestaurantMenuItem
    {
        [DataMember(Order = 1)]
        public string description { get; set; }
        [DataMember(Order = 2)]
        public string student_prize { get; set; }
        [DataMember(Order = 3)]
        public string staff_prize { get; set; }
        [DataMember(Order = 4)]
        public List<string> diets { get; private set; }

        public RestaurantMenuItem()
        {
            diets = new List<string>();
            student_prize = "";
            staff_prize = "";
        }
    }

    public class MenuDate
    {
        
        private DateTime _realDate;
        
        public MenuDate()
        {
            open_hours = new OpenHours();
            lunch_hours = new LunchHours();
            foods = new List<RestaurantMenuItem>();
        }
        
        public string date {
            get { return _realDate.ToString("dd.MM.yyyy"); }
        }

        public OpenHours open_hours { get; set; }
        public LunchHours lunch_hours { get; set; }
        public IList<RestaurantMenuItem> foods { get; private set; }

        public void SetRealDate(DateTime value)
        {
            _realDate = value;
        }
    }

    public class Location
    {
        public string longitude { get; set; }
        public string latitude { get; set; }
    }
}