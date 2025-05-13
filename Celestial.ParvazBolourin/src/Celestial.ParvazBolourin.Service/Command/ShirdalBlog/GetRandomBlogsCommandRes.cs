using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ShirdalBlog;


public sealed class GetTopBlogsCommandReq : BaseEntity, IRequest<GetTopBlogsCommandRes>
{
    public int TopCount { get; set; }
    public int LanguageId { get; set; }
}

public class GetTopBlogsCommandRes : ServiceResContextBase
{
    public List<TopBlogDto> Blogs { get; set; } = new();
}

public class TopBlogDto
{
    public long Id { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
}
