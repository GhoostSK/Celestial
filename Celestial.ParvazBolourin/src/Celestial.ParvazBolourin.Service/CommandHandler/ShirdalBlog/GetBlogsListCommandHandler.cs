using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog;

public sealed class GetBlogsListCommandHandler : BaseRequestHandler<GetBlogsListCommandReq, GetBlogsListCommandRes>
{
    private readonly IBlogService _blogService;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly IBlogTagService _blogTagService;

    public GetBlogsListCommandHandler(
        ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService,
        IBlogService blogService,
        IBlogTagService blogTagService)
    {
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
        _blogService = blogService;
        _blogTagService = blogTagService;
    }

    protected override async Task<GetBlogsListCommandRes> HandleCore(GetBlogsListCommandReq request, CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
        {
            return new GetBlogsListCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };
        }

        var blogTags = new List<BlogTagDto>();

        var popularTag = await _blogTagService.GetTopTagsByViewCountAsync(request.PopularTagCount, cancellationToken);
        foreach (var tag in popularTag)
        {
            var localizeTag = await _localizedPropertyService.FirstOrDefaultAsync(p => p.EntityId == tag.Id && p.LocalKeyGroup == "BlogTag" && p.LocalKey == "Name" && p.LanguageId == request.LanguageId);
            blogTags.Add(new()
            {
                Id = tag.Id,
                Name = localizeTag.LocalValue!
            });
        }

        var blogs = await _blogService.GetBlogsList(request.CategoriesId, request.TagsId, cancellationToken);

        var response = new GetBlogsListCommandRes();
        foreach (var blog in blogs)
        {
            var localize = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = blog.Id,
                    LanguageId = request.LanguageId,
                    LocalKeyGroup = "Blog"
                }, cancellationToken);

            response.Items.Add(new BlogItem
            {
                Id = blog.Id,
                PicturePath = blog.PicturePath,
                Likes = blog.Likes,
                HeaderText = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogItem.HeaderText))?.LocalValue ?? "",
                Title = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogItem.Title))?.LocalValue ?? "",
                Slug = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogItem.Slug))?.LocalValue ?? ""
            });
        }
        if (!string.IsNullOrEmpty(request.Title))
        {
            var filtredData = response.Items.Where(p => p.Title.Contains(request.Title)).ToList();
            response.Items = filtredData;

        }
        response.BlogTagsList = blogTags;
        return response;
    }
}
