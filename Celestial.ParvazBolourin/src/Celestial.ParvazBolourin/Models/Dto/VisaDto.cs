using Celestial.ParvazBolourin.Core.Enum.Visa;

namespace Celestial.ParvazBolourin.Api.Models.Dto;

public class GetVisaListReqDto
{
    public int LanguageId { get; set; }
}

public sealed class GetVisaByIdReqDto
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
}

public sealed class GetVisaByIdResDto
{
    public string Country { get; set; } = String.Empty;
    public bool Status { get; set; } = true;
    public String PicturePath { get; set; } = null!;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    public int LanguageId { get; set; }
    public string Text { get; set; } = String.Empty;

    public string Name { get; set; } = String.Empty;
    public List<string> Address { get; set; } = new();
    public List<string> PhoneNumber { get; set; } = new();
}