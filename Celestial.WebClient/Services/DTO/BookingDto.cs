using System.ComponentModel.DataAnnotations;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class AddTourBookingReqDto
{
    public string Language { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Validation_Length")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Validation_Phone")]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Validation_Email")]
    public string Email { get; set; } = string.Empty;

    [RegularExpression(@"^\d{10}$", ErrorMessage = "Validation_NationalCode")]
    public string NationalCode { get; set; } = string.Empty;

    [Range(1, 50, ErrorMessage = "Validation_Range")]
    public int PassengerCount { get; set; } = 1;

    [StringLength(1000, ErrorMessage = "Validation_Length")]
    public string Note { get; set; } = string.Empty;
}

public sealed class AddTourBookingResDto : ServiceResContextBase
{
    public long Id { get; set; }
    public string BookingCode { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public BookingStatus BookingStatus { get; set; }
}

public sealed class GetTourBookingByCodeReqDto
{
    public string Language { get; set; } = string.Empty;
    public string BookingCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

public sealed class GetTourBookingByCodeResDto : ServiceResContextBase
{
    public TourBookingItemDto Item { get; set; } = new();
}

public sealed class TourBookingItemDto
{
    public long Id { get; set; }
    public string BookingCode { get; set; } = string.Empty;
    public long TourId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public int PassengerCount { get; set; }
    public string Note { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public BookingStatus BookingStatus { get; set; }
    public DateTime CreatedAt { get; set; }
}
