using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog.BlogCategories;

public sealed class DeleteBlogCategoryCommandHandler : IRequestHandler<DeleteBlogCategoryCommandReq, DeleteBlogCategoryCommandRes>
{
    private readonly IBlogCategoryService _categoryService;

    public DeleteBlogCategoryCommandHandler(IBlogCategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<DeleteBlogCategoryCommandRes> Handle(DeleteBlogCategoryCommandReq request, CancellationToken cancellationToken)
    {
        var entity = await _categoryService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null) return new DeleteBlogCategoryCommandRes()
        {
            Error = ValidationError.UnhandledValidationError("Category Not Found", "Category not Found")
        };
        var Blog = await _categoryService.FindByIdAsync(cancellationToken, entity);
        Blog.IsDeleted = true;
        return new DeleteBlogCategoryCommandRes();
    }
}