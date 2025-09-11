using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

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