using Celestial.ParvazBolourin.Core.DAL.Model.Global;
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
    public string Title { get; set; } = string.Empty;
    public int FromDay { get; set; }
    public int ToDay { get; set; }
    public double ToPrice { get; set; }
    public double FromPrice { get; set; }
    public List<Continent> TourContinent { get; set; } = new();
    public List<TourType> TourType { get; set; } = new();
    public List<TourScope> TourScope { get; set; } = new();

    public bool IsActiveTour { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public sealed class GetToursListCommandRes : ServiceResContextBase
{
    public List<GetToursListItemRes> Items { get; set; } = new();
    public TourFiltersDto Filters { get; set; } = new();
    public PaginationMeta Pagination { get; set; } = new();


}

public sealed class GetToursListItemRes
{
    public long Id { get; set; }
    public int TotalDay { get; set; }
    public int Score { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public string TourText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public List<string> Country { get; set; } = new();
    public DateOnly? RegistrationDeadline { get; set; }
    public int? DaysUntilRegistrationDeadline { get; set; }
    public TourStatus TourStatus { get; set; } = new();

    public int RemainingCapacity { get; set; }
    public decimal BasePrice { get; set; }
    public decimal? ForeignPrice { get; set; }
    public string? ForeignCurrency { get; set; }
    //seo
    public string Slug { get; set; } = string.Empty;
}


public class TourFiltersDto
{
    public List<string> Countries { get; set; } = new();
    public int FromDay { get; set; }
    public int ToDay { get; set; }
    public double FromPrice { get; set; }
    public double ToPrice { get; set; }
    public List<Continent> TourContinent { get; set; } = new();
    public List<TourType> TourType { get; set; } = new();
    public List<TourScope> TourScope { get; set; } = new();
    public List<bool> IsActiveTour { get; set; }
}