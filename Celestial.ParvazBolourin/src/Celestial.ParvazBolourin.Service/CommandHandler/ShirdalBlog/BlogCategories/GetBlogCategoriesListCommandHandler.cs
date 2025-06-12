using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog.BlogCategories;

public sealed class GetBlogTagsListCommandHandler(
    IBlogCategoryService categoryService,
    ILocalizedPropertyService localizedPropertyService)
    : IRequestHandler<GetBlogCategoryListCommandReq, GetBlogCategoryListCommandRes>
{
    public async Task<GetBlogCategoryListCommandRes> Handle(GetBlogCategoryListCommandReq request, CancellationToken cancellationToken)
    {
        var categories = await categoryService.GetBlogCategoriesList(cancellationToken);
        if (categories.Count == 0)
        {
            return new()
            {
                Error = ValidationError.UnhandledValidationError("B404", "Blog Not Found")
            };
        }


        var Data = new List<BlogCategoryListItemDto>();

        foreach (var item in categories)
        {
            var localize = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = item.Id,
                    LanguageId = request.LanguageId,
                    LocalKeyGroup = LocalKeyGroupType.BlogCategory
                }, cancellationToken);
            Data.Add(new BlogCategoryListItemDto()
            {
                Id = item.Id,
                Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? "",
                Slug = localize.FirstOrDefault(p => p.LocalKey == "Slug")?.LocalValue ?? ""
            });
        }

        return new()
        {
            Items = Data
        };
    }
}
