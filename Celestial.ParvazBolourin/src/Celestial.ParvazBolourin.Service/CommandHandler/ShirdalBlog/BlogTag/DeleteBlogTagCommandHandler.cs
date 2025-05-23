using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog.BlogTag;

public sealed class DeleteBlogTagCommandHandler : IRequestHandler<DeleteBlogCategoryCommandReq, DeleteBlogCategoryCommandRes>
{
    private readonly IBlogTagService _blogTagService;

    public DeleteBlogTagCommandHandler(IBlogTagService blogTagService)
    {
        _blogTagService = blogTagService;
    }

    public async Task<DeleteBlogCategoryCommandRes> Handle(DeleteBlogCategoryCommandReq request, CancellationToken cancellationToken)
    {
        var entity = await _blogTagService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null) return new DeleteBlogCategoryCommandRes()
        {
            Error = ValidationError.UnhandledValidationError("Tag Not Found", "Tag not Found")
        };
        var BlogTag = await _blogTagService.FindByIdAsync(cancellationToken, entity);
        BlogTag.IsDeleted = true;
        return new DeleteBlogCategoryCommandRes();
    }
}