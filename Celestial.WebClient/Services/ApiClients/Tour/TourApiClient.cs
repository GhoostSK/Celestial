using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.ApiClients.Tour;

public class TourApiClient(HttpJsonClient httpClient)
{
    public async Task<GetTourListResDto> GetTourListAsync(GetTourListReqDto request)
    {
        var query = new ApiQueryBuilder()
            .Add("Title", request.Title)
            .AddRange("Countries", request.Countries)
            .AddRange("Cities", request.Cities)
            .AddRange("TourContinent", request.TourContinent)
            .AddRange("TourType", request.TourType)
            .AddRange("TourScope", request.TourScope)
            .Add("FromDay", request.FromDay)
            .Add("ToDay", request.ToDay)
            .Add("FromPrice", request.FromPrice)
            .Add("ToPrice", request.ToPrice)
            .Add("IsActiveTour", request.IsActiveTour)
            .Add("SortBy", request.SortBy.ToString())
            .Add("PageNumber", request.PageNumber)
            .Add("PageSize", request.PageSize);

        return await httpClient.GetAsync<GetTourListResDto>(
            $"/api/{request.Language}/Tour/GetToursList{query}");
    }

    public async Task<GetTourBySlugResDto> GetTourBySlugAsync(GetTourBySlugReqDto request)
    {
        return await httpClient.GetAsync<GetTourBySlugResDto>(
            $"/api/{request.Language}/Tour/GetTour/{Uri.EscapeDataString(request.Slug)}");
    }

    public async Task<GetFeaturedTourListResDto> GetFeaturedTourListAsync(GetFeaturedTourListReqDto request)
    {
        var query = new ApiQueryBuilder()
            .Add("TopCount", request.TopCount);

        return await httpClient.GetAsync<GetFeaturedTourListResDto>(
            $"/api/{request.Language}/Tour/GetFeaturedTours{query}");
    }

    public async Task<GetTourEquipmentListResDto> GetEquipmentListAsync(GetTourEquipmentListReqDto request)
    {
        return await httpClient.GetAsync<GetTourEquipmentListResDto>(
            $"/api/{request.Language}/TourEquipment/GetEquipmentList");
    }
}
