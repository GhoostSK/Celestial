using Celestial.Agency.Core.Enum.Booking;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Booking;

public sealed class UpdateTourBookingStatusCommandReq : BaseEntity, IRequest<UpdateTourBookingStatusCommandRes>
{
    public long Id { get; set; }
    public BookingStatus BookingStatus { get; set; }
}

public sealed class UpdateTourBookingStatusCommandRes : ServiceResContextBase
{
}
