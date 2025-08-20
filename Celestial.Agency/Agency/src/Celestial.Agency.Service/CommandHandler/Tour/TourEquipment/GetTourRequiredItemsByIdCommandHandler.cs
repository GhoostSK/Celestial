using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.Service.Command.Tour.TourEquipment;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Tour.TourEquipment;

public sealed class GetTourEquipmentByIdCommandHandler(
    ITourEquipmentsService tourEquipmentsService,
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService)
    : IRequestHandler<GetTourEquipmentByIdCommandReq, GetTourEquipmentByIdCommandRes>
{
    public async Task<GetTourEquipmentByIdCommandRes> Handle(GetTourEquipmentByIdCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        if (languages.Any(p => p.LanguageCulture.Split('-')[0] == request.Language))
            return new GetTourEquipmentByIdCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };
        var entity = await tourEquipmentsService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null)
            return new GetTourEquipmentByIdCommandRes
            {
                Error = ValidationError.UnhandledValidationError("T404", "Required Item not Found")
            };

        var localize = await localizedPropertyService.GetPropertyByLanguageAndEntityId(new GetLocalizedPropertyReq
        {
            EntityId = request.Id,
            Language = request.Language,
            LocalKeyGroup = LocalKeyGroupType.TourEquipment
        }, cancellationToken);

        return new GetTourEquipmentByIdCommandRes
        {
            Id = request.Id,
            Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? ""
        };
    }
}