using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog.BlogCategories;

public sealed class AddBlogCategoryCommandHandler : IRequestHandler<AddBlogCategoryCommandReq, AddBlogCategoryCommandRes>
{
    private readonly IBlogCategoryService _categoryService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly ILanguageService _languageService;

    public AddBlogCategoryCommandHandler(
        IBlogCategoryService categoryService,
        ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService)
    {
        _categoryService = categoryService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    public async Task<AddBlogCategoryCommandRes> Handle(AddBlogCategoryCommandReq request, CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        if (request.LocalizedNames.Any(item => !languages.Any(lang => lang.Id == item.LanguageId)))
        {
            return new AddBlogCategoryCommandRes
            {
                Error = new ValidationError("LA400", "Invalid Language")
            };
        }

        foreach (var localizeSlug in request.LocalizedNames)
        {
            var existingSlug = await _localizedPropertyService.FirstOrDefaultAsync(
                p => p.LocalKeyGroup == LocalKeyGroupType.BlogCategory &&
                     p.LocalKey == "Slug" &&
                     p.LocalValue == localizeSlug.Slug &&
                     p.LanguageId == localizeSlug.LanguageId,
                cancellationToken: cancellationToken);

            if (existingSlug != null)
            {
                return new AddBlogCategoryCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("S403", $"Slug Already exist ({existingSlug.LanguageId})")
                };
            }
        }

        var entity = new BlogCategory();
        await _categoryService.AddAsync(entity, cancellationToken);
        await _categoryService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var localizedProperties = request.LocalizedNames.SelectMany(name => new List<LocalizedProperty>
        {
            new LocalizedProperty
            {
                EntityId = entity.Id,
                LanguageId = name.LanguageId,
                LocalKey = nameof(name.Name),
                LocalKeyGroup = LocalKeyGroupType.BlogCategory,
                LocalValue = name.Name
            },
            new LocalizedProperty
            {
                EntityId = entity.Id,
                LanguageId = name.LanguageId,
                LocalKey = nameof(name.Slug),
                LocalKeyGroup = LocalKeyGroupType.BlogCategory,
                LocalValue = name.Slug
            }
        }).ToList();

        await _localizedPropertyService.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);

        return new AddBlogCategoryCommandRes();
    }
}
