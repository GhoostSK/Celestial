using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;

public sealed class UpdateBlogTagCommandReq : BaseEntity, IRequest<UpdateBlogTagCommandRes>
{
    public long Id { get; set; }
    public List<LocalizedTagNameDto> LocalizedNames { get; set; } = new();
}

public sealed class UpdateBlogTagCommandRes : ServiceResContextBase
{
}