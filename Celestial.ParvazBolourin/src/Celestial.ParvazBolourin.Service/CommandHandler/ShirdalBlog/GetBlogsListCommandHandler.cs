using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog;

public sealed class GetBlogsListCommandHandler(
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService,
    IBlogService blogService,
    IBlogTagService blogTagService,
    IBlogCategoryService blogCategoryService)
    : BaseRequestHandler<GetBlogsListCommandReq, GetBlogsListCommandRes>
{
    private readonly IBlogCategoryService _blogCategoryService = blogCategoryService;

    protected override async Task<GetBlogsListCommandRes> HandleCore(GetBlogsListCommandReq request, CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
        {
            return new GetBlogsListCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };
        }

        var blogTags = new List<BlogTagDto>();

        var popularTag = await blogTagService.GetTopTagsByViewCountAsync(request.PopularTagCount, cancellationToken);
        foreach (var tag in popularTag)
        {
            var localizeTag = await localizedPropertyService.FirstOrDefaultAsync(p => p.EntityId == tag.Id && p.LocalKeyGroup == LocalKeyGroupType.BlogTag && p.LocalKey == "Name" && p.LanguageId == request.LanguageId, cancellationToken: cancellationToken);
            var localizeSlug = await localizedPropertyService.FirstOrDefaultAsync(p => p.EntityId == tag.Id && p.LocalKeyGroup == LocalKeyGroupType.BlogTag && p.LocalKey == "Slug" && p.LanguageId == request.LanguageId, cancellationToken: cancellationToken);

            blogTags.Add(new()
            {
                Id = tag.Id,
                Name = localizeTag?.LocalValue ?? string.Empty,
                Slug = localizeSlug?.LocalValue ?? string.Empty
            });
        }

        var blogs = await blogService.GetBlogsList(request.CategoriesId, request.TagsId, request.PageNumber, request.PageSize, cancellationToken);

        var response = new GetBlogsListCommandRes();
        foreach (var blog in blogs.Items)
        {
            var localize = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = blog.Id,
                    LanguageId = request.LanguageId,
                    LocalKeyGroup = LocalKeyGroupType.Blog
                }, cancellationToken);
            var Tag = new List<Tag>();
            foreach (var TagId in blog.TagsId)
            {
                var Title = await localizedPropertyService.FirstOrDefaultAsync(p => p.LocalKeyGroup == LocalKeyGroupType.BlogTag && p.LocalKey == "Name" && p.EntityId == TagId, cancellationToken: cancellationToken);
                var Slug = await localizedPropertyService.FirstOrDefaultAsync(p => p.LocalKeyGroup == LocalKeyGroupType.BlogTag && p.LocalKey == "Slug" && p.EntityId == TagId, cancellationToken: cancellationToken);
                Tag.Add(new()
                {
                    Title = Title!.LocalValue ?? string.Empty,
                    Slug = Slug!.LocalValue ?? string.Empty
                });
            }

            var BlogCategory = new List<Categories>();
            foreach (var categoryId in blog.CategoriesId)
            {
                var Title = await localizedPropertyService.FirstOrDefaultAsync(p => p.LocalKeyGroup == LocalKeyGroupType.BlogCategory && p.LocalKey == "Name" && p.EntityId == categoryId, cancellationToken: cancellationToken);
                var Slug = await localizedPropertyService.FirstOrDefaultAsync(p => p.LocalKeyGroup == LocalKeyGroupType.BlogCategory && p.LocalKey == "Slug" && p.EntityId == categoryId, cancellationToken: cancellationToken);
                BlogCategory.Add(new()
                {
                    Title = Title!.LocalValue ?? string.Empty,
                    Slug = Slug!.LocalValue ?? string.Empty
                });
            }

            response.Items.Add(new BlogItem
            {
                Id = blog.Id,
                PicturePath = blog.PicturePath,
                Likes = blog.Likes,
                HeaderText = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogItem.HeaderText))?.LocalValue ?? "",
                Title = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogItem.Title))?.LocalValue ?? "",
                Slug = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogItem.Slug))?.LocalValue ?? "",
                Categories = BlogCategory,
                Tags = Tag,
                RegistrationDate = blog.RegistrationDate,
                CreatedBy = "meysam Peyghamy",
                CreatedByProfilePicture = "images/Profile/sample-User.jpg"
            });
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            var filtredData = response.Items.Where(p => p.Title.Contains(request.Title)).ToList();
            response.Items = filtredData;

        }
        response.BlogTagsList = blogTags;
        response.Pagination = new()
        {
            TotalRecords = blogs.totalRecord,
            CurrentPage = request.PageNumber,
            TotalPages = blogs.TotalPages,
            PrevPage = request.PageNumber > 1 ? request.PageNumber - 1 : null,
            NextPage = request.PageNumber < blogs.TotalPages ? request.PageNumber + 1 : null
        };

        return response;
    }
}
