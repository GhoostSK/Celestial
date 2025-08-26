namespace Celestial.WebClient.Services.DTO;

public class GetBlogListReqDto
{
    public string lang { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public sealed class GetBlogListRes
{
    public List<BlogItem> Items { get; set; } = new();
    public List<BlogTagDto> BlogTagsList { get; set; } = new();
    public List<BlogCategoryItemDto> Categories { get; set; } = new();
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
    public Category Category { get; set; } = new();
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