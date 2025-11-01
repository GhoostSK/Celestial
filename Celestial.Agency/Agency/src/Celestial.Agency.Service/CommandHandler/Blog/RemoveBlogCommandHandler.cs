using Celestial.Agency.Core.DAL.Repositories.Blog;
using Celestial.Agency.Service.Command.ShirdalBlog;
using Celestial.SharedKernel.Mediator;

namespace Celestial.Agency.Service.CommandHandler.Blog;

public sealed class RemoveBlogCommandHandler : BaseRequestHandler<RemoveBlogCommandReq, RemoveBlogCommandRes>
{
    private readonly IBlogService _blogService;

    public RemoveBlogCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    protected override async Task<RemoveBlogCommandRes> HandleCore(RemoveBlogCommandReq request,
        CancellationToken cancellationToken)
    {
        var blog = await _blogService.GetBlogById(request.Id, cancellationToken);
        blog!.IsDeleted = true;
        await _blogService.UnitOfWork.SaveChangesAsync(cancellationToken);
        return new RemoveBlogCommandRes();
    }
}