// ---------------------- Requests ----------------------

using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class GetVisaListReqDto
{
    public string Language { get; set; } = string.Empty;
    public int PageSize { get; set; } = 12;
    public int PageNumber { get; set; } = 1;
}

public sealed class GetVisaBySlugReqDto
{
    public string Language { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

// ---------------------- Responses ----------------------

public sealed class GetVisaListResDto : ServiceResContextBase
{
    public List<VisaListItemDto> Items { get; set; } = new();
}

public sealed class GetVisaBySlugResDto : ServiceResContextBase
{
    public string Language { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
    public bool Status { get; set; }
    public List<Dictionary<string, string>> Embassy { get; set; } = new();
    public string Slug { get; set; } = string.Empty;
}

// ---------------------- DTOs ----------------------

public sealed class VisaListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
    public bool Status { get; set; }
    public string Language { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}
