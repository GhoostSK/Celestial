using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ShirdalBlog;

public sealed class GetBlogsListCommandReq : BaseEntity, IRequest<GetBlogsListCommandRes>
{
    public int LanguageId { get; set; }
    public List<long> CategoryIds { get; set; } = new();
}


public sealed class GetBlogsListCommandRes : ServiceResContextBase
{
    public List<BlogItem> Items { get; set; } = new();
}

public sealed class BlogItem
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public int Likes { get; set; }

    //seo
    public string Slug { get; set; } = string.Empty;
}

