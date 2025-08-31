using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Blog;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Service.Command.ShirdalBlog;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Blog;

public sealed class GetTopBlogsCommandHandler(
    IBlogService blogService,
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService)
    : BaseRequestHandler<GetTopBlogsCommandReq, GetTopBlogsCommandRes>
{
    protected override async Task<GetTopBlogsCommandRes> HandleCore(GetTopBlogsCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        if (languages.Any(p => p.LanguageCulture.Split('-')[0] == request.Language))
            return new GetTopBlogsCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };

        var blogs = await blogService.GetTopBlogsByLikes(request.TopCount, cancellationToken);

        var resList = new List<TopBlogDto>();

        foreach (var blog in blogs)
        {
            var localizedProps = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = blog.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.Blog
                }, cancellationToken);
            var Tag = new List<Tag>();
            foreach (var TagId in blog.TagsId)
            {
                var title = await localizedPropertyService.FirstOrDefaultAsync(
                    p => p.LocalKeyGroup == LocalKeyGroupType.BlogTag && p.LocalKey == "Name" && p.EntityId == TagId,
                    cancellationToken: cancellationToken);
                var slug = await localizedPropertyService.FirstOrDefaultAsync(
                    p => p.LocalKeyGroup == LocalKeyGroupType.BlogTag && p.LocalKey == "Slug" && p.EntityId == TagId,
                    cancellationToken: cancellationToken);
                Tag.Add(new Tag
                {
                    Title = title?.LocalValue ?? string.Empty,
                    Slug = slug?.LocalValue ?? string.Empty
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
                Title = categoryTitle?.LocalValue ?? string.Empty,
                Slug = categorySlug?.LocalValue ?? string.Empty
            };


            resList.Add(new TopBlogDto
            {
                Id = blog.Id,
                PicturePath = blog.PicturePath,
                Title = localizedProps.FirstOrDefault(p => p.LocalKey == "Title")?.LocalValue ?? string.Empty,
                HeaderText = localizedProps.FirstOrDefault(p => p.LocalKey == "HeaderText")?.LocalValue ?? string.Empty,
                Slug = localizedProps.FirstOrDefault(p => p.LocalKey == "Slug")?.LocalValue ?? string.Empty,
                Tags = Tag,
                Categories = blogCategory,
                RegistrationDate = blog.RegistrationDate,
                CreatedBy = "Admin",
                CreatedByProfilePicture = "images/Profile/sample-User.jpg",
                Likes = blog.Likes
            });
        }

        return new GetTopBlogsCommandRes
        {
            Items = resList
        };
    }
}