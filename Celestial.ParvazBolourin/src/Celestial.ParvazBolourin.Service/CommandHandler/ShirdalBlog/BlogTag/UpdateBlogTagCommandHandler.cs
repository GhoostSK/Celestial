using System.Linq.Expressions;
using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Service.CommandHandler.ShirdalBlog.BlogTag;

public sealed class UpdateBlogTagCommandHandler : IRequestHandler<UpdateBlogCategoryCommandReq, UpdateBlogCategoryCommandRes>
{
    private readonly IBlogCategoryService _categoryService;
    private readonly ILocalizedPropertyService _localizedPropertyService;

    public UpdateBlogTagCommandHandler(IBlogCategoryService categoryService, ILocalizedPropertyService localizedPropertyService)
    {
        _categoryService = categoryService;
        _localizedPropertyService = localizedPropertyService;
    }

    public async Task<UpdateBlogCategoryCommandRes> Handle(UpdateBlogCategoryCommandReq request, CancellationToken cancellationToken)
    {
        var entity = await _categoryService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null)
        {
            return new UpdateBlogCategoryCommandRes()
            {
                Error = ValidationError.UnhandledValidationError("B404", "Blog Not Found")
            };
        }
        Expression<Func<LocalizedProperty, bool>> predicate = property =>
            property.EntityId == request.Id &&
            property.LocalKey == "Name" &&
            property.LocalKeyGroup == LocalKeyGroupType.BlogCategory;

        await _localizedPropertyService.BulkRemove(predicate,cancellationToken);

        var localizedNames = request.LocalizedNames.Select(name => new LocalizedProperty
        {
            EntityId = request.Id,
            Language = name.Language,
            LocalKey = nameof(name.Name),
            LocalKeyGroup = LocalKeyGroupType.BlogCategory,
            LocalValue = name.Name
        }).ToList();

        await _localizedPropertyService.BulkInsertAsync(localizedNames, cancellationToken: cancellationToken);
        return new UpdateBlogCategoryCommandRes();
    }
}