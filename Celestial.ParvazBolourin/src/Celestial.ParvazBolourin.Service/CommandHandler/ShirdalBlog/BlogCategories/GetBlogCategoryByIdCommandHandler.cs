using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog.BlogCategories;

public sealed class GetBlogCategoryByIdCommandHandler : IRequestHandler<GetBlogCategoryByIdCommandReq, GetBlogCategoryByIdCommandRes>
{
    private readonly IBlogCategoryService _categoryService;
    private readonly ILocalizedPropertyService _localizedPropertyService;

    public GetBlogCategoryByIdCommandHandler(IBlogCategoryService categoryService, ILocalizedPropertyService localizedPropertyService)
    {
        _categoryService = categoryService;
        _localizedPropertyService = localizedPropertyService;
    }

    public async Task<GetBlogCategoryByIdCommandRes> Handle(GetBlogCategoryByIdCommandReq request, CancellationToken cancellationToken)
    {
        var entity = await _categoryService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null) return null;

        var localize = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(new GetLocalizedPropertyReq()
        {
            EntityId = request.Id,
            LanguageId = request.LanguageId,
            LocalKeyGroup = "BlogCategory"
        }, cancellationToken);

        return new()
        {
            Id = request.Id,
            Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? ""
        };
    }
}