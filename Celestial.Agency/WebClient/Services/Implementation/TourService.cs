using Celestial.Agency.WebClient.Services.ApiClients;
using Celestial.Agency.WebClient.Services.DTO;
using Celestial.Agency.WebClient.Services.Interface;

namespace Celestial.Agency.WebClient.Services.Implementation;

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