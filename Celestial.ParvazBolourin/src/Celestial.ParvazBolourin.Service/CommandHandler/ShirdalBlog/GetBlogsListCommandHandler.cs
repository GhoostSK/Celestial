using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog;

public sealed class GetBlogsListCommandHandler : BaseRequestHandler<GetBlogsListCommandReq, GetBlogsListCommandRes>
{
    private readonly IBlogService _blogService;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;

    public GetBlogsListCommandHandler(ILocalizedPropertyService localizedProperty,
        ILanguageService languageService, IBlogService blogService)
    {
        _localizedPropertyService = localizedProperty;
        _languageService = languageService;
        _blogService = blogService;
    }

    protected override async Task<GetBlogsListCommandRes> HandleCore(GetBlogsListCommandReq request, CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
        {
            return new GetBlogsListCommandRes()
            {
                Error = new ValidationError("LA400", "invalid Language")
            };
        }

        var blogs = await _blogService.GetBlogsList(cancellationToken);
        var response = new GetBlogsListCommandRes();
        foreach (var blog in blogs)
        {
            var localize = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq()
                {
                    EntityId = blog.Id,
                    LanguageId = request.LanguageId,
                    LocalKeyGroup = "Blog",
                }, cancellationToken);
            var tags = new List<string>();


            response.Items.Add(new BlogItem()
            {
                HeaderText = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogItem.HeaderText))?.LocalValue!,
                Title = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogItem.Title))?.LocalValue!,
                PicturePath = blog.PicturePath,
                Likes = blog.Likes,
                Id = blog.Id
            });


        }

        return response;
    }
}