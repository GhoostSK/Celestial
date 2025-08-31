using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;
using Celestial.Agency.Service.Command.ValidateLanguage;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Blog.BlogTag;

public sealed class GetBlogTagsListCommandHandler(
    IBlogTagService blogTagService,
    ILocalizedPropertyService localizedPropertyService,
    IMediator mediator)
    : IRequestHandler<GetBlogTagListCommandReq, GetBlogTagListCommandRes>
{
    public async Task<GetBlogTagListCommandRes> Handle(GetBlogTagListCommandReq request,
        CancellationToken cancellationToken)
    {
        var langs = new List<string> { request.Language };
        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = langs }, cancellationToken);
        if (validateLanguage.HasError)
            return new GetBlogTagListCommandRes
            {
                Error = validateLanguage.Error
            };

        var tags = await blogTagService.GetTagsList(cancellationToken);
        if (tags.Count == 0)
            return new GetBlogTagListCommandRes
            {
                Error = ValidationError.UnhandledValidationError("BT404", "Tag Not Found")
            };

        var data = new List<BlogTagListItemDto>();
        foreach (var item in tags)
        {
            var localize = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = item.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.BlogTag
                }, cancellationToken);

            data.Add(new BlogTagListItemDto
            {
                Id = item.Id,
                Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? "",
                Slug = localize.FirstOrDefault(p => p.LocalKey == "Slug")?.LocalValue ?? ""
            });
        }

        return new GetBlogTagListCommandRes
        {
            Items = data
        };
    }
}