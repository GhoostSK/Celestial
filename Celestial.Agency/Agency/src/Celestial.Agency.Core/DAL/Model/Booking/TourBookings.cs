using Celestial.Agency.Core.Enum.Booking;
using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Booking;

public sealed class TourBooking : DbBaseEntity<long>
{
    public string BookingCode { get; set; } = string.Empty;
    public long TourId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public int PassengerCount { get; set; } = 1;
    public string Note { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public BookingStatus BookingStatus { get; set; } = BookingStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
}
