using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Global;

public sealed class Language : DbBaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string LanguageCulture { get; set; } = string.Empty;
    public bool Rtl { get; set; }
    public bool Published { get; set; }
    public int DisplayOrder { get; set; }
}