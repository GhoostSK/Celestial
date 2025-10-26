// ---------------------- Requests ----------------------

using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class GetBlogListReqDto
{
    public string Language { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    public List<long> CategoryIds { get; set; } = new();
    public List<long> TagIds { get; set; } = new();
    public int PopularTagCount { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public sealed class GetLatestBlogListReqDto
{
    public string Language { get; set; } = string.Empty;
    public int Count { get; set; }
}

public sealed class GetBlogBySlugReqDto
{
    public string Language { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public sealed class GetBlogCategoryListReqDto
{
    public string Language { get; set; } = string.Empty;
    public int Count { get; set; }
}

// ---------------------- Responses ----------------------

public sealed class GetBlogListResDto : ServiceResContextBase
{
    public List<BlogListItemDto> Items { get; set; } = new();
    public List<BlogTagItemDto> Tags { get; set; } = new();
    public List<BlogCategoryItemDto> Categories { get; set; } = new();
    public PaginationMeta Pagination { get; set; } = new();
}

public sealed class GetLatestBlogListResDto : ServiceResContextBase
{
    public List<LatestBlogItemDto> Items { get; set; } = new();
}

public sealed class GetBlogBySlugResDto : ServiceResContextBase
{
    public BlogDetailDto Item { get; set; } = new();
}

public sealed class GetBlogCategoryListResDto : ServiceResContextBase
{
    public List<BlogCategoryItemDto> Items { get; set; } = new();
}

// ---------------------- DTOs ----------------------

public sealed class BlogListItemDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public int Likes { get; set; }
    public List<TagItemDto> Tags { get; set; } = new();
    public CategoryItemDto Category { get; set; } = new();
    public DateTime RegistrationDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string CreatedByProfilePicture { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class BlogTagListItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class BlogTagItemDto
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

public sealed class TagItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class CategoryItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
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
    public string Slug { get; set; } = string.Empty;
}

public sealed class BlogDetailDto
{
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public int Likes { get; set; }
    public BlogCategoryItemDto Category { get; set; } = new();
    public List<BlogTagItemDto> Tags { get; set; } = new();
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string CreatedByProfilePicture { get; set; } = string.Empty;
}

public sealed class GetBlogTagListReqDto
{
    public string Language { get; set; } = string.Empty;
    public int Count { get; set; }
}

public sealed class GetBlogTagListResDto : ServiceResContextBase
{
    public List<BlogTagListItemDto> Items { get; set; } = new();
    public int ReturnedCount { get; set; }
}