using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;
using Celestial.SharedKernel.Common;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Blog.BlogTag;

public sealed class GetBlogTagBySlugCommandHandler(
    IBlogTagService blogTagService,
    ILocalizedPropertyService localizedPropertyService)
    : IRequestHandler<GetBlogTagBySlugCommandReq, GetBlogTagBySlugCommandRes>
{
    public async Task<GetBlogTagBySlugCommandRes> Handle(GetBlogTagBySlugCommandReq request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.Slug))
            return new GetBlogTagBySlugCommandRes
            {
                Error = new ValidationError("404", "Blog not found with slug")
            };
        var localizedProperty = await localizedPropertyService.FirstOrDefaultAsync(p =>
                p.LocalKey == "Slug" && p.LocalValue == request.Slug &&
                p.LocalKeyGroup == LocalKeyGroupType.BlogTag,
            cancellationToken: cancellationToken);

        if (localizedProperty == null)
            return new GetBlogTagBySlugCommandRes
            {
                Error = new ValidationError("404", "Blog not found with slug")
            };

        await blogTagService.FindByIdAsync(cancellationToken, localizedProperty.EntityId);
        request.Language = localizedProperty.Language;


        var localize = await localizedPropertyService.GetPropertyByLanguageAndEntityId(new GetLocalizedPropertyReq
        {
            EntityId = localizedProperty.EntityId,
            Language = request.Language,
            LocalKeyGroup = LocalKeyGroupType.BlogTag
        }, cancellationToken);

        return new GetBlogTagBySlugCommandRes
        {
            Id = localizedProperty.EntityId,
            Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? ""
        };
    }
}