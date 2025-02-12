using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Geo;

public sealed class Country : DbBaseEntity<int>
{
    public string Shortname { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public int PhoneCode { get; set; }
}