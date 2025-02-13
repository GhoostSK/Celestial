using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tours;

public sealed class GetToursListCommandHandler(
    ITourService tourService,
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService)
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

            data.Add(new GetToursListItemRes
            {
                Id = item.Id,
                Score = item.Score,
                Name = name!,
                StartDate = item.StartDate,
                TourText = tourText!,
                PicturePath = item.PicturePath,
                Country = item.Countries,
                TourStatus = item.TourStatus.ToString()
            });
        }

        response.Items = data;
        return response;
    }
}