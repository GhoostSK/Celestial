using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Service.CommandHandler.ShirdalBlog.BlogTag;

public sealed class DeleteBlogTagCommandHandler(IBlogTagService blogTagService)
    : IRequestHandler<DeleteBlogCategoryCommandReq, DeleteBlogCategoryCommandRes>
{
    public async Task<DeleteBlogCategoryCommandRes> Handle(DeleteBlogCategoryCommandReq request, CancellationToken cancellationToken)
    {
        var entity = await blogTagService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null) return new DeleteBlogCategoryCommandRes()
        {
            Error = ValidationError.UnhandledValidationError("Tag Not Found", "Tag not Found")
        };
        var BlogTag = await blogTagService.FindByIdAsync(cancellationToken, entity);
        BlogTag.IsDeleted = true;
        return new DeleteBlogCategoryCommandRes();
    }
}