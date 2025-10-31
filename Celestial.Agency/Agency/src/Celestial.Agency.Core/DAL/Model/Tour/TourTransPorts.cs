using Celestial.Agency.Core.Enum.Tour;
using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Tour;

public sealed class TourTransPorts : DbBaseEntity<long>
{
    public long TourId { get; set; }
    public TransportVehicleType TransPortType { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}