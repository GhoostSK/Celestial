using System.Linq.Expressions;
using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog;

public class UpdateBlogCommandHandler : BaseRequestHandler<UpdateBlogCommandReq, UpdateBlogCommandRes>
{
    private readonly IBlogService _blogService;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;

    public UpdateBlogCommandHandler(IBlogService blogService, ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService)
    {
        _blogService = blogService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    protected override async Task<UpdateBlogCommandRes> HandleCore(UpdateBlogCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        foreach (var item in request.BlogLocalizedProperties)
            if (!languages.Any(p => p.Id == item.LanguageId))
                return new UpdateBlogCommandRes
                {
                    Error = new ValidationError("LA400", "invalid Language")
                };

        var blog = await _blogService.FindByIdAsync(cancellationToken, request.Id);
        if (blog == null)
            return new UpdateBlogCommandRes
            {
                Error = new ValidationError("E404", "Blog Not Found")
            };

        blog.PicturePath = request.PicturePath;

        await _blogService.UnitOfWork.SaveChangesAsync(cancellationToken);
        Expression<Func<LocalizedProperty, bool>> predicate = property =>
            property.EntityId == request.Id &&
            property.LocalKey == "Tags" &&
            property.LocalKeyGroup == "Blog";

        await _localizedPropertyService.BulkRemove(predicate, cancellationToken);
        await _localizedPropertyService.UnitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var item in request.BlogLocalizedProperties)
        {
            var localizedData = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    LanguageId = item.LanguageId,
                    LocalKeyGroup = "Blog",
                    EntityId = request.Id
                }, cancellationToken);

            if (!localizedData.Any(p => p.LocalKey == nameof(item.Title)))
                await _localizedPropertyService.AddAsync(new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.Title),
                    EntityId = request.Id,
                    LocalKeyGroup = "Blog",
                    LocalValue = item.Title
                }, cancellationToken);

            if (!localizedData.Any(p => p.LocalKey == nameof(item.MainText)))
                await _localizedPropertyService.AddAsync(new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.MainText),
                    EntityId = request.Id,
                    LocalKeyGroup = "Blog",
                    LocalValue = item.MainText
                }, cancellationToken);

            if (!localizedData.Any(p => p.LocalKey == nameof(item.HeaderText)))
                await _localizedPropertyService.AddAsync(new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.HeaderText),
                    EntityId = request.Id,
                    LocalKeyGroup = "Blog",
                    LocalValue = item.HeaderText
                }, cancellationToken);

            if (!localizedData.Any(p => p.LocalKey == nameof(item.Tags)))
                await _localizedPropertyService.AddAsync(new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.Tags),
                    EntityId = request.Id,
                    LocalKeyGroup = "Blog",
                    LocalValue = string.Join(",", item.Tags)
                }, cancellationToken);

            if (!localizedData.Any(p => p.LocalKey == nameof(item.Title)))
                await _localizedPropertyService.AddAsync(new LocalizedProperty
                {
                    LanguageId = item.LanguageId,
                    LocalKey = nameof(item.Title),
                    EntityId = request.Id,
                    LocalKeyGroup = "Blog",
                    LocalValue = item.Title
                }, cancellationToken);

            foreach (var property in localizedData)
            {
                var existingProperty = await _localizedPropertyService.FindByIdAsync(cancellationToken, property.Id);

                switch (property.LocalKey)
                {
                    case nameof(item.Title):
                        existingProperty.LocalValue = item.Title;
                        break;
                    case nameof(item.Tags):
                        existingProperty.LocalValue = string.Join(",", item.Tags);
                        break;
                    case nameof(item.HeaderText):
                        existingProperty.LocalValue = item.HeaderText;
                        break;
                    case nameof(item.MainText):
                        existingProperty.LocalValue = item.MainText;
                        break;
                }
            }

            await _localizedPropertyService.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        return new UpdateBlogCommandRes();
    }
}