using Celestial.Agency.Core.DAL.Model.Hotel;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Hotel.Model;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Hotel;

public sealed class AddHotelCommandReq : BaseEntity, IRequest<AddHotelCommandRes>
{
    public List<PictureItem> HotelPictures { get; set; } = new();
    public Core.DAL.Model.Hotel.Hotel Hotel { get; set; } = new();
    public List<HotelServiceLocalizedProperty> LocalizeProperty { get; set; } = new();
    public Guid CreatedBy { get; set; }
}

public sealed class AddHotelCommandRes : ServiceResContextBase
{
}