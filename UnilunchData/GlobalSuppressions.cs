#region using directives

using System.Diagnostics.CodeAnalysis;

#endregion

[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "restaurant",
        Scope = "member", Target = "UnilunchData.RestaurantJsonContainer.#restaurant")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unilunch")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unilunch",
        Scope = "namespace", Target = "UnilunchData")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "id", Scope = "member",
        Target = "UnilunchData.RestaurantDetail.#id")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "name", Scope = "member"
        , Target = "UnilunchData.RestaurantDetail.#name")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "company",
        Scope = "member", Target = "UnilunchData.RestaurantDetail.#company")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "location",
        Scope = "member", Target = "UnilunchData.RestaurantDetail.#location")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "address",
        Scope = "member", Target = "UnilunchData.RestaurantDetail.#address")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "contact",
        Scope = "member", Target = "UnilunchData.RestaurantDetail.#contact")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "dates",
        Scope = "member", Target = "UnilunchData.RestaurantDetail.#dates")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "street",
        Scope = "member", Target = "UnilunchData.Address.#street_address")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "UnilunchData.Address.#street_address")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "address",
        Scope = "member", Target = "UnilunchData.Address.#street_address")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "UnilunchData.Address.#postal_code")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "postal",
        Scope = "member", Target = "UnilunchData.Address.#postal_code")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "code", Scope = "member"
        , Target = "UnilunchData.Address.#postal_code")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "city", Scope = "member"
        , Target = "UnilunchData.Address.#city")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "phone",
        Scope = "member", Target = "UnilunchData.Contact.#phone_number")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "UnilunchData.Contact.#phone_number")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "number",
        Scope = "member", Target = "UnilunchData.Contact.#phone_number")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "email",
        Scope = "member", Target = "UnilunchData.Contact.#email")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "website",
        Scope = "member", Target = "UnilunchData.Contact.#website")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "UnilunchData.OpenHours.#start_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "time", Scope = "member"
        , Target = "UnilunchData.OpenHours.#start_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "start",
        Scope = "member", Target = "UnilunchData.OpenHours.#start_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "time", Scope = "member"
        , Target = "UnilunchData.OpenHours.#end_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "UnilunchData.OpenHours.#end_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "end", Scope = "member",
        Target = "UnilunchData.OpenHours.#end_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "time", Scope = "member"
        , Target = "UnilunchData.LunchHours.#start_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "UnilunchData.LunchHours.#start_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "start",
        Scope = "member", Target = "UnilunchData.LunchHours.#start_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "time", Scope = "member"
        , Target = "UnilunchData.LunchHours.#end_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "UnilunchData.LunchHours.#end_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "end", Scope = "member",
        Target = "UnilunchData.LunchHours.#end_time")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "description",
        Scope = "member", Target = "UnilunchData.RestaurantMenuItem.#description")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "student",
        Scope = "member", Target = "UnilunchData.RestaurantMenuItem.#student_prize")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "UnilunchData.RestaurantMenuItem.#student_prize")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "prize",
        Scope = "member", Target = "UnilunchData.RestaurantMenuItem.#student_prize")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "staff",
        Scope = "member", Target = "UnilunchData.RestaurantMenuItem.#staff_prize")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "UnilunchData.RestaurantMenuItem.#staff_prize")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "prize",
        Scope = "member", Target = "UnilunchData.RestaurantMenuItem.#staff_prize")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "diets",
        Scope = "member", Target = "UnilunchData.RestaurantMenuItem.#diets")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "date", Scope = "member"
        , Target = "UnilunchData.MenuDate.#date")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "UnilunchData.MenuDate.#open_hours")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "open", Scope = "member"
        , Target = "UnilunchData.MenuDate.#open_hours")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "hours",
        Scope = "member", Target = "UnilunchData.MenuDate.#open_hours")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "lunch",
        Scope = "member", Target = "UnilunchData.MenuDate.#lunch_hours")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "UnilunchData.MenuDate.#lunch_hours")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "hours",
        Scope = "member", Target = "UnilunchData.MenuDate.#lunch_hours")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "foods",
        Scope = "member", Target = "UnilunchData.MenuDate.#foods")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "longitude",
        Scope = "member", Target = "UnilunchData.Location.#longitude")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "latitude",
        Scope = "member", Target = "UnilunchData.Location.#latitude")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sonaatti",
        Scope = "type", Target = "UnilunchData.Sonaatti")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Utils",
        Scope = "type", Target = "UnilunchData.Utils")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sonaatti",
        Scope = "member", Target = "UnilunchData.Utils.#ConstructDateFromSonaattiDate(System.String)")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member",
        Target = "UnilunchData.RestaurantDetail.#dates")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "category",
        Scope = "member", Target = "UnilunchData.RestaurantDetail.#category")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sonaatti",
        Scope = "type", Target = "UnilunchData.SonaattiParser")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member",
        Target = "UnilunchData.RestaurantJsonContainer.#restaurant")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sonaatti",
        Scope = "member", Target = "UnilunchData.SonaattiParser.#ConstructDateFromSonaattiDate(System.String)")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member",
        Target = "UnilunchData.RestaurantMenuItem.#diets")]