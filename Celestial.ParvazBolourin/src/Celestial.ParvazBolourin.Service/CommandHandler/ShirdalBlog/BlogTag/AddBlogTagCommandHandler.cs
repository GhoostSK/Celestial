using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogTag;
using Celestial.ParvazBolourin.Service.Command.Tour;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog.BlogTag;

public sealed class AddBlogTagCommandHandler : IRequestHandler<AddBlogTagCommandReq, AddBlogTagCommandRes>
{
    private readonly IBlogTagService _blogtagService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly ILanguageService _languageService;

    public AddBlogTagCommandHandler(IBlogTagService BlogTagService, ILocalizedPropertyService localizedPropertyService, ILanguageService languageService)
    {
        _blogtagService = BlogTagService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    public async Task<AddBlogTagCommandRes> Handle(AddBlogTagCommandReq request, CancellationToken cancellationToken)
    {

        var languages = await _languageService.GetLanguages(cancellationToken);
        if (request.LocalizedNames.Any(item => !languages.Any(p => p.Id == item.LanguageId)))
            return new AddBlogTagCommandRes { Error = new ValidationError("LA400", "Invalid Language") };
        foreach (var localizeSlug in request.LocalizedNames)
        {
            var checkSlug = await _localizedPropertyService.FirstOrDefaultAsync(p => p.LocalKeyGroup == "BlogTag" && p.LocalKey == "Slug" && p.LocalValue == localizeSlug.Slug && p.LanguageId == localizeSlug.LanguageId, cancellationToken: cancellationToken);
            if (checkSlug != null)
            {
                return new AddBlogTagCommandRes()
                {
                    Error = ValidationError.UnhandledValidationError("S403", $"Slug Already exist({checkSlug.LanguageId})")
                };
            }
        }
        var entity = new BlogTags();
        await _blogtagService.AddAsync(entity, cancellationToken);
        await _blogtagService.UnitOfWork.SaveChangesAsync(cancellationToken);



        var localizedProperties = request.LocalizedNames.SelectMany(name => new List<LocalizedProperty>
    {
        new LocalizedProperty
        {
            EntityId = entity.Id,
            LanguageId = name.LanguageId,
            LocalKey = nameof(name.Name),
            LocalKeyGroup = "BlogTag",
            LocalValue = name.Name
        },
        new LocalizedProperty
        {
            EntityId = entity.Id,
            LanguageId = name.LanguageId,
            LocalKey = nameof(name.Slug),
            LocalKeyGroup = "BlogTag",
            LocalValue = name.Slug
        }
    }).ToList();

        await _localizedPropertyService.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);
        return new AddBlogTagCommandRes();
    }

}