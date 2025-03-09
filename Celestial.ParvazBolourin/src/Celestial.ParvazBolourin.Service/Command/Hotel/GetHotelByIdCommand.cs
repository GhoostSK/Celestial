using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Hotel;

public sealed class GetHotelByIdCommandReq : BaseEntity, IRequest<GetHotelByIdCommandRes>
{
    public int HotelId { get; set; }
    public int LanguageId { get; set; }
}

public sealed class GetHotelByIdCommandRes : ServiceResContextBase
{
    public string Name { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string HotelCountry { get; set; } = string.Empty;
    public int HotelScore { get; set; }
    public string BookingUrl { get; set; } = string.Empty;
    public string HotelCode { get; set; } = string.Empty;
    public string MapCoordinate { get; set; } = string.Empty;
    public string HotelReception { get; set; } = string.Empty;
    public List<Comments> HotelComments { get; set; } = new();
    public string ShortText { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public List<Images> HotelPictures { get; set; } = new();
}