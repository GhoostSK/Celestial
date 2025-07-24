using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;

public sealed class GetBlogCategoryByIdCommandReq : BaseEntity, IRequest<GetBlogCategoryByIdCommandRes>
{
    public long Id { get; set; }
    public string Language { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class GetBlogCategoryByIdCommandRes : ServiceResContextBase
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}