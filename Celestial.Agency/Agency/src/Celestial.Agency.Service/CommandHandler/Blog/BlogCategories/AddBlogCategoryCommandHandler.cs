using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.localization;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using Celestial.SharedKernel.Common;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Blog.BlogCategories;

public sealed class
    AddBlogCategoryCommandHandler(
        IBlogCategoryService categoryService,
        ILocalizedPropertyService localizedPropertyService,
        IMediator mediator)
    : IRequestHandler<AddBlogCategoryCommandReq, AddBlogCategoryCommandRes>
{
    public async Task<AddBlogCategoryCommandRes> Handle(AddBlogCategoryCommandReq request,
        CancellationToken cancellationToken)
    {
        var langs = new List<string>();
        foreach (var lang in request.LocalizeProperty)
        {
            lang.Language = lang.Language?.ToLowerInvariant()!;
            langs.Add(lang.Language);
        }

        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = langs }, cancellationToken);
        if (validateLanguage.HasError)
            return new AddBlogCategoryCommandRes
            {
                Error = validateLanguage.Error
            };

        foreach (var localizeSlug in request.LocalizeProperty)
        {
            var existingSlug = await localizedPropertyService.FirstOrDefaultAsync(
                p => p.LocalKeyGroup == LocalKeyGroupType.BlogCategory &&
                     p.LocalKey == "Slug" &&
                     p.LocalValue == localizeSlug.Slug &&
                     p.Language == localizeSlug.Language,
                cancellationToken: cancellationToken);

            if (existingSlug != null)
                return new AddBlogCategoryCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("S403",
                        $"Slug Already exist ({existingSlug.Language})")
                };
        }

        var entity = new BlogCategory();
        await categoryService.AddAsync(entity, cancellationToken);
        await categoryService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var localizedProperties = request.LocalizeProperty.SelectMany(name => new List<LocalizedProperty>
        {
            new()
            {
                EntityId = entity.Id,
                Language = name.Language,
                LocalKey = nameof(name.Name),
                LocalKeyGroup = LocalKeyGroupType.BlogCategory,
                LocalValue = name.Name
            },
            new()
            {
                EntityId = entity.Id,
                Language = name.Language,
                LocalKey = nameof(name.Slug),
                LocalKeyGroup = LocalKeyGroupType.BlogCategory,
                LocalValue = name.Slug
            }
        }).ToList();

        await localizedPropertyService.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);

        return new AddBlogCategoryCommandRes();
    }
}