using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.ShirdalBlog.BlogTag;

public sealed class GetBlogTagByIdCommandHandler : IRequestHandler<GetBlogTagByIdCommandReq, GetBlogTagByIdCommandRes>
{
    private readonly IBlogTagService _blogTagService;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;

    public GetBlogTagByIdCommandHandler(IBlogTagService blogTagService,
        ILocalizedPropertyService localizedPropertyService, ILanguageService languageService)
    {
        _blogTagService = blogTagService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    public async Task<GetBlogTagByIdCommandRes> Handle(GetBlogTagByIdCommandReq request,
        CancellationToken cancellationToken)
    {
        var entity = new BlogTags();

        if (request.Id != 0)
        {
            var languages = await _languageService.GetLanguages(cancellationToken);
            if (languages.Any(p => p.LanguageCulture.Split('-')[0] == request.Language))
                return new GetBlogTagByIdCommandRes
                {
                    Error = new ValidationError("LA400", "invalid Language")
                };
            entity = await _blogTagService.FindByIdAsync(cancellationToken, request.Id);
            if (entity == null)
                return new GetBlogTagByIdCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("T404", "Tag not Found")
                };
        }
        else if (!string.IsNullOrEmpty(request.Slug))
        {
            var localizedProperty = await _localizedPropertyService.FirstOrDefaultAsync(p =>
                    p.LocalKey == "Slug" && p.LocalValue == request.Slug &&
                    p.LocalKeyGroup == LocalKeyGroupType.BlogTag,
                cancellationToken: cancellationToken);

            if (localizedProperty == null)
                return new GetBlogTagByIdCommandRes
                {
                    Error = new ValidationError("404", "Blog not found with slug")
                };

            entity = await _blogTagService.FindByIdAsync(cancellationToken, localizedProperty.EntityId);
            request.Language = localizedProperty.Language;
            request.Id = localizedProperty.EntityId;
        }

        var localize = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(new GetLocalizedPropertyReq
        {
            EntityId = request.Id,
            Language = request.Language,
            LocalKeyGroup = LocalKeyGroupType.BlogTag
        }, cancellationToken);

        return new()
        {
            Id = request.Id,
            Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? ""
        };
    }
}