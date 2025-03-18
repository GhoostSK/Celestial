using Celestial.ParvazBolourin.Core.Enum.Visa;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Visa;

public class Visas : DbBaseEntity<int>
{
    public string Country { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public string PicturePath { get; set; } = null!;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
    public bool IsDeleted { get; set; } = false;

    //seo
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}