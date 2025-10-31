using Celestial.Agency.Core.Enum.Visa;
using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Visa;

public class Visas : DbBaseEntity<int>
{
    public string Country { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public string PicturePath { get; set; } = null!;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}