using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.Agency.Core.DAL.Model.Geo;

public sealed class City : DbBaseEntity<long>
{
    public string Name { get; set; } = string.Empty;
    public int State_id { get; set; }
    public int country_id { get; set; } = 0;
}