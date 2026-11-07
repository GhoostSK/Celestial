using System.ComponentModel.DataAnnotations;
using Celestial.Agency.Core.Attributes;
using Celestial.Agency.Core.Enum.Booking;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Models.Dto;

public sealed class AddTourBookingReqDto
{
    [FromRoute] public string Language { get; set; } = string.Empty;

    [Required] [Slug] public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "FullName is required.")]
    [StringLength(150, MinimumLength = 3)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "PhoneNumber is required.")]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress] public string Email { get; set; } = string.Empty;

    [RegularExpression(@"^\d{10}$", ErrorMessage = "National code must be exactly 10 digits.")]
    public string NationalCode { get; set; } = string.Empty;

    [Range(1, 50)] public int PassengerCount { get; set; } = 1;

    [StringLength(1000)] public string Note { get; set; } = string.Empty;
}

public sealed class GetTourBookingByCodeReqDto
{
    [Required] public string BookingCode { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}

public sealed class GetTourBookingListReqDto
{
    public List<BookingStatus> BookingStatus { get; set; } = new();
    public long? TourId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string BookingCode { get; set; } = string.Empty;
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public sealed class UpdateTourBookingStatusReqDto
{
    [Required] public long Id { get; set; }

    [Required] public BookingStatus BookingStatus { get; set; }
}
