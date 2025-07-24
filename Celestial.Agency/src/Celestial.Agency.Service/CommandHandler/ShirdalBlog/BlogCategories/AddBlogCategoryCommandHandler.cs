using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.ShirdalBlog.BlogCategories;

public sealed class
    AddBlogCategoryCommandHandler : IRequestHandler<AddBlogCategoryCommandReq, AddBlogCategoryCommandRes>
{
    private readonly IBlogCategoryService _categoryService;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;

    public AddBlogCategoryCommandHandler(
        IBlogCategoryService categoryService,
        ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService)
    {
        _categoryService = categoryService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    public async Task<AddBlogCategoryCommandRes> Handle(AddBlogCategoryCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        if (request.LocalizedNames.Any(item =>
                languages.Any(lang => lang.LanguageCulture.Split('-')[0] == item.Language)))
            return new AddBlogCategoryCommandRes
            {
                Error = new ValidationError("LA400", "Invalid Language")
            };

        foreach (var localizeSlug in request.LocalizedNames)
        {
            var existingSlug = await _localizedPropertyService.FirstOrDefaultAsync(
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
        await _categoryService.AddAsync(entity, cancellationToken);
        await _categoryService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var localizedProperties = request.LocalizedNames.SelectMany(name => new List<LocalizedProperty>
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

        await _localizedPropertyService.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);

        return new AddBlogCategoryCommandRes();
    }
}