#region using directives

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

namespace UnilunchData
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable MemberCanBePrivate.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassWithVirtualMembersNeverInherited.Global


    /// <summary>
    ///     Container object for Unilunch restaurant data.
    ///     Using the object to JSON serialization
    ///     (with JSON.net serializer) produces an object conforming
    ///     (but not limited to) spec at:
    ///     https://trac.cc.jyu.fi/projects/dotnet/wiki/moba/s2012/specs
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

            weekDays = new Dictionary<DayOfWeek, int>
                {
                {DayOfWeek.Monday, 0},
                {DayOfWeek.Tuesday, 1},
                {DayOfWeek.Wednesday, 2},
                {DayOfWeek.Thursday, 3},
                {DayOfWeek.Friday, 4},
                {DayOfWeek.Saturday, 5},
                {DayOfWeek.Sunday, 6}
            };
        }

        [JsonIgnore]
        public int RestaurantDetailId { get; set; }

        [NotMapped]
        public string id
        {
            get { return RestaurantDetailId.ToString(CultureInfo.InvariantCulture); }
        }

        public string name { get; set; }
        public string company { get; set; }
        public Location location { get; set; }
        public Address address { get; set; }
        public Contact contact { get; set; }
        public List<MenuDate> dates { get; private set; }
        public string category { get; set; }

        [JsonIgnore]
        [NotMapped]
        public static Dictionary<DayOfWeek, int> weekDays;
    }

    [ComplexType]
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

    [ComplexType]
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

    [ComplexType]
    public class OpenHours
    {

        [JsonIgnore]
        public DateTime? RealStart { get; set; }
        [JsonIgnore]
        public DateTime? RealEnd { get; set; }

        [NotMapped]
        public string start_time
        {
            get
            {
                if (RealStart == null) return "";
                return RealStart.Value.ToString("HH:mm");
            }
        }

        [NotMapped]
        public string end_time
        {
            get
            {
                if (RealEnd == null) return "";
                return RealEnd.Value.ToString("HH:mm");
            }
        }
    }

    [ComplexType]
    public class LunchHours
    {
        [JsonIgnore]
        public DateTime? RealStart { get; set; }
        [JsonIgnore]
        public DateTime? RealEnd { get; set; }
        [NotMapped]
        public string start_time
        {
            get
            {
                if (RealStart == null) return "";
                return RealStart.Value.ToString("HH:mm");
            }
        }

        [NotMapped]
        public string end_time
        {
            get
            {
                if (RealEnd == null) return "";
                return RealEnd.Value.ToString("HH:mm");
            }
        }
    }

    public class RestaurantMenuItem
    {
        [JsonIgnore]
        public int RestaurantMenuItemId { get; set; }

        [DataMember(Order = 1)]
        public string description { get; set; }

        [DataMember(Order = 2)]
        public string student_prize { get; set; }

        [DataMember(Order = 3)]
        public string staff_prize { get; set; }

        [DataMember(Order = 4)]
        public virtual List<string> diets { get; private set; }

        public RestaurantMenuItem()
        {
            diets = new List<string>();
            student_prize = "";
            staff_prize = "";
        }

        [JsonIgnore]
        public int MenuDateId { get; set; }
        [JsonIgnore]
        public virtual MenuDate MenuDate { get; set; }
    }

    public class MenuDate
    {
        [JsonIgnore]
        public int MenuDateId { get; set; }
        [JsonIgnore]
        public DateTime RealDate { get; set; }
        public MenuDate()
        {
            open_hours = new OpenHours();
            lunch_hours = new LunchHours();
            foods = new List<RestaurantMenuItem>();
        }

        [NotMapped]
        public string date
        {
            get { return RealDate.ToString("dd.MM.yyyy"); }
        }

        public OpenHours open_hours { get; set; }
        public LunchHours lunch_hours { get; set; }
        public virtual IList<RestaurantMenuItem> foods { get; private set; }

        public void SetRealDate(DateTime value)
        {
            RealDate = value;
        }

        [JsonIgnore]
        public int RestaurantDetailId { get; set; }

        [JsonIgnore]
        public virtual RestaurantDetail RestaurantDetail { get; set; }
    }

    [ComplexType]
    public class Location
    {
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class UnilunchContext : DbContext
    {
        public DbSet<RestaurantDetail> Restaurants { get; set; }
        public DbSet<RestaurantMenuItem> Menus { get; set; }
        public DbSet<MenuDate> MenuDates { get; set; }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore UnusedAutoPropertyAccessor.Global
    // ReSharper restore MemberCanBePrivate.Global
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassWithVirtualMembersNeverInherited.Global
}