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


    public GetBlogByIdCommandHandler(IBlogService blogService, ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService)
    {
        _blogService = blogService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    protected override async Task<GetBlogByIdCommandRes> HandleCore(GetBlogByIdCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
            return new GetBlogByIdCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };

        var blog = await _blogService.GetBlogById(request.Id, cancellationToken);
        if (blog == null)
            return new GetBlogByIdCommandRes
            {
                Error = new ValidationError("BLOG404", "Blog not found")
            };

        var response = blog.Adapt<GetBlogByIdCommandRes>();

        var localizedProperties = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
            new GetLocalizedPropertyReq
            {
                EntityId = blog.Id,
                LanguageId = request.LanguageId,
                LocalKeyGroup = "Blog"
            },
            cancellationToken
        );

        response.Title = localizedProperties.SingleOrDefault(p => p.LocalKey == "Title")?.LocalValue ?? string.Empty;
        response.HeaderText = localizedProperties.SingleOrDefault(p => p.LocalKey == "HeaderText")?.LocalValue ??
                              string.Empty;
        response.MainText = localizedProperties.SingleOrDefault(p => p.LocalKey == "MainText")?.LocalValue ??
                            string.Empty;
        response.Tags = localizedProperties
            .Where(p => p.LocalKey == "Tags")
            .Select(p => p.LocalValue)
            .ToList();
        response.MetaDescription = localizedProperties.SingleOrDefault(p => p.LocalKey == "MetaDescription")?.LocalValue ?? string.Empty;
        response.MetaKeywords = localizedProperties.SingleOrDefault(p => p.LocalKey == "MetaKeywords")?.LocalValue ?? string.Empty;
        response.MetaTitle = localizedProperties.SingleOrDefault(p => p.LocalKey == "MetaTitle")?.LocalValue ?? string.Empty;
        response.Slug = localizedProperties.SingleOrDefault(p => p.LocalKey == "Slug")?.LocalValue ?? string.Empty;

        return response;
    }
}