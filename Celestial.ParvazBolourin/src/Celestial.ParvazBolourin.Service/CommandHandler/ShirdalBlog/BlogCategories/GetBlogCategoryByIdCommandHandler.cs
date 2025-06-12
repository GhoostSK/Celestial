using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogTag;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog.BlogCategories;

public sealed class GetBlogCategoryByIdCommandHandler : IRequestHandler<GetBlogCategoryByIdCommandReq, GetBlogCategoryByIdCommandRes>
{
    private readonly IBlogCategoryService _categoryService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly ILanguageService _languageService;


    public GetBlogCategoryByIdCommandHandler(IBlogCategoryService categoryService, ILocalizedPropertyService localizedPropertyService, ILanguageService languageService)
    {
        _categoryService = categoryService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    public async Task<GetBlogCategoryByIdCommandRes> Handle(GetBlogCategoryByIdCommandReq request, CancellationToken cancellationToken)
    {
        var entity = new BlogCategory();

        if (request.Id != 0)
        {
            var languages = await _languageService.GetLanguages(cancellationToken);
            if (!languages.Any(p => p.Id == request.LanguageId))
                return new GetBlogCategoryByIdCommandRes
                {
                    Error = new ValidationError("LA400", "invalid Language")
                };
            entity = await _categoryService.FindByIdAsync(cancellationToken, request.Id);
            if (entity == null) return new GetBlogCategoryByIdCommandRes()
            {
                Error = ValidationError.UnhandledValidationError("C404", "Category not Found")
            };

        }
        else if (!string.IsNullOrEmpty(request.Slug))
        {
            var localizedProperty = await _localizedPropertyService.FirstOrDefaultAsync(p =>
                p.LocalKey == "Slug" && p.LocalValue == request.Slug && p.LocalKeyGroup == LocalKeyGroupType.BlogCategory, cancellationToken: cancellationToken);

            if (localizedProperty == null)
                return new GetBlogCategoryByIdCommandRes
                {
                    Error = new ValidationError("404", "Blog not found with slug")
                };

            entity = await _categoryService.FindByIdAsync(cancellationToken, localizedProperty.EntityId);
            request.LanguageId = localizedProperty.LanguageId;
            request.Id = localizedProperty.EntityId;
        }



        var localize = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(new GetLocalizedPropertyReq()
        {
            EntityId = request.Id,
            LanguageId = request.LanguageId,
            LocalKeyGroup = LocalKeyGroupType.BlogCategory
        }, cancellationToken);

        return new()
        {
            Id = request.Id,
            Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? ""
        };
    }
}