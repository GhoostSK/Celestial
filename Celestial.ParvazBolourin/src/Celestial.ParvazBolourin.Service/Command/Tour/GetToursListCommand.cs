using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Tour;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Tour;

public sealed class GetToursListCommandReq : BaseEntity, IRequest<GetToursListCommandRes>
{
    public int LanguageId { get; set; }
    public List<string> Countries { get; set; } = new();
    public List<string> Cities { get; set; } = new();
    public int FromDay { get; set; }
    public int ToDay { get; set; }
    public long ToPrice { get; set; }
    public long FromPrice { get; set; }
    public List<Continent> TourContinent { get; set; } = new();
    public List<TourType> TourType { get; set; } = new();
    public List<TourScope> TourScope { get; set; } = new();
    public bool IsActiveTour { get; set; }

}

public sealed class GetToursListCommandRes : ServiceResContextBase
{
    public List<GetToursListItemRes> Items { get; set; } = new();
}

public sealed class GetToursListItemRes
{
    public int Id { get; set; }
    public int Score { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public string TourText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public List<string> Country { get; set; } = new();
    public string TourStatus { get; set; } = string.Empty;
}