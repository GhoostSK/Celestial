using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.ApiClients.Booking;

public class BookingApiClient(HttpJsonClient httpClient)
{
    public async Task<AddTourBookingResDto> AddTourBookingAsync(AddTourBookingReqDto request)
    {
        return await httpClient.PostAsync<AddTourBookingReqDto, AddTourBookingResDto>(
            $"/api/{request.Language}/Booking/AddTourBooking",
            request);
    }

    public async Task<GetTourBookingByCodeResDto> GetBookingByCodeAsync(GetTourBookingByCodeReqDto request)
    {
        var query = new ApiQueryBuilder()
            .Add("BookingCode", request.BookingCode)
            .Add("PhoneNumber", request.PhoneNumber);

        return await httpClient.GetAsync<GetTourBookingByCodeResDto>(
            $"/api/{request.Language}/Booking/GetBookingByCode{query}");
    }
}
