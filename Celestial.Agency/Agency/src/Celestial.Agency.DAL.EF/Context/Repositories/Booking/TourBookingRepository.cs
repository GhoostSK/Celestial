using Celestial.Agency.Core.DAL.Model.Booking;
using Celestial.Agency.Core.DAL.Repositories.Booking;
using Celestial.Agency.Core.DAL.Repositories.Booking.Model;
using Celestial.Agency.Core.Enum.Booking;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Booking;

public sealed class TourBookingRepository(MainDbContext dbContext)
    : RepositoryBase<TourBooking>(dbContext), ITourBookingService
{
    public async Task<(List<TourBooking> Items, int TotalPages, int TotalRecord)> GetBookingList(
        GetTourBookingListReq model, CancellationToken cancellationToken)
    {
        var query = TableNoTracking.Where(p => !p.IsDeleted);

        if (model.BookingStatus.Any())
            query = query.Where(p => model.BookingStatus.Contains(p.BookingStatus));

        if (model.TourId.HasValue)
            query = query.Where(p => p.TourId == model.TourId.Value);

        if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
            query = query.Where(p => p.PhoneNumber == model.PhoneNumber);

        if (!string.IsNullOrWhiteSpace(model.BookingCode))
            query = query.Where(p => p.BookingCode == model.BookingCode);

        var totalRecord = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecord / (double)model.PageSize);

        var items = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((model.PageNumber - 1) * model.PageSize)
            .Take(model.PageSize)
            .ToListAsync(cancellationToken);

        return (items, totalPages, totalRecord);
    }

    public async Task<TourBooking?> GetBookingByCode(string bookingCode, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .FirstOrDefaultAsync(p => p.BookingCode == bookingCode && !p.IsDeleted, cancellationToken);
    }

    public async Task<int> GetReservedSeatCount(long tourId, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .Where(p => p.TourId == tourId
                        && !p.IsDeleted
                        && p.BookingStatus != BookingStatus.Cancelled
                        && p.BookingStatus != BookingStatus.Rejected)
            .SumAsync(p => p.PassengerCount, cancellationToken);
    }

    public async Task<string> GenerateUniqueBookingCodeAsync(CancellationToken cancellationToken)
    {
        string code;
        do
        {
            code = GenerateBookingCode();
        } while (await Table.AnyAsync(p => p.BookingCode == code, cancellationToken));

        return code;
    }

    private static string GenerateBookingCode()
    {
        const string upper = "ABCDEFGHIJKLMNPQRSTUVWXYZ";
        const string digits = "123456789";

        var random = new Random();
        var letters = new string(Enumerable.Range(0, 2).Select(_ => upper[random.Next(upper.Length)]).ToArray());
        var numbers = new string(Enumerable.Range(0, 6).Select(_ => digits[random.Next(digits.Length)]).ToArray());

        return $"CB-{letters}{numbers}";
    }
}
