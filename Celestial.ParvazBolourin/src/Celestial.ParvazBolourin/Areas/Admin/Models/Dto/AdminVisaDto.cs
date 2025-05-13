using Celestial.ParvazBolourin.Core.Attributes;
using Celestial.ParvazBolourin.Core.DAL.Model.Visa;
using Celestial.ParvazBolourin.Core.Enum.Visa;
using Celestial.ParvazBolourin.Service.Command.Visa;
using System.ComponentModel.DataAnnotations;

namespace Celestial.ParvazBolourin.Api.Areas.Admin.Models.Dto;

public sealed class AddVisaReqDto
{
    public string Country { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public string PicturePath { get; set; } = null!;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
    public List<AddVisaLocalizedProperty> VisaLocalizedProperty { get; set; } = new();
    [MaxLength(255)]
    public string MetaTitle { get; set; } = string.Empty;

    [MaxLength(500)]
    public string MetaDescription { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string MetaKeywords { get; set; } = string.Empty;
    [Slug]

    public string Slug { get; set; } = string.Empty;
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