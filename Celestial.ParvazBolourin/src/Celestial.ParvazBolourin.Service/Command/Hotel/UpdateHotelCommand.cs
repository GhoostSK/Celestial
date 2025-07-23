using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel.Model;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Service.Command.Hotel;

public sealed class UpdateHotelCommandReq : BaseEntity, IRequest<UpdateHotelCommandRes>
{
    public long Id { get; set; }
    public Hotels Hotel { get; set; } = new();
    public List<Images> Images { get; set; } = new();
    public List<HotelServiceLocalizedProperty> LocalizedProperty { get; set; } = new();
}

public sealed class UpdateHotelCommandRes : ServiceResContextBase
{
}