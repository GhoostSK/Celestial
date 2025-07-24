using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog;
using Celestial.Agency.Service.Command.ValidateLanguage;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.ShirdalBlog;

public sealed class AddBlogCommandHandler(
    ILocalizedPropertyService localizedProperty,
    IMediator mediator,
    IBlogService blogService,
    IBlogCategoryService blogCategoryService,
    IBlogTagService blogTagService)
    : BaseRequestHandler<AddBlogCommandReq, AddBlogCommandRes>
{
    protected override async Task<AddBlogCommandRes> HandleCore(AddBlogCommandReq request,
        CancellationToken cancellationToken)
    {
        var langs = new List<string>();
        foreach (var lang in request.BlogLocalizedProperties)
        {
            lang.Language = lang.Language?.ToLowerInvariant()!;
            langs.Add(lang.Language);
        }

        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = langs }, cancellationToken);
        if (validateLanguage.HasError)
            return new AddBlogCommandRes
            {
                Error = validateLanguage.Error
            };
        foreach (var localizeSlug in request.BlogLocalizedProperties)
        {
            var checkSlug = await localizedProperty.FirstOrDefaultAsync(
                p => p.LocalKeyGroup == LocalKeyGroupType.Blog && p.LocalKey == "Slug" &&
                     p.LocalValue == localizeSlug.Slug && p.Language == localizeSlug.Language,
                cancellationToken: cancellationToken);
            if (checkSlug != null)
                return new AddBlogCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("S403",
                        $"Slug Already exist({checkSlug.Language})")
                };
        }

        foreach (var catId in request.BlogCategoryIds)
        {
            var category = await blogCategoryService.FindByIdAsync(cancellationToken, catId);
            if (category == null)
                return new AddBlogCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("C404", "Invalid Category")
                };
        }

        foreach (var tagId in request.BlogTagIds)
        {
            var tag = await blogTagService.FindByIdAsync(cancellationToken, tagId);
            if (tag == null)
                return new AddBlogCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("C404", "Invalid Tag")
                };
        }

        var blog = new Blog
        {
            PicturePath = request.PicturePath,
            CategoriesId = request.BlogCategoryIds,
            TagsId = request.BlogTagIds,
            RegistrationDate = DateTime.Now
        };

        await blogService.AddBlog(blog, cancellationToken);

        var localizedData = new List<LocalizedProperty>();

        foreach (var item in request.BlogLocalizedProperties)
            localizedData.AddRange(new[]
            {
                new LocalizedProperty
                {
                    Language = item.Language,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.Title),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.Title
                },
                new LocalizedProperty
                {
                    Language = item.Language,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.HeaderText),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.HeaderText
                },
                new LocalizedProperty
                {
                    Language = item.Language,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MainText),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.MainText
                },
                new LocalizedProperty
                {
                    Language = item.Language,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.Slug),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.Slug
                },
                new LocalizedProperty
                {
                    Language = item.Language,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MetaKeywords),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.MetaKeywords
                },
                new LocalizedProperty
                {
                    Language = item.Language,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MetaDescription),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.MetaDescription
                },
                new LocalizedProperty
                {
                    Language = item.Language,
                    EntityId = blog.Id,
                    LocalKey = nameof(item.MetaTitle),
                    LocalKeyGroup = LocalKeyGroupType.Blog,
                    LocalValue = item.MetaTitle
                }
            });

        await localizedProperty.BulkInsertAsync(localizedData, cancellationToken: cancellationToken);

        return new AddBlogCommandRes();
    }
}