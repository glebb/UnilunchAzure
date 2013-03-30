using System;
namespace UnilunchData
{
    interface IRestaurantPlugin
    {
        System.Collections.Generic.IList<RestaurantDetail> Restaurants { get; }
    }
}
