using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ShirdalBlog;

public sealed class GetBlogsListCommandReq : BaseEntity, IRequest<GetBlogsListCommandRes>
{
    public int LanguageId { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<long> CategoriesId { get; set; } = new();
    public List<long> TagsId { get; set; } = new();
    public int PopularTagCount { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}


public sealed class GetBlogsListCommandRes : ServiceResContextBase
{
    public List<BlogItem> Items { get; set; } = new();
    public List<BlogTagDto> BlogTagsList { get; set; } = new();
    public List<BlogCategoryListItemDto> Categories { get; set; } = new();
    public PaginationMeta Pagination { get; set; } = new();

}

public sealed class BlogItem
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public int Likes { get; set; }
    public List<Tag> Tags { get; set; } = new();
    public List<Categories> Categories { get; set; } = new();
    public DateTime RegistrationDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string CreatedByProfilePicture { get; set; } = string.Empty;

    //seo
    public string Slug { get; set; } = string.Empty;
}

