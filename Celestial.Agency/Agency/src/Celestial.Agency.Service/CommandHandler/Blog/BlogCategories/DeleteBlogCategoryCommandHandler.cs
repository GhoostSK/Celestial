using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Blog.BlogCategories;

public sealed class DeleteBlogCategoryCommandHandler(IBlogCategoryService categoryService)
    : IRequestHandler<DeleteBlogCategoryCommandReq, DeleteBlogCategoryCommandRes>
{
    public async Task<DeleteBlogCategoryCommandRes> Handle(DeleteBlogCategoryCommandReq request,
        CancellationToken cancellationToken)
    {
        var entity = await categoryService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null)
            return new DeleteBlogCategoryCommandRes
            {
                Error = ValidationError.UnhandledValidationError("Category Not Found", "Category not Found")
            };
        var Blog = await categoryService.FindByIdAsync(cancellationToken, entity);
        Blog.IsDeleted = true;
        return new DeleteBlogCategoryCommandRes();
    }
}