using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Blog;

public sealed class GetBlogByIdCommandHandler(
    IBlogService blogService,
    ILocalizedPropertyService localizedPropertyService,
    IBlogCategoryService blogCategoryService,
    IBlogTagService blogTagService)
    : BaseRequestHandler<GetBlogCommandReq, GetBlogByIdCommandRes>
{
    protected override async Task<GetBlogByIdCommandRes> HandleCore(GetBlogCommandReq request,
        CancellationToken cancellationToken)
    {
        Core.DAL.Model.ShirdalBlog.Blog? blog;
        var localizedProperty = await localizedPropertyService.FirstOrDefaultAsync(p =>
                p.LocalKey == "Slug" && p.LocalValue == request.Slug && p.LocalKeyGroup == LocalKeyGroupType.Blog,
            cancellationToken: cancellationToken);

        if (localizedProperty == null)
            return new GetBlogByIdCommandRes
            {
                Error = new ValidationError("404", "Blog not found with slug")
            };

        blog = await blogService.GetBlogById(localizedProperty.EntityId, cancellationToken);
        blog!.Id = localizedProperty.EntityId;

        if (blog == null)
            return new GetBlogByIdCommandRes
            {
                Error = ValidationError.UnhandledValidationError("406", "Blog Not Found")
            };

        var response = blog.Adapt<GetBlogByIdCommandRes>();

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
            return new GetBlogByIdCommandRes
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
        response.Title = localizedProperties.FirstOrDefault(p => p.LocalKey == "Title")?.LocalValue ?? string.Empty;
        response.HeaderText = localizedProperties.FirstOrDefault(p => p.LocalKey == "HeaderText")?.LocalValue ??
                              string.Empty;
        response.MainText = localizedProperties.FirstOrDefault(p => p.LocalKey == "MainText")?.LocalValue ??
                            string.Empty;
        response.Slug = localizedProperties.FirstOrDefault(p => p.LocalKey == "Slug")?.LocalValue ?? string.Empty;
        response.MetaTitle = localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaTitle")?.LocalValue ??
                             string.Empty;
        response.MetaKeywords = localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaKeywords")?.LocalValue ??
                                string.Empty;
        response.MetaDescription =
            localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaDescription")?.LocalValue ?? string.Empty;

        response.BlogCategory = blogCategory;
        response.BlogTags = tags;
        response.RegistrationDate = blog.RegistrationDate;
        response.CreatedBy = "meysam peyghamy";
        response.CreatedByProfilePicture = "images/Profile/sample-User.jpg";
        return response;
    }
}