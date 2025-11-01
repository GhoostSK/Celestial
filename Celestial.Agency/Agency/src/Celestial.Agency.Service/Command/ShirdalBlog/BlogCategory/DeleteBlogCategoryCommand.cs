using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;

public sealed class DeleteBlogCategoryCommandReq : BaseEntity, IRequest<DeleteBlogCategoryCommandRes>
{
    public long Id { get; set; }
}

public sealed class DeleteBlogCategoryCommandRes : ServiceResContextBase
{
}