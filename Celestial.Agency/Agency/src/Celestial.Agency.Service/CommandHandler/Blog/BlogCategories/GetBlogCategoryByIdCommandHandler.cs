using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Blog.BlogCategories;

public sealed class GetBlogCategoryByIdCommandHandler(
    IBlogCategoryService categoryService,
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService)
    : IRequestHandler<GetBlogCategoryByIdCommandReq, GetBlogCategoryByIdCommandRes>
{
    public async Task<GetBlogCategoryByIdCommandRes> Handle(GetBlogCategoryByIdCommandReq request,
        CancellationToken cancellationToken)
    {
        var entity = new BlogCategory();

        if (request.Id != 0)
        {
            var languages = await languageService.GetLanguages(cancellationToken);
            if (languages.Any(p => p.LanguageCulture.Split('-')[0] == request.Language))
                return new GetBlogCategoryByIdCommandRes
                {
                    Error = new ValidationError("LA400", "invalid Language")
                };
            entity = await categoryService.FindByIdAsync(cancellationToken, request.Id);
            if (entity == null)
                return new GetBlogCategoryByIdCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("C404", "Category not Found")
                };
        }
        else if (!string.IsNullOrEmpty(request.Slug))
        {
            var localizedProperty = await localizedPropertyService.FirstOrDefaultAsync(p =>
                p.LocalKey == "Slug" && p.LocalValue == request.Slug &&
                p.LocalKeyGroup == LocalKeyGroupType.BlogCategory, cancellationToken: cancellationToken);

            if (localizedProperty == null)
                return new GetBlogCategoryByIdCommandRes
                {
                    Error = new ValidationError("404", "Blog not found with slug")
                };

            entity = await categoryService.FindByIdAsync(cancellationToken, localizedProperty.EntityId);
            request.Language = localizedProperty.Language;
            request.Id = localizedProperty.EntityId;
        }

        var localize = await localizedPropertyService.GetPropertyByLanguageAndEntityId(new GetLocalizedPropertyReq
        {
            EntityId = request.Id,
            Language = request.Language,
            LocalKeyGroup = LocalKeyGroupType.BlogCategory
        }, cancellationToken);

        return new GetBlogCategoryByIdCommandRes
        {
            Id = request.Id,
            Name = localize.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? ""
        };
    }
}