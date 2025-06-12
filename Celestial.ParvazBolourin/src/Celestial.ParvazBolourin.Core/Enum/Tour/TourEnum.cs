namespace Celestial.ParvazBolourin.Core.Enum.Tour;

public enum TransportVehicleType
{
    Car = 0,
    Bus = 1,
    Plane = 2,
    Ship = 3
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

public enum TransportType
{
    None,
    Combined,
    Ground,
    Air,
    Sea
}