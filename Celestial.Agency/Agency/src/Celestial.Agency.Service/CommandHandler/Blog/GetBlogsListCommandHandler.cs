using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Blog;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog;
using Celestial.Agency.Service.Command.ValidateLanguage;
using MediatR;
using MofidApp.SharedKernel.Common.Model;

namespace Celestial.Agency.Service.CommandHandler.Blog;

public sealed class GetBlogsListCommandHandler(
    ILocalizedPropertyService localizedPropertyService,
    IBlogService blogService,
    IBlogTagService blogTagService,
    IBlogCategoryService blogCategoryService,
    IMediator mediator)
    : BaseRequestHandler<GetBlogsListCommandReq, GetBlogsListCommandRes>
{
    private readonly IBlogCategoryService _blogCategoryService = blogCategoryService;

    protected override async Task<GetBlogsListCommandRes> HandleCore(GetBlogsListCommandReq request,
        CancellationToken cancellationToken)
    {
        var langs = new List<string> { request.Language };
        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = langs }, cancellationToken);
        if (validateLanguage.HasError)
            return new GetBlogsListCommandRes
            {
                Error = validateLanguage.Error
            };

        var blogTags = new List<BlogTagDto>();

        var popularTag = await blogTagService.GetTopTagsByViewCountAsync(request.PopularTagCount, cancellationToken);
        foreach (var tag in popularTag)
        {
            var localizeTag = await localizedPropertyService.FirstOrDefaultAsync(
                p => p.EntityId == tag.Id && p.LocalKeyGroup == LocalKeyGroupType.BlogTag && p.LocalKey == "Name" &&
                     p.Language == request.Language, cancellationToken: cancellationToken);
            var localizeSlug = await localizedPropertyService.FirstOrDefaultAsync(
                p => p.EntityId == tag.Id && p.LocalKeyGroup == LocalKeyGroupType.BlogTag && p.LocalKey == "Slug" &&
                     p.Language == request.Language, cancellationToken: cancellationToken);

            blogTags.Add(new BlogTagDto
            {
                Id = tag.Id,
                Name = localizeTag?.LocalValue ?? string.Empty,
                Slug = localizeSlug?.LocalValue ?? string.Empty
            });
        }

        var blogs = await blogService.GetBlogsList(request.CategoryId, request.TagsId, request.PageNumber,
            request.PageSize, cancellationToken);

        var response = new GetBlogsListCommandRes();
        foreach (var blog in blogs.Items)
        {
            var localize = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = blog.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.Blog
                }, cancellationToken);
            var tag = new List<Tag>();
            foreach (var tagId in blog.TagsId)
            {
                var title = await localizedPropertyService.FirstOrDefaultAsync(
                    p => p.LocalKeyGroup == LocalKeyGroupType.BlogTag && p.LocalKey == "Name" && p.EntityId == tagId,
                    cancellationToken: cancellationToken);
                var slug = await localizedPropertyService.FirstOrDefaultAsync(
                    p => p.LocalKeyGroup == LocalKeyGroupType.BlogTag && p.LocalKey == "Slug" && p.EntityId == tagId,
                    cancellationToken: cancellationToken);
                tag.Add(new Tag
                {
                    Title = title!.LocalValue ?? string.Empty,
                    Slug = slug!.LocalValue ?? string.Empty
                });
            }


            var categoryTitle = await localizedPropertyService.FirstOrDefaultAsync(
                p => p.LocalKeyGroup == LocalKeyGroupType.BlogCategory && p.LocalKey == "Name" &&
                     p.EntityId == blog.CategoryId, cancellationToken: cancellationToken);
            var categorySlug = await localizedPropertyService.FirstOrDefaultAsync(
                p => p.LocalKeyGroup == LocalKeyGroupType.BlogCategory && p.LocalKey == "Slug" &&
                     p.EntityId == blog.CategoryId, cancellationToken: cancellationToken);
            var blogCategory = new Category
            {
                Title = categoryTitle!.LocalValue ?? string.Empty,
                Slug = categorySlug!.LocalValue ?? string.Empty
            };


            response.Items.Add(new BlogItem
            {
                Id = blog.Id,
                PicturePath = blog.PicturePath,
                Likes = blog.Likes,
                HeaderText = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogItem.HeaderText))?.LocalValue ?? "",
                Title = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogItem.Title))?.LocalValue ?? "",
                Slug = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogItem.Slug))?.LocalValue ?? "",
                Category = blogCategory,
                Tags = tag,
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
        response.Pagination = new PaginationMeta
        {
            TotalRecords = blogs.TotalRecord,
            CurrentPage = request.PageNumber,
            TotalPages = blogs.TotalPages,
            PrevPage = request.PageNumber > 1 ? request.PageNumber - 1 : null,
            NextPage = request.PageNumber < blogs.TotalPages ? request.PageNumber + 1 : null
        };

        return response;
    }
}