using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Tour;

public sealed class GetTourFeaturedCommandReq : BaseEntity, IRequest<GetTourFeaturedCommandRes>
{
    public int TopCount { get; set; }
    public int LanguageId { get; set; }

}
public sealed class GetTourFeaturedCommandRes : ServiceResContextBase
{
    public List<GetTourFeaturedItemDto> Items { get; set; } = new();

}

public sealed class GetTourFeaturedItemDto
{
    public long PictureId { get; set; }
    public DateOnly StartDate { get; set; }
    public List<int> CountriesId { get; set; } = new();
    public string Title { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public decimal? ForeignPrice { get; set; }
    public string? ForeignCurrency { get; set; }
    public int DaysUntilRegistrationDeadline { get; set; }
    public int Score { get; set; }= 0;
    public int VoteCount { get; set; } = 0;
}