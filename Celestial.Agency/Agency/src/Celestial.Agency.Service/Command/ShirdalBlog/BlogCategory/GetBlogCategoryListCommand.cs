using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;

public sealed class GetBlogCategoryListCommandReq : BaseEntity, IRequest<GetBlogCategoryListCommandRes>
{
    public string Language { get; set; } = string.Empty;
    public int Count { get; set; } = 0;
}

public sealed class GetBlogCategoryListCommandRes : ServiceResContextBase
{
    public List<BlogCategoryItemDto> Items { get; set; } = new();
    public int ReturnedCount { get; set; }
}