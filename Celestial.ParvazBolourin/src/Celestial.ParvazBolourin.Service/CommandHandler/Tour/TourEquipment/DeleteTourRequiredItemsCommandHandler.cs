using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour.TourEquipment;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tour.TourEquipment;

public sealed class DeleteTourEquipmentCommandHandler(ITourEquipmentsService tourEquipmentsService)
    : IRequestHandler<DeleteTourEquipmentCommandReq, DeleteTourEquipmentCommandRes>
{
    public async Task<DeleteTourEquipmentCommandRes> Handle(DeleteTourEquipmentCommandReq request, CancellationToken cancellationToken)
    {
        var entity = await tourEquipmentsService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null) return new DeleteTourEquipmentCommandRes()
        {
            Error = ValidationError.UnhandledValidationError("Tag Not Found", "Tag not Found")
        };
        var tourEquipment = await tourEquipmentsService.FindByIdAsync(cancellationToken, entity);
        tourEquipment.IsDeleted = true;
        return new DeleteTourEquipmentCommandRes();
    }
}