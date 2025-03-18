using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel.Model;
using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Hotel;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Hotel;

public sealed class GetHotelsListCommandReq : BaseEntity, IRequest<GetHotelsListCommandRes>
{
    public int LanguageId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<HotelLocation> HotelLocation { get; set; } = new();
    public List<Continent> HotelContinent { get; set; } = new();
    public double FromPrice { get; set; } = 0;
    public double ToPrice { get; set; } = 0;
    public List<string> HotelCountry { get; set; } = new();
    public List<int> HotelScore { get; set; } = new();
}

public sealed class GetHotelsListCommandRes : ServiceResContextBase
{
    public List<GetAllHotelsServiceRes> Items { get; set; } = new();
}