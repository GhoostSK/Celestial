using System.Linq.Expressions;
using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.ShirdalBlog.BlogCategories;

public sealed class UpdateBlogCategoryCommandHandler(
    IBlogCategoryService categoryService,
    ILocalizedPropertyService localizedPropertyService)
    : IRequestHandler<UpdateBlogCategoryCommandReq, UpdateBlogCategoryCommandRes>
{
    public async Task<UpdateBlogCategoryCommandRes> Handle(UpdateBlogCategoryCommandReq request,
        CancellationToken cancellationToken)
    {
        var entity = await categoryService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null)
            return new UpdateBlogCategoryCommandRes
            {
                Error = ValidationError.UnhandledValidationError("B404", "Blog Not Found")
            };
        Expression<Func<LocalizedProperty, bool>> predicate = property =>
            property.EntityId == request.Id &&
            property.LocalKey == "Name" &&
            property.LocalKeyGroup == LocalKeyGroupType.BlogCategory;

        await localizedPropertyService.BulkRemove(predicate, cancellationToken);

        var localizedNames = request.LocalizedNames.Select(name => new LocalizedProperty
        {
            EntityId = request.Id,
            Language = name.Language,
            LocalKey = nameof(name.Name),
            LocalKeyGroup = LocalKeyGroupType.BlogCategory,
            LocalValue = name.Name
        }).ToList();

        await localizedPropertyService.BulkInsertAsync(localizedNames, cancellationToken: cancellationToken);
        return new UpdateBlogCategoryCommandRes();
    }
}