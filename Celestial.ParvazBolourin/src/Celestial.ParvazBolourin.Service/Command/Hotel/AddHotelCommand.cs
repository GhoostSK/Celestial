using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel.Model;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Service.Command.Hotel;

public sealed class AddHotelCommandReq : BaseEntity, IRequest<AddHotelCommandRes>
{
    public List<PictureItem> HotelPictures { get; set; } = new();
    public Hotels Hotel { get; set; } = new();
    public List<HotelServiceLocalizedProperty> HotelLocalizeProperty { get; set; } = new();
}

public sealed class AddHotelCommandRes : ServiceResContextBase
{
}
