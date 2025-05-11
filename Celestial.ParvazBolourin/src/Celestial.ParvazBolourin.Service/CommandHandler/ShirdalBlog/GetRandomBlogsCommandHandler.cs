using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

public sealed class GetTopBlogsCommandHandler : BaseRequestHandler<GetTopBlogsCommandReq, GetTopBlogsCommandRes>
{
    private readonly IBlogService _blogService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly ILanguageService _languageService;

    public GetTopBlogsCommandHandler(
        IBlogService blogService,
        ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService)
    {
        _blogService = blogService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    protected override async Task<GetTopBlogsCommandRes> HandleCore(GetTopBlogsCommandReq request, CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
        {
            return new GetTopBlogsCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };
        }

        var blogs = await _blogService.GetTopBlogsByLikes(request.TopCount, cancellationToken);

        var resList = new List<TopBlogDto>();

        foreach (var blog in blogs)
        {
            var localizedProps = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(new()
            {
                EntityId = blog.Id,
                LanguageId = request.LanguageId,
                LocalKeyGroup = "Blog"
            }, cancellationToken);

            resList.Add(new TopBlogDto
            {
                Id = blog.Id,
                PicturePath = blog.PicturePath,
                Title = localizedProps.FirstOrDefault(p => p.LocalKey == "Title")?.LocalValue ?? string.Empty,
                HeaderText = localizedProps.FirstOrDefault(p => p.LocalKey == "HeaderText")?.LocalValue ?? string.Empty,
                Slug = localizedProps.FirstOrDefault(p => p.LocalKey == "Slug")?.LocalValue ?? string.Empty,
                Tags = localizedProps.Where(p => p.LocalKey == "Tags").Select(p => p.LocalValue).ToList()
            });
        }

        return new GetTopBlogsCommandRes
        {
            Blogs = resList
        };
    }
}
