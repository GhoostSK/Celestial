using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog;

public sealed class GetTopBlogsCommandReq : BaseEntity, IRequest<GetTopBlogsCommandRes>
{
    public int TopCount { get; set; }
    public string Language { get; set; } = string.Empty;
}

public class GetTopBlogsCommandRes : ServiceResContextBase
{
    public List<TopBlogDto> Items { get; set; } = new();
}

public class TopBlogDto
{
    public long Id { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Likes { get; set; }
    public string HeaderText { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public List<Tag> Tags { get; set; } = new();
    public Category Categories { get; set; } = new();
    public DateTime RegistrationDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string CreatedByProfilePicture { get; set; } = string.Empty;
}

public class Tag
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public class Category
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}