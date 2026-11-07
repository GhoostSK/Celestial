using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Booking;
using Celestial.Agency.Core.DAL.Repositories.Booking.Model;
using Celestial.Agency.Service.Command.Booking;
using Celestial.SharedKernel.Mediator;
using Mapster;

namespace Celestial.Agency.Service.CommandHandler.Booking;

public sealed class GetTourBookingListCommandHandler(ITourBookingService tourBookingService)
    : BaseRequestHandler<GetTourBookingListCommandReq, GetTourBookingListCommandRes>
{
    protected override async Task<GetTourBookingListCommandRes> HandleCore(GetTourBookingListCommandReq request,
        CancellationToken cancellationToken)
    {
        var (items, totalPages, totalRecord) =
            await tourBookingService.GetBookingList(request.Adapt<GetTourBookingListReq>(), cancellationToken);

        return new GetTourBookingListCommandRes
        {
            Items = items.Adapt<List<TourBookingItemRes>>(),
            Pagination = new PaginationMeta
            {
                TotalRecords = totalRecord,
                CurrentPage = request.PageNumber,
                TotalPages = totalPages,
                NextPage = request.PageNumber < totalPages ? request.PageNumber + 1 : null,
                PrevPage = request.PageNumber > 1 ? request.PageNumber - 1 : null
            }
        };
    }
}
