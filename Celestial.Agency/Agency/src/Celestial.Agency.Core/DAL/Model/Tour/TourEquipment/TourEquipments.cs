using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Tour.TourEquipment;

public sealed class TourEquipment : DbBaseEntity<long>
{
    public string Icon { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}