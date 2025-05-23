using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogTag;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog.BlogTag;

public sealed class GetBlogTagByIdCommandHandler : IRequestHandler<GetBlogTagByIdCommandReq, GetBlogTagByIdCommandRes>
{
    private readonly IBlogTagService _blogTagService;
    private readonly ILocalizedPropertyService _localizedPropertyService;

    public GetBlogTagByIdCommandHandler(IBlogTagService blogTagService, ILocalizedPropertyService localizedPropertyService)
    {
        _blogTagService = blogTagService;
        _localizedPropertyService = localizedPropertyService;
    }

    public async Task<GetBlogTagByIdCommandRes> Handle(GetBlogTagByIdCommandReq request, CancellationToken cancellationToken)
    {
        var entity = await _blogTagService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null) return new GetBlogTagByIdCommandRes()
        {
            Error = ValidationError.UnhandledValidationError("T404", "Tag not Found")
        };

        var localize = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(new GetLocalizedPropertyReq()
        {
            EntityId = request.Id,
            LanguageId = request.LanguageId,
            LocalKeyGroup = "BlogTag"
        }, cancellationToken);

        return new()
        {
            Id = request.Id,
            Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? ""
        };
    }
}