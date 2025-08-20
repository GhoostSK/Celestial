using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.Service.Command.Tour.TourEquipment;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Tour.TourEquipment;

public sealed class DeleteTourEquipmentCommandHandler(ITourEquipmentsService tourEquipmentsService)
    : IRequestHandler<DeleteTourEquipmentCommandReq, DeleteTourEquipmentCommandRes>
{
    public async Task<DeleteTourEquipmentCommandRes> Handle(DeleteTourEquipmentCommandReq request,
        CancellationToken cancellationToken)
    {
        var entity = await tourEquipmentsService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null)
            return new DeleteTourEquipmentCommandRes
            {
                Error = ValidationError.UnhandledValidationError("Tag Not Found", "Tag not Found")
            };
        var tourEquipment = await tourEquipmentsService.FindByIdAsync(cancellationToken, entity);
        tourEquipment.IsDeleted = true;
        return new DeleteTourEquipmentCommandRes();
    }
}