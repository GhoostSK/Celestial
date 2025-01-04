using Celestial.ParvazBolourin.Core.Enum.Visa;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Visa;

public class Visas : DbBaseEntity<int>
{
    public string Country { get; set; } = String.Empty;
    public bool Status { get; set; } = true;
    public String PicturePath { get; set; } = null!;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}

