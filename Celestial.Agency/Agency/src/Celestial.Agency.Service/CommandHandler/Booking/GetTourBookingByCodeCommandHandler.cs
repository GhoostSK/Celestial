using Celestial.Agency.Core.DAL.Repositories.Booking;
using Celestial.Agency.Service.Command.Booking;
using Celestial.SharedKernel.Mediator;
using Mapster;

namespace Celestial.Agency.Service.CommandHandler.Booking;

public sealed class GetTourBookingByCodeCommandHandler(ITourBookingService tourBookingService)
    : BaseRequestHandler<GetTourBookingByCodeCommandReq, GetTourBookingByCodeCommandRes>
{
    protected override async Task<GetTourBookingByCodeCommandRes> HandleCore(
        GetTourBookingByCodeCommandReq request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.BookingCode))
            return Failure("400", "Booking code is required.");

        var booking = await tourBookingService.GetBookingByCode(request.BookingCode, cancellationToken);

        if (booking == null)
            return Failure("404", "Booking not found");

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber) && booking.PhoneNumber != request.PhoneNumber)
            return Failure("403", "The phone number does not match this booking.");

        return new GetTourBookingByCodeCommandRes
        {
            Item = booking.Adapt<TourBookingItemRes>()
        };
    }
}
