using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.Tour.TourEquipment;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.Service.Command.Tour.TourEquipment;
using Celestial.SharedKernel.Common;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Tour.TourEquipment;

public sealed class AddTourEquipmentsCommandHandler(
    ITourEquipmentsService blogTagService,
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService)
    : IRequestHandler<AddTourEquipmentCommandReq, AddTourEquipmentCommandRes>
{
    public async Task<AddTourEquipmentCommandRes> Handle(AddTourEquipmentCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        if (request.LocalizedNames.Any(item => languages.Any(p => p.LanguageCulture.Split('-')[0] == item.Language)))
            return new AddTourEquipmentCommandRes { Error = new ValidationError("LA400", "Invalid Language") };

        var entity = new TourEquipments();
        await blogTagService.AddAsync(entity, cancellationToken);
        await blogTagService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var localizedProperties = request.LocalizedNames.SelectMany(name => new List<LocalizedProperty>
        {
            new()
            {
                EntityId = entity.Id,
                Language = name.Language,
                LocalKey = nameof(name.Name),
                LocalKeyGroup = LocalKeyGroupType.TourEquipment,
                LocalValue = name.Name
            }
        }).ToList();

        await localizedPropertyService.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);
        return new AddTourEquipmentCommandRes();
    }
}