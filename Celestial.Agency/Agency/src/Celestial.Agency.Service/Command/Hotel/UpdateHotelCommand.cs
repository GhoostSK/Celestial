using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.Hotel;
using Celestial.Agency.Core.DAL.Repositories.Hotel.Model;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Hotel;

public sealed class UpdateHotelCommandReq : BaseEntity, IRequest<UpdateHotelCommandRes>
{
    public long Id { get; set; }
    public Core.DAL.Model.Hotel.Hotel Hotel { get; set; } = new();
    public List<Image> Images { get; set; } = new();
    public List<HotelServiceLocalizedProperty> LocalizedProperty { get; set; } = new();
    public Guid ModifiedBy { get; set; }
}

public sealed class UpdateHotelCommandRes : ServiceResContextBase
{
}