using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;
using System.Reflection.Metadata;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog;

public sealed class AddBlogCommandHandler : BaseRequestHandler<AddBlogCommandReq, AddBlogCommandRes>
{
    private readonly IBlogService _blogService;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedProperty;
    private readonly IBlogCategoryService _blogCategoryService;
    private readonly IBlogTagService _blogTagService;

    public AddBlogCommandHandler(
        ILocalizedPropertyService localizedProperty,
        ILanguageService languageService,
        IBlogService blogService, IBlogCategoryService blogCategoryService, IBlogTagService blogTagService)
    {
        _localizedProperty = localizedProperty;
        _languageService = languageService;
        _blogService = blogService;
        _blogCategoryService = blogCategoryService;
        _blogTagService = blogTagService;
    }

    protected override async Task<AddBlogCommandRes> HandleCore(AddBlogCommandReq request, CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);

        if (request.BlogLocalizedProperties.Any(x => !languages.Any(lang => lang.Id == x.LanguageId)))
        {
            return new AddBlogCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };
        }
        foreach (var localizeSlug in request.BlogLocalizedProperties)
        {
            var checkSlug = await _localizedProperty.FirstOrDefaultAsync(p => p.LocalKeyGroup == "Blog" && p.LocalKey == "Slug" && p.LocalValue == localizeSlug.Slug && p.LanguageId == localizeSlug.LanguageId, cancellationToken: cancellationToken);
            if (checkSlug != null)
            {
                return new AddBlogCommandRes()
                {
                    Error = ValidationError.UnhandledValidationError("S403", $"Slug Already exist({checkSlug.LanguageId})")
                };
            }
        }
        foreach (var catId in request.BlogCategoryIds)
        {
            var category = await _blogCategoryService.FindByIdAsync(cancellationToken, catId);
            if (category == null)
            {
                return new AddBlogCommandRes()
                {
                    Error = ValidationError.UnhandledValidationError("C404", "Invalid Category")
                };
            }

        }
        foreach (var tagId in request.BlogTagIds)
        {
            var tag = await _blogTagService.FindByIdAsync(cancellationToken, tagId);
            if (tag == null)
            {
                return new AddBlogCommandRes()
                {
                    Error = ValidationError.UnhandledValidationError("C404", $"Invalid Tag")
                };
            }
        }


        var blog = new Blog
        {
            PicturePath = request.PicturePath,
            CategoriesId = request.BlogCategoryIds,
            TagsId = request.BlogTagIds,
            RegistrationDate = DateTime.Now,
        };



        await _blogService.AddBlog(blog, cancellationToken);

        var localizedData = new List<LocalizedProperty>();

        foreach (var item in request.BlogLocalizedProperties)
        {
            localizedData.AddRange(new[]
            {
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.Title),
                    LocalKeyGroup = "Blog",
                    LocalValue = item.Title
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.HeaderText),
                    LocalKeyGroup = "Blog",
                    LocalValue = item.HeaderText
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MainText),
                    LocalKeyGroup = "Blog",
                    LocalValue = item.MainText
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.Slug),
                    LocalKeyGroup = "Blog",
                    LocalValue = item.Slug
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MetaKeywords),
                    LocalKeyGroup = "Blog",
                    LocalValue = item.MetaKeywords
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MetaDescription),
                    LocalKeyGroup = "Blog",
                    LocalValue = item.MetaDescription
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MetaTitle),
                    LocalKeyGroup = "Blog",
                    LocalValue = item.MetaTitle
                }
            });
        }


        await _localizedProperty.BulkInsertAsync(localizedData, cancellationToken: cancellationToken);

        return new AddBlogCommandRes();
    }
}
