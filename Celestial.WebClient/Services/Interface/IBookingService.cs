using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface IBookingService
{
    public Task<AddTourBookingResDto> AddTourBookingAsync(AddTourBookingReqDto request);
    public Task<GetTourBookingByCodeResDto> GetBookingByCodeAsync(GetTourBookingByCodeReqDto request);
}
