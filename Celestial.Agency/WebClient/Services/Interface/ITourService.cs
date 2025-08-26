using Celestial.Agency.WebClient.Services.DTO;

namespace Celestial.Agency.WebClient.Services.Interface;

public interface ITourService
{
    Task<List<TourDto>> GetToursAsync();
    Task<TourDto?> GetTourByIdAsync(int id);
}