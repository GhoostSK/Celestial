using Celestial.Agency.Core.DAL.Model.Hotel;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Hotel.Model;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Hotel;

public sealed class AddHotelCommandReq : BaseEntity, IRequest<AddHotelCommandRes>
{
    public List<PictureItem> HotelPictures { get; set; } = new();
    public Hotels Hotel { get; set; } = new();
    public List<HotelServiceLocalizedProperty> HotelLocalizeProperty { get; set; } = new();
}

public sealed class AddHotelCommandRes : ServiceResContextBase
{
}