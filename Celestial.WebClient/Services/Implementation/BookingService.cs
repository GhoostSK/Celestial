using Celestial.WebClient.Services.ApiClients.Booking;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class BookingService(BookingApiClient client) : IBookingService
{
    public async Task<AddTourBookingResDto> AddTourBookingAsync(AddTourBookingReqDto request)
    {
        return await client.AddTourBookingAsync(request);
    }

    public async Task<GetTourBookingByCodeResDto> GetBookingByCodeAsync(GetTourBookingByCodeReqDto request)
    {
        return await client.GetBookingByCodeAsync(request);
    }
}
