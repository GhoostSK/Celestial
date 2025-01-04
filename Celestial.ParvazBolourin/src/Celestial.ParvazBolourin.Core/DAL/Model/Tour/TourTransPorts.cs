using Celestial.ParvazBolourin.Core.Enum.Tour;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Tour;

public sealed class TourTransPorts : DbBaseEntity<int>
{
    public int TourId { get; set; }
    public TransportVehicleType TransPortType { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}