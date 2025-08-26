using Celestial.Agency.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.ApiClients;

public class TourApiClient(HttpClient http)
{
    public async Task<List<TourDto>> GetToursAsync(string lang)
    {
        return await http.GetFromJsonAsync<List<TourDto>>($"api/{lang}/tours")
               ?? new List<TourDto>();
    }

    public async Task<TourDto?> GetTourByIdAsync(int id)
    {
        return await http.GetFromJsonAsync<TourDto>($"api/tours/{id}");
    }
}