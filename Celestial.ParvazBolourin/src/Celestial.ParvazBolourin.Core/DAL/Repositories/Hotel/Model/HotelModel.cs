using System.ComponentModel.DataAnnotations;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel.Model;

public sealed class HotelServiceLocalizedProperty
{
    [Required] public int LanguageId { get; set; }

    [Required] public string ShortText { get; set; } = string.Empty;

    [Required] public string MainText { get; set; } = string.Empty;

    [Required] public string Address { get; set; } = string.Empty;
}

public class GetAllHotelsServiceRes
{
    public int HotelId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string HotelCountry { get; set; } = string.Empty;
    public int HotelScore { get; set; }
    public string BookingUrl { get; set; } = string.Empty;
    public string HotelReception { get; set; } = string.Empty;
    public string? ShortText { get; set; }
    public string ImagePath { get; set; } = string.Empty;
}