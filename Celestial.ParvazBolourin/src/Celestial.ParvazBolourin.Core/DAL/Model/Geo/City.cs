using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Geo;

public sealed class City : DbBaseEntity<long>
{
    public string Name { get; set; } = string.Empty;
    public int State_id { get; set; }
}