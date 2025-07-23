using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Service.CommandHandler.ShirdalBlog.BlogCategories;

public sealed class DeleteBlogCategoryCommandHandler(IBlogCategoryService categoryService)
    : IRequestHandler<DeleteBlogCategoryCommandReq, DeleteBlogCategoryCommandRes>
{
    public async Task<DeleteBlogCategoryCommandRes> Handle(DeleteBlogCategoryCommandReq request, CancellationToken cancellationToken)
    {
        var entity = await categoryService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null) return new DeleteBlogCategoryCommandRes()
        {
            Error = ValidationError.UnhandledValidationError("Category Not Found", "Category not Found")
        };
        var Blog = await categoryService.FindByIdAsync(cancellationToken, entity);
        Blog.IsDeleted = true;
        return new DeleteBlogCategoryCommandRes();
    }
}