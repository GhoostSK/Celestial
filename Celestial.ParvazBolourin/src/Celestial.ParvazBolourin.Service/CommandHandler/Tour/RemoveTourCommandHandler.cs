using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tour;

public sealed class RemoveTourCommandHandler : BaseRequestHandler<RemoveTourCommandReq, RemoveTourCommandRes>
{
    private readonly ITourService _tourService;

    public RemoveTourCommandHandler(ITourService tourService)
    {
        _tourService = tourService;
    }

    protected override async Task<RemoveTourCommandRes> HandleCore(RemoveTourCommandReq request,
        CancellationToken cancellationToken)
    {
        var tour = await _tourService.GetTourById(request.Id, cancellationToken);
        if (tour == null)
            return new RemoveTourCommandRes
            {
                Error = new ValidationError("T404", "Tour not found")
            };
        tour.IsDeleted = true;
        await _tourService.UnitOfWork.SaveChangesAsync(cancellationToken);
        return new RemoveTourCommandRes();
    }
}