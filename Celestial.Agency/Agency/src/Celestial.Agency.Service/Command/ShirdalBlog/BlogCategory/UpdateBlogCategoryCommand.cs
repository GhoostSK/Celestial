using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;

public sealed class UpdateBlogCategoryCommandReq : BaseEntity, IRequest<UpdateBlogCategoryCommandRes>
{
    public long Id { get; set; }
    public List<LocalizedNameDto> LocalizedNames { get; set; } = new();
}

public sealed class UpdateBlogCategoryCommandRes : ServiceResContextBase
{
}