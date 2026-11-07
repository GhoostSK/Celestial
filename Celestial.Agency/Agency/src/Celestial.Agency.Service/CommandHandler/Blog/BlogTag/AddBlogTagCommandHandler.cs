using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;
using Celestial.SharedKernel.Common;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Blog.BlogTag;

public sealed class AddBlogTagCommandHandler(
    IBlogTagService blogTagService,
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService)
    : IRequestHandler<AddBlogTagCommandReq, AddBlogTagCommandRes>
{
    public async Task<AddBlogTagCommandRes> Handle(AddBlogTagCommandReq request, CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        if (request.LocalizedNames.Any(item => languages.Any(p => p.LanguageCulture.Split('-')[0] == item.Language)))
            return new AddBlogTagCommandRes { Error = new ValidationError("LA400", "Invalid Language") };
        foreach (var localizeSlug in request.LocalizedNames)
        {
            var checkSlug = await localizedPropertyService.FirstOrDefaultAsync(
                p => p.LocalKeyGroup == LocalKeyGroupType.BlogTag && p.LocalKey == "Slug" &&
                     p.LocalValue == localizeSlug.Slug && p.Language == localizeSlug.Language,
                cancellationToken: cancellationToken);
            if (checkSlug != null)
                return new AddBlogTagCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("S403",
                        $"Slug Already exist({checkSlug.Language})")
                };
        }

        var entity = new Core.DAL.Model.ShirdalBlog.BlogTag();
        await blogTagService.AddAsync(entity, cancellationToken);
        await blogTagService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var localizedProperties = request.LocalizedNames.SelectMany(name => new List<LocalizedProperty>
        {
            new()
            {
                EntityId = entity.Id,
                Language = name.Language,
                LocalKey = nameof(name.Name),
                LocalKeyGroup = LocalKeyGroupType.BlogTag,
                LocalValue = name.Name
            },
            new()
            {
                EntityId = entity.Id,
                Language = name.Language,
                LocalKey = nameof(name.Slug),
                LocalKeyGroup = LocalKeyGroupType.BlogTag,
                LocalValue = name.Slug
            }
        }).ToList();

        await localizedPropertyService.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);
        return new AddBlogTagCommandRes();
    }
}