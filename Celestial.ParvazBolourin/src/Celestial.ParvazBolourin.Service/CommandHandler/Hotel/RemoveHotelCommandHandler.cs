using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Hotel;

public sealed class RemoveHotelCommandHandler : BaseRequestHandler<RemoveHotelCommandReq, RemoveHotelCommandRes>
{
    private readonly IHotelService _hotelRepository;

    public RemoveHotelCommandHandler(IHotelService hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    protected override async Task<RemoveHotelCommandRes> HandleCore(RemoveHotelCommandReq request,
        CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.FindByIdAsync(cancellationToken, request.HotelId);
        if (hotel == null)
            return new RemoveHotelCommandRes
            {
                Error = new ValidationError("500", "Hotel Not Found")
            };
        hotel.IsDeleted = true;
        await _hotelRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return new RemoveHotelCommandRes();
    }
}