using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;

public sealed class UpdateBlogCategoryCommandReq : BaseEntity, IRequest<UpdateBlogCategoryCommandRes>
{
    public long Id { get; set; }
    public List<LocalizedNameDto> LocalizedNames { get; set; } = new();
}

public sealed class UpdateBlogCategoryCommandRes : ServiceResContextBase
{
}