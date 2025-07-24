using Celestial.Agency.Core.Enum.Visa;

namespace Celestial.Agency.Api.Models.Dto;

public class GetVisaListReqDto
{
    public string Language { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public sealed class GetVisaByIdReqDto
{
    public int Id { get; set; }
    public string Language { get; set; }
    public string Slug { get; set; }
}

public sealed class GetVisaByIdResDto
{
    public string Country { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public string PicturePath { get; set; } = null!;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string Language { get; set; }
    public string Text { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public List<string> Address { get; set; } = new();
    public List<string> PhoneNumber { get; set; } = new();
}