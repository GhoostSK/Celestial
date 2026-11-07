using Celestial.Agency.Core.DAL.Model.Booking;
using Celestial.Agency.Core.DAL.Repositories.Booking.Model;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Booking;

public interface ITourBookingService : IRepositoryEF<TourBooking>, IScopeLifetime
{
    public Task<(List<TourBooking> Items, int TotalPages, int TotalRecord)> GetBookingList(GetTourBookingListReq model,
        CancellationToken cancellationToken);

    public Task<TourBooking?> GetBookingByCode(string bookingCode, CancellationToken cancellationToken);

    public Task<int> GetReservedSeatCount(long tourId, CancellationToken cancellationToken);

    public Task<string> GenerateUniqueBookingCodeAsync(CancellationToken cancellationToken);
}
