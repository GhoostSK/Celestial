using System.ComponentModel.DataAnnotations;
using Celestial.Agency.Core.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Models.Dto;

public sealed class AddBlogReqDto
{
    public List<BlogLocalizedProperty> BlogLocalizedProperties { get; set; } = new();
    public string PicturePath { get; set; } = string.Empty;
    public long BlogCategoryId { get; set; }
    public List<long> BlogTagIds { get; set; } = new();
}

public sealed class GetBlogCategoryByIdReqDto
{
    public long Id { get; set; }
    public string slug { get; set; }
    public string Language { get; set; }
}

public sealed class GetBlogTagByIdReqDto
{
    public long Id { get; set; }

    [Slug] public string Slug { get; set; }
}

public sealed class BlogLocalizedProperty
{
    public string Language { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public string HeaderText { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;

    [MaxLength(255)] public string MetaTitle { get; set; } = string.Empty;

    [MaxLength(500)] public string MetaDescription { get; set; } = string.Empty;

    [MaxLength(1000)] public string MetaKeywords { get; set; } = string.Empty;

    [Slug] public string Slug { get; set; } = string.Empty;
}

public sealed class GetBlogsListReqDto
{
    [FromRoute] public string Language { get; set; }

    public string Title { get; set; } = string.Empty;
    public List<long> CategoriesId { get; set; } = new();
    public List<long> TagsId { get; set; } = new();
    public int PopularTagCount { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public sealed class RemoveBlogsReqDto
{
    public int BlogId { get; set; }
}

public sealed class GetBlogBySlugReqDto
{
    [FromRoute] public string Language { get; set; }

    [Slug] [FromRoute] public string Slug { get; set; }
}

public sealed class GetLatestBlogsReqDto
{
    [FromRoute] public string Language { get; set; }
    public int Count { get; set; }
}

public sealed class GetTagListReqDto
{
    [FromRoute] public string Language { get; set; } = string.Empty;
    public int Count { get; set; }
}

public sealed class GetBlogTagBySlugReqDto
{
    public string Language { get; set; } = string.Empty;

    [Slug] public string Slug { get; set; } = string.Empty;
}