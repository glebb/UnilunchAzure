using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Runtime.Serialization;

namespace UnilunchData
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable MemberCanBePrivate.Global
    // ReSharper disable UnusedMember.Global

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
        
        public int RestaurantDetailId { get; set; }

        public bool ShouldSerializeRestaurantDetailId()
        {
            return false;
        }

        [NotMapped]
        public string id { get { return RestaurantDetailId.ToString(CultureInfo.InvariantCulture); } }
        public string name { get; set; }
        public string company { get; set; }
        public Location location { get; set; }
        public Address address { get; set; }
        public Contact contact { get; set; }
        public virtual List<MenuDate> dates { get; private set; }
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
        public int AddressId { get; set; }
        public bool ShouldSerializeAddressDetail()
        {
            return false;
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

        public int ContactId { get; set; }
        public bool ShouldSerializeContactId()
        {
            return false;
        }


        public string phone_number { get; set; }
        public string email { get; set; }
        public string website { get; set; }
    }

    public class OpenHours
    {
        public DateTime? RealStart { get; set; }
        public bool ShouldSerializeRealStart()
        {
            return false;
        }

        public DateTime? RealEnd { get; set; }
        public bool ShouldSerializeRealEnd()
        {
            return false;
        }


        [NotMapped]
        public string start_time
        {
            get
            {
                if (RealStart == null) return "";
                return RealStart.Value.ToString("hh:mm");
            }
        }
        [NotMapped]
        public string end_time
        {
            get
            {
                if (RealEnd == null) return "";
                return RealEnd.Value.ToString("hh:mm");
            }
        }

        public int OpenHoursId { get; set; }
        public bool ShouldSerializeOpenHoursId()
        {
            return false;
        }

    }

    public class LunchHours
    {

        public int LunchHoursId { get; set; }
        public bool ShouldSerializeLunchHoursId()
        {
            return false;
        }

        public DateTime? RealStart { get; set; }
        public bool ShouldSerializeRealStart()
        {
            return false;
        }

        public DateTime? RealEnd { get; set; }
        public bool ShouldSerializeRealEnd()
        {
            return false;
        }


        [NotMapped]
        public string start_time
        {
            get
            {
                if (RealStart == null) return "";
                return RealStart.Value.ToString("hh:mm");
            }
        }
        [NotMapped]
        public string end_time
        {
            get
            {
                if (RealEnd == null) return "";
                return RealEnd.Value.ToString("hh:mm");
            }
        }
    }

// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
    public class RestaurantMenuItem
// ReSharper restore ClassWithVirtualMembersNeverInherited.Global
    {
        public int RestaurantMenuItemId { get; set; }
        public bool ShouldSerializeRestaurantMenuItemId()
        {
            return false;
        }

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

        public int MenuDateId { get; set; }
        public bool ShouldSerializeMenuDateId()
        {
            return false;
        }

        public virtual MenuDate MenuDate { get; set; }
        public bool ShouldSerializeMenuDate()
        {
            return false;
        }
    }

    public class MenuDate
    {

        public int MenuDateId { get; set; }
        public bool ShouldSerializeMenuDateId()
        {
            return false;
        }

        public DateTime RealDate { get; set; }
        public bool ShouldSerializeRealDate()
        {
            return false;
        }
        
        public MenuDate()
        {
            open_hours = new OpenHours();
            lunch_hours = new LunchHours();
            foods = new List<RestaurantMenuItem>();
        }

        [NotMapped]
        public string date {
            get { return RealDate.ToString("dd.MM.yyyy"); }
        }

        public OpenHours open_hours { get; set; }
        public LunchHours lunch_hours { get; set; }
        public virtual IList<RestaurantMenuItem> foods { get; private set; }

        public void SetRealDate(DateTime value)
        {
            RealDate = value;
        }
    }

    public class Location
    {
        public int LocationId { get; set; }
        public bool ShouldSerializeLocationlId()
        {
            return false;
        }

        
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


}