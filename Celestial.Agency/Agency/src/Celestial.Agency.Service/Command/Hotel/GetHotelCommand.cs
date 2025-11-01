using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Hotel;

public sealed class GetHotelCommandReq : BaseEntity, IRequest<GetHotelByIdCommandRes>
{
    public string? Slug { get; set; }
}

public sealed class GetHotelByIdCommandRes : ServiceResContextBase
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
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

    //seo
    public string MetaTitle { get; set; } = string.Empty;

    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}