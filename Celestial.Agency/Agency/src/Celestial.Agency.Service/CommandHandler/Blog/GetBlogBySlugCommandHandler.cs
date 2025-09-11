using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Blog;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Blog;

public sealed class GetBlogBySlugCommandHandler(
    IBlogService blogService,
    ILocalizedPropertyService localizedPropertyService,
    IBlogCategoryService blogCategoryService,
    IBlogTagService blogTagService)
    : BaseRequestHandler<GetBlogBySlugCommandReq, GetBlogBySlugCommandRes>
{
    protected override async Task<GetBlogBySlugCommandRes> HandleCore(GetBlogBySlugCommandReq request,
        CancellationToken cancellationToken)
    {
        Core.DAL.Model.ShirdalBlog.Blog? blog;
        var localizedProperty = await localizedPropertyService.FirstOrDefaultAsync(p =>
                p.LocalKey == "Slug" && p.LocalValue == request.Slug && p.LocalKeyGroup == LocalKeyGroupType.Blog,
            cancellationToken: cancellationToken);

        if (localizedProperty == null)
            return new GetBlogBySlugCommandRes
            {
                Error = new ValidationError("404", "Blog not found with slug")
            };

        blog = await blogService.GetBlogById(localizedProperty.EntityId, cancellationToken);
        blog!.Id = localizedProperty.EntityId;

        if (blog == null)
            return new GetBlogBySlugCommandRes
            {
                Error = ValidationError.UnhandledValidationError("406", "Blog Not Found")
            };

        var response = new GetBlogBySlugCommandRes
        {
            Item = blog.Adapt<BlogItem>()
        };

        var localizedProperties = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
            new GetLocalizedPropertyReq
            {
                EntityId = blog.Id,
                Language = localizedProperty.Language,
                LocalKeyGroup = LocalKeyGroupType.Blog
            },
            cancellationToken
        );

        if (localizedProperties.Count == 0)
            return new GetBlogBySlugCommandRes
            {
                Error = new ValidationError("500", "Language not found")
            };


        var category = await blogCategoryService.FindByIdAsync(cancellationToken, blog.CategoryId);
        var categoryName = await localizedPropertyService.FirstOrDefaultAsync(
            p => p.EntityId == category.Id && p.Language == localizedProperty.Language && p.LocalKey == "Name" &&
                 p.LocalKeyGroup == LocalKeyGroupType.BlogCategory, cancellationToken: cancellationToken);
        var Slug = await localizedPropertyService.FirstOrDefaultAsync(
            p => p.EntityId == category.Id && p.Language == localizedProperty.Language && p.LocalKey == "Name" &&
                 p.LocalKeyGroup == LocalKeyGroupType.BlogCategory, cancellationToken: cancellationToken);
        var blogCategory = new BlogCategoryDto
        {
            Id = category.Id,
            Name = categoryName?.LocalValue ?? string.Empty,
            Slug = Slug?.LocalValue ?? string.Empty
        };


        var tags = new List<BlogTagDto>();

        foreach (var catId in blog.TagsId)
        {
            var tag = await blogTagService.FindByIdAsync(cancellationToken, catId);
            tag.ViewCount++;
            var tagName = await localizedPropertyService.FirstOrDefaultAsync(
                p => p.EntityId == tag.Id && p.Language == localizedProperty.Language && p.LocalKey == "Name" &&
                     p.LocalKeyGroup == LocalKeyGroupType.BlogTag, cancellationToken: cancellationToken);

            var slug = await localizedPropertyService.FirstOrDefaultAsync(
                p => p.EntityId == tag.Id && p.Language == localizedProperty.Language && p.LocalKey == "Slug" &&
                     p.LocalKeyGroup == LocalKeyGroupType.BlogTag, cancellationToken: cancellationToken);
            tags.Add(new BlogTagDto
            {
                Id = tag.Id,
                Name = tagName?.LocalValue ?? string.Empty,
                Slug = slug?.LocalValue ?? string.Empty
            });
        }

        await blogTagService.UnitOfWork.SaveChangesAsync(cancellationToken);
        response.Item.Title =
            localizedProperties.FirstOrDefault(p => p.LocalKey == "Title")?.LocalValue ?? string.Empty;
        response.Item.HeaderText = localizedProperties.FirstOrDefault(p => p.LocalKey == "HeaderText")?.LocalValue ??
                                   string.Empty;
        response.Item.MainText = localizedProperties.FirstOrDefault(p => p.LocalKey == "MainText")?.LocalValue ??
                                 string.Empty;
        response.Item.Slug = localizedProperties.FirstOrDefault(p => p.LocalKey == "Slug")?.LocalValue ?? string.Empty;
        response.Item.MetaTitle = localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaTitle")?.LocalValue ??
                                  string.Empty;
        response.Item.MetaKeywords =
            localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaKeywords")?.LocalValue ??
            string.Empty;
        response.Item.MetaDescription =
            localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaDescription")?.LocalValue ?? string.Empty;

        response.Item.Category = blogCategory;
        response.Item.Tags = tags;
        //response.Item.RegistrationDate = blog.RegistrationDate;
        //response.Item.CreatedBy = "meysam peyghamy";
        //response.Item.CreatedByProfilePicture = "images/Profile/sample-User.jpg";
        //response.Item.PicturePath = blog.PicturePath;
        return response;
    }
}