using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
using MofidApp.SharedKernel.Common.Model;

namespace Celestial.ParvazBolourin.Service.CommandHandler.ShirdalBlog;

public sealed class RemoveBlogCommandHandler : BaseRequestHandler<RemoveBlogCommandReq, RemoveBlogCommandRes>
{
    private readonly IBlogService _blogService;

    public RemoveBlogCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    protected override async Task<RemoveBlogCommandRes> HandleCore(RemoveBlogCommandReq request, CancellationToken cancellationToken)
    {
        var blog = await _blogService.GetBlogById(request.Id, cancellationToken);
        blog!.IsDeleted = true;
        await _blogService.UnitOfWork.SaveChangesAsync(cancellationToken);
        return new RemoveBlogCommandRes();
    }
}