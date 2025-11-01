using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.Service.Command.Tour;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Mediator;

namespace Celestial.Agency.Service.CommandHandler.Tour;

public sealed class RemoveTourCommandHandler(ITourService tourService)
    : BaseRequestHandler<RemoveTourCommandReq, RemoveTourCommandRes>
{
    protected override async Task<RemoveTourCommandRes> HandleCore(RemoveTourCommandReq request,
        CancellationToken cancellationToken)
    {
        var tour = await tourService.GetTourById(request.Id, cancellationToken);
        if (tour == null)
            return new RemoveTourCommandRes
            {
                Error = new ValidationError("T404", "Tour not found")
            };
        tour.IsDeleted = true;
        await tourService.UnitOfWork.SaveChangesAsync(cancellationToken);
        return new RemoveTourCommandRes();
    }
}