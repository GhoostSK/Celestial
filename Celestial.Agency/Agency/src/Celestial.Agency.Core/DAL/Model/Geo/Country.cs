using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.Agency.Core.DAL.Model.Geo;

public sealed class Country : DbBaseEntity<int>
{
    public string Shortname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int PhoneCode { get; set; }
}