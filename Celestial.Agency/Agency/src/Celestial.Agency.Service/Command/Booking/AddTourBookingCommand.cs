using Celestial.Agency.Core.Enum.Booking;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Booking;

public sealed class AddTourBookingCommandReq : BaseEntity, IRequest<AddTourBookingCommandRes>
{
    public string Language { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public int PassengerCount { get; set; } = 1;
    public string Note { get; set; } = string.Empty;
}

public sealed class AddTourBookingCommandRes : ServiceResContextBase
{
    public long Id { get; set; }
    public string BookingCode { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public BookingStatus BookingStatus { get; set; }
}
