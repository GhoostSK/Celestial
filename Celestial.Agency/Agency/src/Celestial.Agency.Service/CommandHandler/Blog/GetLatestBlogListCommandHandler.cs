using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Blog;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Service.Command.localization;
using Celestial.Agency.Service.Command.ShirdalBlog;
using MediatR;
using MofidApp.SharedKernel.Common.Model;

namespace Celestial.Agency.Service.CommandHandler.Blog;

public sealed class GetLatestBlogListCommandHandler(
    ILocalizedPropertyService localizedPropertyService,
    IBlogService blogService,
    IMediator mediator)
    : BaseRequestHandler<GetLatestBlogListCommandReq, GetLatestBlogListCommandRes>
{
    protected override async Task<GetLatestBlogListCommandRes> HandleCore(GetLatestBlogListCommandReq request,
        CancellationToken cancellationToken)
    {
        var langs = new List<string> { request.Language };
        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = langs }, cancellationToken);
        if (validateLanguage.HasError)
            return new GetLatestBlogListCommandRes
            {
                Error = validateLanguage.Error
            };

        var blogs = await blogService.GetLatestBlogs(3, cancellationToken);

        var response = new GetLatestBlogListCommandRes();
        foreach (var blog in blogs)
        {
            var localize = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = blog.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.Blog
                }, cancellationToken);


            response.Items.Add(new LatestBlogItem
            {
                Id = blog.Id,
                PicturePath = blog.PicturePath,
                Likes = blog.Likes,
                HeaderText = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogListItem.HeaderText))?.LocalValue ??
                             "",
                Title = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogListItem.Title))?.LocalValue ?? "",
                Slug = localize.FirstOrDefault(p => p.LocalKey == nameof(BlogListItem.Slug))?.LocalValue ?? "",
                RegistrationDate = blog.RegistrationDate,
                CreatedBy = "meysam Peyghamy",
                CreatedByProfilePicture = "images/Profile/sample-User.jpg"
            });
        }


        return response;
    }
}