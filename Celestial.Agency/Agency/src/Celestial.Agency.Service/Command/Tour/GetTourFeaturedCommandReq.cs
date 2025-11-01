using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Tour;

public sealed class GetTourFeaturedCommandReq : BaseEntity, IRequest<GetTourFeaturedCommandRes>
{
    public int TopCount { get; set; }
    public string Language { get; set; } = string.Empty;
}

public sealed class GetTourFeaturedCommandRes : ServiceResContextBase
{
    public List<GetTourFeaturedItemDto> Items { get; set; } = new();
}

public sealed class GetTourFeaturedItemDto
{
    public string PicturePath { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public List<string> Countries { get; set; } = new();
    public string Title { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public decimal? ForeignPrice { get; set; }
    public string? ForeignCurrency { get; set; }
    public int DaysUntilRegistrationDeadline { get; set; }
    public int Score { get; set; } = 0;
    public int VoteCount { get; set; } = 0;
}