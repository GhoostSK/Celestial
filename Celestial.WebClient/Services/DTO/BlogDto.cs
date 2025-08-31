using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class GetBlogListReqDto
{
    public string Language { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public List<long> CategoriesId { get; set; } = new();
    public List<long> TagsId { get; set; } = new();
    public int PopularTagCount { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public sealed class GetBlogListRes : ServiceResContextBase
{
    public List<BlogLisItemDto> Items { get; set; } = new();
    public List<BlogTagDto> BlogTagsList { get; set; } = new();
    public List<BlogCategoryItemDto> Categories { get; set; } = new();
    public PaginationMeta Pagination { get; set; } = new();
}

public sealed class BlogLisItemDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public int Likes { get; set; }
    public List<Tag> Tags { get; set; } = new();
    public CategoryDto Category { get; set; } = new();
    public DateTime RegistrationDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string CreatedByProfilePicture { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class BlogTagDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class BlogCategoryItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class PaginationMeta
{
    public int TotalRecords { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int? NextPage { get; set; }
    public int? PrevPage { get; set; }
}

public sealed class Tag
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class CategoryDto
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class GetLatestBlogListReqDto
{
    public string Language { get; set; } = string.Empty;
    public int Count { get; set; }
}

public sealed class GetLatestBlogListResDto : ServiceResContextBase
{
    public List<LatestBlogItemDto> Items { get; set; } = new();
}

public sealed class LatestBlogItemDto
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

public sealed class LatestBlogResDto : ServiceResContextBase
{
    public LatestBlogItemDto Item { get; set; } = new();
}

public sealed class GetBlogBySlugReqDto
{
    public string Language { get; set; }
    public string? Slug { get; set; }
}

public sealed class GetBlogByIdCommandResDto : ServiceResContextBase
{
    public BlogItemDto Item { get; set; } = new();
}

public sealed class BlogItemDto
{
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public int Likes { get; set; }
    public BlogCategoryDto BlogCategory { get; set; } = new();
    public List<BlogTagDto> BlogTags { get; set; } = new();
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string CreatedByProfilePicture { get; set; } = string.Empty;
}

public sealed class BlogCategoryDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}