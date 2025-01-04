using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel.Model;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Hotel;

public sealed class UpdateHotelCommandReq : BaseEntity, IRequest<UpdateHotelCommandRes>
{
    public int Id { get; set; }
    public Hotels Hotel { get; set; } = new();
    public List<HotelServiceLocalizedProperty> LocalizedProperty { get; set; } = new();
}

public sealed class UpdateHotelCommandRes : ServiceResContextBase
{
}