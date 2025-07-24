using System.ComponentModel.DataAnnotations;
using Celestial.Agency.Core.Attributes;

namespace Celestial.Agency.Core.DAL.Repositories.Hotel.Model;

public sealed class HotelServiceLocalizedProperty
{
    [Required] public string Language { get; set; } = string.Empty;

    public string ShortText { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    [MaxLength(255)] public string MetaTitle { get; set; } = string.Empty;

    [MaxLength(500)] public string MetaDescription { get; set; } = string.Empty;

    [MaxLength(1000)] public string MetaKeywords { get; set; } = string.Empty;

    [Slug] public string Slug { get; set; } = string.Empty;
}

public class GetAllHotelsServiceRes
{
    public long HotelId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string HotelCountry { get; set; } = string.Empty;
    public int HotelScore { get; set; }
    public string BookingUrl { get; set; } = string.Empty;
    public string HotelReception { get; set; } = string.Empty;
    public string? ShortText { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public string? Slug { get; set; } = string.Empty;
}