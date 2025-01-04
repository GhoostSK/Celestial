using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Global;

public sealed class Language : DbBaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string LanguageCulture { get; set; } = string.Empty;
    public bool Rtl { get; set; }
    public bool Published { get; set; }
    public int DisplayOrder { get; set; }
}