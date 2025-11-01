using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog;

public sealed class GetBlogsListCommandReq : BaseEntity, IRequest<GetBlogsListCommandRes>
{
    public string Language { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public List<long> TagsId { get; set; } = new();
    public int PopularTagCount { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public sealed class GetBlogsListCommandRes : ServiceResContextBase
{
    public List<BlogListItem> Items { get; set; } = new();
    public List<BlogTagDto> Tags { get; set; } = new();
    public List<BlogCategoryItemDto> Categories { get; set; } = new();
    public PaginationMeta Pagination { get; set; } = new();
}

public sealed class BlogListItem
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public int Likes { get; set; }
    public List<Tag> Tags { get; set; } = new();
    public Category Category { get; set; } = new();
    public DateTime RegistrationDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string CreatedByProfilePicture { get; set; } = string.Empty;

    //seo
    public string Slug { get; set; } = string.Empty;
}

public sealed class LatestBlogItem
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public int Likes { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;

    public string CreatedByProfilePicture { get; set; } = string.Empty;

    //seo
    public string Slug { get; set; } = string.Empty;
}