using Celestial.Agency.Core.DAL.Model.Booking;
using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Booking;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.Core.Enum.Booking;
using Celestial.Agency.Core.Enum.Tour;
using Celestial.Agency.Service.Command.Booking;
using Celestial.SharedKernel.Mediator;

namespace Celestial.Agency.Service.CommandHandler.Booking;

public sealed class AddTourBookingCommandHandler(
    ILocalizedPropertyService localizedPropertyService,
    ITourBookingService tourBookingService,
    ITourService tourService)
    : BaseRequestHandler<AddTourBookingCommandReq, AddTourBookingCommandRes>
{
    protected override async Task<AddTourBookingCommandRes> HandleCore(AddTourBookingCommandReq request,
        CancellationToken cancellationToken)
    {
        if (request.PassengerCount < 1)
            return Failure("400", "Passenger count must be at least 1.");

        var slugProperty = await localizedPropertyService.FirstOrDefaultAsync(
            p => p.LocalKey == "Slug"
                 && p.LocalKeyGroup == LocalKeyGroupType.Tour
                 && p.LocalValue.ToLower() == request.Slug.ToLower(),
            cancellationToken: cancellationToken);

        if (slugProperty == null)
            return Failure("404", "Tour not found with slug");

        var tour = await tourService.GetTourById(slugProperty.EntityId, cancellationToken);

        if (tour == null)
            return Failure("404", "Tour not found");

        if (tour.TourStatus != TourStatus.Active)
            return Failure("400", "This tour is not open for registration.");

        if (tour.RegistrationDeadline < DateOnly.FromDateTime(DateTime.UtcNow))
            return Failure("400", "The registration deadline for this tour has passed.");

        var reservedSeats = await tourBookingService.GetReservedSeatCount(tour.Id, cancellationToken);
        var remainingCapacity = tour.PassengerCapacity - reservedSeats;

        if (remainingCapacity < request.PassengerCount)
            return Failure("400", $"Only {(remainingCapacity < 0 ? 0 : remainingCapacity)} seats are available.");

        var booking = new TourBooking
        {
            BookingCode = await tourBookingService.GenerateUniqueBookingCodeAsync(cancellationToken),
            TourId = tour.Id,
            UserId = request.UserId,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            NationalCode = request.NationalCode,
            PassengerCount = request.PassengerCount,
            Note = request.Note,
            TotalPrice = tour.BasePrice * request.PassengerCount,
            Currency = tour.ForeignCurrency ?? string.Empty,
            BookingStatus = BookingStatus.Pending
        };

        await tourBookingService.AddAsync(booking, cancellationToken);
        await tourBookingService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new AddTourBookingCommandRes
        {
            Id = booking.Id,
            BookingCode = booking.BookingCode,
            TotalPrice = booking.TotalPrice,
            Currency = booking.Currency,
            BookingStatus = booking.BookingStatus
        };
    }
}
