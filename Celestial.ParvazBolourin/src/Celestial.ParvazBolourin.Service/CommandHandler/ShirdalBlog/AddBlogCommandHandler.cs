using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;
using System.Reflection.Metadata;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog;

public sealed class AddBlogCommandHandler(
    ILocalizedPropertyService localizedProperty,
    ILanguageService languageService,
    IBlogService blogService,
    IBlogCategoryService blogCategoryService,
    IBlogTagService blogTagService)
    : BaseRequestHandler<AddBlogCommandReq, AddBlogCommandRes>
{
    protected override async Task<AddBlogCommandRes> HandleCore(AddBlogCommandReq request, CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);

        if (request.BlogLocalizedProperties.Any(x => !languages.Any(lang => lang.Id == x.LanguageId)))
        {
            return new AddBlogCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };
        }
        foreach (var localizeSlug in request.BlogLocalizedProperties)
        {
            var checkSlug = await localizedProperty.FirstOrDefaultAsync(p => p.LocalKeyGroup == LocalKeyGroupType.Blog && p.LocalKey == "Slug" && p.LocalValue == localizeSlug.Slug && p.LanguageId == localizeSlug.LanguageId, cancellationToken: cancellationToken);
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
            var category = await blogCategoryService.FindByIdAsync(cancellationToken, catId);
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
            var tag = await blogTagService.FindByIdAsync(cancellationToken, tagId);
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



        await blogService.AddBlog(blog, cancellationToken);

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
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.Title
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.HeaderText),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.HeaderText
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MainText),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.MainText
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.Slug),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.Slug
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MetaKeywords),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.MetaKeywords
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MetaDescription),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.MetaDescription
                },
                new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MetaTitle),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.MetaTitle
                }
            });
        }


        await localizedProperty.BulkInsertAsync(localizedData, cancellationToken: cancellationToken);

        return new AddBlogCommandRes();
    }
}
