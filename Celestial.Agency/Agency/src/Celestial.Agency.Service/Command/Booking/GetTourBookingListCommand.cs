using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.Enum.Booking;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Booking;

public sealed class GetTourBookingListCommandReq : BaseEntity, IRequest<GetTourBookingListCommandRes>
{
    public List<BookingStatus> BookingStatus { get; set; } = new();
    public long? TourId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string BookingCode { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public sealed class GetTourBookingListCommandRes : ServiceResContextBase
{
    public List<TourBookingItemRes> Items { get; set; } = new();
    public PaginationMeta Pagination { get; set; } = new();
}

public sealed class TourBookingItemRes
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
