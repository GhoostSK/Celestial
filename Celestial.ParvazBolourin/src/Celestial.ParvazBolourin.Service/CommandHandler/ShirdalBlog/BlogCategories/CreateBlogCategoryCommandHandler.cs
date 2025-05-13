using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog.BlogCategories;

public sealed class CreateBlogCategoryCommandHandler : IRequestHandler<AddBlogCategoryCommandReq, AddBlogCategoryCommandRes>
{
    private readonly IBlogCategoryService _categoryService;
    private readonly ILocalizedPropertyService _localizedPropertyService;

    public CreateBlogCategoryCommandHandler(IBlogCategoryService categoryService, ILocalizedPropertyService localizedPropertyService)
    {
        _categoryService = categoryService;
        _localizedPropertyService = localizedPropertyService;
    }

    public async Task<AddBlogCategoryCommandRes> Handle(AddBlogCategoryCommandReq request, CancellationToken cancellationToken)
    {
        var entity = new BlogCategory();
        await _categoryService.AddAsync(entity, cancellationToken);
        await _categoryService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var localizedNames = request.LocalizedNames.Select(name => new LocalizedProperty
        {
            EntityId = entity.Id,
            LanguageId = name.LanguageId,
            LocalKey = nameof(name.Name),
            LocalKeyGroup = "BlogCategory",
            LocalValue = name.Name
        }).ToList();

        await _localizedPropertyService.BulkInsertAsync(localizedNames, cancellationToken: cancellationToken);
        return new AddBlogCategoryCommandRes();
    }
}