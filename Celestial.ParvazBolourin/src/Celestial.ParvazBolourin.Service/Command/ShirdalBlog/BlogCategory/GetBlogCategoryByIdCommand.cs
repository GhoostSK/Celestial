using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;

public sealed class GetBlogCategoryByIdCommandReq : BaseEntity, IRequest<GetBlogCategoryByIdCommandRes>
{
    public long Id { get; set; }
    public int LanguageId { get; set; }
    public string Slug { get; set; }
}

public sealed class GetBlogCategoryByIdCommandRes : ServiceResContextBase
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}