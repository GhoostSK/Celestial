using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog;

public sealed class GetBlogByIdCommandHandler : BaseRequestHandler<GetBlogByIdCommandReq, GetBlogByIdCommandRes>
{
    private readonly IBlogService _blogService;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly IBlogCategoryService _blogCategoryService;
    private readonly IBlogTagService _blogTagService;

    public GetBlogByIdCommandHandler(
        IBlogService blogService,
        ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService, IBlogCategoryService blogCategoryService, IBlogTagService blogTagService)
    {
        _blogService = blogService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
        _blogCategoryService = blogCategoryService;
        _blogTagService = blogTagService;
    }

    protected override async Task<GetBlogByIdCommandRes> HandleCore(GetBlogByIdCommandReq request, CancellationToken cancellationToken)
    {
        var blog = new Blog();

        if (request.Id != 0)
        {
            var languages = await _languageService.GetLanguages(cancellationToken);
            if (!languages.Any(p => p.Id == request.LanguageId))
                return new GetBlogByIdCommandRes
                {
                    Error = new ValidationError("LA400", "invalid Language")
                };
            blog = await _blogService.GetBlogById(request.Id, cancellationToken);
        }
        else if (!string.IsNullOrEmpty(request.Slug))
        {
            var localizedProperty = await _localizedPropertyService.FirstOrDefaultAsync(p =>
                p.LocalKey == "Slug" && p.LocalValue == request.Slug && p.LocalKeyGroup == LocalKeyGroupType.Blog, cancellationToken: cancellationToken);

            if (localizedProperty == null)
                return new GetBlogByIdCommandRes
                {
                    Error = new ValidationError("404", "Blog not found with slug")
                };

            blog = await _blogService.GetBlogById(localizedProperty.EntityId, cancellationToken);
            request.LanguageId = localizedProperty.LanguageId;
            request.Id = localizedProperty.EntityId;
        }

        if (blog == null)
            return new GetBlogByIdCommandRes
            {
                Error = ValidationError.UnhandledValidationError("406", "Blog Not Found")
            };

        var response = blog.Adapt<GetBlogByIdCommandRes>();

        var localizedProperties = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
            new GetLocalizedPropertyReq
            {
                EntityId = request.Id,
                LanguageId = request.LanguageId,
                LocalKeyGroup = LocalKeyGroupType.Blog
            },
            cancellationToken
        );

        if (localizedProperties.Count == 0)
            return new GetBlogByIdCommandRes
            {
                Error = new ValidationError("500", "Language not found")
            };
        var categories = new List<BlogCategoryDto>();
        foreach (var catId in blog.CategoriesId)
        {
            var category = await _blogCategoryService.FindByIdAsync(cancellationToken, catId);
            var categoryName = await _localizedPropertyService.FirstOrDefaultAsync(
                p => p.EntityId == category.Id && p.LanguageId == request.LanguageId && p.LocalKey == "Name" &&
                     p.LocalKeyGroup == LocalKeyGroupType.BlogCategory, cancellationToken: cancellationToken);
            var Slug = await _localizedPropertyService.FirstOrDefaultAsync(
    p => p.EntityId == category.Id && p.LanguageId == request.LanguageId && p.LocalKey == "Name" &&
         p.LocalKeyGroup == LocalKeyGroupType.BlogCategory, cancellationToken: cancellationToken);
            categories.Add(new BlogCategoryDto()
            {
                Id = category.Id,
                Name = categoryName?.LocalValue ?? string.Empty,
                Slug = Slug?.LocalValue ?? string.Empty
            });
        }
        var tags = new List<BlogTagDto>();

        foreach (var catId in blog.TagsId)
        {
            var tag = await _blogTagService.FindByIdAsync(cancellationToken, catId);
            tag.ViewCount++;
            var tagName = await _localizedPropertyService.FirstOrDefaultAsync(
                p => p.EntityId == tag.Id && p.LanguageId == request.LanguageId && p.LocalKey == "Name" &&
                     p.LocalKeyGroup == LocalKeyGroupType.BlogTag, cancellationToken: cancellationToken);

            var slug = await _localizedPropertyService.FirstOrDefaultAsync(
     p => p.EntityId == tag.Id && p.LanguageId == request.LanguageId && p.LocalKey == "Slug" &&
          p.LocalKeyGroup == LocalKeyGroupType.BlogTag, cancellationToken: cancellationToken);
            tags.Add(new BlogTagDto()
            {
                Id = tag.Id,
                Name = tagName?.LocalValue ?? string.Empty,
                Slug = slug?.LocalValue ?? string.Empty

            });
        }
        await _blogTagService.UnitOfWork.SaveChangesAsync(cancellationToken);
        response.Title = localizedProperties.FirstOrDefault(p => p.LocalKey == "Title")?.LocalValue ?? string.Empty;
        response.HeaderText = localizedProperties.FirstOrDefault(p => p.LocalKey == "HeaderText")?.LocalValue ?? string.Empty;
        response.MainText = localizedProperties.FirstOrDefault(p => p.LocalKey == "MainText")?.LocalValue ?? string.Empty;
        response.Slug = localizedProperties.FirstOrDefault(p => p.LocalKey == "Slug")?.LocalValue ?? string.Empty;
        response.MetaTitle = localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaTitle")?.LocalValue ?? string.Empty;
        response.MetaKeywords = localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaKeywords")?.LocalValue ?? string.Empty;
        response.MetaDescription = localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaDescription")?.LocalValue ?? string.Empty;

        response.BlogCategories = categories;
        response.BlogTags = tags;
        response.RegistrationDate = blog.RegistrationDate;
        response.CreatedBy = "meysam peyghamy";
        response.CreatedByProfilePicture = "images/Profile/sample-User.jpg";
        return response;
    }
}
