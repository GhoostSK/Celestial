using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;

public sealed class GetBlogCategoryListCommandReq : BaseEntity, IRequest<GetBlogCategoryListCommandRes>
{
    public string Language { get; set; } = string.Empty;
}

public sealed class GetBlogCategoryListCommandRes : ServiceResContextBase
{
    public List<BlogCategoryListItemDto> Items { get; set; } = new();
}