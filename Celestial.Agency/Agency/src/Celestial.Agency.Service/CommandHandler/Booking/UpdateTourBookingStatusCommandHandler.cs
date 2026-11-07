using Celestial.Agency.Core.DAL.Repositories.Booking;
using Celestial.Agency.Service.Command.Booking;
using Celestial.SharedKernel.Mediator;

namespace Celestial.Agency.Service.CommandHandler.Booking;

public sealed class UpdateTourBookingStatusCommandHandler(ITourBookingService tourBookingService)
    : BaseRequestHandler<UpdateTourBookingStatusCommandReq, UpdateTourBookingStatusCommandRes>
{
    protected override async Task<UpdateTourBookingStatusCommandRes> HandleCore(
        UpdateTourBookingStatusCommandReq request, CancellationToken cancellationToken)
    {
        var booking = await tourBookingService.FirstOrDefaultAsync(p => p.Id == request.Id && !p.IsDeleted,
            false, cancellationToken);

        if (booking == null)
            return Failure("404", "Booking not found");

        booking.BookingStatus = request.BookingStatus;

        tourBookingService.Update(booking);
        await tourBookingService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateTourBookingStatusCommandRes();
    }
}
