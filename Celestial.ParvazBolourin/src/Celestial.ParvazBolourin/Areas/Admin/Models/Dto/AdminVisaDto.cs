using Celestial.ParvazBolourin.Core.DAL.Model.Visa;
using Celestial.ParvazBolourin.Core.Enum.Visa;
using Celestial.ParvazBolourin.Service.Command.Visa;

namespace Celestial.ParvazBolourin.Api.Areas.Admin.Models.Dto;

public sealed class AddVisaReqDto
{
    public string Country { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public string PicturePath { get; set; } = null!;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
    public List<AddVisaLocalizedProperty> VisaLocalizedProperty { get; set; } = new();
}

public sealed class RemoveVisaReqDto
{
    public int Id { get; set; }
}

public class UpdateVisaDtoReqDto
{
    public int Id { get; set; }
    public Visas Visa { get; set; } = new();
    public List<AddVisaLocalizedProperty> VisaLocalizedProperty { get; set; } = new();
}