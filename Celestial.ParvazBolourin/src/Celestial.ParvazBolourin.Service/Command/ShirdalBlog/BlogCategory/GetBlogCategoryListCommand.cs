using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;

public sealed class GetBlogCategoryListCommandReq : BaseEntity, IRequest<GetBlogCategoryListCommandRes>
{
    public int LanguageId { get; set; }
    public int Id { get; set; }
}

public sealed class GetBlogCategoryListCommandRes : ServiceResContextBase
{
    public List<BlogCategoryListItemDto> Items { get; set; } = new();
}