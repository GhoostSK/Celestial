using Celestial.Agency.Core.DAL.Repositories.Hotel;
using Celestial.Agency.Service.Command.Hotel;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Hotel;

public sealed class RemoveHotelCommandHandler(IHotelService hotelRepository)
    : BaseRequestHandler<RemoveHotelCommandReq, RemoveHotelCommandRes>
{
    protected override async Task<RemoveHotelCommandRes> HandleCore(RemoveHotelCommandReq request,
        CancellationToken cancellationToken)
    {
        var hotel = await hotelRepository.FindByIdAsync(cancellationToken, request.HotelId);
        if (hotel == null)
            return new RemoveHotelCommandRes
            {
                Error = new ValidationError("500", "Hotel Not Found")
            };
        hotel.IsDeleted = true;
        await hotelRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return new RemoveHotelCommandRes();
    }
}