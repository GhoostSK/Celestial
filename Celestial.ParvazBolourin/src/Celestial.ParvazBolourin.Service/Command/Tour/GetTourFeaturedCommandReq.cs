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
    public List<GetTourFeaturedItemDto> Data { get; set; } = new();

}

public sealed class GetTourFeaturedItemDto
{
    public double Price { get; set; }
    public long PictureId { get; set; }
    public DateTime StartDate { get; set; }
    public List<int> CountriesId { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    
}