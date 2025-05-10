using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog;

public sealed class AddBlogCommandHandler : BaseRequestHandler<AddBlogCommandReq, AddBlogCommandRes>
{
    private readonly IBlogService _blogService;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedProperty;

    public AddBlogCommandHandler(ILocalizedPropertyService localizedProperty,
        ILanguageService languageService, IBlogService blogService)
    {
        _localizedProperty = localizedProperty;
        _languageService = languageService;
        _blogService = blogService;
    }

    protected override async Task<AddBlogCommandRes> HandleCore(AddBlogCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        foreach (var item in request.BlogLocalizedProperties)
            if (!languages.Any(p => p.Id == item.LanguageId))
                return new AddBlogCommandRes
                {
                    Error = new ValidationError("LA400", "invalid Language")
                };

        var blogId = await _blogService.AddBlog(new Blog
        {
            PicturePath = request.PicturePath
        }, cancellationToken);
        var localizedData = new List<LocalizedProperty>();

        foreach (var item in request.BlogLocalizedProperties)
        {
            localizedData.AddRange([
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.Title),
                    EntityId = blogId,
                    LocalKeyGroup = "Blog",
                    LocalValue = item.Title
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.HeaderText),
                    EntityId = blogId,
                    LocalKeyGroup = "Blog",
                    LocalValue = item.HeaderText
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.MainText),
                    EntityId = blogId,
                    LocalKeyGroup = "Blog",
                    LocalValue = item.MainText
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.Slug),
                    EntityId = blogId,
                    LocalKeyGroup = "Blog",
                    LocalValue = item.Slug
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.MetaKeywords),
                    EntityId = blogId,
                    LocalKeyGroup = "Blog",
                    LocalValue = item.MetaKeywords
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.MetaDescription),
                    EntityId = blogId,
                    LocalKeyGroup = "Blog",
                    LocalValue = item.MetaDescription
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.MetaTitle),
                    EntityId = blogId,
                    LocalKeyGroup = "Blog",
                    LocalValue = item.MetaTitle
                },
            ]);

            foreach (var tag in item.Tags)
                localizedData.Add(new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.Tags),
                    EntityId = blogId,
                    LocalKeyGroup = "Blog",
                    LocalValue = tag
                });
        }

        await _localizedProperty.BulkInsertAsync(localizedData, cancellationToken: cancellationToken);

        return new AddBlogCommandRes();
    }
}