using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Blog.BlogCategories;

public sealed class GetBlogTagsListCommandHandler(
    IBlogCategoryService categoryService,
    ILocalizedPropertyService localizedPropertyService)
    : IRequestHandler<GetBlogCategoryListCommandReq, GetBlogCategoryListCommandRes>
{
    public async Task<GetBlogCategoryListCommandRes> Handle(
        GetBlogCategoryListCommandReq request,
        CancellationToken cancellationToken)
    {
        var categories = await categoryService.GetBlogCategoriesList(request.Count, cancellationToken);

        if (categories.Count == 0)
            return new GetBlogCategoryListCommandRes
            {
                Error = ValidationError.UnhandledValidationError("B404", "Blog Not Found")
            };


        var data = new List<BlogCategoryItemDto>();

        foreach (var item in categories)
        {
            var localize = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = item.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.BlogCategory
                },
                cancellationToken);

            data.Add(new BlogCategoryItemDto
            {
                Id = item.Id,
                Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? string.Empty,
                Slug = localize.FirstOrDefault(p => p.LocalKey == "Slug")?.LocalValue ?? string.Empty
            });
        }

        return new GetBlogCategoryListCommandRes
        {
            Items = data,
            ReturnedCount = data.Count
        };
    }
}