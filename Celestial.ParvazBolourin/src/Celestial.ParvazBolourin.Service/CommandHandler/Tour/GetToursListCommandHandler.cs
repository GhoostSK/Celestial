using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;
using System.Reflection;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tour;

public sealed class GetToursListCommandHandler(
    ITourService tourService,
    ILocalizedPropertyService localizedPropertyService,
    ICountryService countryService,
    ILanguageService languageService, IImageService imageService)
    : BaseRequestHandler<GetToursListCommandReq, GetToursListCommandRes>
{
    protected override async Task<GetToursListCommandRes> HandleCore(GetToursListCommandReq request,
     CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
            return new GetToursListCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };

        var response = new GetToursListCommandRes();
        var tourData = await tourService.GetToursList(request.Adapt<GetTourListReq>(), cancellationToken);

        if (!tourData.Any())
            return new GetToursListCommandRes
            {
                Error = new ValidationError("T404", "Tour Not Found")
            };

        response.MinPrice = tourData.Min(t => t.Price);
        response.MaxPrice = tourData.Max(t => t.Price);

        var data = new List<GetToursListItemRes>();

        foreach (var item in tourData)
        {
            var localizeData = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = item.Id,
                    LanguageId = request.LanguageId,
                    LocalKeyGroup = "Tour"
                }, cancellationToken);

            var name = localizeData.FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.Name))
                ?.LocalValue;
            var tourText = localizeData
                .FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.TourMainText))?.LocalValue;
            var slug = localizeData.FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.Slug))?.LocalValue;
            var picture = await imageService.FirstOrDefaultAsync(p => p.EntityId == item.Id && p.KeyGroup == "Tour" && p.DisplayOrder == 0, cancellationToken: cancellationToken);

            var countries = new List<string>();
            foreach (var country in item.CountriesId)
            {
                var countryData = await countryService.FindByIdAsync(cancellationToken, country);
                countries.Add(countryData.Shortname);
            }
            data.Add(new GetToursListItemRes
            {
                Id = item.Id,
                Score = item.Score,
                Name = name!,
                StartDate = item.StartDate,
                TourText = tourText!,
                PicturePath = picture.PicturePath,
                Country = countries,
                TourStatus = item.TourStatus.ToString(),
                Slug = slug!
            });
        }
        //TEMP
        if (!string.IsNullOrEmpty(request.Title))
        {
            var filtredData = data.Where(p => p.Name.Contains(request.Title)).ToList();
            response.Items = filtredData;
        }
        else
        {
            response.Items = data;
        }


        return response;
    }

}