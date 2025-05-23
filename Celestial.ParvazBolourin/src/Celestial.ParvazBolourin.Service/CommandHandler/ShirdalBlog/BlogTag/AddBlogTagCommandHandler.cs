using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogTag;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog.BlogTag;

public sealed class AddBlogTagCommandHandler : IRequestHandler<AddBlogTagCommandReq, AddBlogTagCommandRes>
{
    private readonly IBlogTagService _blogtagService;
    private readonly ILocalizedPropertyService _localizedPropertyService;

    public AddBlogTagCommandHandler(IBlogTagService BlogTagService, ILocalizedPropertyService localizedPropertyService)
    {
        _blogtagService = BlogTagService;
        _localizedPropertyService = localizedPropertyService;
    }

    public async Task<AddBlogTagCommandRes> Handle(AddBlogTagCommandReq request, CancellationToken cancellationToken)
    {
        var entity = new BlogTags();
        await _blogtagService.AddAsync(entity, cancellationToken);
        await _blogtagService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var localizedNames = request.LocalizedNames.Select(name => new LocalizedProperty
        {
            EntityId = entity.Id,
            LanguageId = name.LanguageId,
            LocalKey = nameof(name.Name),
            LocalKeyGroup = "BlogTag",
            LocalValue = name.Name
        }).ToList();

        await _localizedPropertyService.BulkInsertAsync(localizedNames, cancellationToken: cancellationToken);
        return new AddBlogTagCommandRes();
    }
}