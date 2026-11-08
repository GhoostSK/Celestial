namespace Celestial.WebClient.Services.DTO;

/// <summary>
///     آینه‌ی enum های سمت API. چون API مقادیر را به‌صورت رشته سریالایز می‌کند،
///     نام اعضا باید دقیقاً با بک‌اند یکسان باشد.
/// </summary>
public enum LocalKeyGroupType
{
    Hotel,
    Blog,
    BlogCategory,
    BlogTag,
    Tour,
    TourPlan,
    Visa,
    TourEquipment
}

public enum Continent
{
    None,
    Africa,
    Antarctica,
    Asia,
    Europe,
    NorthAmerica,
    SouthAmerica,
    Australia
}

public enum TourStatus
{
    Active = 1,
    Planning = 2,
    Completed = 3,
    RegistrationClosed = 4
}

public enum TourType
{
    None,
    NatureTour,
    CityTour,
    Historical,
    Sports,
    Educational,
    Adventure
}

public enum TourScope
{
    None,
    SingleCity,
    MultipleCities,
    MultipleCountries,
    SingleCountry
}

public enum TransportVehicleType
{
    Car = 0,
    Bus = 1,
    Plane = 2,
    Ship = 3
}

public enum HotelLocation
{
    None,
    CityCenter,
    Suburb,
    NearCity
}

public enum VisaIssuer
{
    Embassy,
    Consulate,
    VisaApplicationCenter,
    AuthorizedAgency,
    Ministry,
    PrivateAgency,
    SpecialProgram
}

public enum TourSortBy
{
    Newest,
    StartDate,
    PriceAsc,
    PriceDesc,
    ScoreDesc
}

public enum BookingStatus
{
    Pending = 1,
    Confirmed = 2,
    Cancelled = 3,
    Rejected = 4
}
