using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.ShirdalBlog.BlogTag;

public sealed class GetBlogTagsListCommandHandler(
    IBlogTagService blogTagService,
    ILocalizedPropertyService localizedPropertyService)
    : IRequestHandler<GetBlogTagListCommandReq, GetBlogTagListCommandRes>
{
    public async Task<GetBlogTagListCommandRes> Handle(GetBlogTagListCommandReq request,
        CancellationToken cancellationToken)
    {
        var categories = await blogTagService.GetTagsList(cancellationToken);
        if (categories.Count == 0)
            return new GetBlogTagListCommandRes
            {
                Error = ValidationError.UnhandledValidationError("BT404", "Tag Not Found")
            };

        var Data = new List<BlogTagListItemDto>();
        foreach (var item in categories)
        {
            var localize = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = item.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.BlogTag
                }, cancellationToken);
            Data.Add(new BlogTagListItemDto
            {
                Id = item.Id,
                Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? "",
                Slug = localize.FirstOrDefault(p => p.LocalKey == "Slug")?.LocalValue ?? ""
            });
        }

        return new GetBlogTagListCommandRes
        {
            Items = Data
        };
    }
}