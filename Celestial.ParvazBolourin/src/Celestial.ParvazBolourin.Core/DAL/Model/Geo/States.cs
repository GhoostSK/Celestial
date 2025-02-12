using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Geo;

public sealed class State : DbBaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public int Country_id { get; set; }
}