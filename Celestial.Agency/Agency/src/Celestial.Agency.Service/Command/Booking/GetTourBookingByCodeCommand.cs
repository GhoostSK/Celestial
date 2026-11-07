using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Booking;

public sealed class GetTourBookingByCodeCommandReq : BaseEntity, IRequest<GetTourBookingByCodeCommandRes>
{
    public string BookingCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

public sealed class GetTourBookingByCodeCommandRes : ServiceResContextBase
{
    public TourBookingItemRes Item { get; set; } = new();
}
