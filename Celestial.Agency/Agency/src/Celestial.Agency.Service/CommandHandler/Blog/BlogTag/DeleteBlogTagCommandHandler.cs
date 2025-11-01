using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using Celestial.SharedKernel.Common;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Blog.BlogTag;

public sealed class DeleteBlogTagCommandHandler(IBlogTagService blogTagService)
    : IRequestHandler<DeleteBlogCategoryCommandReq, DeleteBlogCategoryCommandRes>
{
    public async Task<DeleteBlogCategoryCommandRes> Handle(DeleteBlogCategoryCommandReq request,
        CancellationToken cancellationToken)
    {
        var entity = await blogTagService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null)
            return new DeleteBlogCategoryCommandRes
            {
                Error = ValidationError.UnhandledValidationError("Tag Not Found", "Tag not Found")
            };
        var BlogTag = await blogTagService.FindByIdAsync(cancellationToken, entity);
        BlogTag.IsDeleted = true;
        return new DeleteBlogCategoryCommandRes();
    }
}