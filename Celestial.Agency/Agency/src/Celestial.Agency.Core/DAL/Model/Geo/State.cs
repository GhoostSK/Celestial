using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Geo;

public sealed class State : DbBaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public int Country_id { get; set; }
}