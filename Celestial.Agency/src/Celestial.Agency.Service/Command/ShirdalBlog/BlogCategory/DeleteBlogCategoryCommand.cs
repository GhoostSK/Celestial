using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;

public sealed class DeleteBlogCategoryCommandReq : BaseEntity, IRequest<DeleteBlogCategoryCommandRes>
{
    public long Id { get; set; }
}

public sealed class DeleteBlogCategoryCommandRes : ServiceResContextBase
{
}