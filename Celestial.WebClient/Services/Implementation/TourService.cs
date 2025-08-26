using Celestial.Agency.WebClient.Services.DTO;
using Celestial.WebClient.Services.ApiClients;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class TourService(TourApiClient client) : ITourService
{
    public async Task<List<TourDto>> GetToursAsync()
    {
        // اینجا میشه قبل از برگشت دادن دیتا Validation یا Mapping بزنی
        return await client.GetToursAsync("");
    }

    public async Task<TourDto?> GetTourByIdAsync(int id)
    {
        return await client.GetTourByIdAsync(id);
    }
}