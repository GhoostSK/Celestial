using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour.TourEquipment;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Service.CommandHandler.Tour.TourEquipment;

public sealed class GetTourEquipmentListCommandHandler(
    ITourEquipmentsService tourEquipmentService,
    ILocalizedPropertyService localizedPropertyService)
    : IRequestHandler<GetTourEquipmentListCommandReq, GetTourEquipmentListCommandRes>
{
    public async Task<GetTourEquipmentListCommandRes> Handle(GetTourEquipmentListCommandReq request, CancellationToken cancellationToken)
    {
        var equipments = await tourEquipmentService.GetListAsync(cancellationToken);
        if (equipments.Count == 0)
        {
            return new()
            {
                Error = ValidationError.UnhandledValidationError("BT404", "Equipment Not Found")
            };
        }


        var Data = new List<TourEquipmentListItemDto>();
        foreach (var item in equipments)
        {
            var localize = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = item.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.TourEquipment
                }, cancellationToken);
            Data.Add(new TourEquipmentListItemDto()
            {
                Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? "",
                Icon = item.Icon,
                Id = item.Id
            });
        }

        return new()
        {
            Items = Data
        };
    }
}
