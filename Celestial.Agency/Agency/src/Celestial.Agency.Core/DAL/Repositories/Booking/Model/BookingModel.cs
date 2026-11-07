using Celestial.Agency.Core.Enum.Booking;

namespace Celestial.Agency.Core.DAL.Repositories.Booking.Model;

public sealed class GetTourBookingListReq
{
    public List<BookingStatus> BookingStatus { get; set; } = new();
    public long? TourId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string BookingCode { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
