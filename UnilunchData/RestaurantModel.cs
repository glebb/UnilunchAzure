﻿using System;
using System.Collections.Generic;

namespace UnilunchData
{

    /// <summary>
    /// Container object for Unilunch restaurant data.
    /// Using the object to JSON serialization 
    /// (with DataContractJsonSerializer for example) produces an object specified in
    /// https://trac.cc.jyu.fi/projects/dotnet/wiki/moba/s2012/specs
    /// </summary>
    public class RestaurantJsonContainer
    {
        public RestaurantJsonContainer()
        {
            restaurant = new List<RestaurantDetail>();
        }
        public List<RestaurantDetail> restaurant { get; set; }
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

    public class Address
    {
        public string street_address { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
    }

    public class Contact
    {
        public string website { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
    }

    public class OpenHours
    {
        public string start_time { get; set; }
        public string end_time { get; set; }

    }

    public class LunchHours
    {
        public string start_time { get; set; }
        public string end_time { get; set; }
    }

    public class RestaurantMenuItem
    {
        public RestaurantMenuItem()
        {
            diets = new List<string>();
        }
        public string description { get; set; }
        public string student_price { get; set; }
        public string staff_price { get; set; }
        public List<string> diets;
    
    }

    public class MenuDate
    {
        public MenuDate()
        {
            open_hours = new OpenHours();
            lunch_hours = new LunchHours();
            foods = new List<RestaurantMenuItem>();
        }
        public string date { get; set; }
        public OpenHours open_hours { get; set; }
        public LunchHours lunch_hours { get; set; }
        public List<RestaurantMenuItem> foods { get; set; }
    }

    public class Location
    {
        public string longitude { get; set; }
        public string latitude { get; set; }
    }
}