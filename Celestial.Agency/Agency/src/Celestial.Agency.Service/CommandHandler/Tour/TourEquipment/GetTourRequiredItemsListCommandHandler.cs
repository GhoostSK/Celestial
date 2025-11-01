using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.Service.Command.Tour.TourEquipment;
using Celestial.SharedKernel.Common;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Tour.TourEquipment;

public sealed class GetTourEquipmentListCommandHandler(
    ITourEquipmentsService tourEquipmentService,
    ILocalizedPropertyService localizedPropertyService)
    : IRequestHandler<GetTourEquipmentListCommandReq, GetTourEquipmentListCommandRes>
{
    public async Task<GetTourEquipmentListCommandRes> Handle(GetTourEquipmentListCommandReq request,
        CancellationToken cancellationToken)
    {
        var equipments = await tourEquipmentService.GetListAsync(cancellationToken);
        if (equipments.Count == 0)
            return new GetTourEquipmentListCommandRes
            {
                Error = ValidationError.UnhandledValidationError("BT404", "Equipment Not Found")
            };

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
            Data.Add(new TourEquipmentListItemDto
            {
                Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? "",
                Icon = item.Icon,
                Id = item.Id
            });
        }

        return new GetTourEquipmentListCommandRes
        {
            Items = Data
        };
    }
}